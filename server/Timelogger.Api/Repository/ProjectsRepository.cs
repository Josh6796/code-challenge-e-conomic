using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Timelogger.Entities;

namespace Timelogger.Api.Repository
{
    public class ProjectsRepository : IProjectsRepository
    {
        private readonly ApiContext _context;

        public ProjectsRepository(ApiContext context)
        {
            _context = context;
        }

        public Project GetById(int id)
        {
            return _context.Projects
                .Include(p => p.TimeRegistrations)
                .SingleOrDefault(p => p.Id == id);
        }

        public List<Project> GetAll()
        {
            return _context.Projects
                .Include(p => p.TimeRegistrations)
                .ToList();
        }

        public Project Add(Project project)
        {
            if (_context.Projects.Contains(project))
                return null;

            var addedProject = _context.Projects?.Add(project).Entity;
            _context.SaveChanges();

            return addedProject;
        }

        public Project RegisterTime(Project project, TimeRegistration timeRegistration)
        {
            project.TimeRegistrations.Add(timeRegistration);
            _context.SaveChanges();

            return project;
        }
    }
}
