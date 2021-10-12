using System;
using System.Collections.Generic;
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

        public async Task<MasterViewReport> GetReportAsync(int storeid, bool local = false)
        {
            MasterReport = (await service.RefreshDataAsync())[0];
            return MasterReport;
        }

        public async Task<DailySaleReport> GetSaleReportAsync(int storeid, bool local = false)
        {
            return MasterReport.SaleReport = (await service.GetByUrl(Constants.MasterViewUrl + "/salereport")).SaleReport;
        }

        public async Task<TailoringReport> GetTailoringReportAsync(int storeid, bool local = false)
        {
            return MasterReport.TailoringReport = (await service.GetByUrl(Constants.MasterViewUrl + "/tailoringreport")).TailoringReport;
        }

        public async Task<AccountsInfo> GetAccountInfoAsync(int storeid, bool local = false)
        {
            return MasterReport.AccountsInfo = (await service.GetByUrl(Constants.MasterViewUrl + "/accountInfoReport")).AccountsInfo;
        }
        public async Task<List<EmployeeInfo>> GetEmployeeInfoReportAsync(int storeid, bool local = false)
        {
            return MasterReport.EmpInfoList = (await service.GetByUrl(Constants.MasterViewUrl + "/employeeInfoReport")).EmpInfoList;
        }
        public async Task<List<string>> GetLeadingSalesmanAsync(int storeid, bool local = false)
        {
            return MasterReport.LeadingSalesman = (await service.GetByUrl(Constants.MasterViewUrl + "/leadingSalesman")).LeadingSalesman;
        }



    }

}