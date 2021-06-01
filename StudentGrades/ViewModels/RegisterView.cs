using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace StudentGrades.ViewModels
{
    public class RegisterView
    {
        [Required]
        [Display(Name="ПІБ")]
        [DataType(DataType.Text)]
        public string Name { get; set; }
        
        [Required]
        [Display(Name="Login")]
        [DataType(DataType.Text)]
        public string Login { get; set; }
        
        [Required]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [Display(Name="Номер телефону")]
        [DataType(DataType.PhoneNumber)]
        public string Telephone { get; set; }

        [Required]
        [Display(Name="Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Паролі не співпадають")]
        [Display(Name = "Підтвердження паролю")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }
        [Display(Name = "Ви вчитель?")]
        public bool IsTeacher { get; set; }
        
    }
}
