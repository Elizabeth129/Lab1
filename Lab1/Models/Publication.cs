using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Lab1
{
    public partial class Publication
    {
        public Publication()
        {
            ProfessorPublicationLinker = new HashSet<ProfessorPublicationLinker>();
        }

        public int Id { get; set; }

        [Display(Name = "Видавництво")]
        public int PublishingId { get; set; }
        [Display(Name = "Версія")]
        [Required(ErrorMessage = "Поле не може бути порожнім")]
        [RegularExpression(@"[0-9]+$", ErrorMessage = "Некорректний номер")]
        [Remote(action: "CheckVersion", controller: "Publications", ErrorMessage = "Версія уже використовується")]
        public int Version { get; set; }
        [Display(Name = "Назва")]
        [Required(ErrorMessage = "Поле не може бути порожнім")]
 
        [Remote(action: "CheckNumber", controller: "Professors", ErrorMessage = "Персональний номер уже використовується")]
        public string NamePublication { get; set; }
        [Display(Name = "Кількість сторінок")]
        [Required(ErrorMessage = "Поле не може бути порожнім")]
        [RegularExpression(@"[0-9]+$", ErrorMessage = "Некорректна кількість сторінок")]
        public int PageAmount { get; set; }
        [Display(Name = "Видавництво")]
        public virtual PublishingCollection Publishing { get; set; }
        public virtual ICollection<ProfessorPublicationLinker> ProfessorPublicationLinker { get; set; }
    }
}
