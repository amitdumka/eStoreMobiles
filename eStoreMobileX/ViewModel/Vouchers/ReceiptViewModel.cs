﻿//using System;
using eStore.Shared.Models.Accounts;
using eStoreMobile.Core.DataViewModel;
using eStoreMobile.Core.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace eStoreMobileX.ViewModel.Vouchers
{
    public class ReceiptViewModel
    {
        public ObservableCollection<Receipt> ItemList { get; set; }
        public ReceiptVM Item { get; set; }
        private ReceiptDataModel dm = new ReceiptDataModel();
        private List<DropListVM> empList;
        private List<DropListVM> partyList;
        private List<DropListVM> accountList;

        public async System.Threading.Tasks.Task<List<DropListVM>> LoadAccountList()
        {
            BankAccountDataModel eDM = new BankAccountDataModel();
            if (accountList == null || accountList.Count <= 0)
                accountList = await eDM.GetBankAccountListAsync();
            return accountList;
        }

        public async System.Threading.Tasks.Task<List<DropListVM>> LoadEmployeeList()
        {
            EmployeeDataModel eDM = new EmployeeDataModel();
            if (empList == null || empList.Count <= 0)
                empList = await eDM.GetEmployeeListAsync(ApplicationContext.StoreId);
            return empList;
        }
        public async System.Threading.Tasks.Task<List<DropListVM>> LoadPartyList()
        {
            PartyDataModel eDM = new PartyDataModel();
            if (partyList == null || partyList.Count <= 0)
                partyList = await eDM.GetPartyListAsync();
            return partyList;
        }

        public ReceiptViewModel()
        {
            ItemList = new ObservableCollection<Receipt>();
            this.Item = new ReceiptVM
            {
                OnDate = DateTime.Today.Date,
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
                dm = new ReceiptDataModel();// (ApplicationContext.EmpId, ApplicationContext.Role);
                List<Receipt> Data = await dm.GetItemsAsync(ApplicationContext.StoreId, true);

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

        public async void SaveReceipt(Receipt payment, bool isNew=true)
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