using eStore.Shared.Models.Payroll;
using eStoreMobile.Core.DataViewModel;
using eStoreMobile.Core.Models.Dtos;
using eStoreMobileX.ViewModel.Payroll;
using Syncfusion.XForms.DataForm;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace eStoreMobileX.Views.Payroll
{
    public partial class AttendanceEditorPage : ContentPage
    {
        private readonly List<DropListVM> empList;
        public AttendanceEditorPage()
        {
            InitializeComponent();
            _ = viewModel.LoadEmployeeList();
        }

        private async Task<List<DropListVM>> GetEmpList()
        {
            if (empList != null && empList.Count > 0) return empList;
            else return await viewModel.LoadEmployeeList();
        }

        private async void SaveAttendance_Clicked(System.Object sender, System.EventArgs e)
        {
            try
            {
                dataForm.Commit();

                var data = dataForm.DataObject as AttendanceVM;
                if (data != null)
                {
                    bool flag = true;
                    if (data.AttDate == null) flag = false;
                    //if (data.EmployeeId <= 0) flag = false;
                    if (string.IsNullOrEmpty(data.EntryTime)) flag = false;
                    if (flag)
                    {
                        Attendance att = new Attendance
                        {
                            Status = data.Status,
                            EmployeeId = ApplicationContext.EmpId,
                            EntryStatus = EntryStatus.Rejected,
                            AttDate = data.AttDate,
                            EntryTime = data.EntryTime,
                            IsReadOnly = false,
                            IsTailoring = data.IsTailoring,
                            StoreId = ApplicationContext.StoreId,
                            Remarks = data.Remarks,
                            UserId = "X_" + ApplicationContext.UserName 
                        };
                        AttendanceDataModel dm = new AttendanceDataModel(ApplicationContext.EmpId, ApplicationContext.Role);
                        int status=await dm.SaveAttendance(att, true);
                        if (status > 0)
                        {
                            
                            await DisplayAlert("Info", "Attendance is saved!.", "Ok"); 
                        }

                        else
                            await DisplayAlert("Error", "An error occured while saving, Kindly try again!.", "Ok");
                    }
                    else
                    {
                        await DisplayAlert("Error", "Kindly verify Attendance, before adding...", "Ok");
                    }
                    dataForm.DataObject = new Attendance();
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Exceptions", "Error: " + ex.Message, "Ok");
            }
        }

        private async void dataForm_AutoGeneratingDataFormItem(object sender, Syncfusion.XForms.DataForm.AutoGeneratingDataFormItemEventArgs e)
        {
            // Attendance at; at.

            //if (e.DataFormItem.Name == "IsTailoring")
            //    e.DataFormItem.Editor = "Switch";
            if (e.DataFormItem.Name.Equals("IsTailoring"))
            {
                
                e.DataFormItem.LayoutOptions = LayoutType.Default;
                (e.DataFormItem as DataFormCheckBoxItem).IsThreeState = false;
                (e.DataFormItem as DataFormCheckBoxItem).Text = "Tailor";
                
            }

            if (e.DataFormItem.Name == "IsReadOnly")
                e.Cancel = true;
            if (e.DataFormItem.Name == "AttendanceId")
                e.Cancel = true;
            if (e.DataFormItem.Name == "EmployeeId")
            {
                e.DataFormItem = new DataFormDropDownItem()
                {
                    Name = "EmployeeId",
                    Editor = "DropDown",
                    LabelText="Employee"  ,
                    ItemsSource = (await GetEmpList()),
                    PlaceHolderText = "Select a Employee"
                };
                (e.DataFormItem as DataFormDropDownItem).DisplayMemberPath = nameof(DropListVM.Label);
                (e.DataFormItem as DataFormDropDownItem).SelectedValuePath = nameof(DropListVM.Value);
            }
            if (e.DataFormItem.Name == "Employee")
                e.Cancel = true;
            if (e.DataFormItem.Name == "Store")
                e.Cancel = true;
            if (e.DataFormItem.Name == "StoreId")
                e.Cancel = true;
            if (e.DataFormItem.Name == "UserId")
                e.Cancel = true;
        }
    }
}