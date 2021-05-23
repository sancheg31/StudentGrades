using System;
using System.Collections.Generic;

#nullable disable

namespace StudentGrades
{
    public partial class Student
    {
        public Student()
        {
            CourseFinals = new HashSet<CourseFinal>();
            CourseModuleResults = new HashSet<CourseModuleResult>();
        }

        public int Id { get; set; }
        public int StudentId { get; set; }
        public int StudentGroupId { get; set; }
        public int TermId { get; set; }
        public int UserInfoId { get; set; }
        public int Age { get; set; }
        public DateTime Brirthday { get; set; }

        public virtual StudentGroup StudentGroup { get; set; }
        public virtual StudentId StudentNavigation { get; set; }
        public virtual Term Term { get; set; }
        public virtual UserInfo UserInfo { get; set; }
        public virtual ICollection<CourseFinal> CourseFinals { get; set; }
        public virtual ICollection<CourseModuleResult> CourseModuleResults { get; set; }
    }
}
