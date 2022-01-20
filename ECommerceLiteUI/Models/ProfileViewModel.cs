using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ECommerceLiteUI.Models
{
    public class ProfileViewModel
    {
        #region Name
        [Required]
        [Display(Name = "Ad")]
        public string Name { get; set; }
        #endregion

        #region Surname
        [Required]
        [Display(Name = "Soyad")]
        public string Surname { get; set; }
        #endregion

        #region Username
        [Display(Name = "Kullanıcı Adı")]
        public string Username { get; set; }
        #endregion

        #region Email
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        #endregion

        #region CurrentPassword
        [Display(Name = "Mevut Şifre")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; } 
        #endregion

        #region NewPassword
        [StringLength(100)]
        [Display(Name = "Yeni Şifre")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^[a-zA-Z]\w{4,14}$", ErrorMessage = @"	
The password's first character must be a letter, it must contain at least 5 characters and no more than 15 characters and no characters other than letters, numbers and the underscore may be used")]
        public string NewPassword { get; set; }
        #endregion

        #region ConfirmNewPassword
        [StringLength(100)]
        [Display(Name = "Yeni Şifre Tekrar")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Şifreler uyuşmuyor")]
        public string ConfirmNewPassword { get; set; }
        #endregion


    }
}