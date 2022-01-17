using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceLiteEntity.Models
{
    [Table("Orders")]
   public class Order : TheBase<int>
    {
        #region OrderNumber
        [Required]
        [StringLength(7,ErrorMessage ="Satış Numarası gereklidir.")]
        public string OrderNumber { get; set; }
        #endregion

        #region Defining ForeignKey - CustomerTCNumber
        public string CustomerTCNumber { get; set; }

        [ForeignKey("CustomerTCNumber")]
        public virtual Customer Customer { get; set; }
        #endregion

        public virtual List<OrderDetail> OrderDetailList { get; set; }
    }
}
