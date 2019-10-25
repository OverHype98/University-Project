using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityProject.Models;
using UniversityProject.Repositories;
using UniversityProject.ViewModel;

namespace UniversityProject.Services
{
    public class StudentDistributionService : IStudentDistributionService
    {
        
        private readonly IUnitOfWork unitOfWork;

        public StudentDistributionService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public void AssignUnassignedStudents(List<StudentGradeViewModel> unassignedStudents, List<Student> students, Dictionary<int, List<StudentGradeViewModel>> orderedDict)
        {

            foreach (var it in unassignedStudents)
            {
                foreach (var stud in students)
                {
                    if (it.Student.StudentId == stud.StudentId)
                    {
                        if (stud.RequestedColleges.Count() != 0)//if the student has other options
                        {
                            foreach (var reqCol in stud.RequestedColleges)
                            {
                                //if the college has some available places
                                if (orderedDict.ContainsKey(reqCol.CollegeId))
                                {
                                    if (orderedDict[reqCol.CollegeId].Count() < reqCol.NumberOfPlaces)
                                    {

                                        orderedDict[reqCol.CollegeId].Add(it);
                                        orderedDict = OrderDictionaryByGrade(orderedDict);

                                    }
                                    else
                                    {
                                        if (orderedDict[reqCol.CollegeId].Last().FinalGrade < it.FinalGrade)
                                        {

                                            unassignedStudents.Add(orderedDict[reqCol.CollegeId].Last());

                                            orderedDict[reqCol.CollegeId].Remove(orderedDict[reqCol.CollegeId].Last());

                                            orderedDict[reqCol.CollegeId].Add(it);


                                        }


                                    }

                                }
                                else
                                {
                                    it.College = reqCol;
                                    orderedDict.Add(reqCol.CollegeId, new List<StudentGradeViewModel> { it });
                                }
                            }

                        }
                    }
                }




            }
        }

        public void GenerateStudentGrades(List<Student> students, List<StudentGradeViewModel> StudentGradeList, Dictionary<int, List<StudentGradeViewModel>> colStud)
        {
            foreach (var it in students)
            {

                it.StudentGrade = new List<StudentGradeViewModel>();
                decimal testVal = 0;
                
               College reqCol = null;
                
                if (it.RequestedColleges.Count()!= 0)
                {
                     reqCol = it.RequestedColleges.First();
                }
               

                


                if(reqCol != null)
                {

                
                foreach (var colCrit in reqCol.CollegeCriterias)
                {
                    foreach (var mark in it.Grades)
                    {
                        if (mark.CriteriaId == colCrit.CriteriaId)
                        {

                            testVal += (colCrit.GradeWeight * mark.GradeValue) / 100;

                        }
                    }

                }
                
                StudentGradeViewModel studentGradeVM = new StudentGradeViewModel
                {
                    FinalGrade = testVal,
                    College = reqCol,
                    Student = it
                };

                StudentGradeList.Add(studentGradeVM);

                if (colStud.ContainsKey(reqCol.CollegeId))
                {
                    colStud[reqCol.CollegeId].Add(studentGradeVM);
                }
                else
                {
                    colStud.Add(reqCol.CollegeId, new List<StudentGradeViewModel> { studentGradeVM });
                }//dictionary having as key the college id and as list the potential candidates

                it.RequestedColleges.Remove(reqCol);

                }


            }
        }

        public List<StudentGradeViewModel> GetAssignedStudents(Dictionary<int, List<StudentGradeViewModel>> orderedDict,List<Student> students)
        {
            List<StudentGradeViewModel> FinalList = new List<StudentGradeViewModel>();

            foreach(var stud in students)
            {

                stud.CollegeId = null;
                unitOfWork.Commit();

            }

            foreach (var it in orderedDict)
            {
                foreach (var stud in it.Value)
                {
                    stud.College = unitOfWork.CollegeRepository.GetCollegeById(it.Key);
                }

            }

            foreach (var it in orderedDict)
            {
                FinalList.AddRange(it.Value);
            }

            foreach (var it in FinalList)
            {

                var admissedStudent = students.FirstOrDefault(i => i.StudentId == it.Student.StudentId);

                var result = unitOfWork.StudentRepository
                    .GetFirstOrDefault(b => b.StudentId == admissedStudent.StudentId);

                if (result != null)
                {
                    result.CollegeId = it.College.CollegeId;
                    unitOfWork.Commit();
                }

            }


            

                return FinalList;
        }

        public List<Student> GetStudents()
        {
            List<Application> applications = unitOfWork.ApplicationRepository.GetAll().ToList();
            List<ApplicationCollege> applicationCollege = unitOfWork.ApplicationCollegeRepository.GetAll().ToList();
            List<Student> students = new List<Student>();
            List<CriteriaCollege> criterias = unitOfWork.CriteriaCollegeRepository.GetAll().ToList();
            List<Grade> grades = unitOfWork.GradeRepository.GetAll().ToList();


            Student student = new Student();

            foreach (var app in applications)
            {
                student = unitOfWork.StudentRepository.Get(app.StudentId);
                student.Grades = new List<Grade>();
                student.RequestedColleges = new List<College>();
                foreach (var grade in grades)
                {
                    if (grade.Application.ApplicationId == app.ApplicationId)
                    {
                        student.Grades.Add(grade);
                    }
                }

                foreach (var appCol in applicationCollege)
                {
                    if (appCol.ApplicationId == app.ApplicationId)
                    {
                        var college = unitOfWork.CollegeRepository
                            .Get(appCol.CollegeId);
                        college.CollegeCriterias = new List<CriteriaCollege>();

                        foreach (var crit in criterias)
                        {
                            if (crit.CollegeId == college.CollegeId)
                            {
                                college.CollegeCriterias.Add(crit);
                            }
                        }

                        student.RequestedColleges.Add(college);//add the colleges in the order the student wants
                    }
                }

                students.Add(student);//we store the students with their grades and requested colleges
            }

            return students;
        }

        public List<StudentGradeViewModel> GetUnassignedStudents(Dictionary<int, List<StudentGradeViewModel>> orderedDict)
        {

            List<StudentGradeViewModel> unassignedStudents = new List<StudentGradeViewModel>();

            foreach (KeyValuePair<int, List<StudentGradeViewModel>> it in orderedDict)
            {
                var col = unitOfWork.CollegeRepository.Get(it.Key);

                if (it.Value.Count() > col.NumberOfPlaces)
                {

                    foreach (var studentGrade in it.Value.Skip(col.NumberOfPlaces))
                    {

                        unassignedStudents.Add(studentGrade);//add to unassigned students

                    }

                    it.Value.RemoveRange(col.NumberOfPlaces, it.Value.Count() - col.NumberOfPlaces);

                }

            }

            return unassignedStudents;
        }

        public Dictionary<int, List<StudentGradeViewModel>> OrderDictionaryByGrade(Dictionary<int, List<StudentGradeViewModel>> colStud)
        {
            Dictionary<int, List<StudentGradeViewModel>> orderedDict = new Dictionary<int, List<StudentGradeViewModel>>();
            foreach (var it in colStud)
            {
                var orderedList = it.Value.OrderByDescending(a => a.FinalGrade).ToList();

                orderedDict.Add(it.Key, orderedList);
            }
            return orderedDict;
        }

    }
}
