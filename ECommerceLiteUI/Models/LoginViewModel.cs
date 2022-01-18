using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ECommerceLiteUI.Models
{
    public class LoginViewModel
    {
        #region Email
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        #endregion

        #region Password
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        #endregion

        #region RememberMe
        public bool RememberMe { get; set; }
        #endregion

        #region ReturnUrl
        public string ReturnUrl { get; set; } 
        #endregion
    }
}