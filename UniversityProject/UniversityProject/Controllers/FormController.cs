using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using UniversityProject.Models;
using UniversityProject.Repositories;
using UniversityProject.Services;
using UniversityProject.ViewModel;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UniversityProject.Controllers
{
    public class FormController : Controller
    {

        private readonly IUnitOfWork unitOfWork;
        private readonly IApplicationFormService applicationService;

        public FormController(IUnitOfWork unitOfWork, IApplicationFormService applicationService)
        {
            this.unitOfWork = unitOfWork;
            this.applicationService = applicationService;

        }

        // GET: /<controller>/
        // GET: Form/Create
        public IActionResult Create()
        {

            ApplicationViewModel viewModel = new ApplicationViewModel
            {
                Colleges = unitOfWork.CollegeRepository.GetAll().ToList()
            };

            return View(viewModel);
        }

        // POST: Form/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ApplicationViewModel applicationVM)
        {
          

            applicationVM.Colleges = unitOfWork.CollegeRepository.GetAll().ToList();

            HttpContext.Session.SetObjectAsJson("test", applicationVM);

            return RedirectToAction("Submit",applicationVM);

        }


        public IActionResult Submit(ApplicationViewModel applicationVM)
        {

            SubmitViewModel submitVM = new SubmitViewModel();

            applicationService.GenerateBonusCriterias(applicationVM,submitVM);//fill the objects

            if(submitVM.ViewModells.Count() == 0)
            {

                Submit_Post(submitVM);

                return RedirectToAction("Index", "Students");

            }


            return View(submitVM);
        }

        [HttpPost]
        [ActionName("Submit")]
        [ValidateAntiForgeryToken]
        public IActionResult Submit_Post(SubmitViewModel   submitVM)
        {
            var applicationVM = HttpContext.Session.GetObjectFromJson<ApplicationViewModel>("test");
            applicationService.GenerateBonusCriterias(applicationVM, submitVM);//use submit view model

            applicationService.GenerateSubmitViewModel(submitVM);//submitVM contains bonus criteria name and grade

            Application application = applicationService.GenerateApplication(applicationVM);

            applicationService.AddGradesToApplication(application, submitVM);
           

            if (ModelState.IsValid)
            {
               
                unitOfWork.ApplicationRepository.Add(application);
                unitOfWork.Commit();
                
            }

            List<ApplicationCollege> applicationColleges = applicationService
                .GenerateCollegeApplication(applicationVM, application);

            unitOfWork.ApplicationCollegeRepository.AddInRange(applicationColleges);

            unitOfWork.Commit();

         


  

            return RedirectToAction("Index","Students");
        }
        



    }
}
