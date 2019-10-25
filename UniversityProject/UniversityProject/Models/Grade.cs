using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityProject.Models
{
    public class Grade
    {
        public int GradeId { get; set; }


        public decimal GradeValue { get; set; }


        public int CriteriaId { get; set; }

        public Application Application { get; set; }

        public static implicit operator List<object>(Grade v)
        {
            throw new NotImplementedException();
        }
    }
}
