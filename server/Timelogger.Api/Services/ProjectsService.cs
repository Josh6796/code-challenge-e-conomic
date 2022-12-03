using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Timelogger.Api.Repository;
using Timelogger.Entities;

namespace Timelogger.Api.Services
{
    public class ProjectsService : IProjectsService
    {
        private readonly IProjectsRepository _projectsRepository;

        public ProjectsService(IProjectsRepository projectsRepository)
        {
            _projectsRepository = projectsRepository;
        }

        public Project GetById(int id)
        {
            return _projectsRepository.GetById(id);
        }

        public List<Project> GetAll()
        {
            return _projectsRepository.GetAll();
        }
    }
}
