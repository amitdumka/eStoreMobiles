using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eStore.Shared.Models.Accounts;

namespace eStoreMobile.Core.DataViewModel
{
    public class LedgerTypeDataModel : LocalDataModel<LedgerType>
    {
        public LedgerTypeDataModel() : base(Constants.LedgerTypeUrl, "LedgerType")
        {

        }
        public async Task<bool> SyncAsync()
        {
            Items = await service.RefreshDataAsync();

            Items = Items.OrderBy(c => c.LedgerTypeId).ToList();
            return await this.Sync(Items);
        }

        public override Task<List<LedgerType>> FindAsync(string query, bool local = false)
        {
            throw new NotImplementedException();
        }
    }
}
