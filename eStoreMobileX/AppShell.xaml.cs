using eStoreMobileX.Views;
using eStoreMobileX.Views.Payroll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace eStoreMobileX
{
    [XamlCompilation (XamlCompilationOptions.Compile)]
    public partial class AppShell : Shell
    {
       
        public AppShell()
        {
            InitializeComponent ();
            Routing.RegisterRoute (nameof (MainPage), typeof (MainPage));
            Routing.RegisterRoute (nameof (StockForm), typeof (StockForm));
            Routing.RegisterRoute(nameof(StockListPage), typeof(StockListPage));
            Routing.RegisterRoute(nameof(AttendancePage), typeof(AttendancePage));
            Routing.RegisterRoute(nameof(AttendanceEditorPage), typeof(AttendanceEditorPage));
            Routing.RegisterRoute(nameof(AboutUsPageWithCards), typeof(AboutUsPageWithCards));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(ResetPasswordPageWithGradient), typeof(ResetPasswordPageWithGradient));
            Routing.RegisterRoute(nameof(ContactUsPage), typeof(ContactUsPage));
            Routing.RegisterRoute(nameof(EmployeeProfilePage), typeof(EmployeeProfilePage));
            Routing.RegisterRoute(nameof(DailyCaloriesReportPage), typeof(DailyCaloriesReportPage));
            Routing.RegisterRoute(nameof(DailyTimelinePage), typeof(DailyTimelinePage));
            Routing.RegisterRoute(nameof(HealthCarePage), typeof(HealthCarePage));

            Routing.RegisterRoute(nameof(MyWalletPage), typeof(MyWalletPage));
            Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));

            //Remove when Auth is implemented.
            ApplicationContext.EmpId = 4;
            ApplicationContext.IsLoggedIn = true;
            ApplicationContext.Role = EmpType.Owner;
            ApplicationContext.StoreId = 1;
            ApplicationContext.StoreName = "Aprajita Retails, Dumka";
            ApplicationContext.UserName = "Admin"; 
            

        }
        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
             App.Current.MainPage = new LoginPage();
            
        }
    }
}