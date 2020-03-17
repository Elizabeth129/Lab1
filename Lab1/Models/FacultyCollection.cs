using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Lab1
{
    public partial class FacultyCollection
    {
        public FacultyCollection()
        {
            Cathedra = new HashSet<Cathedra>();
        }

        public int Id { get; set; }
        [Display(Name= "Назва факультету")]
        [Required(ErrorMessage = "Поле не може бути порожнім")]
        [RegularExpression(@"[А-Я|І|Є]{1}[а-я|i|є|`]+$", ErrorMessage = "Некорректне ім'я")]
        [Remote(action: "CheckFaculty", controller: "FacultyCollections", ErrorMessage = "Факультет уже використовується")]
        public string FacultyName { get; set; }

        public virtual ICollection<Cathedra> Cathedra { get; set; }
    }
}
