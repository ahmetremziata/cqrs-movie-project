
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
        
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(InsertResponse))]
        [HttpPost("")]
        public async  Task<IActionResult> InsertPerson([FromBody] InsertPersonInfoResponse insertPersonInfoResponse)
        {
            var result = await _messages.InsertDispatch(new InsertPersonInfoCommand()
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
            return result.Result.IsSuccess ? Ok(result.InsertResponse) : Error(result.Result.Error);        
        }
        
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [HttpPut("{id}")]
        public async  Task<IActionResult> EditPerson(int id, [FromBody] EditPersonInfoResponse editPersonInfoResponse)
        {
            var result = await _messages.Dispatch(new EditPersonInfoCommand()
            {
                Id = id,
                Name = editPersonInfoResponse.Name,
                AvatarUrl = editPersonInfoResponse.AvatarUrl,
                Biography = editPersonInfoResponse.Biography,
                BirthDate = editPersonInfoResponse.BirthDate,
                BirthPlace = editPersonInfoResponse.BirthPlace,
                DeathDate = editPersonInfoResponse.DeathDate,
                DeathPlace = editPersonInfoResponse.DeathPlace,
                RealName = editPersonInfoResponse.RealName
            });
            return result.IsSuccess ? Ok() : Error(result.Error);        
        }
        
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [HttpDelete("{id}")]
        public async  Task<IActionResult> DeletePerson(int id)
        {
            var result = await _messages.Dispatch(new DeletePersonCommand()
            {
                Id = id,
            });
            return result.IsSuccess ? Ok() : Error(result.Error);        
        }
    }
}