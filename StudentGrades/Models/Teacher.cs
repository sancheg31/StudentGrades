using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        [Required]
        [Display(Name = "Посада")]
        public int AcademicDegreeId { get; set; }
        [Required]
        [Display(Name = "Кафедра")]
        public int CaphedraId { get; set; }
        [Required]
        [Display(Name = "Вік")]
        public int Age { get; set; }
        [Required]
        [Display(Name = "День народження")]
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }
        [Required]
        [Display(Name = "Про себе")]
        [DataType(DataType.Text)]
        public string Info { get; set; }

        public virtual AcademicDegree AcademicDegree { get; set; }
        public virtual Caphedra Caphedra { get; set; }
        public virtual UserInfo UserInfo { get; set; }
        public virtual ICollection<CoursesTeacher> CoursesTeachers { get; set; }
    }
}
