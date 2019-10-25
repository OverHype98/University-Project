using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityProject.Models;

namespace UniversityProject.Repositories
{
    public interface ICriteriaRepository : IGenericRepository<Criteria>
    {

        Criteria GetCriteriaById(int id);

        Criteria GetCriteriaByName(string name);

    }
}
