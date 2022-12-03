using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Timelogger.Api.Services;

namespace Timelogger.Api.Controllers
{
	[Route("api/[controller]")]
	public class ProjectsController : Controller
	{
		private readonly IProjectsService _projectsService;

		public ProjectsController(IProjectsService projectsService)
		{
            _projectsService = projectsService;
		}

		[HttpGet]
		[Route("hello-world")]
		public string HelloWorld()
		{
			return "Hello Back!";
		}

		// GET api/projects
		[HttpGet]
        public IActionResult Get()
        {
            if (_projectsService.GetAll().Count != 0)
                return Ok(_projectsService.GetAll());

            return NotFound("There are no Projects in the Database");
        }
	}
}
