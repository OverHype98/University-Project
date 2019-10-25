using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using UniversityProject.Models;

namespace UniversityProject.Repositories
{
    public class CollegeRepository : GenericRepository<College>,ICollegeRepository
    {
        public CollegeRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        
        }

        public College GetCollegeById(int id)
        {
            return applicationDbContext.Colleges.Include(c => c.CollegeCriterias)
                .FirstOrDefault(c => c.CollegeId == id);//how to add criterias name in criterias 
        }

     
    }
}
