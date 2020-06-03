using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WebUI.Models
{
    public class AppUserAddViewModel
    {
        [Required(ErrorMessage = "Kullanıcı Adı boş geçilemez.")]
        [Display(Name="Kullanıcı Adı :  ")]
        public string UserName { get; set; }

        [Display(Name = "Parola :")]
        [Required(ErrorMessage = "Parola alanı boş geçilemez." )]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Şifreniz en az 8 karakterli olmalıdır.")]
        public string Password { get; set; }

        [Display(Name="Parolanızı Tekrar Giriniz : ")]
        [Compare("Password" , ErrorMessage = "Parolalar eşleşmiyor.")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Şifreniz en az 8 karakterli olmalıdır.")]
        public string ConfirmPassword { get; set; }


        [Display(Name="Email :")]
        [EmailAddress(ErrorMessage = "Geçersiz email.")]
        [Required(ErrorMessage = "Email alanı boş geçilemez.")]
        public string Email { get; set; }

        [Display(Name="Adınız :")]
        [Required(ErrorMessage = "Ad alanı boş geçilemez.")]
        public string FirstName { get; set; }

        [Display(Name="Soyadınız : ")]
        [Required(ErrorMessage = "Soyad alanı boş geçilemez.")]
        public string LastName { get; set; }

   
    }
}
