using System.Threading.Tasks;
using Logic.AppServices.Queries;
using Logic.Utils;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet]
        public async  Task<IActionResult> GetTypes()
        {
            var list = await _messages.Dispatch(new GetTypeListQuery());
            return Ok(list);
        }
    }
}