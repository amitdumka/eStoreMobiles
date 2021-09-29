using eStoreMobile.ViewModels;
using eStoreMobile.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace eStoreMobile
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent ();
            //Routing.RegisterRoute (nameof (ItemDetailPage), typeof (ItemDetailPage));
            // Routing.RegisterRoute (nameof (NewItemPage), typeof (NewItemPage));
            Routing.RegisterRoute (nameof (TestPage1), typeof (TestPage1));

        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync ("//LoginPage");
        }
    }
}
