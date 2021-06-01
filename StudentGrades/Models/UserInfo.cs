using System;
using System.Collections.Generic;

using System.ComponentModel.DataAnnotations;

#nullable disable

namespace StudentGrades
{
    public partial class UserInfo
    {
        public UserInfo()
        {
            Students = new HashSet<Student>();
            Teachers = new HashSet<Teacher>();
        }

        public int Id { get; set; }
        [Required]
        [Display(Name="Ім'я")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Логін")]
        public string Login { get; set; }
        [Required]
        [Display(Name = "Електронна пошта")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
        [Required]
        [Display(Name = "Номер телефону")]
        [DataType(DataType.PhoneNumber)]
        public string Telephone { get; set; }

        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<Teacher> Teachers { get; set; }
    }
}
