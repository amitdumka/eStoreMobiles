using eStore.Shared.Models.Payroll;
using eStoreMobile.Core.DataViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace eStoreMobileX.ViewModel.Payroll
{
    /// <summary>
    /// Attendance View Model
    /// </summary>
    public class AttendanceViewModel
    {
        private int StoreId = 1;
        private ObservableCollection<Attendance> attendances;

        public ObservableCollection<Attendance> AttendanceList
        {
            get { return attendances; }
            set { this.attendances = value; }
        }

        public AttendanceViewModel()
        {
            attendances = new ObservableCollection<Attendance>();
            this.LoadData();
        }

        private async void LoadData()
        {
            try
            {

                AttendanceDataModel dm = new AttendanceDataModel(ApplicationContext.EmpId, ApplicationContext.Role);
                List<Attendance> Data = await dm.GetAttendances(StoreId, false);
                attendances.Clear();
                foreach (var item in Data)
                {
                    attendances.Add(item);
                }
            }
            catch (Exception e)
            {

                await App.Current.MainPage.DisplayAlert("Exception", "Error: " + e.Message, "Ok");
            }
        }
         /// <summary>
         /// Refresh Data store.
         /// </summary>
        public void ItemsSourceRefresh()
        {
            LoadData();
        }
    }
}
