using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eStoreMobile.Core.Models.Invoicing
{
    public enum InvoiceType { Sales,SalesReturn,ManualInovice, ManaulSaleReturn}

    public class Invoice
    {
        [Display(AutoGenerateField = false)]
        public int InvoiceId { get; set; }

        [Key]
        public string InvoiceNumber { get; set; }
        public DateTime OnDate { get; set; }
        public string MobileNo { get; set; }
        public string CustomerName { get; set; }

        public decimal TotalAmount { get; set; }
        public decimal TotalTax { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal TotalQty { get; set; }
        public InvoiceType InvoiceType { get; set; }
        public string? Remarks { get; set; }
        
        [Display(AutoGenerateField = false)]
        public virtual ICollection<InvoiceItem> InvoiceItems { get; set; }
        [Display(AutoGenerateField = false)]
        public virtual InvoicePayment Payment { get; set; }
    }

    public class InvoiceItem
    {
        [Display(AutoGenerateField = false)]
        public int InvoiceItemId { get; set; }
        public string InvoiceNumber { get; set; }
        public string Barcode { get; set; }
        public decimal Qty { get; set; }
        public decimal DiscountAmt { get; set; }
        public decimal BasicAmount { get; set; }
        public decimal TaxRate { get; set; }
        [Display(AutoGenerateField = false)]
        public decimal Amount { get { return BasicAmount + ((BasicAmount * 100) / TaxRate); } }
        public TaxType TaxType { get; set; }
    }
    public class InvoicePayment
    {
        [Display(AutoGenerateField = false)]
        public int InvoicePaymentId { get; set; }
        [ForeignKey("Invoice")]
        public string InvoiceNumber { get; set; }
        public decimal Amount { get; set; }
        public PayMode Mode { get; set; }
        public string? RefDetails { get; set; }
        public int? CardLastDigit { get; set; }
        public int? AuthCode { get; set; }
        public CardType? CardType { get; set; }
        public CardMode? CardMode { get; set; }
        public int? EDCId { get; set; }
        [Display(AutoGenerateField = false)]
        public virtual Invoice Invoice { get; set; }
    }
}
