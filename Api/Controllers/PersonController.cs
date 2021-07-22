
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
    [Route("api/persons")]
    public class PersonController : BaseController
    {
        private readonly Messages _messages;

        public PersonController(Messages messages)
        {
            _messages = messages;
        }
        
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(List<PersonResponse>))]
        [HttpGet]
        public async  Task<IActionResult> GetPersons()
        {
            var list = await _messages.Dispatch(new GetPersonListQuery());
            return Ok(list);
        }
        
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(PersonDetailResponse))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        [HttpGet("{personId}")]
        public async  Task<IActionResult> GetPersonById(int personId)
        {
            var response = await _messages.Dispatch(new GetPersonByIdQuery()
            {
                PersonId = personId
            });            
            
            return response == null ? NotFound() : Ok(response);
        }
        
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [HttpPost("")]
        public async  Task<IActionResult> InsertPerson([FromBody] InsertPersonInfoResponse insertPersonInfoResponse)
        {
            var result = await _messages.Dispatch(new InsertPersonInfoCommand()
            {
                Name = insertPersonInfoResponse.Name,
                AvatarUrl = insertPersonInfoResponse.AvatarUrl,
                Biography = insertPersonInfoResponse.Biography,
                BirthDate = insertPersonInfoResponse.BirthDate,
                BirthPlace = insertPersonInfoResponse.BirthPlace,
                DeathDate = insertPersonInfoResponse.DeathDate,
                DeathPlace = insertPersonInfoResponse.DeathPlace,
                RealName = insertPersonInfoResponse.RealName
            });
            return result.IsSuccess ? Ok() : Error(result.Error);        
        }
    }
}