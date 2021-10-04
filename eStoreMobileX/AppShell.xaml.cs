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
            //Remove when Auth is implemented.
            ApplicationContext.EmpId = 3;
            ApplicationContext.IsLoggedIn = true;
            ApplicationContext.Role = EmpType.Owner;
            ApplicationContext.StoreId = 1;
            ApplicationContext.StoreName = "Aprajita Retails, Dumka";
            ApplicationContext.UserName = "Admin"; 
            

        }
        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync ("//LoginPage");
        }
    }
}