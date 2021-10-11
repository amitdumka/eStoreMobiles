using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using eStore.Shared.Models.Common;
using eStoreMobile.Core.Database;
using eStoreMobile.Core.Models.Dtos;
using Microsoft.EntityFrameworkCore;

namespace eStoreMobile.Core.DataViewModel
{
    public class TranscationModeDataModel
    {
        public List<TranscationMode> TranscationModes { get; set; }
        private RestApi.RestService<TranscationMode> service;
        private eStoreDbContext _context;
        public TranscationModeDataModel()
        {
            service = new RestApi.RestService<TranscationMode>(Constants.TranscationModeUrl, "TrabscationMode");
        }

        public async Task<bool> Delete(int id,bool local=false)
        {
            if (local)
            {
                using (_context = new eStoreDbContext())
                {
                    var emp = _context.Stores.Find(id);
                    if (emp != null)
                    {
                        _context.Stores.Remove(emp);
                    }
                    return  (await _context.SaveChangesAsync())>0;

                }
            }else
            return await service.DeleteAsync(id);
        }

        public async Task<TranscationMode> GetById(int id,bool local = false)
        {
            if (local)
            {
                using (_context = new eStoreDbContext())
                { return await _context.TranscationModes.FindAsync(id); }
            }
            return await service.GetByIdAsync(id);
        }

        public async Task<List<TranscationMode>> GetItems(bool local = false)
        {
            if (local)
            {
                using (_context = new eStoreDbContext())
                {
                    return await _context.TranscationModes.ToListAsync();
                }
            }
            else
            {
                TranscationModes = await service.RefreshDataAsync();
                return TranscationModes;
            }
        }

        public bool IsExists(int id)
        {
            if (_context.TranscationModes.Find(id) != null)
                return true;
            return false;
        }

        public async Task<bool> Save(TranscationMode item, bool isNew = true, bool local = false)
        {
            if (local)
            {
                using (_context = new eStoreDbContext())
                {
                    if (isNew)
                    {
                        await _context.TranscationModes.AddAsync(item);
                    }
                    else
                    {
                        _context.TranscationModes.Update(item);
                    }
                    return (await _context.SaveChangesAsync())>0;
                }



            }
            else
            {
                return await service.SaveAsync(item, isNew);
                    

            }
        }

        public async Task<bool> Sync()
        {
            try
            {
                using (_context = new eStoreDbContext())
                {
                    TranscationModes = await _context.TranscationModes.ToListAsync();

                    if (TranscationModes == null || TranscationModes.Count <= 0)
                        TranscationModes = await service.RefreshDataAsync();

                    TranscationModes = TranscationModes.OrderBy(c => c.TranscationModeId).ToList();
                    foreach (var str in TranscationModes)
                    {
                        str.TranscationModeId = 0;

                    }
                    _context.Database.ExecuteSqlCommand("Delete from TranscationModes; UPDATE SQLITE_SEQUENCE SET SEQ=0 WHERE NAME='TranscationModes';");
                    if (TranscationModes.Count <= 1)
                    {
                        await _context.TranscationModes.AddAsync(TranscationModes[0]);

                    }
                    else
                        await _context.TranscationModes.AddRangeAsync(TranscationModes);
                    int record = await _context.SaveChangesAsync();
                    Debug.WriteLine("No of Record added: " + record);
                    return record > 0 ? true : false;
                }

            }
            catch (Exception ex)
            {

                Debug.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<List<DropListVM>> GetTranscationModeListAsync()
        {
            var list = GetTranscationModeNameList();
            if (list == null || list.Count <= 0)
                await Sync().ConfigureAwait(false);
            return list;
        }


        public List<DropListVM> GetTranscationModeNameList()
        {
            using (_context = new eStoreDbContext())
            {
               
                    return _context.TranscationModes.Select(c => new DropListVM { Value = c.TranscationModeId, Label = c.Transcation }).ToList();

               
            }

        }

    }
}
