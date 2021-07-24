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
    public sealed class DeactivateMovieCommandHandler : ICommandHandler<DeactivateMovieCommand>
    {
        private readonly MovieDataContext _dataContext;
        private readonly IConfiguration _configuration;
        private readonly IProducerService _producerService;
        private readonly ILogger<DeactivateMovieCommandHandler> _logger;
        
        public DeactivateMovieCommandHandler(MovieDataContext dataContext, IConfiguration configuration, IProducerService producerService, ILogger<DeactivateMovieCommandHandler> logger)
        {
            _dataContext = dataContext;
            _configuration = configuration;
            _producerService = producerService;
            _logger = logger;
        }
        
        public async Task<Result> Handle(DeactivateMovieCommand command)
        {
            var topic = _configuration["MovieDeactivatedTopicName"];
            Movie movie =  await _dataContext.Movies.FirstOrDefaultAsync(item => item.Id == command.MovieId);
            if (movie == null)
            {
                return Result.Failure($"No movie found for Id {command.MovieId}");
            }

            if (!movie.IsActive)
            {
                return Result.Failure($"Movie already passive for movieId {command.MovieId}");
            }
            
            movie.IsActive = false;
            await _dataContext.SaveChangesAsync();
            
            MovieDeactivedEvent movieDeactivatedEvent = new MovieDeactivedEvent
            {
                MovieId = command.MovieId
            };
            
            var isSendEvent = await _producerService.Produce(topic, movieDeactivatedEvent);

            if (!isSendEvent)
            {
                _logger.LogError(
                    $"{nameof(ActivateMovieCommandHandler)} {nameof(Handle)} not produce movie deactivated event - Event:{JsonConvert.SerializeObject(movieDeactivatedEvent)} - Topic: {topic}");
            }
            return Result.Success();
        }
    }
}