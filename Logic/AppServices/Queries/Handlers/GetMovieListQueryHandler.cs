using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logic.Data.DataContexts;
using Logic.Data.Entities;
using Logic.Decorators;
using Logic.Responses;
using Microsoft.EntityFrameworkCore;

namespace Logic.AppServices.Queries.Handlers
{
    public sealed class GetMovieListQueryHandler : IQueryHandler<GetMovieListQuery, List<MovieResponse>>
    {
        private readonly MovieDataContext _dataContext;

        public GetMovieListQueryHandler(MovieDataContext dataContext)
        {
            _dataContext = dataContext;
        }
        
        public async Task<List<MovieResponse>> Handle(GetMovieListQuery query)
        {
            IReadOnlyList<Movie> movies = await _dataContext.Movies.ToListAsync();
            List<MovieResponse> dtos = movies.Select(x => ConvertToDto(x)).ToList();
            return dtos;
        }
        
        private MovieResponse ConvertToDto(Movie movie)
        {
            return new MovieResponse
            {
                Id = movie.Id,
                Name = movie.Name,
                OriginalName = movie.OriginalName,
                ConstructionYear = movie.ConstructionYear,
                TotalMinute = movie.TotalMinute,
                IsActive = movie.IsActive
            };
        }
    }
}