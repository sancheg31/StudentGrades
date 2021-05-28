using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        [Required]
        [Display(Name = "Студентський білет")]
        public int StudentId { get; set; }

        [Required]
        [Display(Name = "Група")]
        public int StudentGroupId { get; set; }

        [Required]
        [Display(Name = "Семестр")]
        public int TermId { get; set; }
        public int UserInfoId { get; set; }

        [Required]
        [Display(Name = "Вік")]
        public int Age { get; set; }
        
        [Required]
        [Display(Name = "Дата народження")]
        [DataType(DataType.Date)]
        public DateTime Brirthday { get; set; }

        public virtual StudentGroup StudentGroup { get; set; }
        public virtual StudentId StudentNavigation { get; set; }
        public virtual Term Term { get; set; }
        public virtual UserInfo UserInfo { get; set; }
        public virtual ICollection<CourseFinal> CourseFinals { get; set; }
        public virtual ICollection<CourseModuleResult> CourseModuleResults { get; set; }
        public virtual ICollection<CoursesStudent> CoursesStudents { get; set; }
    }
}
