using eStoreMobileX.Features;
using eStoreMobileX.Views;
using eStoreMobileX.Views.Invoices;
using eStoreMobileX.Views.Payroll;
using eStoreMobileX.Views.Vochers;
using eStoreMobileX.Views.Vochers.Editors;
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
            Routing.RegisterRoute(nameof(StoresPage), typeof(StoresPage));
            Routing.RegisterRoute(nameof(EmployeesPage), typeof(EmployeesPage));

            Routing.RegisterRoute(nameof(ExpensePage), typeof(ExpensePage));
            Routing.RegisterRoute(nameof(ExpenseEditorPage), typeof(ExpenseEditorPage));
            Routing.RegisterRoute(nameof(PaymentPage), typeof(PaymentPage));
            Routing.RegisterRoute(nameof(PaymentEditorPage), typeof(PaymentEditorPage));
            Routing.RegisterRoute(nameof(CashPaymentPage), typeof(CashPaymentPage));
            Routing.RegisterRoute(nameof(CashPaymentEditorPage), typeof(CashPaymentEditorPage));
            Routing.RegisterRoute(nameof(ReceiptPage), typeof(ReceiptPage));
            Routing.RegisterRoute(nameof(ReceiptEditorPage), typeof(ReceiptEditorPage));
            Routing.RegisterRoute(nameof(CashReceiptPage), typeof(CashReceiptPage));
            Routing.RegisterRoute(nameof(CashReceiptEditorPage), typeof(CashReceiptEditorPage));
            Routing.RegisterRoute(nameof(InvoicePage), typeof(InvoicePage));
            Routing.RegisterRoute(nameof(InvoiceEditorPage), typeof(InvoiceEditorPage));
            Routing.RegisterRoute(nameof(InvoiceReturnEditorPage), typeof(InvoiceReturnEditorPage));

            //Routing.RegisterRoute(nameof(ResetPasswordPageWithGradient), typeof(ResetPasswordPageWithGradient));
            //Routing.RegisterRoute(nameof(ContactUsPage), typeof(ContactUsPage));
            //Routing.RegisterRoute(nameof(EmployeeProfilePage), typeof(EmployeeProfilePage));
            //Routing.RegisterRoute(nameof(DailyCaloriesReportPage), typeof(DailyCaloriesReportPage));
            //Routing.RegisterRoute(nameof(DailyTimelinePage), typeof(DailyTimelinePage));
            //Routing.RegisterRoute(nameof(HealthCarePage), typeof(HealthCarePage));

            //Routing.RegisterRoute(nameof(MyWalletPage), typeof(MyWalletPage));
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

        private async void OnSyncMenuClicked(System.Object sender, System.EventArgs e)
        {
            await DisplayAlert("Alert", "Please Wait while eStore get Sync With remote server!", "OK").ConfigureAwait(false);
            if ((await SyncUpService.SyncWithServer()))
            {
                await DisplayAlert("Alert", "eStore is fully sync with server!!", "OK").ConfigureAwait(false);
            }
            else {
                await DisplayAlert("Error", "eStore is not sync with server!! Try again in some time!!", "OK").ConfigureAwait(false);
            }
        }
    }
}