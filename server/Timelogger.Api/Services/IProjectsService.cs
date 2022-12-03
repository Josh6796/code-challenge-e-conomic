using System.Collections.Generic;
using Timelogger.Entities;

namespace Timelogger.Api.Services
{
    public interface IProjectsService
    {
        /// <summary>
        /// Get Project from Repository by ID
        /// </summary>
        /// <param name="id">ID of Project</param>
        /// <returns>Project with specified ID</returns>
        Project GetById(int id);

        /// <summary>
        /// Get All Projects from Repository
        /// </summary>
        /// <returns>List of Projects</returns>
        List<Project> GetAll();
    }
}
