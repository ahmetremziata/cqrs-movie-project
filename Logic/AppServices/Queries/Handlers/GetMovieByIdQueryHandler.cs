using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logic.Data.DataContexts;
using Logic.Data.Entities;
using Logic.Decorators;
using Logic.Infrastructures.Enums;
using Logic.Responses;
using Microsoft.EntityFrameworkCore;

namespace Logic.AppServices.Queries.Handlers
{
    public sealed class GetMovieByIdQueryHandler : IQueryHandler<GetMovieByIdQuery, MovieDetailResponse>
    {
        private readonly MovieDataContext _dataContext;

        public GetMovieByIdQueryHandler(MovieDataContext dataContext)
        {
            _dataContext = dataContext;
        }
        
        public async Task<MovieDetailResponse> Handle(GetMovieByIdQuery query)
        {
            Movie movie = await _dataContext.Movies.Where(item => item.Id == query.MovieId).SingleOrDefaultAsync();
            List<MovieType> movieTypes = await _dataContext.MovieTypes.Where(item => item.MovieId == query.MovieId).ToListAsync();
            List<MovieCountry> movieCountries = await _dataContext.MovieCountries.Where(item => item.MovieId == query.MovieId).ToListAsync();
            List<MoviePerson> movieActors = await _dataContext.MoviePersons.Where(item => item.MovieId == query.MovieId && item.RoleId == (int) RoleEnum.Actor)
                .ToListAsync();
            
            if (movie == null)
            {
                return null;
            }
            
            return await ConvertToDto(movie, movieActors, movieTypes, movieCountries);
        }
        
        private async Task<MovieDetailResponse> ConvertToDto(Movie movie, List<MoviePerson> movieActors, List<MovieType> movieTypes, List<MovieCountry> movieCountries)
        {
            return new MovieDetailResponse()
            {
                Id = movie.Id,
                Name = movie.Name,
                OriginalName = movie.OriginalName,
                Description = movie.Description,
                ConstructionYear = movie.ConstructionYear,
                TotalMinute = movie.TotalMinute,
                PosterUrl = movie.PosterUrl,
                IsActive = movie.IsActive,
                VisionEntryDate = movie.VisionEntryDate,
                TotalActorCount = movieActors.Count,
                Actors = await GetActors(movieActors),
                Countries = await GetCountries(movieCountries),
                Types = await GetTypes(movieTypes)
            };
        }

        private async Task<List<MovieActorResponse>> GetActors(List<MoviePerson> movieActors)
        {
            List<MovieActorResponse> result = new List<MovieActorResponse>();

            foreach (var movieActor in movieActors)
            {
                var actor = await _dataContext.Persons.SingleAsync(item => item.Id == movieActor.PersonId);
                result.Add(new MovieActorResponse()
                {
                    ActorId = movieActor.PersonId,
                    CharacterName = movieActor.CharacterName,
                    Name = actor.Name
                });
            }

            return result;
        }
        
        private async Task<List<MovieTypeResponse>> GetTypes(List<MovieType> movieTypes)
        {
            List<MovieTypeResponse> result = new List<MovieTypeResponse>();

            foreach (var movieType in movieTypes)
            {
                var type = await _dataContext.Types.SingleAsync(item => item.Id == movieType.TypeId);

                result.Add(new MovieTypeResponse()
                {
                    TypeId = movieType.TypeId,
                    Name = type.Name
                });
            }

            return result;
        }
        
        private async Task<List<MovieCountryResponse>> GetCountries(List<MovieCountry> movieCountries)
        {
            List<MovieCountryResponse> result = new List<MovieCountryResponse>();

            foreach (var movieCountry in movieCountries)
            {
                var country = await _dataContext.Countries.SingleAsync(item => item.Id == movieCountry.CountryId);

                result.Add(new MovieCountryResponse()
                {
                    CountryId = movieCountry.CountryId,
                    Name = country.Name
                });
            }

            return result;
        }
    }
}