using curso.api.Model.Inputs;
using curso.api.Model.Outputs;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace curso.api.Controllers
{
    [Route("api/v1/usuario")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        /// <summary>
        /// Este serviço permite autenticar um usuário cadastrado e ativo
        /// </summary>
        /// <param name="login">login object</param>
        /// <returns>Retorna status ok, dados do usuário e o token em caso de sucesso</returns>
        [SwaggerResponse(statusCode: 200, description: "Authentication success", type: typeof(LoginInput))]
        [SwaggerResponse(statusCode: 400, description: "Mandatory fields", type: typeof(ValidFieldsOutput))]
        [SwaggerResponse(statusCode: 500, description: "Internal Error", type: typeof(ErrorOutput))]
        [HttpPost]
        [Route("login")]
        public IActionResult Logar(LoginInput login)
        {
            return Ok(login);
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Registrar(RegisterInput register)
        {
            return Created("", register);
        }
    }
}
