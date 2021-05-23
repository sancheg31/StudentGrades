using System;
using System.Collections.Generic;

#nullable disable

namespace StudentGrades
{
    public partial class AssessmentType
    {
        public AssessmentType()
        {
            Courses = new HashSet<Course>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
    }
}
