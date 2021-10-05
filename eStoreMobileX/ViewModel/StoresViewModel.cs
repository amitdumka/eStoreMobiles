using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using eStore.Shared.Models.Stores;
using eStoreMobile.Core.DataViewModel;

namespace eStoreMobileX.ViewModel
{
    public class StoresViewModel
    {
        private ObservableCollection<Store> stores;
        private Store store;
        //private StoreVM StoreVM { get; set; }
        StoreDataModel dm;
        public ObservableCollection<Store> StoreList
        {
            get { return stores; }
            set { this.stores = value; }
        }
        public Store Store
        {
            get { return store; }
            set { this.store = value; }
        }

        public StoresViewModel()
        {
            
            stores = new ObservableCollection<Store>();
            this.store = new Store();
            this.store.OpeningDate = DateTime.Today.Date;
            
            //Enable if required.
            //this.attendance = new Attendance ();
            this.LoadData();
        }
        private async void LoadData()
        {
            try
            {
                dm = new StoreDataModel();// (ApplicationContext.EmpId, ApplicationContext.Role);
                List<Store> Data = await dm.GetStores(true);
                if (Data == null  ||Data.Count<=0)
                    Data = await dm.GetStores(false);
                if (Data != null)
                {
                    stores.Clear();
                    foreach (var item in Data)
                    {
                        stores.Add(item);
                    }
                }
                else
                {
                    _ = dm.Sync();
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
    }
}
