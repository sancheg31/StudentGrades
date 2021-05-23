using System;
using System.Collections.Generic;

#nullable disable

namespace StudentGrades
{
    public partial class CoursesTeacher
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public int TeacherId { get; set; }
        public int TeacherRoleId { get; set; }

        public virtual Course Course { get; set; }
        public virtual Teacher Teacher { get; set; }
        public virtual TeacherRole TeacherRole { get; set; }
    }
}
