using System;
using eStoreMobile.Core.DataViewModel;

namespace eStoreMobileX.Features
{
    public class SyncUpService
    {
        public static async System.Threading.Tasks.Task<bool> SyncWithServer()
        {
            bool flag = false;
            StoreDataModel store = new StoreDataModel();
            flag= await store.Sync();
            
            EmployeeDataModel employee = new EmployeeDataModel();
            flag=await employee.Sync(); 
            BankDataModel bank = new BankDataModel();
            flag=await bank.SyncAsync(); 
            BankAccountDataModel account = new BankAccountDataModel();
            flag = await account.SyncAsync();
            PartyDataModel party = new PartyDataModel(); ;
            flag = await party.SyncAsync(); 
            LedgerMasterDataModel master = new LedgerMasterDataModel();
            flag = await master.SyncAsync();
            LedgerTypeDataModel ledger = new LedgerTypeDataModel();
            flag = await ledger.SyncAsync();
            TranscationModeDataModel mode = new TranscationModeDataModel();
            flag = await mode.Sync();

            return flag;
        }

    }
}
