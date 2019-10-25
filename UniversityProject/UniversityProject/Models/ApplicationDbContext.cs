using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityProject.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
                :base(options)
        {

        }

        public ApplicationDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationCollege>()
                .HasKey(t => new { t.CollegeId, t.ApplicationId });

            builder.Entity<ApplicationCollege>()
                .HasOne(p => p.College)
                .WithMany(a => a.Application)
                .HasForeignKey(y => y.CollegeId);

            builder.Entity<ApplicationCollege>()
              .HasOne(p => p.Application)
              .WithMany(a => a.Colleges)
              .HasForeignKey(y => y.ApplicationId);



        }


        public DbSet<Application> Applications { get; set; }

        public DbSet<ApplicationCollege> ApplicationCollege { get; set; }

        public DbSet<College> Colleges { get; set; }

        public DbSet<CriteriaCollege> CriteriaCollege { get; set; }

        public DbSet<Criteria> Criteria{ get; set; }

        public DbSet<Grade> Grades { get; set; }

        public DbSet<Student> Students { get; set; }
        public DbSet<University> Universities { get; set; }

        
        
        

       

       
    }
}
