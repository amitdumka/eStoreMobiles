using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using eStore.Shared.Models.Accounts;
using eStoreMobileX.Views.Vochers.Editors;
using Xamarin.Forms;

namespace eStoreMobileX.Views.Vochers
{
    public partial class ReceiptPage : ContentPage
    {
        public ReceiptPage()
        {
            InitializeComponent();
        }
       private async void pullToRefresh_Refreshing(System.Object sender, System.EventArgs e)
        {
            pullToRefresh.IsRefreshing = true;
            await Task.Delay(1200);
            this.viewModel.ItemsSourceRefresh();
            pullToRefresh.IsRefreshing = false;
            
           
        }

        async void ToolbarItem_Clicked(System.Object sender, System.EventArgs e)
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(ReceiptEditorPage));
            }
            catch (Exception ex)
            {
                await DisplayAlert("Alert", "Error: " + ex.Message, "Ok");
            }
        }
    }
}
