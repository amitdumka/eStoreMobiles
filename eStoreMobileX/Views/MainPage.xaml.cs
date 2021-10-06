using eStoreMobileX.Views.Payroll;
using Syncfusion.SfCalendar.XForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace eStoreMobileX.Views
{
    public class CalendarViewModel
    {
        public CalendarEventCollection CalendarInlineEvents { get; set; } = new CalendarEventCollection();
        public CalendarViewModel()
        {
            CalendarInlineEvent event1 = new CalendarInlineEvent();
            event1.StartTime = DateTime.Now;
            event1.EndTime = DateTime.Now.AddHours(2);
            event1.Subject = "Sale Report Pending";
            event1.Color = Color.Fuchsia;

            CalendarInlineEvent event2 = new CalendarInlineEvent();
            event2.StartTime = DateTime.Now.AddHours(1);
            event2.EndTime = DateTime.Now.AddHours(3);
            event2.Subject = "Attendance Report Pending";
            event2.Color = Color.Green;

            CalendarInlineEvents.Add(event1);
            CalendarInlineEvents.Add(event2);
        }
    }
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            hamburgerButton.Image = (FileImageSource)ImageSource.FromFile("lambda.png");
            UserName.Text = ApplicationContext.UserName;
            List<string> list = new List<string>();
            list.Add("Home");
            list.Add("Attendance");
            list.Add("Employee");
            list.Add("Store");
            list.Add("Stock");
            list.Add("Settings");
            listView.ItemsSource = list;
            this.Title =  ApplicationContext.StoreName;
        }

        void hamburgerButton_Clicked(object sender, EventArgs e)
        {
            navigationDrawer.ToggleDrawer();
        }

        //private void listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        //{
        //    // Your codes here
        //    navigationDrawer.ToggleDrawer();
        //}
        private void listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            headerLabel.Text = e.SelectedItem.ToString();
              
            //if (e.SelectedItem.ToString() == "Home")
            //    headerLabel.Text = "Home";
            //else if (e.SelectedItem.ToString() == "Attendance")
            //    navigationDrawer.ContentView = new AttendancePage().Content;
            //else if (e.SelectedItem.ToString() == "Settings")
            //    navigationDrawer.ContentView = new SettingsPage().Content;
            //else if (e.SelectedItem.ToString() == "Employee")
            //    navigationDrawer.ContentView = new EmployeesPage().Content;
            //else if (e.SelectedItem.ToString() == "Store")
            //    navigationDrawer.ContentView = new StoresPage().Content;
            //else if (e.SelectedItem.ToString() == "Stock")
            //    navigationDrawer.ContentView = new StockListPage().Content;
            navigationDrawer.ToggleDrawer();
        }
    }
}
