using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Logic.AppServices;
using Logic.AppServices.Commands;
using Logic.Data.DataContexts;
using Logic.Data.Entities;
using Logic.Data.Repositories.Interfaces;
using Logic.Dtos;
using Logic.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/movies")]
    public sealed class MovieController : BaseController
    {
        private readonly IMovieRepository _movieRepository;
        private readonly MovieDataContext _dataContext;
        private readonly Messages _messages;

        public MovieController(IMovieRepository movieRepository, MovieDataContext dataContext, Messages messages)
        {
            _movieRepository = movieRepository;
            _dataContext = dataContext;
            _messages = messages;
        }

        [HttpGet]
        public async  Task<IActionResult> GetList()
        {
            #region "New Code"
            var list = await _messages.Dispatch(new GetMovieListQuery());
            return Ok(list);
            #endregion

            #region "Old Code"
            /*
            IReadOnlyList<Movie> movies = await _movieRepository.GetMovies();
            List<MovieDto> dtos = movies.Select(x => ConvertToDto(x)).ToList();
            return Ok(dtos); */
            #endregion
        }

        [HttpPost("")]
        public async Task<IActionResult> InsertMovie( [FromBody] InsertMovieInfoDto infoDto)
        {
            var command = new InsertMovieInfoCommand()
            {
                Name = infoDto.Name,
                OriginalName = infoDto.OriginalName,
                ConstructionYear = infoDto.ConstructionYear,
                Description = infoDto.Description,
                PosterUrl = infoDto.PosterUrl,
                TotalMinute = infoDto.TotalMinute,
                VisionEntryDate = infoDto.VisionEntryDate
            };
            
            Result result = await _messages.Dispatch(command);
            return result.IsSuccess ? Ok() : Error(result.Error);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditMovie(int id, [FromBody] EditMovieInfoDto infoDto)
        {
            #region "With messages"
            var command = new EditMovieInfoCommand()
            {
                Name = infoDto.Name,
                OriginalName = infoDto.OriginalName,
                ConstructionYear = infoDto.ConstructionYear,
                Description = infoDto.Description,
                PosterUrl = infoDto.PosterUrl,
                TotalMinute = infoDto.TotalMinute,
                VisionEntryDate = infoDto.VisionEntryDate,
                Id = id
                
            };
            Result result = await _messages.Dispatch(command);
            return result.IsSuccess ? Ok() : Error(result.Error);
            #endregion
            
            #region "With command handler"
            /*var command = new EditMovieInfoCommand()
            {
                Name = infoDto.Name,
                OriginalName = infoDto.OriginalName,
                ConstructionYear = infoDto.ConstructionYear,
                Description = infoDto.Description,
                PosterUrl = infoDto.PosterUrl,
                TotalMinute = infoDto.TotalMinute,
                VisionEntryDate = infoDto.VisionEntryDate,
                Id = id
            };
            var handler = new EditMovieInfoCommandHandler(_movieRepository, _dataContext);
            Result result = await handler.Handle(command);
            return result.IsSuccess ? Ok() : Error(result.Error);*/
            #endregion
            
            #region "Old Code"
            /*
            Movie movie = await _movieRepository.GetMovieById(id);
            if (movie == null)
            {
                return Error($"No movie found for Id {id}");
            }
            movie.OriginalName = infoDto.OriginalName;
            movie.Description = infoDto.Description;
            movie.Name = infoDto.Name;
            movie.PosterUrl = infoDto.PosterUrl;
            movie.TotalMinute = infoDto.TotalMinute;
            movie.VisionEntryDate = infoDto.VisionEntryDate;
            movie.ConstructionYear = infoDto.ConstructionYear;
            _dataContext.SaveChanges();
            return Ok(); */
            #endregion
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var command = new DeleteMovieCommand() {Id = id};
            Result result = await _messages.Dispatch(command);
            return result.IsSuccess ? Ok() : Error(result.Error);
        }
        
        [HttpPut("upsert-person/{movieId}")]
        public async Task<IActionResult> UpsertPersonToMovie(int movieId, [FromBody] UpsertPersonToMovieDto dto)
        {
            var command = new UpsertPersonToMovieCommand()
            {
                MovieId = movieId,
                MoviePersons = dto.MoviePersons
            };
            
            Result result = await _messages.Dispatch(command);
            return result.IsSuccess ? Ok() : Error(result.Error);
        }
        
        [HttpPut("upsert-country/{movieId}")]
        public async Task<IActionResult> UpsertCountryToMovie(int movieId, [FromBody] UpsertCountryToMovieDto dto)
        {
            var command = new UpsertCountryToMovieCommand()
            {
                MovieId = movieId,
                CountryIds = dto.CountryIds
            };
            
            Result result = await _messages.Dispatch(command);
            return result.IsSuccess ? Ok() : Error(result.Error);
        }
        
        [HttpPut("upsert-type/{movieId}")]
        public async Task<IActionResult> UpsertTypeToMovie(int movieId, [FromBody] UpsertTypeToMovieDto dto)
        {
            var command = new UpsertTypeToMovieCommand()
            {
                MovieId = movieId,
                TypeIds = dto.TypeIds
            };
            
            Result result = await _messages.Dispatch(command);
            return result.IsSuccess ? Ok() : Error(result.Error);
        }
        
        [HttpPut("activate/{movieId}")]
        public async Task<IActionResult> ActivateMovie(int movieId)
        {
            var command = new ActivateMovieCommand()
            {
                MovieId = movieId
            };
            
            Result result = await _messages.Dispatch(command);
            return result.IsSuccess ? Ok() : Error(result.Error);
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