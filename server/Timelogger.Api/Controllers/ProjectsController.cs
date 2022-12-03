using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Timelogger.Api.Services;
using Timelogger.Entities;

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

		//[HttpGet]
		//[Route("hello-world")]
		//public string HelloWorld()
		//{
		//	return "Hello Back!";
		//}

		// GET api/projects
		[HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_projectsService.GetAll());
            }
            catch (InvalidOperationException e)
            {
                return NotFound(e.Message);
            }
        }

        // GET api/projects/ordered-by-deadline
        [HttpGet]
        [Route("ordered-by-deadline")]
        public IActionResult GetOrderedByDeadline(bool sortDesc = false)
        {
            try
            {
                return Ok(_projectsService.GetAllOrderByDeadline(sortDesc));
            }
            catch (InvalidOperationException e)
            {
                return NotFound(e.Message);
            }
        }

        // GET api/projects/{id}
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                return Ok(_projectsService.GetById(id));
            }
            catch (InvalidOperationException e)
            {
                return NotFound(e.Message);
            }
        }

        // POST api/projects
        [HttpPost]
        public IActionResult Add(Project project)
        {
            try
            {
                return Ok(_projectsService.Add(project));
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST api/projects/register-time
        [HttpPost]
        [Route("register-time")]
        public IActionResult RegisterTime(int id, TimeRegistration timeRegistration)
        {
            try
            {
                return Ok(_projectsService.RegisterTime(id, timeRegistration));
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
