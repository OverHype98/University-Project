using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityProject.Models
{
    public class University
    {
        public int UniversityId { get; set; }
        public string UniversityName { get; set; }

        public List<College> Colleges { get; set; }

    }
}
