using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityProject.Models;
using UniversityProject.ViewModel;

namespace UniversityProject.Services
{
    public interface IStudentDistributionService
    {

        Dictionary<int, List<StudentGradeViewModel>> OrderDictionaryByGrade
            (Dictionary<int, List<StudentGradeViewModel>> sgVM);//orders the student depending on the grade

        List<Student> GetStudents();//get students with their  grades/requested colleges

        void GenerateStudentGrades(List<Student> students, List<StudentGradeViewModel> StudentGradeList
            , Dictionary<int, List<StudentGradeViewModel>> colStud);

        List<StudentGradeViewModel> GetUnassignedStudents(Dictionary<int, List<StudentGradeViewModel>> orderedDict);

        void AssignUnassignedStudents(List<StudentGradeViewModel> unassignedStudents,
            List<Student> students, Dictionary<int, List<StudentGradeViewModel>> orderedDict);

        List<StudentGradeViewModel>  GetAssignedStudents(Dictionary<int, List<StudentGradeViewModel>> orderedDict
            , List<Student> students);

    }
}
