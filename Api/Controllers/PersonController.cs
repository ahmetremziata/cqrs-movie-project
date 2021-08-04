
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Logic.AppServices.Commands;
using Logic.AppServices.Queries;
using Logic.Requests;
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
        public async  Task<IActionResult> InsertPerson([FromBody] InsertPersonInfoRequest insertPersonInfoRequest)
        {
            var result = await _messages.InsertDispatch(new InsertPersonInfoCommand()
            {
                Name = insertPersonInfoRequest.Name,
                AvatarUrl = insertPersonInfoRequest.AvatarUrl,
                Biography = insertPersonInfoRequest.Biography,
                BirthDate = insertPersonInfoRequest.BirthDate,
                BirthPlace = insertPersonInfoRequest.BirthPlace,
                DeathDate = insertPersonInfoRequest.DeathDate,
                DeathPlace = insertPersonInfoRequest.DeathPlace,
                RealName = insertPersonInfoRequest.RealName
            });
            return result.Result.IsSuccess ? Ok(result.InsertResponse) : Error(result.Result.Error);        
        }
        
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [HttpPut("{id}")]
        public async  Task<IActionResult> EditPerson(int id, [FromBody] EditPersonInfoRequest editPersonInfoRequest)
        {
            var result = await _messages.Dispatch(new EditPersonInfoCommand()
            {
                Id = id,
                Name = editPersonInfoRequest.Name,
                AvatarUrl = editPersonInfoRequest.AvatarUrl,
                Biography = editPersonInfoRequest.Biography,
                BirthDate = editPersonInfoRequest.BirthDate,
                BirthPlace = editPersonInfoRequest.BirthPlace,
                DeathDate = editPersonInfoRequest.DeathDate,
                DeathPlace = editPersonInfoRequest.DeathPlace,
                RealName = editPersonInfoRequest.RealName
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