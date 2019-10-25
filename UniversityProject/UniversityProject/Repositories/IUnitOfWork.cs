using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityProject.Models;

namespace UniversityProject.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<University> UniversityRepository { get; }

        IGenericRepository<Student> StudentRepository { get; }

        ICollegeRepository CollegeRepository { get; }

        IGenericRepository<Application> ApplicationRepository { get; }

        IGenericRepository<ApplicationCollege> ApplicationCollegeRepository { get; }

        IGenericRepository<CriteriaCollege> CriteriaCollegeRepository { get; }

        ICriteriaRepository CriteriaRepository { get; }

        IGenericRepository<Grade> GradeRepository { get; }
        void Commit();
    }

}
