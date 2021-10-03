using eStore.Shared.Models.Payroll;
using eStore.Shared.Models.Stores;
using eStore.Shared.Models.Tailoring;
using eStore.Shared.Models.Users;
using eStoreMobile.Core.DataViewModel;
using eStoreMobile.Core.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace eStoreMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AttendancePage : ContentPage
    {
        public AttendancePage()
        {
            InitializeComponent();
        }



        void AddButton_Clicked(System.Object sender, System.EventArgs e)
        {

        }

        void EmployeeButton_Clicked(System.Object sender, System.EventArgs e)
        {
        }
    }

    public class AttendanceRepository{
        int StoreId =1;
        private ObservableCollection<Attendance> attendances;

        public ObservableCollection<Attendance> StockListCollection
        {
            get { return attendances; }
            set { this.attendances = value; }
        }

        public AttendanceRepository()
        {
            attendances = new ObservableCollection<Attendance>();
            this.LoadData();
        }

        private async void LoadData()
        {
            AttendanceDataModel dm = new AttendanceDataModel();

            List<Attendance> Data = await dm.GetAttendances(StoreId);

            attendances.Clear();
            foreach (var item in Data)
            {
                attendances.Add(item);
            }

        }
        public void ItemsSourceRefresh()
        {
            LoadData();
        }
    }
    
}