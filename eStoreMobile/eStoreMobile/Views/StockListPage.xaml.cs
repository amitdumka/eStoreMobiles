using eStoreMobile.Core.Database;
using eStoreMobile.Core.Models;
using Microsoft.EntityFrameworkCore;
using Syncfusion.SfDataGrid.XForms;
using Syncfusion.SfDataGrid.XForms.Exporting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
            using ( var db = new eStoreDbContext () )
            {
                List<StockList> Data = await db.StockLists.ToListAsync ();
                foreach ( var item in Data )
                {
                    stockLists.Add (item);
                }
            }
        }
    }


    [XamlCompilation (XamlCompilationOptions.Compile)]
    public partial class StockListPage : ContentPage
    {
       // StockRepo viewModel = new StockRepo ();
       // SfDataGrid dataGrid;
       
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
        private void ExportToExcel_Clicked(object sender, EventArgs e)
        {
            //DataGridExcelExportingController excelExport = new DataGridExcelExportingController ();
            //var excelEngine = excelExport.ExportToExcel (this.dataGrid);
            //var workbook = excelEngine.Excel.Workbooks [0];
            //MemoryStream stream = new MemoryStream ();
            //workbook.SaveAs (stream);
            //workbook.Close ();
            //excelEngine.Dispose ();

            //Xamarin.Forms.DependencyService.Get<ISave> ().Save ("DataGrid.xlsx", "application/msexcel", stream);
        }
        private void PDFExport_Activated(object sender, EventArgs e)
        {
            /////   isExporting = true;
            //DataGridPdfExportingController pdfExport = new DataGridPdfExportingController ();
            //MemoryStream stream = new MemoryStream ();
            //var doc = pdfExport.ExportToPdf (this.dataGrid, new DataGridPdfExportOption () { FitAllColumnsInOnePage = true });
            //doc.Save (stream);
            //doc.Close (true);
            //if ( Device.OS == TargetPlatform.WinPhone || Device.OS == TargetPlatform.Windows )
            //    Xamarin.Forms.DependencyService.Get<ISaveWindowsPhone> ().Save ("DataGrid.pdf", "application/pdf", stream);
            //else
            //    Xamarin.Forms.DependencyService.Get<ISave> ().Save ("DataGrid.pdf", "application/pdf", stream);
            //DataGridPdfExportingController pdfExport = new DataGridPdfExportingController ();
            //MemoryStream stream = new MemoryStream ();
            //var exportToPdf = pdfExport.ExportToPdf (this.dataGrid, new DataGridPdfExportOption ()
            //{
            //    FitAllColumnsInOnePage = true,
            //});
            //exportToPdf.Save (stream);
            //exportToPdf.Close (true);
            //if ( Device.OS == TargetPlatform.WinPhone || Device.OS == TargetPlatform.Windows )
            //    Xamarin.Forms.DependencyService.Get<ISaveWindowsPhone> ().Save ("DataGrid.pdf", "application/pdf", stream);
            //else
            //    Xamarin.Forms.DependencyService.Get<ISave> ().Save ("DataGrid.pdf", "application/pdf", stream);
        }
    }
}