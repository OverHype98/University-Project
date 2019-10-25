using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityProject.Models
{
    public class Application
    {
        public int ApplicationId { get; set; }
                    
        public int StudentId { get; set; }

        public Student Student { get; set; }

        public List<Grade> Grades { get; set; }

        public List<ApplicationCollege> Colleges { get; set; }
        
        [NotMapped]
        public List<Criteria> Criterias { get; set; }




    }
}
