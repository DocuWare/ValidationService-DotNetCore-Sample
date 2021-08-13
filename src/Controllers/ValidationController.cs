using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ValidationServiceDotNetCoreSample.Interfaces;
using ValidationServiceDotNetCoreSample.Models;

namespace ValidationServiceDotNetCoreSample.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ValidationController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IInputModelValidationService _inputModelCheckService;

        public ValidationController(IUserService userService, IInputModelValidationService inputModelCheckService)
        {
            _userService = userService;
            _inputModelCheckService = inputModelCheckService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody]AuthenticateModel model)
        {
            var user = await _userService.Authenticate(model.Username, model.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        [HttpGet]
        public ObjectResult Get()
        {
            return StatusCode(200, new ReturnValueModel { Status = ReturnModelStatus.Ok, Reason = "This is a test" });
        }

        [HttpPost]
        [Route("console")]
        public async Task<ObjectResult> PostToConsoleAsync([FromBody] InputValueModel model)
        {
            return StatusCode(200, await _inputModelCheckService.WriteAllValuesToConsole(model));
        }

        [HttpPost]
        [Route("dwsystem")]
        public async Task<ObjectResult> PostDwSystemAsync([FromBody] InputValueModel model)
        {
            return StatusCode(200, await _inputModelCheckService.CheckValuesAgainstDocuWareSystem(model));
        }

        [HttpPost]
        [Route("rootValues")]
        public async Task<ObjectResult> PostValidateRootValuesAsync([FromBody] InputValueModel model)
        {
            return StatusCode(200, await _inputModelCheckService.CheckRootValuesSimple(model));
        }

        [HttpPost]
        [Route("values")]
        public async Task<ObjectResult> PostValidateValuesAsync([FromBody] InputValueModel model)
        {
            return StatusCode(200, await _inputModelCheckService.CheckValuesSimple(model));
        }
    }
}