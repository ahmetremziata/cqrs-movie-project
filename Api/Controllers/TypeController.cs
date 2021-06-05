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
        
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [HttpPost("")]
        public async  Task<IActionResult> InsertType([FromBody] string typeName)
        {
            var result = await _messages.Dispatch(new InsertTypeInfoCommand()
            {
                Name = typeName
            });
            return result.IsSuccess ? Ok() : Error(result.Error);        
        }
        
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        [HttpPut("{typeId}")]
        public async  Task<IActionResult> UpdateType(int typeId, [FromBody] string typeName)
        {
            var result = await _messages.Dispatch(new EditTypeInfoCommand()
            {
                TypeId = typeId,
                Name = typeName
            });
            return result.IsSuccess ? Ok() : NotFound(result.Error);        
        }
        
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        [HttpDelete("{typeId}")]
        public async  Task<IActionResult> Delete(int typeId)
        {
            var result = await _messages.Dispatch(new DeleteTypeCommand()
            {
                TypeId = typeId
            });
            return result.IsSuccess ? Ok() : NotFound(result.Error);        
        }
    }
}