using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using eStore.Shared.Models.Payroll;
using eStoreMobile.Core.DataViewModel;

namespace eStoreMobileX.ViewModel.Payroll
{
    public class EmployeeViewModel
    {
        private int StoreId = 1;
        private ObservableCollection<Employee> employees;
        private Employee employee;
        //private EmployeeVM employeeVM { get; set; }
        EmployeeDataModel dm;
        public ObservableCollection<Employee> EmployeeList
        {
            get { return employees; }
            set { this.employees = value; }
        }
        public Employee Employee
        {
            get { return employee; }
            set { this.employee = value; }
        }

        public EmployeeViewModel()
        {
       
            employees= new ObservableCollection<Employee>();
            this.employee = new Employee();
            this.employee.JoiningDate = DateTime.Today.Date;
            this.employee.DateOfBirth = DateTime.Today.AddYears(-18).Date;
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
                    employees.Clear();
                    foreach (var item in Data)
                    {
                        employees.Add(item);
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
