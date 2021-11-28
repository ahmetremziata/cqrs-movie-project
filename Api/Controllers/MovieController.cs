using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Logic.AppServices.Commands;
using Logic.AppServices.Commands.Handlers;
using Logic.AppServices.Queries;
using Logic.Business.Service.Crud;
using Logic.Data.DataContexts;
using Logic.Dtos;
using Logic.Model;
using Logic.Requests;
using Logic.Responses;
using Logic.Utils;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers
{
    [Route("api/movies")]
    public sealed class MovieController : BaseController
    {
        private readonly MovieDataContext _dataContext;
        private readonly IMovieService _movieService;
        private readonly Messages _messages;
        private readonly IMediator _mediator;

        public MovieController(MovieDataContext dataContext, Messages messages, IMediator mediator, IMovieService movieService)
        {
            _dataContext = dataContext;
            _messages = messages;
            _mediator = mediator;
            _movieService = movieService;
        }
        
        [HttpGet("without-cqrs")]
        public async  Task<IActionResult> GetListWithoutCqrs()
        {
            List<CrudMovieModel> response = await _movieService.GetMovies();
            return Ok(response);
        }
        
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(InsertResponse))]
        [HttpPost("without-cqrs")]
        public async Task<IActionResult> InsertMovieWithoutCqrs( [FromBody] InsertMovieInfoRequest infoRequest)
        {
            InsertResult result = await _movieService.InsertMovie(new CrudMovieModel()
            {
                Name = infoRequest.Name,
                OriginalName = infoRequest.OriginalName,
                ConstructionYear = infoRequest.ConstructionYear,
                Description = infoRequest.Description,
                PosterUrl = infoRequest.PosterUrl,
                TotalMinute = infoRequest.TotalMinute,
                VisionEntryDate = infoRequest.VisionEntryDate
            });
            
            return result.Result.IsSuccess ? Ok(result.InsertResponse) : Error(result.Result.Error);
        }
        
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        [HttpPut("without-cqrs/{id}")]
        public async Task<IActionResult> EditMovieWithoutCqrs(int id, [FromBody] EditMovieInfoRequest infoRequest)
        {
            var result =  await _movieService.EditMovie(new CrudMovieModel()
            {
                Id = id,
                ConstructionYear = infoRequest.ConstructionYear,
                Description = infoRequest.Description,
                Name = infoRequest.Name,
                OriginalName = infoRequest.OriginalName,
                PosterUrl = infoRequest.PosterUrl,
                VisionEntryDate = infoRequest.VisionEntryDate
            });
            return result.IsSuccess ? Ok() : Error(result.Error);
        }
        
        //With handler
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        [HttpPut("with-handler/{id}")]
        public async Task<IActionResult> EditMovieWithHandler(int id, [FromBody] EditMovieInfoRequest infoRequest)
        {
            var command = new EditMovieInfoCommand(id, infoRequest.Name, infoRequest.OriginalName,
                infoRequest.Description, infoRequest.ConstructionYear, infoRequest.TotalMinute, infoRequest.PosterUrl,
                infoRequest.VisionEntryDate);
            var handler = new EditMovieInfoCommandHandler(_dataContext);
            Result result = await handler.Handle(command);
            return result.IsSuccess ? Ok() : Error(result.Error);
        }

        //With mediator pattern
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        [HttpPut("with-mediator-pattern/{id}")]
        public async Task<IActionResult> EditMovieWithMediatorPattern(int id, [FromBody] EditMovieInfoRequest infoRequest)
        {
            #region "With messages"
            var command = new EditMovieInfoCommand(id, infoRequest.Name, infoRequest.OriginalName,
                infoRequest.Description, infoRequest.ConstructionYear, infoRequest.TotalMinute, infoRequest.PosterUrl,
                infoRequest.VisionEntryDate);
            Result result = await _messages.Dispatch(command);
            return result.IsSuccess ? Ok() : Error(result.Error);
            #endregion
        }
        
        //With mediator pattern (mediatr package)
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        [HttpPut("with-mediatr/{id}")]
        public async Task<IActionResult> EditMovieWithMediatr(int id, [FromBody] EditMovieInfoRequest infoRequest)
        {
            #region "With messages"
            var command = new EditMovieInfoCommandWithMediatr(id, infoRequest.Name, infoRequest.OriginalName,
                infoRequest.Description, infoRequest.ConstructionYear, infoRequest.TotalMinute, infoRequest.PosterUrl,
                infoRequest.VisionEntryDate);
            Result result = await _mediator.Send(command);
            return result.IsSuccess ? Ok() : Error(result.Error);
            #endregion
        }

        [HttpGet]
        public async  Task<IActionResult> GetList()
        {
            var list = await _messages.Dispatch(new GetMovieListQuery());
            return Ok(list);
        }
        
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(List<MovieResponse>))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        [HttpGet("search-by-filter")]
        public async  Task<IActionResult> GetListByFilter(SearchAdminMovieRequest request)
        {
            var list = await _messages.Dispatch(new GetMovieListByFilterQuery()
            {
                MovieName = request.MovieName,
                ActorName = request.ActorName,
                ConstructionYear = request.ConstructionYear,
                CountryId = request.CountryId,
                TypeId = request.TypeId,
                IsDomestic = request.IsDomestic,
                IsInternational = request.IsInternational
            });
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
        
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var command = new DeleteMovieCommand() {Id = id};
            Result result = await _messages.Dispatch(command);
            return result.IsSuccess ? Ok() : Error(result.Error);
        }
        
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
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
        
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
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
        
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
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
        
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
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
        
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
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
        
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
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

        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [HttpPut("insert/actor/{movieId}")]
        public async Task<IActionResult> InsertActorToMovie(int movieId, [FromBody] MoviePersonRequest request)
        {
            var command = new InsertActorToMovieCommand()
            {
                MovieId = movieId,
                PersonId = request.PersonId,
                RoleId = request.RoleId,
                CharacterName =  request.CharacterName
            };
            
            Result result = await _messages.Dispatch(command);
            return result.IsSuccess ? Ok() : Error(result.Error);
        }

        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [HttpPut("remove/type/{movieId}")]
        public async Task<IActionResult> RemoveTypeFromMovie(int movieId, [FromBody] MovieTypeRequest request)
        {
            var command = new RemoveTypeFromMovieCommand()
            {
                MovieId = movieId,
                TypeId = request.TypeId
            };
            
            Result result = await _messages.Dispatch(command);
            return result.IsSuccess ? Ok() : Error(result.Error);
        }
        
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [HttpPut("insert/type/{movieId}")]
        public async Task<IActionResult> InsertTypeToMovie(int movieId, [FromBody] MovieTypeRequest request)
        {
            var command = new InsertTypeToMovieCommand()
            {
                MovieId = movieId,
                TypeId = request.TypeId
            };
            
            Result result = await _messages.Dispatch(command);
            return result.IsSuccess ? Ok() : Error(result.Error);
        }
        
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [HttpPut("remove/country/{movieId}")]
        public async Task<IActionResult> RemoveCountryFromMovie(int movieId, [FromBody] MovieCountryRequest request)
        {
            var command = new RemoveCountryFromMovieCommand()
            {
                MovieId = movieId,
                CountryId = request.CountryId
            };
            
            Result result = await _messages.Dispatch(command);
            return result.IsSuccess ? Ok() : Error(result.Error);
        }
        
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [HttpPut("insert/country/{movieId}")]
        public async Task<IActionResult> InsertCountryToMovie(int movieId, [FromBody] MovieCountryRequest request)
        {
            var command = new InsertCountryToMovieCommand()
            {
                MovieId = movieId,
                CountryId = request.CountryId
            };
            
            Result result = await _messages.Dispatch(command);
            return result.IsSuccess ? Ok() : Error(result.Error);
        }
        
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
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
    }
}