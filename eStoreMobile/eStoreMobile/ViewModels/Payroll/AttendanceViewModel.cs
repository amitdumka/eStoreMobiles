using eStore.Shared.Models.Payroll;

namespace eStoreMobile.ViewModels.Payroll
{
    public class AttendanceViewModel
    {
        private Attendance Attendance { get; set; }

        public AttendanceViewModel()
        {
            this.Attendance = new Attendance ();
        }
    }
}