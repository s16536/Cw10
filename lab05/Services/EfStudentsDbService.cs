using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lab05.DTOs;
using lab05.DTOs.Requests;
using lab05.GeneratedModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

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
            var student = new Student() {IndexNumber = index};
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
            var studies = _context.Studies.FirstOrDefault(s => s.Name.Equals(request.Studies));
            if (studies == null)
            {
                return null;
            }

            var oldEnrollment =
                _context.Enrollment.FirstOrDefault(e => (e.IdStudy == studies.IdStudy && e.Semester == request.Semester));
            if (oldEnrollment == null)
            {
                return null;
            }

            var newEnrollment = _context.Enrollment.FirstOrDefault(e => (e.IdStudy == studies.IdStudy && e.Semester == request.Semester + 1));
            if (newEnrollment == null)
            {
                newEnrollment = new Enrollment()
                {
                    IdEnrollment = _context.Enrollment.Max(e => e.IdEnrollment) + 1,
                    IdStudyNavigation = studies,
                    Semester = oldEnrollment.Semester + 1,
                    StartDate = DateTime.Now
                };
            }

            var students = _context.Student.Where(s => s.IdEnrollmentNavigation == oldEnrollment);

            foreach (var student in students)
            {
                student.IdEnrollmentNavigation = newEnrollment;
            }

            _context.SaveChanges();
            return newEnrollment;
        }

        public Enrollment EnrollStudent(EnrollStudentRequest request)
        {
            if (_context.Student.Find(request.IndexNumber) != null)
            {
                return null;
            }

            var studies = _context.Studies.FirstOrDefault(s => s.Name.Equals(request.Studies));
            if (studies == null)
            {
                return null;
            }

            var enrollment =
                _context.Enrollment.FirstOrDefault(e => (e.IdStudy == studies.IdStudy && e.Semester == 1));

            if (enrollment == null)
            {
                enrollment = new Enrollment()
                {
                    IdEnrollment = _context.Enrollment.Max(e => e.IdEnrollment) + 1,
                    IdStudyNavigation = studies,
                    Semester = 1,
                    StartDate = DateTime.Now
                };
            }

            var student = new Student()
            {
                BirthDate = request.BirthDate,
                FirstName = request.FirstName,
                IndexNumber = request.IndexNumber,
                LastName = request.LastName,
                IdEnrollmentNavigation = enrollment
            };

            _context.Student.Add(student);
            _context.SaveChanges();
            return enrollment;
        }
    }
}
