using curso.api.Business.Entities;
using curso.api.Business.Repositories;
using curso.api.Configurations;
using curso.api.Filters;
using curso.api.Infraestruture.Data;
using curso.api.Infraestruture.Data.Repositories;
using curso.api.Model.Inputs;
using curso.api.Model.Outputs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace curso.api.Controllers
{
    [Route("api/v1/usuario")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthenticationService _authentication;

        public UserController(IUserRepository userRepository, IAuthenticationService authentication)
        {
            _userRepository = userRepository;
            _authentication = authentication;
        }

        /// <summary>
        /// Este serviço permite autenticar um usuário cadastrado e ativo
        /// </summary>
        /// <param name="login">login object</param>
        /// <returns>Retorna status ok, dados do usuário e o token em caso de sucesso</returns>
        [SwaggerResponse(statusCode: 200, description: "Authentication success", type: typeof(LoginInput))]
        [SwaggerResponse(statusCode: 400, description: "Mandatory fields", type: typeof(ErrorListOutput))]
        [SwaggerResponse(statusCode: 500, description: "Internal Error", type: typeof(ErrorOutput))]
        [HttpPost]
        [Route("login")]
        [FilterModelState]
        public IActionResult Login(LoginInput login)
        {
            User user = _userRepository.GetUser(login.Login);

            if(user == null)
            {
                return BadRequest("Falha no login");
            }

            //if(user.Pass != login.Pass.GeneratePassCripto())
            //{
            //    return BadRequest("Verifique usuário e senha");
            //}

            string token = _authentication.GetToken(user);

            return Ok(new
            {
                Token = token,
                Usuário = new UserOutput()
                { 
                    Code = user.Code,
                    Login = user.Login,
                    Email = user.Email
                }
            });
        }

        /// <summary>
        /// Este serviço permite registrar um usuário
        /// </summary>
        /// <param name="register">register object</param>
        /// <returns>Retorna status created e</returns>
        [SwaggerResponse(statusCode: 201, description: "Register success", type: typeof(User))]
        [SwaggerResponse(statusCode: 400, description: "Mandatory fields", type: typeof(ErrorListOutput))]
        [SwaggerResponse(statusCode: 500, description: "Internal Error", type: typeof(ErrorOutput))]
        [HttpPost]
        [Route("register")]
        [FilterModelState]
        public IActionResult Register(RegisterInput register)
        {
            
            //IEnumerable<string> pendingMigrations = context.Database.GetPendingMigrations();
            //if (pendingMigrations.Count() > 0)
            //{
            //    context.Database.Migrate();
            //}

            User user = new User()
            {
                Login = register.Login,
                Email = register.Email,
                Pass = register.Pass
            };

            _userRepository.Register(user);
            _userRepository.Commit();

            return Created("", user);
        }
    }
}
