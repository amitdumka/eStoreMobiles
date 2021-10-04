using eStoreMobile.Core.DataViewModel;
using eStoreMobile.Core.Models;
using Syncfusion.XForms.DataForm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace eStoreMobileX.ViewModel
{
    public class StockListViewModel
    {
        public StockList StockList { get; set; }
        private ObservableCollection<StockList> stockLists;
        public ObservableCollection<StockList> StockListCollection
        {
            get { return stockLists; }
            set { this.stockLists = value; }
        }
        public StockListViewModel()
        {
            StockList = new StockList ();
            StockList.LastAccess = DateTime.Now.Date;
            stockLists = new ObservableCollection<StockList>();
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
        private void DataForm_AutoGeneratingDataFormItem(object sender, AutoGeneratingDataFormItemEventArgs e)
        {
            if (e.DataFormItem.Name == "Count")
                e.Cancel = true;
            else if (e.DataFormItem.Name == "LastAccess")
                e.DataFormItem.IsReadOnly = true;
        }
    }
    

}
