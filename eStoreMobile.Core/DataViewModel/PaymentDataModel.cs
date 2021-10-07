using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eStore.Shared.Models.Accounts;
using eStoreMobile.Core.Database;

namespace eStoreMobile.Core.DataViewModel
{
    public class PaymentDataModel
    {
        public List<Payment> Payments { get; set; }
        private RestApi.RestService<Payment> service;
        private eStoreDbContext _context;


        public PaymentDataModel()
        {
            service = new RestApi.RestService<Payment>(Constants.EmployeeUrl, "Payment");
        }

        public async Task<List<Payment>> GetItemsAsync(int storeid, bool local = false)
        {
            return (await service.RefreshDataAsync()).Where(c=>c.StoreId==storeid).ToList();
        }
        public async Task<List<Payment>> FindAsync(int storeid, string query,bool local = false)
        {
            return (await service.FindAsync(query)).Where(c => c.StoreId == storeid).ToList();
        }

        public async Task<Payment> GetByIdAsync(int id)
        {
            return await service.GetByIdAsync(id);
        }

        public async Task<bool> SaveAsync(Payment item, bool isNew = true, bool local = false)
        {
            return await service.SaveAsync(item, isNew);
        }

        public async Task<bool> DeleteAsync(int id)
        {
          return await   service.DeleteAsync(id);
        }

        
    }

    public interface IDataModel<T>
    {
        public Task<List<T>> GetItems(int storeid, bool local = false);
        public Task<bool> Sync();
        public Task<T> GetById(int id);
        public Task<bool>  Save(T item, bool isNew = true, bool local = false);
        public Task<bool> Delete(int id);
        public Task<bool> IsExists(int id);    
    }
}
