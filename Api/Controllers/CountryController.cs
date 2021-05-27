using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Logic.AppServices.Queries;
using Logic.Responses;
using Logic.Utils;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers
{
    [Route("api/countries")]
    public sealed class CountryController : BaseController
    {
        private readonly Messages _messages;

        public CountryController(Messages messages)
        {
            _messages = messages;
        }

        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(List<CountryResponse>))]
        [HttpGet]
        public async  Task<IActionResult> GetCountries()
        {
            var list = await _messages.Dispatch(new GetCountryListQuery());
            return Ok(list);
        }
        
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(CountryResponse))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        [HttpGet("{countryId}")]
        public async  Task<IActionResult> GetCountryById(int countryId)
        {
            var response = await _messages.Dispatch(new GetCountryByIdQuery()
            {
                CountryId = countryId
            });

            if (response == null)
            {
                return NotFound();
            }
            
            return Ok(response);
        }
    }
}