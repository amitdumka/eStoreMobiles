using eStoreMobile.Core.DataViewModel;
using eStoreMobile.Core.Models;
using Syncfusion.SfDataGrid.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Syncfusion.SfPullToRefresh.XForms;
using System.Diagnostics.CodeAnalysis;
using Syncfusion.SfDataGrid.XForms.Exporting;
using System.IO;

namespace eStoreMobile.Views
{
    public class StockRepo
    {
        private ObservableCollection<StockList> stockLists;

        public ObservableCollection<StockList> StockListCollection
        {
            get { return stockLists; }
            set { this.stockLists = value; }
        }

        public StockRepo()
        {
            stockLists = new ObservableCollection<StockList> ();
            this.LoadData ();
        }

        private async void LoadData()
        {
            StockListDataModel dm = new StockListDataModel ();

            List<StockList> Data = await dm.RefreshDataAsync ();
            stockLists.Clear ();
            foreach ( var item in Data )
            {
                stockLists.Add (item);
            }

        }
        public void ItemsSourceRefresh()
        {
            LoadData ();
        }





    }

    [XamlCompilation (XamlCompilationOptions.Compile)]
    public partial class StockListPage : ContentPage
    {
        // int swipedRowIndex = -1;
        //StockList selecteRow;
        //void DeleteRow()
        //{
        //    var d = viewModel.StockListCollection[this.swipedRowIndex];
        //}
        //void dataGrid_SwipeStarted(System.Object sender, Syncfusion.SfDataGrid.XForms.SwipeStartedEventArgs e)
        //{
        //}
        //void dataGrid_SwipeEnded(System.Object sender, Syncfusion.SfDataGrid.XForms.SwipeEndedEventArgs e)
        //{
        //    this.selecteRow = (StockList)e.RowData;
        //    this.swipedRowIndex = e.RowIndex;
        //}
        public StockListPage()
        {
            InitializeComponent ();
            dataGrid = new SfDataGrid ();
        }

        private void Load()
        {
            dataGrid.ItemsSource = viewModel.StockListCollection;
        }


        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

        private async void PullToRefresh_Refreshing(object sender, EventArgs e)
        {
            pullToRefresh.IsRefreshing = true;
            await Task.Delay (1200);
            this.viewModel.ItemsSourceRefresh ();
            pullToRefresh.IsRefreshing = false;
        }
        private void ExportToExcel_Clicked(object sender, EventArgs e)
        {
            DataGridExcelExportingController excelExport = new DataGridExcelExportingController ();
            var excelEngine = excelExport.ExportToExcel (this.dataGrid);
            var workbook = excelEngine.Excel.Workbooks [0];
            MemoryStream stream = new MemoryStream ();
            workbook.SaveAs (stream);
            workbook.Close ();
            excelEngine.Dispose ();

            DependencyService.Get<ISave> ().Save ("StockBarcodeList.xlsx", "application/msexcel", stream);
        }

        private void PDFExport_Activated(object sender, EventArgs e)
        {
            // isExporting = true;
            DataGridPdfExportingController pdfExport = new DataGridPdfExportingController ();
            MemoryStream stream = new MemoryStream ();
            var doc = pdfExport.ExportToPdf (this.dataGrid, new DataGridPdfExportOption () { FitAllColumnsInOnePage = true });
            doc.Save (stream);
            doc.Close (true);
            if ( Device.OS == TargetPlatform.WinPhone || Device.OS == TargetPlatform.Windows )
                DependencyService.Get<ISaveWindowsPhone> ().Save ("DataGrid.pdf", "application/pdf", stream);
            else
                DependencyService.Get<ISave> ().Save ("StockBarcodeList.pdf", "application/pdf", stream);


        }

        private async void SfButton_ClickedAsync(object sender, EventArgs e)
        {
            var res = await DisplayAlert ("Alert", "Do you want to upload to server?", "Ok", "Cancel");
            if ( res )
            {
                StockListDataModel dm = new StockListDataModel ();
                await dm.SyncUpAsync ();
            }
        }
    }





}