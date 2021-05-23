using System;
using System.Collections.Generic;

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
        public string Name { get; set; }
        public int CaphedraId { get; set; }
        public string Info { get; set; }

        public virtual Caphedra Caphedra { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
    }
}
