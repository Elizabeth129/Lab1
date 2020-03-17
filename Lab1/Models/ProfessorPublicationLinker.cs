using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lab1
{
    public partial class ProfessorPublicationLinker
    {
        public int Id { get; set; }
        [Display(Name = "Науковець")]
        [Required(ErrorMessage = "Поле не може бути порожнім")]
        public int ProfessorId { get; set; }
        [Display(Name = "Публікація")]
        [Required(ErrorMessage = "Поле не може бути порожнім")]
        public int PublicationId { get; set; }
        [Display(Name = "Науковець")]
        public virtual Professor Professor { get; set; }
        [Display(Name = "Публікація")]
        public virtual Publication Publication { get; set; }
    }
}
