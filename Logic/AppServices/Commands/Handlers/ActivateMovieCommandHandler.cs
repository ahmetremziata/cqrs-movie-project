using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Logic.Business.Service.Kafka.Interfaces;
using Logic.Data.DataContexts;
using Logic.Data.Entities;
using Logic.Events;
using Logic.Infrastructures.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Logic.AppServices.Commands.Handlers
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
            
            if (String.IsNullOrWhiteSpace(movie.PosterUrl))
            {
                return Result.Failure($"Movie's poster url is invalid for movieId {command.MovieId}");
            }
            
            var countries = await _dataContext.MovieCountries.Where(item => item.MovieId == movie.Id).ToListAsync();
            var types = await _dataContext.MovieTypes.Where(item => item.MovieId == movie.Id).ToListAsync();
            var persons = await _dataContext.MoviePersons.Where(item => item.MovieId == movie.Id).ToListAsync();
            
            if (!types.Any())
            {
                return Result.Failure($"Movie's types not entered for movieId {command.MovieId}. At least one type should be entered");
            }
            
            if (!countries.Any())
            {
                return Result.Failure($"Movie's countries not entered for movieId {command.MovieId}. At least one country should be entered");
            }
            
            if (!persons.Any())
            {
                return Result.Failure($"Movie's persons not entered for movieId {command.MovieId}. At least one person should be entered");
            }
            
            movie.IsActive = true;
            movie.IsSynchronized = true;
            await _dataContext.SaveChangesAsync();
            
            var topic = _configuration["MovieActivatedTopicName"];
            var identity = new MovieIdentity()
            {
                VisionEntryDate = movie.VisionEntryDate,
                Directors = await GetActors(persons, (int) RoleEnum.Director),
                Scenarists = await GetActors(persons, (int) RoleEnum.Scenarist),
                Producers = await GetActors(persons, (int) RoleEnum.Producer),
                PhotographyDirectors = await GetActors(persons, (int) RoleEnum.PhotographyDirector),
                Composers = await GetActors(persons, (int) RoleEnum.Composer),
                BookAuthors = await GetActors(persons, (int) RoleEnum.BookAuthor),
                Types = await GetTypes(types),
                Countries = await GetCountries(countries)
            };
            MovieActivatedEvent movieActivatedEvent = new MovieActivatedEvent
            {
                MovieId = command.MovieId,
                Name = movie.Name,
                OriginalName = movie.OriginalName,
                Description = movie.Description,
                ConstructionYear = movie.ConstructionYear,
                TotalMinute = movie.TotalMinute,
                PosterUrl = movie.PosterUrl,
                Identity = identity,
                Actors = await GetActors(persons, (int) RoleEnum.Actor)
            };
            var isSendEvent = await _producerService.Produce(topic, movieActivatedEvent);

            if (!isSendEvent)
            {
                _logger.LogError(
                    $"{nameof(ActivateMovieCommandHandler)} {nameof(Handle)} not produce movie activated event - Event:{JsonConvert.SerializeObject(movieActivatedEvent)} - Topic: {topic}");
            }
            return Result.Success();
        }

        private async Task<List<Actor>> GetActors(List<MoviePerson> moviePersons, int roleId)
        {
            List<Actor> actors = new List<Actor>();

            foreach (var moviePerson in moviePersons.Where(item => item.RoleId == roleId))
            {
                var person = await _dataContext.Persons.SingleAsync(item => item.Id == moviePerson.PersonId);
                Actor actor = new Actor {
                    Id = person.Id,
                    Name = person.Name,
                    AvatarUrl = person.AvatarUrl,
                    CharacterName = moviePerson.CharacterName
                };
                actors.Add(actor);
            }

            return actors;
        }
        
        private async Task<List<Events.Type>> GetTypes(List<MovieType> movieTypes)
        {
            List<Events.Type> types = new List<Events.Type>();
            
            foreach (var movieType in movieTypes)
            {
                var typeEntity = await _dataContext.Types.SingleAsync(item => item.Id == movieType.TypeId);
                Events.Type type = new Events.Type {
                    Id = typeEntity.Id,
                    Name = typeEntity.Name
                };
                types.Add(type);
            }
            return types;
        }
        
        private async Task<List<Events.Country>> GetCountries(List<MovieCountry> movieCountries)
        {
            List<Events.Country> countries = new List<Events.Country>();
            
            foreach (var movieCountry in movieCountries)
            {
                var countryEntity = await _dataContext.Countries.SingleAsync(item => item.Id == movieCountry.CountryId);
                Events.Country country = new Events.Country {
                    Id = countryEntity.Id,
                    Name = countryEntity.Name
                };
                countries.Add(country);
            }
            return countries;
        }
    }
}