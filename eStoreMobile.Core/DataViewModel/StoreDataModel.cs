using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using eStore.Shared.Models.Stores;
using eStoreMobile.Core.Database;
using Microsoft.EntityFrameworkCore;

namespace eStoreMobile.Core.DataViewModel
{
    public class StoreDataModel
    {
        public List<Store> Stores { get; set; }
        private RestApi.RestService<Store> service;
        private eStoreDbContext _context;


        public StoreDataModel()
        {
            service = new RestApi.RestService<Store>(Constants.StoreUrl, "Store");
        }

        public async Task<List<Store>> GetStores(bool local = false)
        {
            if (local)
            {
                using (_context = new eStoreDbContext())
                {
                    return await _context.Stores.ToListAsync();
                }
            }
            else
            {
                Stores = await service.RefreshDataAsync();
                return Stores;
            }
        }

        public async Task<bool> Sync()
        {
            try
            {
                using (_context = new eStoreDbContext())
                {
                    Stores = await _context.Stores.ToListAsync();

                    if (Stores == null || Stores.Count <= 0)
                        Stores = await service.RefreshDataAsync();

                    Stores = Stores.OrderBy(c => c.StoreId).ToList();
                    foreach (var str in Stores)
                    {
                        str.StoreId = 0;
                          
                        str.UserId = "AutoAdmin";
                    }
                     _context.Database.ExecuteSqlCommand("Delete from Stores; UPDATE SQLITE_SEQUENCE SET SEQ=0 WHERE NAME='Stores';");
                    if (Stores.Count <= 1)
                    {
                       await _context.Stores.AddAsync(Stores[0]);

                    }
                    else
                    await _context.Stores.AddRangeAsync(Stores);
                    int record = await _context.SaveChangesAsync();
                    Debug.WriteLine("No of Record added: " + record);
                    return record > 0 ? true : false;
                }

            }
            catch (Exception ex)
            {

                Debug.WriteLine(ex.Message);
                return  false;
            }
        }

        public async Task<int> SaveStore(Store store, bool isNew = true, bool local = false)
        {
            if (local)
            {
                using (_context = new eStoreDbContext())
                {
                    if (isNew)
                    {
                        await _context.Stores.AddAsync(store);
                    }
                    else
                    {
                        _context.Stores.Update(store);
                    }
                    return await _context.SaveChangesAsync();
                }



            }
            else
            {
                if (await service.SaveAsync(store, isNew))
                    return 1;
                else
                    return 0;

            }

        }

        public int DeleteStore(int id)
        {

            using (_context = new eStoreDbContext())
            {
                var emp = _context.Stores.Find(id);
                if (emp != null)
                {
                    _context.Stores.Remove(emp);
                }
                return _context.SaveChanges();

            }
        }

        public bool IsExists(int id)
        {
            if (_context.Stores.Find(id) != null)
                return true;
            return false;
        }
        public void GetStoreById(int id)
        {

        }
        public void GetStoreByName(string storeName) { }



    }
}
