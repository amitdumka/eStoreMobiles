using System;
using System.Collections.Generic;
using eStoreMobile.Core.DataViewModel.Invoices;
using eStoreMobile.Core.Models.Invoicing;

namespace eStoreMobileX.ViewModel.Invoicing
{
    public class InvoiceViewModel
    {
        public List<Invoice> Invoices { get; set; }
        public List<InvoiceItem> InvoiceItems { get; set; }
        public Invoice Invoice { get; set; }
        public InvoicePayment InvoicePayment { get; set; }
        public InvoiceDataModel dm;

        public InvoiceViewModel()
        {
            Invoices = new List<Invoice>();
            InvoiceItems = new List<InvoiceItem>();
            Invoice = new Invoice();
            InvoicePayment = new InvoicePayment();
            LoadData();
        }

        [Obsolete]
        private async void LoadData()
        {
            try
            {
                dm = new InvoiceDataModel();
                List<Invoice> Data = await dm.GetItems(ApplicationContext.StoreId,true);
                Invoices.Clear();
                foreach (var item in Data)
                {
                    Invoices.Add(item);
                }
            }
            catch (Exception e)
            {

                await App.Current.MainPage.DisplayAlert("Exception", "Error: " + e.Message, "Ok");
            }
        }

    }
}
