﻿using eStore.Shared.Models.Payroll;
using eStoreMobile.Core.DataViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;
using System.ComponentModel.DataAnnotations;
using eStore.Shared.Models.Payroll;
using Syncfusion.XForms.DataForm;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace eStoreMobileX.ViewModel.Payroll
{
    /// <summary>
    /// Attendance View Model
    /// </summary>
    public class AttendanceViewModel
    {
        private int StoreId = 1;
        private ObservableCollection<Attendance> attendances;
        private Attendance attendance;
        private AttendanceVM attendanceVM { get; set; }

        public AttendanceVM Attendance
        {
            get { return this.attendanceVM; }
            set { this.attendanceVM = value; }
        }

        public ObservableCollection<Attendance> AttendanceList
        {
            get { return attendances; }
            set { this.attendances = value; }
        }

        public AttendanceViewModel()
        {
            attendances = new ObservableCollection<Attendance>();
            this.attendanceVM = new AttendanceVM();
            this.attendanceVM.AttDate = DateTime.Today.Date;
            //Enable if required.
            //this.attendance = new Attendance ();
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
    public class AttendanceVM
    {
        public int AttendanceId { get; set; }

        [Display(Name = "Staff Name")]
        public int EmployeeId { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Attendance Date")]
        public DateTime AttDate { get; set; }

        [Display(Name = "Entry Time")]
        public string EntryTime { get; set; }

        public AttUnit Status { get; set; }
        public string Remarks { get; set; }

        [DisplayOptions(ShowLabel = false)]

        public bool IsTailoring { get; set; }
        public int StoreId { get; set; }
    }

}
