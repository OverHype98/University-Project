using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityProject.Models
{
    public class ApplicationViewModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public int CNP { get; set; }

        public decimal GraduationGrade { get; set; }

        public decimal HighSchoolGrade { get; set; }

        public List<College> Colleges { get; set; }

        public List<int> CollegeIds { get; set; }

        public List<CriteriaCollege> CriteriaColleges { get; set; }

    }
}
