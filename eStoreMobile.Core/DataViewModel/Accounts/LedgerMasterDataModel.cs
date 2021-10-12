using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eStore.Shared.Models.Accounts;

namespace eStoreMobile.Core.DataViewModel
{
    public class LedgerMasterDataModel : LocalDataModel<LedgerMaster>
    {
        public LedgerMasterDataModel() : base(Constants.LedgerMasterUrl, "LedgerMaster")
        {

        }
        public async Task<bool> SyncAsync()
        {
            Items = await service.RefreshDataAsync();

            Items = Items.OrderBy(c => c.LedgerMasterId).ToList();
            return await this.Sync(Items);
        }

        public override Task<List<LedgerMaster>> FindAsync(string query, bool local = false)
        {
            throw new NotImplementedException();
        }

    }
}
