using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceLiteEntity.Models
{

        [Table("ProductPictures")]
        public class ProductPicture : TheBase<int>
        {
            #region ProductId
            public int ProductId { get; set; }
            [StringLength(400, ErrorMessage = "Ürüne ait resim bilgisi en fazla 400 karakter uzunluğunda olabilir!")]
            #endregion

            #region ProductPictures 1-2-3-4-5
            public string ProductPicture1 { get; set; }
            [StringLength(400, ErrorMessage = "Ürüne ait resim bilgisi en fazla 400 karakter uzunluğunda olabilir!")]
           
            public string ProductPicture2 { get; set; }
            [StringLength(400, ErrorMessage = "Ürüne ait resim bilgisi en fazla 400 karakter uzunluğunda olabilir!")]

            public string ProductPicture3 { get; set; }
            [StringLength(400, ErrorMessage = "Ürüne ait resim bilgisi en fazla 400 karakter uzunluğunda olabilir!")]

            public string ProductPicture4 { get; set; }
            [StringLength(400, ErrorMessage = "Ürüne ait resim bilgisi en fazla 400 karakter uzunluğunda olabilir!")]

            public string ProductPicture5 { get; set; }
            [ForeignKey("ProductId")]
            public virtual Product Product { get; set; }

            #endregion
        }
}
