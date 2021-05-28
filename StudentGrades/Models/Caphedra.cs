using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace StudentGrades
{
    public partial class Caphedra
    {
        public Caphedra()
        {
            Disciplines = new HashSet<Discipline>();
            Teachers = new HashSet<Teacher>();
        }

        public int Id { get; set; }
        [Display(Name = "Ім'я")]
        public string Name { get; set; }
        [Display(Name = "Інформація")]
        public string Info { get; set; }

        public virtual ICollection<Discipline> Disciplines { get; set; }
        public virtual ICollection<Teacher> Teachers { get; set; }
    }
}
