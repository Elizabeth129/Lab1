using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Lab1
{
    public partial class PublishingCollection
    {
        public PublishingCollection()
        {
            Publication = new HashSet<Publication>();
        }

        public int Id { get; set; }
        [Display(Name = "Ім'я")]
        [Required(ErrorMessage = "Поле не може бути порожнім")]
       
        [Remote(action: "CheckPublishing", controller: "PublishingCollections", ErrorMessage = "Видавництво уже використовується")]
        public string PublishingName { get; set; }

        public virtual ICollection<Publication> Publication { get; set; }
    }
}
