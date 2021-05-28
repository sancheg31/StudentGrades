using System;
using System.Collections.Generic;

#nullable disable

namespace StudentGrades
{
    public partial class Course
    {
        public Course()
        {
            CourseFinals = new HashSet<CourseFinal>();
            CourseModules = new HashSet<CourseModule>();
            CoursesTeachers = new HashSet<CoursesTeacher>();
        }

        public int Id { get; set; }
        public int DisciplineId { get; set; }
        public int TermId { get; set; }
        public int AssessmentTypeId { get; set; }
        public int LectureHours { get; set; }
        public int PracticeHours { get; set; }
        public int SelfDevelopmentHours { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Info { get; set; }

        public virtual AssessmentType AssessmentType { get; set; }
        public virtual Discipline Discipline { get; set; }
        public virtual Term Term { get; set; }
        public virtual ICollection<CourseFinal> CourseFinals { get; set; }
        public virtual ICollection<CourseModule> CourseModules { get; set; }
        public virtual ICollection<CoursesTeacher> CoursesTeachers { get; set; }
        public virtual ICollection<CoursesStudent> CoursesStudents { get; set; }
    }
}
