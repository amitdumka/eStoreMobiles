using System;
using System.Threading.Tasks;
using eStoreMobile.Core.Models;

namespace eStoreMobile.Core.DataViewModel
{
    public class DashboardDataModel
    {
        public MasterViewReport MasterReport { get; set; }
        
        private RestApi.RestService<MasterViewReport> service;
        public DashboardDataModel()
        {
            service = new RestApi.RestService<MasterViewReport>(Constants.MasterViewUrl, "MasterView");
        }

        public async Task<MasterViewReport> GetReport(int storeid, bool local = false)
        {
            MasterReport = (await service.RefreshDataAsync())[0];
            return MasterReport;
            
        }

    }
    
}