using CustomerService.Services.LoginServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CustomerService.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        /// <summary>
        /// Endpoint para fazer login do usuário
        /// </summary>
        /// <param name="agency"></param>
        /// <param name="account"></param>
        /// <param name="service"></param>
        /// <returns> Retorna mensagem autênticado cliente</returns>
        [AllowAnonymous]
        [HttpGet("/api/login/{agency}/{account}")]
        public async Task<object> Login(string agency, string account, [FromServices] ILoginService service)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await service.FindByLogin(agency, account);

                if (result != null)
                    return Ok(result);
                else
                    return NotFound();
            }
            catch (ArgumentException ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
