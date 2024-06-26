﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceLiteEntity.Models
{
    public class TheBase<T> : ITheBase
    {
        #region T Id
        [Key]
        [Column(Order = 1)]
        public T Id { get; set; }
        #endregion

        #region RegisterDate
        [DataType(DataType.DateTime)]
        [Display(Name ="Kayıt Tarihi")]
        public DateTime RegisterDate { get; set; } = DateTime.Now;
        #endregion
    }
}
