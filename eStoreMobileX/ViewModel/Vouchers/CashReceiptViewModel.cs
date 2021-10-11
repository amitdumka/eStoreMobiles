﻿//using System;
using eStore.Shared.Models.Accounts;
using eStoreMobile.Core.DataViewModel;
using eStoreMobile.Core.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace eStoreMobileX.ViewModel.Vouchers
{
    public class CashReceiptViewModel
    {
        public ObservableCollection<CashReceipt> ItemList { get; set; }
        public CashReceiptVM Item { get; set; }
        private CashReceiptDataModel dm = new CashReceiptDataModel();
        private List<DropListVM> modeList;

        public async System.Threading.Tasks.Task<List<DropListVM>> LoadModeList()
        {
            TranscationModeDataModel eDM = new TranscationModeDataModel();
            if (modeList == null || modeList.Count <= 0)
                modeList = await eDM.GetTranscationModeListAsync();
            return modeList;
        }
        public CashReceiptViewModel()
        {
            ItemList = new ObservableCollection<CashReceipt>();
            this.Item = new CashReceiptVM
            {
                InwardDate = DateTime.Today.Date,
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
                dm = new CashReceiptDataModel();// (ApplicationContext.EmpId, ApplicationContext.Role);
                List<CashReceipt> Data = await dm.GetItemsAsync(ApplicationContext.StoreId, true);

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

        public async void SaveReceit(CashReceipt payment, bool isNew=true)
        {
            if (await dm.SaveAsync(payment, isNew))
            {
                await App.Current.MainPage.DisplayAlert("Alert", "Receipt is salved!", "Ok");
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Error", "Not able to save, Kindly try again!", "Ok");
            }
        }
    }



}