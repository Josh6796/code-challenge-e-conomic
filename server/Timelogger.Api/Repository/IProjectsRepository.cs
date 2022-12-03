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
    }
}