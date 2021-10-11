//using System;
using eStore.Shared.Models.Accounts;
using eStoreMobile.Core.DataViewModel;
using eStoreMobile.Core.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace eStoreMobileX.ViewModel.Vouchers
{
    public class CashPaymentViewModel
    {
        public ObservableCollection<CashPayment> ItemList { get; set; }
        public CashPaymentVM Item { get; set; }
        private CashPaymentDataModel dm = new CashPaymentDataModel();
        private List<DropListVM> modeList;

        public async System.Threading.Tasks.Task<List<DropListVM>> LoadModeList()
        {
            TranscationModeDataModel eDM = new TranscationModeDataModel();
            if (modeList == null || modeList.Count <= 0)
                modeList = await eDM.GetTranscationModeListAsync();
            return modeList;
        }
        public  CashPaymentViewModel()
        {
            ItemList = new ObservableCollection<CashPayment>();
            this.Item = new CashPaymentVM
            {
                PaymentDate = DateTime.Today.Date,
                StoreId = ApplicationContext.StoreId,
                UserId = ApplicationContext.UserName,
                IsReadOnly = false,
                EntryStatus = EntryStatus.Added,
            };
            //Enable if required.
            //this.attendance = new Attendance ();
            this.LoadData();
        }

        public async void LoadData()
        {
            try
            {
                dm = new CashPaymentDataModel();// (ApplicationContext.EmpId, ApplicationContext.Role);
                List<CashPayment> Data = await dm.GetItemsAsync(ApplicationContext.StoreId, true);

                if (Data == null || Data.Count <= 0)
                {
                    Data = await dm.GetItemsAsync(ApplicationContext.StoreId, false);
                    await App.Current.MainPage.DisplayAlert("Info", "Loading Data from server...", "Ok");
                }
                if (Data != null)
                {
                    ItemList.Clear();
                    foreach (var item in Data)
                    {
                        ItemList.Add(item);
                    }
                }
            }
            catch (Exception e)
            {
                await App.Current.MainPage.DisplayAlert("Exception", "Error: " + e.Message, "Ok");
            }
        }

        /// <summary>
        /// Refresh Data store.
        /// </summary>
        public void ItemsSourceRefresh()
        {
            LoadData();
        }

        public async void SavePayment(CashPayment payment, bool isNew=true)
        {
            if (await dm.SaveAsync(payment, isNew))
            {
                await App.Current.MainPage.DisplayAlert("Alert", "Payment is salved!", "Ok");
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Error", "Not able to save, Kindly try again!", "Ok");
            }
        }
    }
}