using eStore.Shared.Models.Payroll;
using eStore.Shared.Models.Stores;
using eStore.Shared.Models.Tailoring;
using eStore.Shared.Models.Users;
using eStoreMobile.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private void dataForm_AutoGeneratingDataFormItem(object sender, Syncfusion.XForms.DataForm.AutoGeneratingDataFormItemEventArgs e)
        {
            // Attendance at; at.
            
            if ( e.DataFormItem.Name == "IsTailoring" )
                e.DataFormItem.Editor = "Switch";
            if ( e.DataFormItem.Name == "IsReadOnly" )
                e.Cancel=true;
            if ( e.DataFormItem.Name == "AttendanceId" )
                e.Cancel = true;
            if ( e.DataFormItem.Name == "EmployeeId" )
                e.Cancel = true;
            if ( e.DataFormItem.Name == "Employee" )
                e.Cancel = true;
            if ( e.DataFormItem.Name == "Store" )
                e.Cancel = true;
            if ( e.DataFormItem.Name == "StoreId" )
                e.Cancel = true;

            
            if ( e.DataFormItem.Name == "UserId" )
                e.Cancel = true;



        }
    }
    public class ViewModel
    {
        private Attendance attendance { get; set; }

        public Attendance Attendance
        {
            get { return this.attendance; }
            set { this.attendance = value; }
        }

        public ViewModel()
        {
            this.attendance = new Attendance ();
        }
    }
}