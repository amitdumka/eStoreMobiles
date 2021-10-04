using System;
using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;
using eStore.Shared.Models.Payroll;
using Syncfusion.XForms.DataForm;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Xamarin.Forms;

namespace eStoreMobileX.Views.Payroll
{
    public partial class AttendanceEditorPage : ContentPage
    {
        public AttendanceEditorPage()
        {
            InitializeComponent();
        }

        void SaveAttendance_Clicked(System.Object sender, System.EventArgs e)
        {
        }
        private void dataForm_AutoGeneratingDataFormItem(object sender, Syncfusion.XForms.DataForm.AutoGeneratingDataFormItemEventArgs e)
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
                e.Cancel = true;
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
