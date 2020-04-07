using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Lab1.ViewModel
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Рік народження")]
        public int Year { get; set; }

        [Required]
        [Display(Name = "Науковий ступінь")]
        public string Rank { get; set; }

        [Required]
        [Display(Name = "Персональний номер")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("PersonalNumber", ErrorMessage = "Персональні номери не співпадають")]
        [Display(Name = "Підтвердження персонального номера")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }

    }
}
