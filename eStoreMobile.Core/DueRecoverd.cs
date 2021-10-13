﻿using eStore.Shared.Models.Stores;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eStore.Shared.Models.Sales
{
    public class DueRecoverd
    {
        public int DueRecoverdId { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Recovery Date")]
        public DateTime PaidDate { get; set; }

        public int DuesListId { get; set; }
        public virtual DuesList DuesList { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal AmountPaid { get; set; }

        [Display(Name = "Is Partial Payment")]
        public bool IsPartialPayment { get; set; }

        public PaymentMode Modes { get; set; }
        public string Remarks { get; set; }

        //Version 3.0
        [DefaultValue(1)]
        public int StoreId { get; set; }

        public virtual Store Store { get; set; }

        public string UserId { get; set; }
    }
}