using System;
using System.Collections.Generic;

#nullable disable

namespace StudentGrades
{
    public partial class ModuleType
    {
        public ModuleType()
        {
            CourseModules = new HashSet<CourseModule>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }

        public virtual ICollection<CourseModule> CourseModules { get; set; }
    }
}
