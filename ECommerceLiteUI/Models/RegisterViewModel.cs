using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ECommerceLiteUI.Models
{
    public class RegisterViewModel
    {
        #region TCNumber
        [Required]
        [StringLength(11, ErrorMessage = "TC Kimlik Numarası 11 haneli olmalıdır.")]
        [Display(Name="TCKN")]
        public string TCNumber { get; set; }
        #endregion

        #region Name
        [Required]
        [Display(Name="Ad")]
        public string Name { get; set; }
        #endregion

        #region Surname
        [Required]
        [Display(Name="Soyad")]
        public string Surname { get; set; }
        #endregion

        #region Username
        public string Username { get; set; } 
        #endregion

        #region Email
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        #endregion

        #region Password
        [Required]
        [Display(Name = "Şifre")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^[a-zA-Z]\w{4,14}$", ErrorMessage = @"	
The password's first character must be a letter, it must contain at least 5 characters and no more than 15 characters and no characters other than letters, numbers and the underscore may be used")]

        public string Password { get; set; }
        #endregion

        #region Confirm Password
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Şifre Tekrar")]
        [Compare("Password", ErrorMessage ="Şifreler aynı değil.")]
        public string ConfirmPassword { get; set; } 
        #endregion
    }
}