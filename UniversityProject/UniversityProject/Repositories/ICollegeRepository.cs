using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityProject.Models;

namespace UniversityProject.Repositories
{
    public interface ICollegeRepository : IGenericRepository<College>
    {

         College GetCollegeById(int id);


    }
}
