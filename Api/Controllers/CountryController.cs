using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Logic.AppServices.Commands;
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

            return response == null ? NotFound() : Ok(response);
        }

        [SwaggerResponse((int) HttpStatusCode.OK)]
        [HttpPost("")]
        public async Task<IActionResult> InsertCountry([FromBody] string countryName)
        {
            var result = await _messages.Dispatch(new InsertCountryInfoCommand()
            {
                Name = countryName
            });

            return result.IsSuccess ? Ok() : Error(result.Error);
        }

        [SwaggerResponse((int) HttpStatusCode.OK)]
        [SwaggerResponse((int) HttpStatusCode.NotFound)]
        [HttpPut("{countryId}")]
        public async Task<IActionResult> UpdateCountry(int countryId, [FromBody] string countryName)
        {
            var result = await _messages.Dispatch(new EditCountryInfoCommand()
            {
                CountryId = countryId,
                Name = countryName
            });
            
            return result.IsSuccess ? Ok() : Error(result.Error);
        }
        
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        [HttpDelete("{countryId}")]
        public async  Task<IActionResult> DeleteCountry(int countryId)
        {
            var result = await _messages.Dispatch(new DeleteCountryCommand()
            {
                CountryId = countryId
            });
            return result.IsSuccess ? Ok() : NotFound(result.Error);        
        }
    }
}