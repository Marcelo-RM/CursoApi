using curso.api.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace curso.api.Controllers
{
    [Route("api/v1/usuario")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
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
