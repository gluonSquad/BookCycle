using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Areas.Admin.Models
{
    public class BookAddViewModel
    {
        [Display(Name="Kitap Adı")]
        [Required(ErrorMessage = "Kitap Adı boş geçilemez")]
        public string Title { get; set; }
        [Display(Name = "Yazar")]
        [Required(ErrorMessage = "Yazar boş geçilemez")]
        public string Author { get; set; }
    }
}
