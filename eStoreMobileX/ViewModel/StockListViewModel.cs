using eStoreMobile.Core.DataViewModel;
using eStoreMobile.Core.Models;
using Syncfusion.SfDataGrid.XForms;
using Syncfusion.SfDataGrid.XForms.Exporting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using Xamarin.Forms;

namespace eStoreMobileX.ViewModel
{
    public class StockListViewModel
    {
        public StockList StockList { get; set; }
        private ObservableCollection<StockList> stockLists;
        private StockListDataModel dm;
        public ObservableCollection<StockList> StockListCollection
        {
            get { return stockLists; }
            set { this.stockLists = value; }
        }

        public StockListViewModel()
        {
            StockList = new StockList();
            StockList.LastAccess = DateTime.Now.Date;
            stockLists = new ObservableCollection<StockList>();
            dm = new StockListDataModel();
            this.LoadData();
        }

        private async void LoadData()
        {
            List<StockList> Data = await dm.RefreshDataAsync();
            stockLists.Clear();
            foreach (var item in Data)
            {
                stockLists.Add(item);
            }
        }

        /// <summary>
        /// Reload items
        /// </summary>
        public void ItemsSourceRefresh()
        {
            LoadData();
        }

        public async void SyncUp()
        {
            var res = await App.Current.MainPage.DisplayAlert("Alert", "Do you want to upload to server?", "Ok", "Cancel");
            if (res)
            {

                _ = dm.SyncUpAsync();
            }
        }

        public void ToExcel(SfDataGrid dataGrid)
        {
            DataGridExcelExportingController excelExport = new DataGridExcelExportingController();
            var excelEngine = excelExport.ExportToExcel(dataGrid);
            var workbook = excelEngine.Excel.Workbooks[0];
            MemoryStream stream = new MemoryStream();
            workbook.SaveAs(stream);
            workbook.Close();
            excelEngine.Dispose();

            DependencyService.Get<ISave>().Save("StockBarcodeList.xlsx", "application/msexcel", stream);
        }

        public void ToPDF(SfDataGrid dataGrid)
        {
            DataGridPdfExportingController pdfExport = new DataGridPdfExportingController();
            MemoryStream stream = new MemoryStream();
            var doc = pdfExport.ExportToPdf(dataGrid, new DataGridPdfExportOption() { FitAllColumnsInOnePage = true });
            doc.Save(stream);
            doc.Close(true);
            if (Device.OS == TargetPlatform.WinPhone || Device.OS == TargetPlatform.Windows)
                DependencyService.Get<ISaveWindowsPhone>().Save("DataGrid.pdf", "application/pdf", stream);
            else
                DependencyService.Get<ISave>().Save("StockBarcodeList.pdf", "application/pdf", stream);
        }
    }
}