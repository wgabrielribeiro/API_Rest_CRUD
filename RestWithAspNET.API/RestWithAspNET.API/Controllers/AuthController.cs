using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestWithAspNET.API.Business;
using RestWithAspNET.API.Data.VO;

namespace RestWithAspNET.API.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AuthController : ControllerBase
    {
        private ILoginBusiness _loginBusiness;

        public AuthController(ILoginBusiness loginBusiness)
        {
            _loginBusiness = loginBusiness;
        }

        [HttpPost]
        [Route("signin")]
        [ProducesResponseType(200, Type = typeof(UserVO))]
        [ProducesResponseType(401)]
        [ProducesResponseType(400)]
        public IActionResult Signin([FromBody] UserVO user)
        {
            if (user == null) return BadRequest("Invalid client request");

            var token = _loginBusiness.ValidateCredentials(user);

            if (token == null) return Unauthorized();

            return Ok(token);
        }

        [HttpPost]
        [Route("refresh")]
        [ProducesResponseType(200, Type = typeof(TokenVO))]
        [ProducesResponseType(400)]
        public IActionResult Refresh([FromBody] TokenVO token)
        {
            if (token is null) return BadRequest("Invalid client request");

            var Datatoken = _loginBusiness.ValidateCredentials(token);

            if (Datatoken == null) return BadRequest("Invalid client request");

            return Ok(Datatoken);
        }

        [HttpGet]
        [Route("revoke")]
        [Authorize("Bearer")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult Revoke()
        {
            var username = User.Identity.Name;

            var result = _loginBusiness.RevokeToken(username);

            if (!result) return BadRequest("Invalid client request");

            return NoContent();
        }


    }
}
