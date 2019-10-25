using System.Collections.Generic;

namespace UniversityProject.Models
{
    public class College
    {
        public int CollegeId { get; set; }

        public string CollegeName { get; set; }

        public string Description { get; set; }

        public int UniversityId { get; set; }

        public int NumberOfPlaces { get; set; }

        public virtual List<CriteriaCollege> CollegeCriterias { get; set; }

        public List<Student> Students { get; set; }

        public List<ApplicationCollege> Application { get; set; }


    }
}
