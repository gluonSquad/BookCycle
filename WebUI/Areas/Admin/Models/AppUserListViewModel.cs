using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Areas.Admin.Models
{
    public class AppUserListViewModel
    {
        public int Id { get; set; }
        [Display(Name="Kullanıcı Adı :")]
        [Required(ErrorMessage = "Kullanıcı Adı alanı boş geçilemez ")]
        public string UserName { get; set; }

        [Display(Name = "Ad : ")]
        [Required(ErrorMessage = "Ad alanı boş geçilemez ")]
        public string FirstName { get; set; }
        [Display(Name = "Soyad : ")]
        [Required(ErrorMessage = "Soyad alanı boş geçilemez ")]
        public string LastName { get; set; }

        [Display(Name = "Sizi Tanıyalım : ")]
        public string Description { get; set; }
        [Display(Name = "Email : ")]
        [Required(ErrorMessage = "Email alanı boş geçilemez ")]
        public string Email { get; set; }
        [Display(Name = "Resim : ")]
        public string Picture { get; set; }
    }
}
