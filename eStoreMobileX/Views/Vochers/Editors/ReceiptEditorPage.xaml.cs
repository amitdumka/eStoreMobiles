using System;
using System.Collections.Generic;
using eStore.Shared.Models.Accounts;
using Syncfusion.XForms.DataForm;
using Xamarin.Forms;

namespace eStoreMobileX.Views.Vochers.Editors
{
    public partial class ReceiptEditorPage : ContentPage
    {
        public ReceiptEditorPage()
        {
            InitializeComponent();
        }

        void SaveData_Clicked(System.Object sender, System.EventArgs e)
        {
        
            dataForm.Commit();
            var data = dataForm.DataObject as ReceiptVM;

            Receipt Receipt = new Receipt
            {
                Amount = data.Amount,
                RecieptSlipNo=data.RecieptSlipNo,
                EntryStatus = EntryStatus.Added,
                IsCash = false,
                IsDyn = false,
                IsOn = false,
                IsReadOnly = false,
                UserId = ApplicationContext.UserName,
                StoreId = ApplicationContext.StoreId,
                OnDate = data.OnDate,
                PayMode = data.PayMode,
                Remarks = data.Remarks,
                PaymentDetails = data.PaymentDetails,
                PartyId = data.PartyId,
                PartyName = data.PartyName,
                BankAccountId = data.BankAccountId
            };
            viewModel.SaveReceipt(Receipt);
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

                else if (e.DataFormItem.Name.Equals("IsOn") || e.DataFormItem.Name.Equals("IsDyn") || e.DataFormItem.Name.Equals("IsCash"))
                {
                    e.Cancel = true;
                    e.DataFormItem.LayoutOptions = LayoutType.Default;
                    (e.DataFormItem as DataFormCheckBoxItem).IsThreeState = false;
                    (e.DataFormItem as DataFormCheckBoxItem).Text = " ";


                }
                else if (e.DataFormItem.Name == "ReceiptId") e.Cancel = true;
                else if (e.DataFormItem.Name == "PartyId")
                {
                    e.DataFormItem = new DataFormDropDownItem()
                    {
                        Name = "PartyId",
                        Editor = "DropDown",
                        LabelText = "Party",
                        // ItemsSource = (await GetEmpList()),
                        PlaceHolderText = "Select a Party",
                        LayoutOptions = LayoutType.TextInputLayout
                    };
                    //(e.DataFormItem as DataFormDropDownItem).DisplayMemberPath = nameof(DropListVM.Label);
                    //(e.DataFormItem as DataFormDropDownItem).SelectedValuePath = nameof(DropListVM.Value);
                }
                else if (e.DataFormItem.Name == "BankAccountId")
                {
                    
                    e.DataFormItem = new DataFormDropDownItem()
                    {
                        Name = "BankAccountId",
                        Editor = "DropDown",
                        LabelText = "Account",
                        // ItemsSource = (await GetEmpList()),
                        PlaceHolderText = "Select a Account No",
                        LayoutOptions = LayoutType.TextInputLayout
                    };
                    //(e.DataFormItem as DataFormDropDownItem).DisplayMemberPath = nameof(DropListVM.Label);
                    //(e.DataFormItem as DataFormDropDownItem).SelectedValuePath = nameof(DropListVM.Value);
                }

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
