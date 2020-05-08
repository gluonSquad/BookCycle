using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Models
{
    public class AppUserSignInModel
    {
        [Required(ErrorMessage = "Kullanıcı Adı boş geçilemez.")]
        [Display(Name = "Kullanıcı Adı :  ")]
        public string UserName { get; set; }

        [Display(Name = "Parola :")]
        [Required(ErrorMessage = "Parola alanı boş geçilemez.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
