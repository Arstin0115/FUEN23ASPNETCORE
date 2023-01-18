using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace prjMvcCoreDemo.Models
{
    public partial class TProduct
    {
        public int FId { get; set; }
        [DisplayName("產品名稱")]
        public string? FName { get; set; }
        [DisplayName("庫存")]
        public int? FQty { get; set; }
        public decimal? FCost { get; set; }
        public decimal? FPrice { get; set; }
        public string? FImagePath { get; set; }
    }
}
