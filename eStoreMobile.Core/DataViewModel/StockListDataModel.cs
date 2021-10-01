using eStoreMobile.Core.Database;
using System;
using System.Collections.Generic;
using System.Text;
using eStore.Shared.Models.Payroll;
using eStoreMobile.Core.Database;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using eStoreMobile.Core.Models;

namespace eStoreMobile.Core.DataViewModel
{
    public  class StockListDataModel
    {
        private eStoreDbContext _context;
        public StockListDataModel() { }

        public async Task<List<StockList>> RefreshDataAsync()
        {
            using(_context= new eStoreDbContext () )
            {
               return await _context.StockLists.ToListAsync ();
            }
        }

        public async Task<int> SaveAsync(StockList item, bool isNew)
        {
            if ( isNew )
                await _context.StockLists.AddAsync (item);
            else
                _context.StockLists.Update (item);
            return await _context.SaveChangesAsync ();
        }
        public int Delete(int id)
        {
            using ( _context = new eStoreDbContext () )
            {
                var stk = _context.StockLists.Find (id);
                if ( stk != null )
                {
                    _context.StockLists.Remove (stk);
                }
                return _context.SaveChanges ();

            }
        }

        public bool IsExists(int id)
        {
            if ( _context.StockLists.Find (id) != null )
                return true;
            return false;
        }

        public async Task<StockList> FindBarCodeAsync(string barcode)
        {
            using(_context=new eStoreDbContext () )
            {
                return await _context.StockLists.Where (c => c.Barcode == barcode).FirstOrDefaultAsync ();
            }
        }

    }
}
