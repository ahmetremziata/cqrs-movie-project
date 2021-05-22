using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Logic.AppServices;
using Logic.Requests;
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
        
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(List<String>))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [HttpGet]
        public async  Task<IActionResult> GetMovies([FromQuery] SearchMovieRequest request)
        {
            var list = await _messages.Dispatch(new GetMoviePresentationListQuery()
            {
                Page = request.Page,
                Size = request.Size,
                Name = request.Name,
                TypeId = request.TypeId,
                ConstructionYear = request.ConstructionYear,
                CountryId = request.CountryId
            });
            return Ok(list);
        }
        
        [HttpGet("{movieId}")]
        public async  Task<IActionResult> GetMovieById(int movieId)
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
    }
}