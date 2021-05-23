using System;
using System.Collections.Generic;

#nullable disable

namespace StudentGrades
{
    public partial class Teacher
    {
        public Teacher()
        {
            CoursesTeachers = new HashSet<CoursesTeacher>();
        }

        public int Id { get; set; }
        public int UserInfoId { get; set; }
        public int AcademicDegreeId { get; set; }
        public int CaphedraId { get; set; }
        public int Age { get; set; }
        public DateTime Birthday { get; set; }
        public string Info { get; set; }

        public virtual AcademicDegree AcademicDegree { get; set; }
        public virtual Caphedra Caphedra { get; set; }
        public virtual UserInfo UserInfo { get; set; }
        public virtual ICollection<CoursesTeacher> CoursesTeachers { get; set; }
    }
}
