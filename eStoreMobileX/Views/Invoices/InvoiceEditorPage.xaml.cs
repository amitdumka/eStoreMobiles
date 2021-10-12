using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace eStoreMobileX.Views.Invoices
{
    public partial class InvoiceEditorPage : ContentPage
    {
        public InvoiceEditorPage()
        {
            InitializeComponent();
        }

        private void AddItem_Clicked(object sender, EventArgs e)
        {
            popup.Show();
        }

        private void dataForm_AutoGeneratingDataFormItem(object sender, Syncfusion.XForms.DataForm.AutoGeneratingDataFormItemEventArgs e)
        {
            if (e.DataFormItem.Name == "InvoiceType"|| e.DataFormItem.Name == "TotalQty"|| e.DataFormItem.Name == "TotalAmount" || e.DataFormItem.Name == "TotalTax")
                e.DataFormItem.IsReadOnly = true;
            if(e.DataFormItem.Name=="InvoiceNumber"|| e.DataFormItem.Name == "TotalDiscount" )
                e.DataFormItem.IsReadOnly = true;
        }
    }
}
