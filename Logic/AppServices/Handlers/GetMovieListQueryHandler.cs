using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logic.Data.DataContexts;
using Logic.Data.Entities;
using Logic.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Logic.AppServices
{
    public sealed class GetMovieListQueryHandler : IQueryHandler<GetMovieListQuery, List<MovieDto>>
    {
        private readonly MovieDataContext _dataContext;

        public GetMovieListQueryHandler(MovieDataContext dataContext)
        {
            _dataContext = dataContext;
        }
        
        public async Task<List<MovieDto>> Handle(GetMovieListQuery command)
        {
            IReadOnlyList<Movie> movies = await _dataContext.Movies.ToListAsync();
            List<MovieDto> dtos = movies.Select(x => ConvertToDto(x)).ToList();
            return dtos;
        }
        
        private MovieDto ConvertToDto(Movie movie)
        {
            return new MovieDto
            {
                Id = movie.Id,
                Name = movie.Name,
                OriginalName = movie.OriginalName,
                Description = movie.Description,
                ConstructionYear = movie.ConstructionYear,
                TotalMinute = movie.TotalMinute,
                PosterUrl = movie.PosterUrl,
                VisionEntryDate = movie.VisionEntryDate
            };
        }
    }
}