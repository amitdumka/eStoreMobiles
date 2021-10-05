using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using eStoreMobile.Core.DataViewModel;
using Xamarin.Forms;
using Syncfusion.SfDataGrid.XForms;
using Syncfusion.SfDataGrid.XForms.Exporting;

namespace eStoreMobileX.Views
{
    /// <summary>
    /// Stock List Page: List of barcode store locally.
    /// </summary>
    public partial class StockListPage : ContentPage
    {
        /// <summary>
        /// Default Initialzation of class
        /// </summary>
        public StockListPage()
        {
            InitializeComponent();
        }
        private void Load()
        {
            dataGrid.ItemsSource = viewModel.StockListCollection;
        }
        void SearchBar_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
        }

        void Excel_Clicked(System.Object sender, System.EventArgs e)
        {
            viewModel.ToExcel(this.dataGrid);
        }

        void PDF_Clicked(System.Object sender, System.EventArgs e)
        {
            viewModel.ToPDF(this.dataGrid);

        }

        async void Sync_Clicked(System.Object sender, System.EventArgs e)
        {
            var res = await DisplayAlert("Alert", "Do you want to upload to server?", "Ok", "Cancel");
            if (res)
            {
                StockListDataModel dm = new StockListDataModel();
                await dm.SyncUpAsync();
            }
        }

        async void pullToRefresh_Refreshing(System.Object sender, System.EventArgs e)
        {
            pullToRefresh.IsRefreshing = true;
            await Task.Delay(1200);
            this.viewModel.ItemsSourceRefresh();
            pullToRefresh.IsRefreshing = false;
        }
    }
}
