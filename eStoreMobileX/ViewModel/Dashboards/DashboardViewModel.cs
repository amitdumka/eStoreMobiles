using eStoreMobile.Core.DataViewModel;
using eStoreMobile.Core.Models;

namespace eStoreMobileX.ViewModel.Dashboards
{
    public class DashboardViewModel
    {
        private DashboardDataModel dm;
        public MasterViewReport masterReport;
        //SaleReport
        //EmpInfoList
        // AccountsInfo
        //LeadingSalesman
        //TailoringReport

        public DashboardViewModel()
        {
            dm = new DashboardDataModel();
            Load();
        }

        public async void Load()
        {
            masterReport = await dm.GetReportAsync(ApplicationContext.StoreId);
        }

        public DailySaleReport SaleReport()
        { 
            return masterReport.SaleReport;
        }
        public AccountsInfo AccountInfo()
        {
            return masterReport.AccountsInfo;
        }

        public System.Collections.Generic.List<EmployeeInfo> EmployeeInfo()
        {
            return masterReport.EmpInfoList;
        }
        
    }
}