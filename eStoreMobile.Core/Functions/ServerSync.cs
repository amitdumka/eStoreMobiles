using eStoreMobile.Core.RestApi;
using System;
using System.Collections.Generic;
using System.Text;
using eStore.Shared.Models;
using eStore.Shared.Models.Payroll;
using eStore.Shared.Models.Common;
using eStore.Shared.Models.Banking;
using eStore.Shared.Models.Stores;
using eStoreMobile.Core.Database;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace eStoreMobile.Core.Functions
{
    public class SyncInfo
    {
        public int Employees { get; set; }
        public int Stores { get; set; }
        public int Users { get; set; }
        public int Bank { get; set; }
        public int Salesman { get; set; }
        public int TranscationMode { get; set; }
        public int TaxName { get; set; }
        public int ProductItems { get; set; }
        public int Category { get; set; }
        public int Supplies { get; set; }
        public int EletricityBill { get; set; }
        public int ElectricityConnection { get; set; }


    }
    public static class ServerSync
    {
        public static async void SyncWithServer(int StoreId)
        {
            RestSingleService service = new RestSingleService("", "SyncServer");
            var info = await service.GetByUrl<List<SyncInfo>>(Constants.SyncServerUrl + $"/{StoreId}");
            //ALTER TABLE `table` AUTO_INCREMENT = number;

        }

      
        public static async System.Threading.Tasks.Task<bool> InitSync(int StoreId)
        {
            RestSingleService service = new RestSingleService("", "SyncServer");
            var Stores = await service.GetByUrl<List<Store>>(Constants.StoreUrl);
            var Employees = await service.GetByUrl<List<Employee>>(Constants.EmployeeUrl);
            var Bank = await service.GetByUrl<List<Bank>>(Constants.BankUrl);
            var Salesman = await service.GetByUrl<List<Salesman>>(Constants.SalesmanUrl);
            var TranscationMode = await service.GetByUrl<List<TranscationMode>>(Constants.TranscationModeUrl);

            using (var db = new eStoreDbContext())
            {
                int record = 0, servercount=0;
                try
                {

                    //UPDATE SQLITE_SEQUENCE SET SEQ=0 WHERE NAME='table_name';
                    _ = await db.Database.ExecuteSqlCommandAsync($"delete from Stores ;UPDATE SQLITE_SEQUENCE SET SEQ=0 WHERE NAME='Stores';");
                    _ = await db.Database.ExecuteSqlCommandAsync($"delete from Banks ; UPDATE SQLITE_SEQUENCE SET SEQ=0 WHERE NAME='Banks';");
                    _ = await db.Database.ExecuteSqlCommandAsync($"delete from Employees ; UPDATE SQLITE_SEQUENCE SET SEQ=0 WHERE NAME='Employees';");
                    _ = await db.Database.ExecuteSqlCommandAsync($"delete from Salesmen ; UPDATE SQLITE_SEQUENCE SET SEQ=0 WHERE NAME='Salesmen';");
                    _ = await db.Database.ExecuteSqlCommandAsync($"delete from TranscationModes ; UPDATE SQLITE_SEQUENCE SET SEQ=0 WHERE NAME='TranscationModes';");
                    _ = await db.Database.ExecuteSqlCommandAsync($"VACUUM");

                    //run VACUUM Command after running from here clear and clean up database.
                    await db.Stores.AddRangeAsync(Stores);
                    record = await db.SaveChangesAsync();

                    await db.Employees.AddRangeAsync(Employees);
                    record += await db.SaveChangesAsync();
                    await db.Banks.AddRangeAsync(Bank);
                    record += await db.SaveChangesAsync();
                    await db.Salesmen.AddRangeAsync(Salesman);
                    record += await db.SaveChangesAsync();
                    await db.TranscationModes.AddRangeAsync(TranscationMode);
                    record += await db.SaveChangesAsync();

                     servercount = Stores.Count + Employees.Count + Bank.Count + Salesman.Count + TranscationMode.Count;

                    if (record != servercount) return false;
                    else return true;
                }
               catch(Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    return false;
                }

            }



        }

    }
}
