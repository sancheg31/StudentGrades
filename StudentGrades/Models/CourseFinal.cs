using System;
using System.Collections.Generic;

#nullable disable

namespace StudentGrades
{
    public partial class CourseFinal
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public int StudentId { get; set; }
        public int Score { get; set; }
        public DateTime ExamDate { get; set; }

        public virtual Course Course { get; set; }
        public virtual Student Student { get; set; }
    }
}
