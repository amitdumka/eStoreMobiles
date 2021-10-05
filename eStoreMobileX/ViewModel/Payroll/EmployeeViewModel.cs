using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using eStore.Shared.Models.Payroll;
using eStoreMobile.Core.DataViewModel;

namespace eStoreMobileX.ViewModel.Payroll
{
    public class EmployeeViewModel
    {
        private readonly int StoreId = 1;

        //private EmployeeVM employeeVM { get; set; }
        EmployeeDataModel dm;
        public ObservableCollection<Employee> EmployeeList { get; set; }
        public Employee Employee { get; set; }

        public EmployeeViewModel()
        {
       
            EmployeeList= new ObservableCollection<Employee>();
            this.Employee = new Employee
            {
                JoiningDate = DateTime.Today.Date,
                DateOfBirth = DateTime.Today.AddYears(-18).Date
            };
            //Enable if required.
            //this.attendance = new Attendance ();
            this.LoadData();
        }
        private async void LoadData()
        {
            try
            {
                dm = new EmployeeDataModel();// (ApplicationContext.EmpId, ApplicationContext.Role);
                List<Employee> Data = await dm.GetEmployees(StoreId, true);
                if (Data == null ||Data.Count<=0) Data = await dm.GetEmployees(StoreId, false);
                if (Data != null)
                {
                    EmployeeList.Clear();
                    foreach (var item in Data)
                    {
                        EmployeeList.Add(item);
                    }
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

        public async void SyncUp()
        {
            await dm.Sync();
        }
    }
}
