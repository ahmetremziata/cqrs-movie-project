
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
    }
}