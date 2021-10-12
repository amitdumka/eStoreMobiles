using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace eStoreMobileX.Views.Invoices
{
    public partial class InvoicePage : ContentPage
    {
        public InvoicePage()
        {
            InitializeComponent();
        }

        private async void Sale_Clicked(object sender, EventArgs e)
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(InvoiceEditorPage));
            }
            catch (Exception ex)
            {

                await DisplayAlert("Alert", "Error: " + ex.Message, "Ok");
            }
        }

        private async void SaleReturn_Clicked(object sender, EventArgs e)
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(InvoiceReturnEditorPage));
            }
            catch (Exception ex)
            {

                await DisplayAlert("Alert", "Error: " + ex.Message, "Ok");
            }
        }
    }
}
