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
    [Route("api/types")]
    public sealed class TypeController : BaseController
    {
        private readonly Messages _messages;

        public TypeController(Messages messages)
        {
            _messages = messages;
        }

        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(List<TypeResponse>))]
        [HttpGet]
        public async  Task<IActionResult> GetTypes()
        {
            var list = await _messages.Dispatch(new GetTypeListQuery());
            return Ok(list);
        }
        
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(TypeResponse))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        [HttpGet("{typeId}")]
        public async  Task<IActionResult> GetTypeById(int typeId)
        {
            var response = await _messages.Dispatch(new GetTypeByIdQuery()
            {
                TypeId = typeId
            });

            if (response == null)
            {
                return NotFound();
            }
            
            return Ok(response);
        }
    }
}