using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceLiteEntity.Models
{
    [Table("Pruducts")]
   public class Product : TheBase<int>
    {
        

        #region ProductName
        [Required]
        [StringLength(50,MinimumLength =2,ErrorMessage ="Ürün adı 2-50 karakter aralığında olmalıdır.")]
        public string ProductName { get; set; }
        #endregion

        #region Description
        [Required]
        [StringLength(50, ErrorMessage = "Ürün açıklaması en fazla 500 karakter olmalıdır.")]
        public string Description { get; set; }
        #endregion

        #region UnitPrice
        [Required]
        [DataType(DataType.Currency)]
        public decimal UnitPrice { get; set; }
        #endregion

        #region Quantity
        [Required]
        public int Quantity { get; set; }
        #endregion

        #region Defining ForeignKey - CategoryId
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
        #endregion

    }
}
