using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            return _context.Projects.FirstOrDefault(p => p.Id == id);
        }

        public List<Project> GetAll()
        {
            return _context.Projects.ToList(); 
        }
    }
}
