using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Models
{
    public class PasswordResetViewModel
    {
        [Required(ErrorMessage = "Kullanıcı Adı boş geçilemez.")]
        [Display(Name = "Kullanıcı Adı :  ")]
        public string UserName { get; set; }


        [Display(Name = "Yeni Şifreniz :")]
        [Required(ErrorMessage = "Parola alanı boş geçilemez.")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Şifreniz en az 8 karakterli olmalıdır.")]
        public string PasswordNew { get; set; }
    }
}
