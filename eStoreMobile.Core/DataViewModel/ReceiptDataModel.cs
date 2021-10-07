using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eStore.Shared.Models.Accounts;
using eStoreMobile.Core.Database;

namespace eStoreMobile.Core.DataViewModel
{
    public class ReceiptDataModel
    {
        public List<Receipt> Receipts { get; set; }
        private RestApi.RestService<Receipt> service;
        private eStoreDbContext _context;


        public ReceiptDataModel()
        {
            service = new RestApi.RestService<Receipt>(Constants.ReceiptsUrl, "Receipt");
        }

        public async Task<List<Receipt>> GetItemsAsync(int storeid, bool local = false)
        {
            return (await service.RefreshDataAsync()).Where(c => c.StoreId == storeid).ToList();
        }
        public async Task<List<Receipt>> FindAsync(int storeid, string query, bool local = false)
        {
            return (await service.FindAsync(query)).Where(c => c.StoreId == storeid).ToList();
        }

        public async Task<Receipt> GetByIdAsync(int id)
        {
            return await service.GetByIdAsync(id);
        }

        public async Task<bool> SaveAsync(Receipt item, bool isNew = true, bool local = false)
        {
            return await service.SaveAsync(item, isNew);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await service.DeleteAsync(id);
        }


    }
    public class CashReceiptDataModel
    {
        public List<CashReceipt> Receipts { get; set; }
        private RestApi.RestService<CashReceipt> service;
        private eStoreDbContext _context;


        public CashReceiptDataModel()
        {
            service = new RestApi.RestService<CashReceipt>(Constants.CashReceiptsUrl, "Receipt");
        }

        public async Task<List<CashReceipt>> GetItemsAsync(int storeid, bool local = false)
        {
            return (await service.RefreshDataAsync()).Where(c => c.StoreId == storeid).ToList();
        }
        public async Task<List<CashReceipt>> FindAsync(int storeid, string query, bool local = false)
        {
            return (await service.FindAsync(query)).Where(c => c.StoreId == storeid).ToList();
        }

        public async Task<CashReceipt> GetByIdAsync(int id)
        {
            return await service.GetByIdAsync(id);
        }

        public async Task<bool> SaveAsync(CashReceipt item, bool isNew = true, bool local = false)
        {
            return await service.SaveAsync(item, isNew);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await service.DeleteAsync(id);
        }


    }

}
