using curso.api.Business.Entities;
using curso.api.Business.Repositories;
using curso.api.Model.Inputs;
using curso.api.Model.Outputs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace curso.api.Controllers
{
    [Route("api/v1/curso")]
    [ApiController]
    [Authorize]
    public class CourseController : ControllerBase
    {

        private readonly ICourseRepository _courseRepository;

        public CourseController(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }
        /// <summary>
        /// Este serviço permite cadastrar curso para o usuário autenticado
        /// </summary>
        /// <param name="cursoInput"></param>
        /// <returns>Retorna status 201 e dados do curso do usuário</returns>
        [SwaggerResponse(statusCode: 201, description: "Sucesso ao cadastrar o curso.")]
        [SwaggerResponse(statusCode: 401, description: "Não autorizado")]
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Create(CourseInput courseInput)
        {
            int userCode = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            Course course = new Course()
            {
                Name = courseInput.Name,
                Description = courseInput.Description,
                UserCode = userCode
            };

            _courseRepository.Create(course);
            _courseRepository.Commit();

            string userLogin = User.FindFirst(c => c.Type == ClaimTypes.Name)?.Value.ToString();
            CourseOutput courseOutput = new CourseOutput()
            {
                Login = userLogin,
                Name = course.Description,
                Description = course.Description,
            };

            return Created("", courseOutput);
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
            int userCode = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            IEnumerable<CourseOutput> courses = _courseRepository.GetList(userCode)
                .Select(s => new CourseOutput()
                {
                    Name = s.Name,
                    Description = s.Description,
                    Login = s.User.Login
                });
                        
            return Ok(courses);
        }

    }
}
