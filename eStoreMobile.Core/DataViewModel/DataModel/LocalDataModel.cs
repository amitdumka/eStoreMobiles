using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using eStoreMobile.Core.Database;
using Microsoft.EntityFrameworkCore;

namespace eStoreMobile.Core.DataViewModel
{
    public abstract class LocalDataModel<TEntity> where TEntity : class
    {
        public List<TEntity> Items { get; set; }
        protected RestApi.RestService<TEntity> service;
        protected eStoreDbContext _context;

        public LocalDataModel(string url, string name)
        {
            service = new RestApi.RestService<TEntity>(url, name);
        }

        public async Task<bool> Delete(int id, bool local = false)
        {
            if (local)
            {
                using (_context = new eStoreDbContext())
                {
                    var element = await _context.FindAsync<TEntity>(id);
                    _context.Remove<TEntity>(element);
                    return (await _context.SaveChangesAsync()) > 0;
                }
            }
            else
            {
                return await service.DeleteAsync(id);
            }

        }
        public async Task<TEntity> GetById(int id, bool local = false)
        {
            if (local)
            {
                using (_context = new eStoreDbContext())
                {
                    return _context.Find<TEntity>(id);
                }
            }
            else
                return await service.GetByIdAsync(id);
        }

        [Obsolete]
        public async Task<List<TEntity>> GetItems(int storeid, bool local = false)
        {
            if (local)
            {
                using (_context = new eStoreDbContext())
                {
                    return _context.Query<TEntity>().ToList();
                }
            }
            else
                return (await service.RefreshDataAsync()).ToList();
        }

        public bool IsExists(int id)
        {
            if (_context.Find<TEntity>(id) != null) return true; else return false;
        }

        public async Task<bool> Save(TEntity item, bool isNew = true, bool local = false)
        {
            if (local)
            {
                using (_context = new eStoreDbContext())
                {
                    await _context.AddAsync<TEntity>(item);
                    return (await _context.SaveChangesAsync()) > 0;
                }
            }
            else return await service.SaveAsync(item, isNew);
        }
        /// <summary>
        /// Remove id and any other virtual objects from parameter
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        protected async Task<bool> Sync(List<TEntity> entities)
        {
            try
            {

                using (_context = new eStoreDbContext())
                {
                    int ctr = _context.Query<TEntity>().Count();
                    if (ctr > 0)
                    {
                        int x = _context.Database.ExecuteSqlCommand($"Delete from {nameof(TEntity)}; UPDATE SQLITE_SEQUENCE SET SEQ=0 WHERE NAME='{nameof(TEntity)}';");
                        Debug.WriteLine("No of Record deelte: " + x);
                    }
                    int record = 0;

                    await _context.AddRangeAsync(entities);
                    record = await _context.SaveChangesAsync();
                    Debug.WriteLine("No of Record added: " + record);
                    return record > 0;
                }

            }
            catch (System.Exception ex)
            {

                Debug.WriteLine(ex.Message + "\n" + ex.InnerException);
                return false;
            }
        }
        public abstract Task<List<TEntity>> FindAsync(string query, bool local = false);
    }
}
