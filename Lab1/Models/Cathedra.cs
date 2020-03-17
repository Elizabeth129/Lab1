using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Lab1
{
    public partial class Cathedra
    {
        public Cathedra()
        {
            PlaceOfWork = new HashSet<PlaceOfWork>();
        }

        public int Id { get; set; }
        [Display(Name = "Факультет")]
        public int FacultyId { get; set; }
        [Display(Name = "Назва кафедри")]
        [Required(ErrorMessage = "Поле не може бути порожнім")]
        [RegularExpression(@"[А-Я|а-я|І|i|є|Є|`]+$", ErrorMessage = "Некорректне ім'я")]
        [Remote(action: "CheckCathedra", controller: "Cathedras", ErrorMessage = "Кафедра уже використовується")]
        public string CathedraName { get; set; }
        [Display(Name = "Назва факультету")]
        
        public virtual FacultyCollection Faculty { get; set; }
        public virtual ICollection<PlaceOfWork> PlaceOfWork { get; set; }
    }
}
