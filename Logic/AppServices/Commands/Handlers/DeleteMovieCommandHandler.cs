using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Logic.Business.Service.Kafka.Interfaces;
using Logic.Data.DataContexts;
using Logic.Data.Entities;
using Logic.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Logic.AppServices.Commands.Handlers
{
    public sealed class DeleteMovieCommandHandler : ICommandHandler<DeleteMovieCommand>
    {
        private readonly MovieDataContext _dataContext;
        private readonly IConfiguration _configuration;
        private readonly IProducerService _producerService;
        private readonly ILogger<DeleteMovieCommandHandler> _logger;

        public DeleteMovieCommandHandler(MovieDataContext dataContext, IConfiguration configuration, IProducerService producerService, ILogger<DeleteMovieCommandHandler> logger)
        {
            _dataContext = dataContext;
            _configuration = configuration;
            _producerService = producerService;
            _logger = logger;
        }
        
        public async Task<Result> Handle(DeleteMovieCommand command)
        {
            var topic = _configuration["MovieDeactivatedTopicName"];
            Movie movie =  await _dataContext.Movies.FirstOrDefaultAsync(item => item.Id == command.Id);
            if (movie == null)
            {
                return Result.Failure($"No movie found for Id {command.Id}");
            }

            if (movie.IsActive)
            {
                return Result.Failure($"Movie cannot be deleted because it is active for Id {command.Id}");
            }
            
            _dataContext.Movies.Remove(movie);

            List<MoviePerson> moviePersons =
                await _dataContext.MoviePersons.Where(item => item.MovieId == command.Id).ToListAsync();

            foreach (var moviePerson in moviePersons)
            {
                _dataContext.MoviePersons.Remove(moviePerson);
            }
            
            List<MovieCountry> movieCountries =
                await _dataContext.MovieCountries.Where(item => item.MovieId == command.Id).ToListAsync();

            foreach (var movieCountry in movieCountries)
            {
                _dataContext.MovieCountries.Remove(movieCountry);
            }
            
            List<MovieType> movieTypes =
                await _dataContext.MovieTypes.Where(item => item.MovieId == command.Id).ToListAsync();

            foreach (var movieType in movieTypes)
            {
                _dataContext.MovieTypes.Remove(movieType);
            }
            
            await _dataContext.SaveChangesAsync();
            
            MovieDeactivedEvent movieDeactivatedEvent = new MovieDeactivedEvent
            {
                MovieId = command.Id
            };
            
            var isSendEvent = await _producerService.Produce(topic, movieDeactivatedEvent);

            if (!isSendEvent)
            {
                _logger.LogError(
                    $"{nameof(DeleteMovieCommandHandler)} {nameof(Handle)} not produce movie deactivated event - Event:{JsonConvert.SerializeObject(movieDeactivatedEvent)} - Topic: {topic}");
            }
            
            return Result.Success();
        }
    }
}