using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eStore.Shared.Models.Banking;
using eStoreMobile.Core.Database;
using eStoreMobile.Core.Models.Dtos;

namespace eStoreMobile.Core.DataViewModel
{
    public class BankAccountDataModel : LocalDataModel<BankAccount>
    {
        public BankAccountDataModel() : base(Constants.BankAccountUrl, "BankAccount")
        {

        }

        public override Task<List<BankAccount>> FindAsync(string query, bool local = false)
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
                
            }
            return await this.Sync(Items);
        }
        public async Task<List<DropListVM>> GetBankAccountListAsync()
        {
            var list = GetBankAccountNameList();
            if (list == null || list.Count <= 0)
                await SyncAsync().ConfigureAwait(false);
            return list;
        }


        private List<DropListVM> GetBankAccountNameList()
        {
            using (_context = new eStoreDbContext())
            {

                var list = _context.BankAccounts.Select(c => new DropListVM { Value = c.BankAccountId, Label = c.Account }).ToList();

                return list;


            }

        }
    }
}
