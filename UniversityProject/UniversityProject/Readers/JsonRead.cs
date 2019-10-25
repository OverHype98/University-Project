using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityProject.Models
{
    public class JsonRead : IReader
    {
        public IEnumerable<University> loadFile(string Path)
        {
            IEnumerable<University> universities = JsonConvert.DeserializeObject<IEnumerable<University>>
                (File.ReadAllText(Path));

            return universities;
        }
    }
}
