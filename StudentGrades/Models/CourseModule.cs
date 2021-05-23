using System;
using System.Collections.Generic;

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
        public int ModuleTypeid { get; set; }
        public string Name { get; set; }
        public int MaxScore { get; set; }
        public DateTime DueDate { get; set; }
        public string Info { get; set; }

        public virtual Course Course { get; set; }
        public virtual ModuleType ModuleType { get; set; }
        public virtual ICollection<CourseModuleResult> CourseModuleResults { get; set; }
    }
}
