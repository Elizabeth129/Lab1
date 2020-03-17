using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Lab1
{
    public partial class PlaceOfWork
    {
        public PlaceOfWork()
        {
            Professor = new HashSet<Professor>();
        }

        public int Id { get; set; }
        [Display(Name = "Кафедра")]
        public int CathedraId { get; set; }
        [Display(Name = "Початок роботи")]
        [Required(ErrorMessage = "Поле не може бути порожнім")]
        [DataType(DataType.Date)]
        //[Remote(action: "CheckDateStart", controller: "PlaceOfWorks", ErrorMessage = "Дата не коректна")]
        public DateTime DateOfStartWork { get; set; }
        [Display(Name = "Кінець роботи")]
        [DataType(DataType.Date)]
       // [Remote(action: "CheckDateEnd", controller: "PlaceOfWorks", ErrorMessage = "Дата не коректна")]
        public DateTime? DateOfEndWork { get; set; }
        [Display(Name = "Назва кафедри")]
        
        public virtual Cathedra Cathedra { get; set; }
        public virtual ICollection<Professor> Professor { get; set; }
    }
}
