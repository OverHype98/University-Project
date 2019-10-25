using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityProject.Models;

namespace UniversityProject.ViewModel
{
    public class StudentGradeViewModel
    {
        public decimal FinalGrade { get; set; }

        public College College { get; set; }

        public Student Student { get; set; }


    }
}
