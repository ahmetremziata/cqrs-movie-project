using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Logic.AppServices;
using Logic.AppServices.Queries;
using Logic.Requests;
using Logic.Responses;
using Logic.Utils;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers
{
    [Route("api/movies/presentation")]
    public class MoviePresentationController : BaseController
    {
        private readonly Messages _messages;

        public MoviePresentationController(Messages messages)
        {
            _messages = messages;
        }
        
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(MoviePresentationResponse))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [HttpGet]
        public async  Task<IActionResult> GetMovies([FromQuery] SearchMovieRequest request)
        {
            var response = await _messages.Dispatch(new GetMoviePresentationListQuery()
            {
                Page = request.Page,
                Size = request.Size,
                Name = request.Name,
                TypeId = request.TypeId,
                ConstructionYear = request.ConstructionYear,
                CountryId = request.CountryId
            });
            return Ok(response);
        }
        
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(Logic.Indexes.Movie))]
        [HttpGet("{movieId}")]
        public async  Task<IActionResult> GetMovieById(int movieId)
        {
            var response = await _messages.Dispatch(new GetMoviePresentationQuery()
            {
                MovieId = movieId
            });
            return Ok(response);
        }
    }
}