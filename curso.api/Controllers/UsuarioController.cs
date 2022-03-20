using curso.api.Business.Entities;
using curso.api.Filters;
using curso.api.Infraestruture.Data;
using curso.api.Model.Inputs;
using curso.api.Model.Outputs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public class UsuarioController : ControllerBase
    {
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

            UsuarioOutput usuarioOutput = new UsuarioOutput()
            {
                Code = "1",
                Login = "Marcelo",
                Email = "marcelordrgs98@gmail.com"
            };

            var secret = Encoding.ASCII.GetBytes("KW7GFQEaauf3peSW");
            var symmetricSecurityKey = new SymmetricSecurityKey(secret);
            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, usuarioOutput.Code.ToString()),
                    new Claim(ClaimTypes.Name, usuarioOutput.Login.ToString()),
                    new Claim(ClaimTypes.Email, usuarioOutput.Email.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature)
            };
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var tokenGenerated = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            var token = jwtSecurityTokenHandler.WriteToken(tokenGenerated);

            return Ok(new
            {
                Token = token,
                Usuário = usuarioOutput
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
            DbContextOptionsBuilder<CourseDbContext> options = new DbContextOptionsBuilder<CourseDbContext>();
            options.UseSqlServer("Server=localhost;Database=Course;user=USR#COURSE;password=course@2022;");
            CourseDbContext context = new CourseDbContext(options.Options);

            IEnumerable<string> pendingMigrations = context.Database.GetPendingMigrations();
            if (pendingMigrations.Count() > 0)
            {
                context.Database.Migrate();
            }

            User user = new User()
            {
                Login = register.Login,
                Email = register.Email,
                Pass = register.Pass
            };

            context.User.Add(user);
            context.SaveChanges();

            return Created("", user);
        }
    }
}
