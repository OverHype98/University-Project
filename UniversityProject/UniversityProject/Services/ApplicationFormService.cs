using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityProject.Models;
using UniversityProject.Repositories;
using UniversityProject.ViewModel;

namespace UniversityProject.Services
{
    public class ApplicationFormService : IApplicationFormService
    {

        private readonly IUnitOfWork unitOfWork;

        public ApplicationFormService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }



        public Application GenerateApplication(ApplicationViewModel applicationVM)
        {
            List<Grade> Grades = new List<Grade>();

           

            Student student = new Student
            {
                FirstName = applicationVM.FirstName,
                LastName = applicationVM.LastName,
                CNP = applicationVM.CNP,
                Age = applicationVM.Age

            };

            Grade GraduationGrade = new Grade
            {
                GradeValue = applicationVM.GraduationGrade,
                CriteriaId = unitOfWork.CriteriaRepository
                    .GetFirstOrDefault(x => x.CriteriaName == "Bac").CriteriaId //get criteria name id
            };


            Grade HighSchoolGrade = new Grade
            {
                GradeValue = applicationVM.HighSchoolGrade,
                CriteriaId = unitOfWork.CriteriaRepository
                 .GetFirstOrDefault(x => x.CriteriaName == "Liceu").CriteriaId
            };

            Grades.Add(HighSchoolGrade);
            Grades.Add(GraduationGrade);

            Application application = new Application
            {
                Grades = Grades,        //Grades list
                Student = student       // a student

            };

            return application;
        }

        public List<ApplicationCollege> GenerateCollegeApplication(ApplicationViewModel viewModel,Application application)
        {
            List<ApplicationCollege> applicationColleges = new List<ApplicationCollege>();

            foreach (int collegeIds in viewModel.CollegeIds) //Saving the college ids in CollegeApplication table
            {
                ApplicationCollege appCollege = new ApplicationCollege
                {
                    ApplicationId = application.ApplicationId,
                    CollegeId = collegeIds
                };
                applicationColleges.Add(appCollege);

            }

            return applicationColleges;
        }

        public void GenerateBonusCriterias(ApplicationViewModel applicationVM, SubmitViewModel submitVM)
        {

            List<College> Colleges = new List<College>();
            List<CriteriaCollege> bonusCriterias = new List<CriteriaCollege>();


            foreach (int collegeIds in applicationVM.CollegeIds)
            {
                Colleges.Add(unitOfWork.CollegeRepository.GetCollegeById(collegeIds));
            }




            foreach (var col in Colleges)
            {
                foreach (var it in col.CollegeCriterias)
                {
                    it.Criteria = unitOfWork.CriteriaRepository.GetCriteriaById(it.CriteriaId);


                    if (it.Criteria.CriteriaName != "Bac" && it.Criteria.CriteriaName != "Liceu")
                    {
                        bonusCriterias.Add(it); //all the criterias different from Bac/Liceu
                        
                    }
                }


            }


            submitVM.CriteriaColleges = bonusCriterias;//add the bonus criterias in the vm
            if(submitVM.ViewModells == null)
            {
                submitVM.ViewModells = new List<ViewModell>();
            }
            else
            {

            }
            
            applicationVM.CriteriaColleges = bonusCriterias;

            

            foreach (var it in submitVM.CriteriaColleges)
            {

                ViewModell viewModell = new ViewModell
                {
                    CriteriaName = it.Criteria.CriteriaName
                };

                submitVM.ViewModells.Add(viewModell);
            }


            
        }

        public void GenerateSubmitViewModel(SubmitViewModel submitVM)
        {
            var critColCount = submitVM.CriteriaColleges.Count();

            for (var i = 0; i < critColCount; i++)
            {
                submitVM.ViewModells[i].CriteriaName = submitVM.CriteriaColleges[i].Criteria.CriteriaName;
    
            }

            submitVM.ViewModells.RemoveRange(critColCount, critColCount);

        }

        public void AddGradesToApplication(Application application, SubmitViewModel submitVM)
        {

            foreach (var it in submitVM.ViewModells)
            {
                Grade grade = new Grade
                {
                    GradeValue = it.Grade,

                    CriteriaId = unitOfWork.CriteriaRepository.GetCriteriaByName(it.CriteriaName).CriteriaId
                };

                application.Grades.Add(grade);

            }

        }
    }
}
