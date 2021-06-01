using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace StudentGrades.ViewModels
{
    public class NewCourseView
    {

        [Display(Name ="Група")]
        public int StudentGroupId { get; set; }

        [Display(Name = "Вчитель")]
        public int TeacherId { get; set; }

        [Display(Name = "Посада")]
        public int TeacherRoleId { get; set; }
    }

}
