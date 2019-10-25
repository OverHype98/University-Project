using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityProject.Models;
using UniversityProject.ViewModel;

namespace UniversityProject.Services
{
    public interface IApplicationFormService
    {

        Application GenerateApplication(ApplicationViewModel viewModel);
        List<ApplicationCollege> GenerateCollegeApplication(ApplicationViewModel viewModel, Application application);

        void GenerateBonusCriterias(ApplicationViewModel applicationVM,SubmitViewModel submitVM);

        void GenerateSubmitViewModel(SubmitViewModel submitVM);

        void AddGradesToApplication(Application application, SubmitViewModel submitVM);
    }
}
