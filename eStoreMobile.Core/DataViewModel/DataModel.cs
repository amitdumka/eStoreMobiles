using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eStoreMobile.Core.Database;

namespace eStoreMobile.Core.DataViewModel
{
    public abstract class DataModel<T>
    {
        public List<T> Items { get; set; }
        private RestApi.RestService<T> service;
        private eStoreDbContext _context;

        public async Task<bool> Delete(int id)
        {
            return await service.DeleteAsync(id);
        }
        public async Task<T> GetById(int id)
        {
            return await service.GetByIdAsync(id);
        }
        public async Task<List<T>> GetItems(int storeid, bool local = false)
        {
            return (await service.RefreshDataAsync()).ToList();
        }


        public async Task<bool> Save(T item, bool isNew = true, bool local = false)
        {
            return await service.SaveAsync(item, isNew);
        }

        public async Task<List<T>> FindAsync(string query, bool local = false)
        {
            if (string.IsNullOrEmpty(query))
            {
                throw new ArgumentException($"'{nameof(query)}' cannot be null or empty.", nameof(query));
            }

            return (await service.FindAsync(query)).ToList();
        }
        public abstract Task<bool> Sync();

        public abstract Task<bool> IsExists(int id);
    }
}
