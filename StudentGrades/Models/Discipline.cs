using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace StudentGrades
{
    public partial class Discipline
    {
        public Discipline()
        {
            Courses = new HashSet<Course>();
        }

        public int Id { get; set; }
        [Display(Name="Ім'я")]
        public string Name { get; set; }
        public int CaphedraId { get; set; }
        [Display(Name="Інформація")]
        public string Info { get; set; }
        [Display(Name = "Кафедра")]
        public virtual Caphedra Caphedra { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
    }
}
