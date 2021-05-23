using System;
using System.Collections.Generic;

#nullable disable

namespace StudentGrades
{
    public partial class Term
    {
        public Term()
        {
            Courses = new HashSet<Course>();
            Students = new HashSet<Student>();
        }

        public int Id { get; set; }
        public int Number { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
}
