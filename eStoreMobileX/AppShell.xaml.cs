using eStoreMobileX.Views;
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

        }
        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync ("//LoginPage");
        }
    }
}