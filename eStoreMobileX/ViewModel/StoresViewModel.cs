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

        //private StoreVM StoreVM { get; set; }
        StoreDataModel dm;
        public ObservableCollection<Store> StoreList
        {
            get { return stores; }
            set { this.stores = value; }
        }
        public Store Store { get; set; }

        internal async void SyncUp()
        {
           await dm.Sync();
        }

        public StoresViewModel()
        {
            
            stores = new ObservableCollection<Store>();
            this.Store = new Store
            {
                OpeningDate = DateTime.Today.Date
            };

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
