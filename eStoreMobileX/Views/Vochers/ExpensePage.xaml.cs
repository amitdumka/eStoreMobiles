using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using eStore.Shared.Models.Accounts;
using Xamarin.Forms;

namespace eStoreMobileX.Views.Vochers
{
    public partial class ExpensePage : ContentPage
    {
        public ExpensePage()
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

        private async void AddExp_Clicked(object sender, EventArgs e)
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(ExpenseEditorPage));
            }
            catch (Exception ex)
            {
                await DisplayAlert("Alert", "Error: " + ex.Message, "Ok");
            }
        }
    }
}
