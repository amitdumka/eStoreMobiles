using eStore.Shared.Models.Payroll;
using eStore.Shared.Models.Stores;
using eStore.Shared.Models.Tailoring;
using eStore.Shared.Models.Users;
using eStoreMobile.Core.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace eStoreMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AttendancePage : ContentPage
    {
        public AttendancePage()
        {
            InitializeComponent();
        }



        void AddButton_Clicked(System.Object sender, System.EventArgs e)
        {
        }

        void EmployeeButton_Clicked(System.Object sender, System.EventArgs e)
        {
        }
    }

    public class AttendanceRepository{
        private ObservableCollection<Attendance> stockLists;

        public ObservableCollection<Attendance> StockListCollection
        {
            get { return stockLists; }
            set { this.stockLists = value; }
        }

        public AttendanceRepository()
        {
            stockLists = new ObservableCollection<Attendance>();
            this.LoadData();
        }

        private async void LoadData()
        {
            StockListDataModel dm = new StockListDataModel();

            List<StockList> Data = await dm.RefreshDataAsync();
            stockLists.Clear();
            foreach (var item in Data)
            {
                stockLists.Add(item);
            }

        }
        public void ItemsSourceRefresh()
        {
            LoadData();
        }
    }
    
}