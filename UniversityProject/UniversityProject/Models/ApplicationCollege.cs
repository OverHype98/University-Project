using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityProject.Models
{
    public class ApplicationCollege
    {
        public Application Application { get; set; }

        public int ApplicationId { get; set; }

        public College College { get; set; }

        public int CollegeId { get; set; }

      


    }
}
