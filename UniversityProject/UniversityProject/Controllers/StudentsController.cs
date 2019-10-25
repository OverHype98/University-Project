using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UniversityProject.Models;
using UniversityProject.Repositories;

namespace UniversityProject.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public StudentsController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // GET: Students
        public IActionResult Index()
        {

            return View(unitOfWork.StudentRepository.GetAll());

        }

        // GET: Students/Details/5
        public IActionResult Details(int? id)
        {
            
            Student student = unitOfWork.StudentRepository
                .GetFirstOrDefault(m => m.StudentId == id);

            

            return View(student);
        }

     
    }
}
