using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Logic.AppServices.Commands;
using Logic.Business.Service.Kafka.Interfaces;
using Logic.Data.DataContexts;
using Logic.Data.Entities;
using Logic.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Logic.AppServices.Handlers
{
    public sealed class ActivateMovieCommandHandler : ICommandHandler<ActivateMovieCommand>
    {
        private readonly MovieDataContext _dataContext;
        private readonly IConfiguration _configuration;
        private readonly IProducerService _producerService;
        private readonly ILogger<ActivateMovieCommandHandler> _logger;


        public ActivateMovieCommandHandler(MovieDataContext dataContext, IConfiguration configuration, IProducerService producerService, ILogger<ActivateMovieCommandHandler> logger)
        {
            _dataContext = dataContext;
            _configuration = configuration;
            _producerService = producerService;
            _logger = logger;
        }
        
        public async Task<Result> Handle(ActivateMovieCommand command)
        {
            Movie movie =  await _dataContext.Movies.FirstOrDefaultAsync(item => item.Id == command.MovieId);
            if (movie == null)
            {
                return Result.Failure($"No movie found for Id {command.MovieId}");
            }

            if (movie.IsActive)
            {
                return Result.Failure($"Movie already active for movieId {command.MovieId}");
            }

            movie.IsActive = true;
            await _dataContext.SaveChangesAsync();
            
            var topic = _configuration["MovieActivatedTopicName"];
            MovieActivatedEvent movieActivatedEvent = new MovieActivatedEvent {MovieId = command.MovieId};
            var isSendEvent = await _producerService.Produce(topic, movieActivatedEvent);

            if (!isSendEvent)
            {
                _logger.LogError(
                    $"{nameof(ActivateMovieCommandHandler)} {nameof(Handle)} not produce movie activated event - Event:{JsonConvert.SerializeObject(movieActivatedEvent)} - Topic: {topic}");
            }
            return Result.Success();
        }
    }
}