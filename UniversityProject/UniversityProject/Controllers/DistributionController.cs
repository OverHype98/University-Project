using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using UniversityProject.Models;
using UniversityProject.Repositories;
using UniversityProject.Services;
using UniversityProject.ViewModel;

namespace UniversityProject.Controllers
{
    public class DistributionController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        private readonly IStudentDistributionService StudentDistributionService;

        public DistributionController(IUnitOfWork unitOfWork,IStudentDistributionService StudentDistributionService)
        {


            this.StudentDistributionService = StudentDistributionService;

            this.unitOfWork = unitOfWork;

        }



        public IActionResult Index()
        {

            List<Student> students = StudentDistributionService.GetStudents();

            List<StudentGradeViewModel> StudentGradeList = new List<StudentGradeViewModel>();

            Dictionary<int, List<StudentGradeViewModel>> colStud = new Dictionary<int, List<StudentGradeViewModel>>();

            List<StudentGradeViewModel> unassignedStudents = new List<StudentGradeViewModel>();

            Dictionary<int, List<StudentGradeViewModel>> orderedDict = new Dictionary<int, List<StudentGradeViewModel>>();

            List<StudentGradeViewModel> FinalList = new List<StudentGradeViewModel>();

            //fills the student lists, studentgradelist and dictionary colStud
            StudentDistributionService.GenerateStudentGrades(students, StudentGradeList, colStud);


            //ordered dict contains the grades of the students ordered
            orderedDict = StudentDistributionService.OrderDictionaryByGrade(colStud);

            //gets a list of students which have not been accepted to any college yet
            unassignedStudents = StudentDistributionService.GetUnassignedStudents(orderedDict);


            StudentDistributionService.AssignUnassignedStudents(unassignedStudents, students, orderedDict);



            FinalList = StudentDistributionService.GetAssignedStudents(orderedDict, students);


            ViewBag.FinalList = FinalList;

            if(students.Count != 0)
            {
                return View(students.First());
            }else
            {
                return View();
            }

            
        }
    }
}