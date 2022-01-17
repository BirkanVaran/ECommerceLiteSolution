using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceLiteEntity.Models
{
    public class PersonBase : IPerson
    {
        #region TCKN
        [Key]
        [Column(Order =1)]
        [MinLength(11)]
        [StringLength(11,ErrorMessage ="TC Kimlik Numarası 11 haneli olmalıdır.")]
        public string TcNumber { get; set; }
        #endregion

        #region LastActiveTime
        [DataType(DataType.DateTime)]
        public DateTime LastActiveTime { get; set; }
        #endregion


    }
}
