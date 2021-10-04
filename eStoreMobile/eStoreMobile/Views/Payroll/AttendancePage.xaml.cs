using eStore.Shared.Models.Payroll;
using eStoreMobileX.Contants;
using eStoreMobile.Core.DataViewModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace eStoreMobile.Views
{
    [XamlCompilation (XamlCompilationOptions.Compile)]
    public partial class AttendancePage : ContentPage
    {
        public AttendancePage()
        {
            InitializeComponent ();
        }

        private void AddButton_Clicked(System.Object sender, System.EventArgs e)
        {
        }

        private void EmployeeButton_Clicked(System.Object sender, System.EventArgs e)
        {
        }
    }

    public class AttendanceRepository
    {
        private int StoreId = 1;
        private ObservableCollection<Attendance> attendances;

        public ObservableCollection<Attendance> AttendanceList
        {
            get { return attendances; }
            set { this.attendances = value; }
        }

        public AttendanceRepository()
        {
            attendances = new ObservableCollection<Attendance> ();
            this.LoadData ();
        }

        private async void LoadData()
        {
            AttendanceDataModel dm = new AttendanceDataModel (ApplicationContext.EmpId, ApplicationContext.Role);
            List<Attendance> Data = await dm.GetAttendances (StoreId);
            if(Data==null || Data.Count<=0)
                Data = await dm.GetAttendances (StoreId,false);
            attendances.Clear ();
            foreach ( var item in Data )
            {
                attendances.Add (item);
            }
        }

        public void ItemsSourceRefresh()
        {
            LoadData ();
        }
    }
}