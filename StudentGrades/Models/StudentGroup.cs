using System;
using System.Collections.Generic;

#nullable disable

namespace StudentGrades
{
    public partial class StudentGroup
    {
        public StudentGroup()
        {
            Students = new HashSet<Student>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
}
