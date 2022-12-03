using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
            var result = _projectsRepository.GetById(id);

            if (result == null)
                throw new InvalidOperationException($"Project with ID {id} does not exist");

            return result;
        }

        public List<Project> GetAll()
        {
            var result = _projectsRepository.GetAll();

            if (result.Count == 0)
                throw new InvalidOperationException("There are no Projects in the Database");

            return result;
        }

        public List<Project> GetAllOrderByDeadline(bool sortDesc)
        {
            var result = _projectsRepository.GetAll();

            if (result.Count == 0)
                throw new InvalidOperationException("There are no Projects in the Database");

            return sortDesc ? result.OrderByDescending(p => p.Deadline).ToList() : result.OrderBy(p => p.Deadline).ToList();
        }

        public Project Add(Project project)
        {
            if (project.Deadline <= DateTime.Today)
                throw new InvalidOperationException("Deadline has to be in the future");

            var result = _projectsRepository.Add(project);

            if (result == null)
                throw new InvalidOperationException("Project already exists in Database");

            return result;
        }

        public Project RegisterTime(int id, TimeRegistration timeRegistration)
        {
            if (timeRegistration.TimeSpent < 30)
                throw new InvalidOperationException("Individual time registrations should be 30 minutes or longer");

            var result = _projectsRepository.RegisterTime(id, timeRegistration);
            
            if (result == null)
                throw new InvalidOperationException($"Project with ID {id} does not exist");
           
            return result;
        }
    }
}
