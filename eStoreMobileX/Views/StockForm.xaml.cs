using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eStoreMobile.Core.DataViewModel;
using eStoreMobile.Core.Models;
using eStoreMobileX.ViewModel;
using Syncfusion.XForms.DataForm;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace eStoreMobileX.Views
{
    [XamlCompilation (XamlCompilationOptions.Compile)]
    public partial class StockForm : ContentPage
    {
        string Barcode = "";
        string CodeType = "";
        decimal Qty = 0.0M;
        StockListDataModel dm;
        StockListViewModel sm;
        public StockForm()
        {
            InitializeComponent ();
             dm = new StockListDataModel();
            
        }
        private void ZXingScannerView_OnScanResult(ZXing.Result result)
        {

           
            Device.BeginInvokeOnMainThread(() =>
           {
               qty.Value = 0.0;
               barcode.Text = "Barcode: " + result.Text;
               codeType.Text = "Type: " + result.BarcodeFormat.ToString();
               Barcode = result.Text;
               CodeType = result.BarcodeFormat.ToString();
               Qty = 0;
           });
            var data = dataForm.DataObject as StockList;
            data.Barcode = result.Text;
            dataForm.DataObject = data;
            
        }
        private void DataForm_AutoGeneratingDataFormItem(object sender, AutoGeneratingDataFormItemEventArgs e)
        {
            if (e.DataFormItem.Name == "Count")
                e.Cancel = true;
            else if(e.DataFormItem.Name == "LastAccess")
                e.DataFormItem.IsReadOnly = true;
        }
        private void qty_ValueChanged(object sender, Syncfusion.SfNumericTextBox.XForms.ValueEventArgs e)
        {
            Qty = (decimal)e.Value;


            Device.BeginInvokeOnMainThread(() =>
           {
               barcode.Text+= $"  Selected Qty: {Qty}";
           });

        }
        private async void Save_Clicked(object sender, EventArgs e)
        {
            try
            {

                //Qty = (decimal) qty.Value;

                if (string.IsNullOrEmpty(Barcode) || Qty == 0)
                {
                    Device.BeginInvokeOnMainThread(() =>
                   {
                       barcode.Text = "Error: Barcode or Qty is missing ";
                   });
                    await DisplayAlert("Error", "Barcode or Qty is missing", "Ok");
                }
                else
                {
                    int ctr = 0;
                    StockList stock;// = new StockList { Barcode = Barcode, Count = 1, LastAccess = DateTime.Now, Stock = Qty };
                    stock = await dm.FindBarCodeAsync(Barcode);
                    if (stock != null)
                    {
                        stock.Count += 1;
                        stock.Stock += Qty;
                        stock.LastAccess = DateTime.Now;
                        ctr = await dm.SaveAsync(stock, false);
                    }
                    else
                    {
                        stock = new StockList { Barcode = Barcode, Count = 1, LastAccess = DateTime.Now, Stock = (decimal)Qty };
                        ctr = await dm.SaveAsync(stock, true);
                    }
                    Device.BeginInvokeOnMainThread(() =>
                   {
                       barcode.Text = $"Save: Barcode {Barcode}, AddedQty: {Qty} TotalQty: {stock.Stock}   \n Status: {(ctr > 0 ? "Saved" : "Error")} ";
                   });
                    await DisplayAlert("Alert", $"Save: Barcode {Barcode}, AddedQty: {Qty} TotalQty: {stock.Stock}   \n Status: {(ctr > 0 ? "Saved" : "Error")} ", "OK");
                }
            }
            catch (Exception ex)
            {

                await DisplayAlert("Error", "An error Occured.." + ex.Message, "OK");
                Debug.WriteLine(ex.Message);
            }
        }
    }
}