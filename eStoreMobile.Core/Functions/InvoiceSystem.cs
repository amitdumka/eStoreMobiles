using System;
using System.Linq;
using eStoreMobile.Core.Database;
using eStoreMobile.Core.Models.Invoicing;

namespace eStoreMobile.Core.Functions
{
    public class InvoiceSystem
    {
        public static string GenerateInvoiceNumber(InvoiceType type) {
            using var db = new eStoreDbContext();
            int count = db.Invoices.Where(c => c.OnDate.Date == DateTime.Today.Date && c.InvoiceType == type).Count();
            string zeros = "";
            if (count < 10) zeros = "00";
            else if (count < 100 && count >= 10) zeros = "0";
            string InvoiceNumber = $"JH006IN{DateTime.Today.Year}{DateTime.Today.Month}{DateTime.Today.Day}{zeros}{ ++count}";
            return InvoiceNumber;
        }

        public static void PrintInvoice(string InvoiceNumber)
        {

        }

        public static void UploadInvoice(string InvoiceNumber) { }

    }

   
}
