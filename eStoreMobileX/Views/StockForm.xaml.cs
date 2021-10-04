using eStoreMobile.Core.DataViewModel;
using eStoreMobile.Core.Models;
using Syncfusion.XForms.DataForm;
using System;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace eStoreMobileX.Views
{
    [XamlCompilation (XamlCompilationOptions.Compile)]
    public partial class StockForm : ContentPage
    {
        private StockListDataModel dm;

        public StockForm()
        {
            InitializeComponent ();
            dm = new StockListDataModel ();
        }

        private void ZXingScannerView_OnScanResult(ZXing.Result result)
        {
            Device.BeginInvokeOnMainThread (() =>
            {
                barcode.Text = "Barcode: " + result.Text;
                codeType.Text = "Type: " + result.BarcodeFormat.ToString ();
                var model = dataForm.DataObject as StockList;
                model.Barcode = result.Text;
                model.Stock = 1;
                model.Count = 1;

                dataForm.UpdateEditor ("Barcode");
                dataForm.UpdateEditor ("Stock");
                dataForm.UpdateEditor ("Count");
            });
        }

        private void DataForm_AutoGeneratingDataFormItem(object sender, AutoGeneratingDataFormItemEventArgs e)
        {
            if ( e.DataFormItem.Name == "Count" )
                e.Cancel = true;
            else if ( e.DataFormItem.Name == "LastAccess" )
                e.DataFormItem.IsReadOnly = true;
        }

        private async void Save_Clicked(object sender, EventArgs e)
        {
            try
            {
                dataForm.Commit ();
                var model = dataForm.DataObject as StockList;
                if ( model != null )
                {
                    if(string.IsNullOrEmpty(model.Barcode) || model.Stock<=0)
                    {
                       await DisplayAlert ("Alert", "Kindly verify details before adding...", "Ok'");
                    }
                    else
                    {
                        int ctr = 0;
                        StockList stock;
                        stock = await dm.FindBarCodeAsync (model.Barcode);
                        if ( stock != null )
                        {
                            stock.Count += 1;
                            stock.Stock += model.Stock;
                            stock.LastAccess = DateTime.Now;
                            ctr = await dm.SaveAsync (stock, false);
                        }
                        else
                        {
                            model.Count = 1;
                            ctr = await dm.SaveAsync (model, true);
                        }
                       await DisplayAlert ("Alert", $"Save: Barcode {model.Barcode}, AddedQty: {model.Stock}  \n Status: {( ctr > 0 ? "Saved" : "Error" )} ", "OK");
                        model.Barcode = "";
                        model.Stock = 0;
                        model.Count = 0;
                        dataForm.UpdateEditor ("Barcode");
                        dataForm.UpdateEditor ("Stock");
                        dataForm.UpdateEditor ("Count");
                    }
                }
                
            }
            catch ( Exception ex )
            {
                await DisplayAlert ("Error", "An error Occured.." + ex.Message, "OK");
                Debug.WriteLine (ex.Message);
            }
        }

        private void List_Clicked(object sender, EventArgs e)
        {

        }

        private void PDF_Export_Clicked(object sender, EventArgs e)
        {

        }

        private void Excel_Export_Clicked(object sender, EventArgs e)
        {

        }
    }
}