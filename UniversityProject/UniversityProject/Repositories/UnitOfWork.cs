using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityProject.Models;

namespace UniversityProject.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        readonly ApplicationDbContext dbContext;

        public IGenericRepository<University> UniversityRepository { get; }

        public IGenericRepository<Student> StudentRepository { get; }

        public ICollegeRepository CollegeRepository { get; }

        public IGenericRepository<Application> ApplicationRepository { get; }

        public IGenericRepository<ApplicationCollege> ApplicationCollegeRepository { get; }

        public IGenericRepository<CriteriaCollege> CriteriaCollegeRepository { get; }

        public ICriteriaRepository CriteriaRepository { get; }

        public IGenericRepository<Grade> GradeRepository { get; }

        public UnitOfWork(ApplicationDbContext context)
        {
            
            this.UniversityRepository = new GenericRepository<University>(context);
            this.StudentRepository = new GenericRepository<Student>(context);
            this.CollegeRepository = new CollegeRepository(context);
            this.ApplicationRepository = new GenericRepository<Application>(context);
            this.ApplicationCollegeRepository = new GenericRepository<ApplicationCollege>(context);
            this.CriteriaCollegeRepository = new GenericRepository<CriteriaCollege>(context);
            this.CriteriaRepository = new CriteriaRepository(context);
            this.GradeRepository = new GenericRepository<Grade>(context);

            dbContext = context;
        }
        public void Commit()
        {
            dbContext.SaveChanges();
        }

        public void Dispose()
        {
            dbContext.Dispose();

        }
    }
}
