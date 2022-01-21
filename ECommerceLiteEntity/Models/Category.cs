using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceLiteEntity.Models
{
    [Table("Categories")]
    public class Category : TheBase<int>
    {
        #region CategoryName
        [Required]
        [StringLength(50,MinimumLength =2,ErrorMessage = "Kategori adı 2-50 karakter aralığında olmalıdır.")]
        public string CategoryName { get; set; }
        #endregion

        #region CategoryDescription
        [StringLength(500,ErrorMessage ="Kategori açıklama uzunluğu en fazla 500 karakter olmalıdır.")]
        public string CategoryDescription { get; set; }
        #endregion

        #region Defining ForeignKey - BaseCategoryId
        public int? BaseCategoryId { get; set; }

        [ForeignKey("BaseCategoryId")]
        public virtual Category BaseCategory { get; set; }
        #endregion

        public virtual List<Product> ProductList { get; set; }

        public virtual List<Category> CategoryList { get; set; }
    }
}
