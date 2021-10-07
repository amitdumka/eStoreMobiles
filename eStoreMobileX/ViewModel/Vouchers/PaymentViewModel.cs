using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using eStore.Shared.Models.Accounts;

namespace eStoreMobileX.ViewModel.Vouchers
{
    public class PaymentViewModel:IViewModel
    {
        
        public ObservableCollection<Payment> ItemList { get; set; }
        public Payment Item { get; set; }
        private PaymentDataModel dm = new PaymentDataModel();
        public PaymentViewModel()
        {

            ItemList = new ObservableCollection<Payment>();
            this.Item = new Payment
            {
                OnDate = DateTime.Today.Date,
                ApplicationContext.StoreId = ApplicationContext.ApplicationContext.StoreId,
                UserId = ApplicationContext.UserName,
                IsReadOnly = false, EntryStatus=EntryStatus.Added,
            };
            //Enable if required.
            //this.attendance = new Attendance ();
            this.LoadData();
        }
        public async void LoadData()
        {
            try
            {
                dm = new PaymentDataModel();// (ApplicationContext.EmpId, ApplicationContext.Role);
                List<Payment> Data = await dm.GetPayments(ApplicationContext.StoreId, true);

                if (Data == null || Data.Count <= 0)
                {
                    Data = await dm.GetPayments(ApplicationContext.StoreId, false);
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

        public async void SyncUp()
        {
            await dm.Sync();
        }

        

        
    }
}
