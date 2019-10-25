using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityProject.Repositories;

namespace UniversityProject.Models
{
    public class DatabaseInitializer : IDatabaseInitializer
    {
        private readonly IReader reader;

        private readonly IUnitOfWork unitOfWork;

        private readonly IOptions<MyOptions> options;

        public DatabaseInitializer(IReader reader, IUnitOfWork unitOfWork, IOptions<MyOptions> options)
        {
            this.reader = reader;

            this.unitOfWork = unitOfWork;

            this.options = options;
        }

        public void Seed()
        {
             if(!unitOfWork.UniversityRepository.Empty())
            {
                IEnumerable<University> universities = reader.loadFile(options.Value.PathString);
                unitOfWork.UniversityRepository.AddInRange(universities);
                unitOfWork.Commit();
            }
        }
        
          


    }
}
