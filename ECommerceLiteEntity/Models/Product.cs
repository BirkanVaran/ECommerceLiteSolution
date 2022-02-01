using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceLiteEntity.Models
{
    [Table("Products")]
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

        #region ProductCode
        [StringLength(8,ErrorMessage ="Ürün kodunuzun uzunluğu en fazla 8 karakter olmalıdır.")]
        [Index(IsUnique =true)]
        public string ProductCode { get; set; }
        #endregion

        #region Price
        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
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
