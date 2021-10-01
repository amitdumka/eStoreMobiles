using eStoreMobile.Core.DataViewModel;
using eStoreMobile.Core.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace eStoreMobile.Views
{
    [XamlCompilation (XamlCompilationOptions.Compile)]
    public partial class StockScanner : ContentPage
    {
        string Barcode = "";
        string CodeType = "";
        decimal Qty = 0.0M;
        StockListDataModel dm ;
        public StockScanner()
        {
            InitializeComponent ();
            dm = new StockListDataModel ();
        }

        private async void Save_Clicked(object sender, EventArgs e)
        {
            try
            {

                //Qty = (decimal) qty.Value;

                if ( string.IsNullOrEmpty (Barcode) || Qty == 0 )
                {
                    Device.BeginInvokeOnMainThread (() =>
                    {
                        statusMsg.Text = "Error: Barcode or Qty is missing ";
                    });
                    await DisplayAlert ("Error", "Barcode or Qty is missing", "Ok");
                }
                else
                {
                    int ctr = 0;
                    StockList stock;// = new StockList { Barcode = Barcode, Count = 1, LastAccess = DateTime.Now, Stock = Qty };
                    stock = await dm.FindBarCodeAsync (Barcode);
                    if ( stock != null )
                    {
                        stock.Count += 1;
                        stock.Stock +=Qty;
                        stock.LastAccess = DateTime.Now;
                        ctr = await dm.SaveAsync (stock, false);
                    }
                    else
                    {
                        stock = new StockList { Barcode = Barcode, Count = 1, LastAccess = DateTime.Now, Stock = (decimal) Qty };
                        ctr = await dm.SaveAsync (stock, true);
                    }
                    Device.BeginInvokeOnMainThread (() =>
                    {
                        statusMsg.Text = $"Save: Barcode {Barcode}, AddedQty: {Qty} TotalQty: {stock.Stock}   \n Status: {( ctr > 0 ? "Saved" : "Error" )} ";
                    });
                    await DisplayAlert ("Alert", $"Save: Barcode {Barcode}, AddedQty: {Qty} TotalQty: {stock.Stock}   \n Status: {( ctr > 0 ? "Saved" : "Error" )} ", "OK");
                }
            }
            catch ( Exception  ex)
            {

                await DisplayAlert ("Error", "An error Occured.." + ex.Message,"OK");
                Debug.WriteLine (ex.Message);
            }
        }
      

        private void ZXingScannerView_OnScanResult(ZXing.Result result)
        {
            Device.BeginInvokeOnMainThread (() =>
            {
                qty.Value = 0.0;
                barcode.Text = "Barcode: " + result.Text;
                codeType.Text = "Type: " + result.BarcodeFormat.ToString ();
                Barcode = result.Text;
                CodeType = result.BarcodeFormat.ToString ();               
                Qty = 0;
            });
        }

        private void qty_ValueChanged(object sender, Syncfusion.SfNumericTextBox.XForms.ValueEventArgs e)
        {
            Qty = (decimal)e.Value;
            Device.BeginInvokeOnMainThread (() =>
            {
                statusMsg.Text = $"Selected Qty: {Qty}";
            });

        }
    }
}