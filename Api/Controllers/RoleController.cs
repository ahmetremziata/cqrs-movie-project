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
    [Route("api/roles")]
    public class RoleController : BaseController
    {
        private readonly Messages _messages;

        public RoleController(Messages messages)
        {
            _messages = messages;
        }
        
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(List<RoleResponse>))]
        [HttpGet]
        public async  Task<IActionResult> GetRoles()
        {
            var list = await _messages.Dispatch(new GetRoleListQuery());
            return Ok(list);
        }
        
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [HttpPost("")]
        public async  Task<IActionResult> InsertRole([FromBody] string roleName)
        {
            var list = await _messages.Dispatch(new InsertRoleInfoCommand()
            {
                Name = roleName
            });
            return Ok();
        }
    }
}