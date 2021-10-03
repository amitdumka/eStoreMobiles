using System;
using System.ComponentModel.DataAnnotations;
using eStore.Shared.Models.Payroll;
using Syncfusion.XForms.DataForm;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace eStoreMobile.Views
{
    [XamlCompilation (XamlCompilationOptions.Compile)]
    public partial class AddAttendanceView : ContentPage
    {
        public AddAttendanceView()
        {
            InitializeComponent ();
        }
        private void dataForm_AutoGeneratingDataFormItem(object sender, Syncfusion.XForms.DataForm.AutoGeneratingDataFormItemEventArgs e)
        {
            // Attendance at; at.

            //if (e.DataFormItem.Name == "IsTailoring")
            //    e.DataFormItem.Editor = "Switch";
            if ( e.DataFormItem.Name.Equals ("IsTailoring") )
            {
                e.DataFormItem.LayoutOptions = LayoutType.Default;
                ( e.DataFormItem as DataFormCheckBoxItem ).IsThreeState = false;
                ( e.DataFormItem as DataFormCheckBoxItem ).Text = "Tailor";
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
        //private async void AddButton_Clicked(object sender, EventArgs e)
        //{
        //    if ( await DisplayAlert ("Alert", "Do you want to save!", "Yes", "No") )
        //    {
        //        //Go Back
        //    }
        //}

        //private async void CancelButton_Clicked(object sender, EventArgs e)
        //{
        //    if ( await DisplayAlert ("Alert", "Are your sure to go back!", "Yes", "No") )
        //    {
        //        //Go Back
        //    }

        //}

        //private void isTailoringSwitch_StateChanged(object sender, Syncfusion.XForms.Buttons.SwitchStateChangedEventArgs e)
        //{

        //}
    }
    public class AttendanceViewModel
    {
        private AttendanceVM attendance { get; set; }

        public AttendanceVM Attendance
        {
            get { return this.attendance; }
            set { this.attendance = value; }
        }

        public AttendanceViewModel()
        {
            this.attendance = new AttendanceVM();
            this.attendance.AttDate = DateTime.Today.Date;
        }
    }
    public class AttendanceVM
    {
        public int AttendanceId { get; set; }

        [Display (Name = "Staff Name")]
        public int EmployeeId { get; set; }

        [DataType (DataType.Date), DisplayFormat (DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display (Name = "Attendance Date")]
        public DateTime AttDate { get; set; }

        [Display (Name = "Entry Time")]
        public string EntryTime { get; set; }

        public AttUnit Status { get; set; }
        public string Remarks { get; set; }

        [ DisplayOptions (ShowLabel = false)]

        public bool IsTailoring { get; set; }
        public int StoreId { get; set; }
    }
}