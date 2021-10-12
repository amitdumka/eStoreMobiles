using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using eStoreMobile.Core.Models.Invoicing;

namespace eStoreMobile.Core.DataViewModel.Invoices
{
    public class InvoiceDataModel : LocalDataModel<Invoice>
    {
        public InvoiceDataModel() : base(Constants.InvoicesUrl, "Invoice")
        {
        }

        public override Task<List<Invoice>> FindAsync(string query, bool local = false)
        {
            throw new NotImplementedException();
        }
    }
}
