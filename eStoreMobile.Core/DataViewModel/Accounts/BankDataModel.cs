using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eStore.Shared.Models.Banking;
using eStoreMobile.Core.Database;
using eStoreMobile.Core.Models.Dtos;

namespace eStoreMobile.Core.DataViewModel
{
    public class BankDataModel : LocalDataModel<Bank>
    {
        public BankDataModel() : base(Constants.BankUrl, "Bank")
        {

        }

        public override Task<List<Bank>> FindAsync(string query, bool local = false)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> SyncAsync()
        {
            Items = await service.RefreshDataAsync();

            Items = Items.OrderBy(c => c.BankId).ToList();
            foreach (var item in Items)
            {
                item.BankId = 0;
                //item.LedgerType = null;
                //item.LedgerMaster = null;
            }
            return await this.Sync(Items);
        }
        public async Task<List<DropListVM>> GetBankListAsync()
        {
            var list = GetBankNameList();
            if (list == null || list.Count <= 0)
                await SyncAsync().ConfigureAwait(false);
            return list;
        }


        private List<DropListVM> GetBankNameList()
        {
            using (_context = new eStoreDbContext())
            {

                var list = _context.Banks.Select(c => new DropListVM { Value = c.BankId, Label = c.BankName }).ToList();

                return list;


            }

        }
    }
}
