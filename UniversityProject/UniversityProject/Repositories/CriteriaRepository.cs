using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityProject.Models;

namespace UniversityProject.Repositories
{
    public class CriteriaRepository : GenericRepository<Criteria>, ICriteriaRepository
    {

        public CriteriaRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {

        }
        public Criteria GetCriteriaById(int id)
        {
            return applicationDbContext.Criteria.FirstOrDefault(c => c.CriteriaId == id);

        }

        public Criteria GetCriteriaByName(string name)
        {
            return applicationDbContext.Criteria.FirstOrDefault(c => c.CriteriaName == name);
        }
    }
}
