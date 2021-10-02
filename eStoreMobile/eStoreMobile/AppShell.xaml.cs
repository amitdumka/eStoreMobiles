using eStore.Shared.Models.Payroll;
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
            Routing.RegisterRoute (nameof (UserEditor), typeof (UserEditor));
            Routing.RegisterRoute (nameof (AttendancePage), typeof (AttendancePage));
            Routing.RegisterRoute (nameof (AddAttendanceView), typeof (AddAttendanceView));

        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync ("//LoginPage");
        }
    }
}
