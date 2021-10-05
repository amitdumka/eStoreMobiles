using eStoreMobile.Core.RestApi;
using System;
using System.Collections.Generic;
using System.Text;

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
            var info = await service.GetByUrl<SyncInfo>(Constants.SyncServerUrl+$"/{StoreId}");
            //ALTER TABLE `table` AUTO_INCREMENT = number;

        }

    }
}
