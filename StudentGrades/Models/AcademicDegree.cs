using System;
using System.Collections.Generic;

#nullable disable

namespace StudentGrades
{
    public partial class AcademicDegree
    {
        public AcademicDegree()
        {
            Teachers = new HashSet<Teacher>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }

        public virtual ICollection<Teacher> Teachers { get; set; }
    }
}
