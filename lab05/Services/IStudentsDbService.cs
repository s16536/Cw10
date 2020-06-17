using System.Collections.Generic;
using lab05.DTOs;
using lab05.DTOs.Requests;
using lab05.GeneratedModels;

namespace lab05.Services
{
    public interface IStudentsDbService
    {
        public IEnumerable<Student> GetStudents();
        public Student GetStudent(string id);
        public Student AddStudent(Student student);
        public void DeleteStudent(string index);
        public void UpdateStudent(string index, Student student);
        public Enrollment PromoteStudents(PromoteStudentsRequest request);
        public Enrollment EnrollStudent(EnrollStudentRequest request);
    }
}
