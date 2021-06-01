using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace StudentGrades
{
    public partial class Course
    {
        public Course()
        {
            CourseFinals = new HashSet<CourseFinal>();
            CourseModules = new HashSet<CourseModule>();
            CoursesTeachers = new HashSet<CoursesTeacher>();
        }

        public int Id { get; set; }
        [Required]
        [Display(Name ="Ім'я")]
        public int DisciplineId { get; set; }
        [Required]
        [Display(Name = "Номер курсу")]
        public int TermId { get; set; }
        [Required]
        [Display(Name = "Тип оцінювання")]
        public int AssessmentTypeId { get; set; }
        [Required]
        [Display(Name = "Лекційні години")]
        public int LectureHours { get; set; }
        [Required]
        [Display(Name = "Практичні години")]
        public int PracticeHours { get; set; }
        [Required]
        [Display(Name = "Самостійні години")]
        public int SelfDevelopmentHours { get; set; }
        [Required]
        [Display(Name = "Дата початку")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [Required]
        [Display(Name = "Дата кінця")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        [Required]
        [Display(Name = "Інформація про курс")]
        public string Info { get; set; }

        public virtual AssessmentType AssessmentType { get; set; }
        public virtual Discipline Discipline { get; set; }
        public virtual Term Term { get; set; }
        public virtual ICollection<CourseFinal> CourseFinals { get; set; }
        public virtual ICollection<CourseModule> CourseModules { get; set; }
        public virtual ICollection<CoursesTeacher> CoursesTeachers { get; set; }
        public virtual ICollection<CoursesStudent> CoursesStudents { get; set; }
    }
}
