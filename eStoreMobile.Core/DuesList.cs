using eStore.Shared.Models.Stores;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eStore.Shared.Models.Sales
{
    public class DuesList
    {
        public int DuesListId { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal Amount { get; set; }

        [Display(Name = "Is Paid")]
        public bool IsRecovered { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Recovery Date")]
        public DateTime? RecoveryDate { get; set; }

        public int DailySaleId { get; set; }
        public virtual DailySale DailySale { get; set; }
        public bool IsPartialRecovery { get; set; }

        //Version 3.0
        [DefaultValue(1)]
        public int StoreId { get; set; }

        public virtual Store Store { get; set; }
        public string UserId { get; set; }
    }
}