using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace eStoreMobile.Core.Models
{
   public class StockList
    {
        [Display (AutoGenerateField = false)]
        public int StockListId { get; set; }
        public string Barcode { get; set; }
        public decimal Stock { get; set; }
        public DateTime LastAccess { get; set; }
        public int Count { get; set; }
    }
}
