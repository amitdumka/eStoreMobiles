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
    public partial class StockListPage : ContentPage
    {
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
            DataGridExcelExportingController excelExport = new DataGridExcelExportingController();
            var excelEngine = excelExport.ExportToExcel(this.dataGrid);
            var workbook = excelEngine.Excel.Workbooks[0];
            MemoryStream stream = new MemoryStream();
            workbook.SaveAs(stream);
            workbook.Close();
            excelEngine.Dispose();

            DependencyService.Get<ISave>().Save("StockBarcodeList.xlsx", "application/msexcel", stream);
        }

        void PDF_Clicked(System.Object sender, System.EventArgs e)
        {
            DataGridPdfExportingController pdfExport = new DataGridPdfExportingController();
            MemoryStream stream = new MemoryStream();
            var doc = pdfExport.ExportToPdf(this.dataGrid, new DataGridPdfExportOption() { FitAllColumnsInOnePage = true });
            doc.Save(stream);
            doc.Close(true);
            if (Device.OS == TargetPlatform.WinPhone || Device.OS == TargetPlatform.Windows)
                DependencyService.Get<ISaveWindowsPhone>().Save("DataGrid.pdf", "application/pdf", stream);
            else
                DependencyService.Get<ISave>().Save("StockBarcodeList.pdf", "application/pdf", stream);

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
