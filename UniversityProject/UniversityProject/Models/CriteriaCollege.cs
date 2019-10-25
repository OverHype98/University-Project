using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityProject.Models
{
    public class CriteriaCollege
    {
        public int CriteriaCollegeId { get; set; }

        public int CriteriaId { get; set; }

        public Criteria Criteria { get; set; }
            
        public decimal GradeWeight { get; set; }

        public int CollegeId { get; set; }

        [NotMapped]
        public List<decimal> ListOfGrades { get; set; }




    }
}
