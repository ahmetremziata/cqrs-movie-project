using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Logic.AppServices.Commands;
using Logic.AppServices.Commands.Handlers;
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
        
        [HttpGet("without-handler")]
        public async  Task<IActionResult> GetListWithoutHandler()
        {
            IReadOnlyList<Movie> movies = await _movieRepository.GetMovies();
            List<MovieResponse> response = movies.Select(x => ConvertToDto(x)).ToList();
            return Ok(response);
        }

        [HttpGet]
        public async  Task<IActionResult> GetList()
        {
            var list = await _messages.Dispatch(new GetMovieListQuery());
            return Ok(list);
        }
        
        
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(MovieDetailResponse))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        [HttpGet("{movieId}")]
        public async  Task<IActionResult> GetMovieById(int movieId)
        {
            var movie = await _messages.Dispatch(new GetMovieByIdQuery()
            {
                MovieId = movieId
            });

            if (movie == null)
            {
                return NotFound();
            }
            
            return Ok(movie);
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
        
        //Old code
        [HttpPut("without-handler/{id}")]
        public async Task<IActionResult> EditMovieWithoutHandler(int id, [FromBody] EditMovieInfoRequest infoRequest)
        {
            Movie movie = await _movieRepository.GetMovieById(id);
            if (movie == null)
            {
                return Error($"No movie found for Id {id}");
            }
            movie.OriginalName = infoRequest.OriginalName;
            movie.Description = infoRequest.Description;
            movie.Name = infoRequest.Name;
            movie.PosterUrl = infoRequest.PosterUrl;
            movie.TotalMinute = infoRequest.TotalMinute;
            movie.VisionEntryDate = infoRequest.VisionEntryDate;
            movie.ConstructionYear = infoRequest.ConstructionYear;
            _dataContext.SaveChanges();
            return Ok();
        }
        
        //Old code
        [HttpPut("with-handler/{id}")]
        public async Task<IActionResult> EditMovieWithHandler(int id, [FromBody] EditMovieInfoRequest infoRequest)
        {
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
            var handler = new EditMovieInfoCommandHandler(_dataContext);
            Result result = await handler.Handle(command);
            return result.IsSuccess ? Ok() : Error(result.Error);
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
        
        [HttpPut("synchronize/{movieId}")]
        public async Task<IActionResult> SynchronizeMovie(int movieId)
        {
            var command = new SynchronizeMovieCommand()
            {
                MovieId = movieId
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
                IsActive = movie.IsActive,
                IsSynchronized = movie.IsSynchronized
            };
        }
    }
}