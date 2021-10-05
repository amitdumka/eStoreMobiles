using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace eStoreMobileX.Views.Payroll
{
    public partial class AttendancePage : ContentPage
    {
        public AttendancePage()
        {
            InitializeComponent();
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {

        }
        async void pullToRefresh_Refreshing(System.Object sender, System.EventArgs e)
        {
            pullToRefresh.IsRefreshing = true;
            await Task.Delay(1200);
            this.viewModel.ItemsSourceRefresh();
            pullToRefresh.IsRefreshing = false;
        }

        private  async void AddAttendance_Clicked(object sender, EventArgs e)
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(AttendanceEditorPage));
            }
            catch (Exception ex)
            {

                await DisplayAlert("Alert", "Error: " + ex.Message, "Ok");
            }
        }

        private void Emp_Clicked(object sender, EventArgs e)
        {

        }

        private void AddEmp_Clicked(object sender, EventArgs e)
        {

        }

        private void SyncUp_Clicked(object sender, EventArgs e)
        {

        }

        private void AddButton_Clicked(object sender, EventArgs e)
        {

        }
    }
}
