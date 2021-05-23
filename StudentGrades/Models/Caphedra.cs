using System;
using System.Collections.Generic;

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
        public string Name { get; set; }
        public string Info { get; set; }

        public virtual ICollection<Discipline> Disciplines { get; set; }
        public virtual ICollection<Teacher> Teachers { get; set; }
    }
}
