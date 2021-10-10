using System;
using System.Collections.Generic;
using eStore.Shared.Models.Accounts;
using Syncfusion.XForms.DataForm;
using Xamarin.Forms;

namespace eStoreMobileX.Views.Vochers.Editors
{
    public partial class PaymentEditorPage : ContentPage
    {
        public PaymentEditorPage()
        {
            InitializeComponent();
        }
        private void SavePayment_Clicked(object sender, EventArgs e)
        {
            dataForm.Commit();
            var data = dataForm.DataObject as PaymentVM;

            Payment Payment = new Payment
            {
                Amount = data.Amount,
                PaymentSlipNo=data.PaymentSlipNo,
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
            viewModel.SavePayment(Payment);
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
                else if (e.DataFormItem.Name == "PaymentId") e.Cancel = true;
                else if (e.DataFormItem.Name == "PaymentId") e.Cancel = true;

                else if (e.DataFormItem.Name == "EmployeeId")
                {

                    e.DataFormItem = new DataFormDropDownItem()
                    {
                        Name = "EmployeeId",
                        Editor = "DropDown",
                        LabelText = "Paid By",
                        // ItemsSource = (await GetEmpList()),
                        PlaceHolderText = "Select a Employee",
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
