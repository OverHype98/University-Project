using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using UniversityProject.ViewModel;

namespace UniversityProject.Models
{
    public class Student
    {
        public int StudentId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int CNP { get; set; }

        public int Age { get; set; }

        public int? CollegeId { get; set; }

        [NotMapped]
        public List<Grade> Grades { get; set; }

        [NotMapped]
        public List<College> RequestedColleges { get; set; }

        [NotMapped]
        public List<StudentGradeViewModel> StudentGrade { get; set; }
   

    }
}
