using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceLiteEntity.Models;

namespace ECommerceLiteEntity.IdentityModels
{
    public class ApplicationUser : IdentityUser
    {
        #region Name
        [StringLength(maximumLength: 25, MinimumLength = 2, ErrorMessage = "İsminiz 2-25 karakter aralığında olmalıdır.")]
        [Display(Name = "Ad")]
        [Required]
        public string Name { get; set; }
        #endregion

        #region Surname
        [StringLength(maximumLength: 25, MinimumLength = 2, ErrorMessage = "Soyisminiz 2-25 karakter aralığında olmalıdır.")]
        [Display(Name = "Soyad")]
        [Required]
        public string Surname { get; set; }
        #endregion

        #region RegisterDate
        [Display(Name = "Kayıt Tarihi")]
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime RegisterDate { get; set; } = DateTime.Now;
        #endregion

        #region ActivationCode
        // TODO: Guid'in kaç haneli olduğuna bakıp stringlength attribiute tanımlayacağız.
        public string ActivationCode { get; set; } 
        #endregion

        public virtual List<Customer> CustomerList { get; set; }
        public virtual List<Admin> AdminList { get; set; }
        public virtual List<PassiveUser> PassiveUserList { get; set; }

    }
}
