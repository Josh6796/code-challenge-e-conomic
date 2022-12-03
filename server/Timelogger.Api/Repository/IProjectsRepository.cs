using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Timelogger.Entities;

namespace Timelogger.Api.Repository
{
    public interface IProjectsRepository
    {
        /// <summary>
        /// Get Project from Database by ID
        /// </summary>
        /// <param name="id">ID of Project</param>
        /// <returns>Project with specified ID</returns>
        Project GetById(int id);

        /// <summary>
        /// Get All Projects from Database
        /// </summary>
        /// <returns>List of Projects</returns>
        List<Project> GetAll();

        /// <summary>
        /// Add Project to Database
        /// </summary>
        /// <param name="project">Project to add</param>
        /// <returns>Added Project</returns>
        Project Add(Project project);

        /// <summary>
        /// Add Time Registration to Project in Database
        /// </summary>
        /// <param name="id">ID of the Project</param>
        /// <param name="timeRegistration">Time Registration to add</param>
        /// <returns>Project with added TimeRegistration</returns>
        Project RegisterTime(int id, TimeRegistration timeRegistration);
    }
}