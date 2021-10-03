using eStoreMobile.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace eStoreMobileX.ViewModel
{
    public class StockListViewModel
    {
        public StockList StockList { get; set; }
        public StockListViewModel()
        {
            StockList = new StockList ();
            StockList.LastAccess = DateTime.Now.Date;
        }
    }
}
