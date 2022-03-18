using curso.api.Model.Inputs;
using curso.api.Model.Outputs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace curso.api.Controllers
{
    [Route("api/v1/curso")]
    [ApiController]
    [Authorize]
    public class CursoController : ControllerBase
    {
        /// <summary>
        /// Este serviço permite cadastrar curso para o usuário autenticado
        /// </summary>
        /// <param name="cursoInput"></param>
        /// <returns>Retorna status 201 e dados do curso do usuário</returns>
        [SwaggerResponse(statusCode: 201, description: "Sucesso ao cadastrar o curso.")]
        [SwaggerResponse(statusCode: 401, description: "Não autorizado")]
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Create(CursoInput cursoInput)
        {
            int codigoUsuario = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            return Created("", cursoInput);
        }

        /// <summary>
        /// Este serviço permite obter todos os cursos ativos do usuário
        /// </summary>
        /// <param name=""></param>
        /// <returns>Retorna status ok e dados do curso do usuário</returns>
        [SwaggerResponse(statusCode: 200, description: "Sucesso ao obter os cursos.")]
        [SwaggerResponse(statusCode: 401, description: "Não autorizado")]
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Get()
        {
            List<CursoOutput> cursos = new List<CursoOutput>();

            int codigoUsuario = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            cursos.Add(new CursoOutput()
            {
                Login = codigoUsuario.ToString(),
                Description = "TestCourseDesc",
                Name = "TestCourseName"
            });
            
            return Ok(cursos);
        }

    }
}
