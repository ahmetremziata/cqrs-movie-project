using System.Net;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Logic.AppServices.Commands;
using Logic.AppServices.Queries;
using Logic.Data.DataContexts;
using Logic.Data.Entities;
using Logic.Data.Repositories.Interfaces;
using Logic.Dtos;
using Logic.Requests;
using Logic.Responses;
using Logic.Utils;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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
        
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(MovieDetailResponse))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        [HttpGet("{movieId}")]
        public async  Task<IActionResult> GetMovieById(int movieId)
        {
            #region "New Code"
            var movie = await _messages.Dispatch(new GetMovieByIdQuery()
            {
                MovieId = movieId
            });

            if (movie == null)
            {
                return NotFound();
            }
            
            return Ok(movie);
            #endregion

            #region "Old Code"
            
            /*IReadOnlyList<Movie> movies = await _movieRepository.GetMovies();
            List<MovieDto> dtos = movies.Select(x => ConvertToDto(x)).ToList();
            return Ok(dtos); */
            #endregion
        }

        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(InsertResponse))]
        [HttpPost("")]
        public async Task<IActionResult> InsertMovie( [FromBody] InsertMovieInfoRequest infoRequest)
        {
            var command = new InsertMovieInfoCommand()
            {
                Name = infoRequest.Name,
                OriginalName = infoRequest.OriginalName,
                ConstructionYear = infoRequest.ConstructionYear,
                Description = infoRequest.Description,
                PosterUrl = infoRequest.PosterUrl,
                TotalMinute = infoRequest.TotalMinute,
                VisionEntryDate = infoRequest.VisionEntryDate
            };
            
            InsertResult result = await _messages.InsertDispatch(command);
            return result.Result.IsSuccess ? Ok(result.InsertResponse) : Error(result.Result.Error);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditMovie(int id, [FromBody] EditMovieInfoRequest infoRequest)
        {
            #region "With messages"
            var command = new EditMovieInfoCommand()
            {
                Name = infoRequest.Name,
                OriginalName = infoRequest.OriginalName,
                ConstructionYear = infoRequest.ConstructionYear,
                Description = infoRequest.Description,
                PosterUrl = infoRequest.PosterUrl,
                TotalMinute = infoRequest.TotalMinute,
                VisionEntryDate = infoRequest.VisionEntryDate,
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
        public async Task<IActionResult> UpsertPersonToMovie(int movieId, [FromBody] UpsertPersonToMovieRequest request)
        {
            var command = new UpsertPersonToMovieCommand()
            {
                MovieId = movieId,
                MoviePersons = request.MoviePersons
            };
            
            Result result = await _messages.Dispatch(command);
            return result.IsSuccess ? Ok() : Error(result.Error);
        }
        
        [HttpPut("upsert-country/{movieId}")]
        public async Task<IActionResult> UpsertCountryToMovie(int movieId, [FromBody] UpsertCountryToMovieRequest request)
        {
            var command = new UpsertCountryToMovieCommand()
            {
                MovieId = movieId,
                CountryIds = request.CountryIds
            };
            
            Result result = await _messages.Dispatch(command);
            return result.IsSuccess ? Ok() : Error(result.Error);
        }
        
        [HttpPut("upsert-type/{movieId}")]
        public async Task<IActionResult> UpsertTypeToMovie(int movieId, [FromBody] UpsertTypeToMovieRequest request)
        {
            var command = new UpsertTypeToMovieCommand()
            {
                MovieId = movieId,
                TypeIds = request.TypeIds
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
        
        [HttpPut("deactivate/{movieId}")]
        public async Task<IActionResult> DeactivateMovie(int movieId)
        {
            var command = new DeactivateMovieCommand()
            {
                MovieId = movieId
            };
            
            Result result = await _messages.Dispatch(command);
            return result.IsSuccess ? Ok() : Error(result.Error);
        }
        
        [HttpPut("remove/actor/{movieId}")]
        public async Task<IActionResult> RemoveActorFromMovie(int movieId, [FromBody] MoviePersonRequest request)
        {
            var command = new RemoveActorFromMovieCommand()
            {
                MovieId = movieId,
                PersonId = request.PersonId,
                RoleId = request.RoleId
            };
            
            Result result = await _messages.Dispatch(command);
            return result.IsSuccess ? Ok() : Error(result.Error);
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