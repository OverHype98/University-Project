using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityProject.Models
{
    public class MyOptions
    {
        public MyOptions()
        {    
            PathString = "value1_from_ctor";
        }

        public string PathString { get; set; }

    }
}
