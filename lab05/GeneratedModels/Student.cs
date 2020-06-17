﻿using System;
using System.Collections.Generic;

namespace lab05.GeneratedModels
{
    public partial class Student
    {
        public Student()
        {
            RefreshToken = new HashSet<RefreshToken>();
        }

        public string IndexNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public int IdEnrollment { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Salt { get; set; }

        public virtual Enrollment IdEnrollmentNavigation { get; set; }
        public virtual ICollection<RefreshToken> RefreshToken { get; set; }
    }
}
