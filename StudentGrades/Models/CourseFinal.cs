using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace StudentGrades
{
    public partial class CourseFinal
    {
        public int Id { get; set; }
        [Display(Name="Назва курсу")]
        public int CourseId { get; set; }
        public int StudentId { get; set; }
        [Display(Name = "Результат")]
        public int Score { get; set; }
        [Display(Name = "Дата екзамену")]
        public DateTime ExamDate { get; set; }

        public virtual Course Course { get; set; }
        public virtual Student Student { get; set; }
    }
}
