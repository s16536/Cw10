using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lab05.DTOs.Requests;
using lab05.GeneratedModels;
using Microsoft.EntityFrameworkCore;

namespace lab05.Services
{
    public class EfStudentsDbService : IStudentsDbService
    {
        private readonly s16536Context _context;

        public EfStudentsDbService(s16536Context context)
        {
            _context = context;
        }

        public IEnumerable<Student> GetStudents()
        {
            return _context.Student.ToList();
        }

        public Student GetStudent(string id)
        { 
            return _context.Student.Find(id);
        }

        public Student AddStudent(Student student)
        {

            _context.Student.Add(student);
            _context.SaveChanges();
            return student;
        }

        public void DeleteStudent(string index)
        {
            var student = new Student() { IndexNumber= index};
            _context.Student.Attach(student);
            _context.Student.Remove(student);
            _context.SaveChanges();
        }

        public void UpdateStudent(string index, Student student)
        {
            student.IndexNumber = index;
            _context.Student.Attach(student);
            _context.Entry(student).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public Enrollment PromoteStudents(PromoteStudentsRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
