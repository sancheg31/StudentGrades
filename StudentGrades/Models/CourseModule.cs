using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace StudentGrades
{
    public partial class CourseModule
    {
        public CourseModule()
        {
            CourseModuleResults = new HashSet<CourseModuleResult>();
        }

        public int Id { get; set; }
        public int CourseId { get; set; }
        [Required]
        [Display(Name = "Вид оцінювання")]
        public int ModuleTypeid { get; set; }
        [Required]
        [Display(Name = "Назва")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Бали")]
        public int MaxScore { get; set; }
        [Required]
        [Display(Name = "Час здачі")]
        [DataType(DataType.DateTime)]
        public DateTime DueDate { get; set; }
        [Display(Name = "Розгорнута інформація")]
        public string Info { get; set; }

        public virtual Course Course { get; set; }
        public virtual ModuleType ModuleType { get; set; }
        public virtual ICollection<CourseModuleResult> CourseModuleResults { get; set; }
    }
}
