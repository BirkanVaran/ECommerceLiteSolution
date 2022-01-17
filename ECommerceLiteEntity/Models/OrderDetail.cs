using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceLiteEntity.Models
{
    [Table("Order Details")]
    public class OrderDetail : TheBase<int>
    {
        #region Defining ForeignKey - OrderId
        public int OrderId { get; set; }

        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }
        #endregion

        #region Defining ForeignKey - ProductId
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
        #endregion

        #region ProductPrice
        [Required]
        [DataType(DataType.Currency)]
        public decimal ProductPrice { get; set; }
        #endregion

        #region Discount
        [Required]
        public double Discount { get; set; }
        #endregion

        #region Quantity
        [Required]
        public int Quantity { get; set; }
        #endregion

        #region TotalPrice
        [Required]
        [DataType(DataType.Currency)]
        public decimal TotalPrice { get; set; } 
        #endregion
    }
}
