using System;
using System.Collections.Generic;

#nullable disable

namespace StudentGrades
{
    public partial class StudentId
    {
        public int Id { get; set; }
        public string Code { get; set; }

        public virtual Student Student { get; set; }
    }
}
