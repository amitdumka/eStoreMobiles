﻿using System;
using System.Collections.Generic;
using eStore.Shared.Models.Accounts;
using Syncfusion.XForms.DataForm;
using Xamarin.Forms;

namespace eStoreMobileX.Views.Vochers.Editors
{
    public partial class CashReceiptEditorPage : ContentPage
    {
        public CashReceiptEditorPage()
        {
            InitializeComponent();
        }

        void SaveData_Clicked(System.Object sender, System.EventArgs e)
        {
            dataForm.Commit();
            var data = dataForm.DataObject as CashReceiptVM;

            CashReceipt Receipt = new CashReceipt
            {
                Amount = data.Amount,
                InwardDate = data.InwardDate,
                ReceiptFrom = data.ReceiptFrom,


                EntryStatus = EntryStatus.Added,

                IsReadOnly = false,
                UserId = ApplicationContext.UserName,
                StoreId = ApplicationContext.StoreId,

                SlipNo = data.SlipNo,
                TranscationModeId = data.TranscationModeId,

                Remarks = data.Remarks,

            };
            viewModel.SaveReceit(Receipt, true);
        }
        private void dataForm_AutoGeneratingDataFormItem(object sender, Syncfusion.XForms.DataForm.AutoGeneratingDataFormItemEventArgs e)
        {
            try
            {
                e.DataFormItem.TextInputLayoutSettings = new Syncfusion.XForms.DataForm.TextInputLayoutSettings()
                {
                    OutlineCornerRadius = 30
                };
                if (e.DataFormItem.Name == "Party" || e.DataFormItem.Name == "LedgerEntry")
                {
                    e.Cancel = true;
                }
                else if (e.DataFormItem.Name == "PaidBy" || e.DataFormItem.Name == "FromAccount")
                    e.Cancel = true;

               
                else if (e.DataFormItem.Name == "CashRecieptId") e.Cancel = true;
             
                else if (e.DataFormItem.Name == "IsReadOnly" || e.DataFormItem.Name == "EntryStatus")
                    e.Cancel = true;
                else if (e.DataFormItem.Name == "Store")
                    e.Cancel = true;
                else if (e.DataFormItem.Name == "StoreId")
                    e.Cancel = true;
                else if (e.DataFormItem.Name == "UserId")
                {
                    e.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                App.Current.MainPage.DisplayAlert("Error", ex.Message, "Ok");

            }

        }
    }
}
