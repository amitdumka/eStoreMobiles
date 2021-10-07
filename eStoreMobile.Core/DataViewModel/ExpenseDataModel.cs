using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eStore.Shared.Models.Accounts;
using eStoreMobile.Core.Database;

namespace eStoreMobile.Core.DataViewModel
{
    public class ExpenseDataModel
    {
        public List<Expense> Expenses { get; set; }
        private RestApi.RestService<Expense> service;
        private eStoreDbContext _context;


        public ExpenseDataModel()
        {
            service = new RestApi.RestService<Expense>(Constants.ExpenseUrl, "Expense");
        }

        public async Task<List<Expense>> GetItemsAsync(int storeid, bool local = false)
        {
            return (await service.RefreshDataAsync()).Where(c => c.StoreId == storeid).ToList();
        }
        public async Task<List<Expense>> FindAsync(int storeid, string query, bool local = false)
        {
            return (await service.FindAsync(query)).Where(c => c.StoreId == storeid).ToList();
        }

        public async Task<Expense> GetByIdAsync(int id)
        {
            return await service.GetByIdAsync(id);
        }

        public async Task<bool> SaveAsync(Expense item, bool isNew = true, bool local = false)
        {
            return await service.SaveAsync(item, isNew);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await service.DeleteAsync(id);
        }


    }

}
