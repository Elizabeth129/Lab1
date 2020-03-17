using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Lab1
{
    public partial class DegreeCollection
    {
        public DegreeCollection()
        {
            Professor = new HashSet<Professor>();
        }

        public int Id { get; set; }
        [Display(Name ="Назва")]
        [Required(ErrorMessage ="Поле не може бути порожнім")]
        [RegularExpression(@"[А-Я|І|Є]{1}[а-я|i|є|`]+$", ErrorMessage = "Некорректне ім'я")]
        [Remote(action: "CheckDegree", controller: "DegreeCollections", ErrorMessage = "Науковий ступінь уже використовується")]
        public string DegreeName { get; set; }

        public virtual ICollection<Professor> Professor { get; set; }
    }
}
