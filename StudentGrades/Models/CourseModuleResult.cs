using System;
using System.Collections.Generic;

#nullable disable

namespace StudentGrades
{
    public partial class CourseModuleResult
    {
        public int Id { get; set; }
        public int CourseModuleId { get; set; }
        public int StudentId { get; set; }
        public int Score { get; set; }

        public virtual CourseModule CourseModule { get; set; }
        public virtual Student Student { get; set; }
    }
}
