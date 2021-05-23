using System;
using System.Collections.Generic;

#nullable disable

namespace StudentGrades
{
    public partial class TeacherRole
    {
        public TeacherRole()
        {
            CoursesTeachers = new HashSet<CoursesTeacher>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }

        public virtual ICollection<CoursesTeacher> CoursesTeachers { get; set; }
    }
}
