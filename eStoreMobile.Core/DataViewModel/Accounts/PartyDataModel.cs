using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eStore.Shared.Models.Accounts;
using eStoreMobile.Core.Database;
using eStoreMobile.Core.Models.Dtos;

namespace eStoreMobile.Core.DataViewModel
{
    public class PartyDataModel : LocalDataModel<Party>
    {
        public PartyDataModel() : base(Constants.PartyUrl, "Party")
        {

        }

        public override Task<List<Party>> FindAsync(string query, bool local = false)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> SyncAsync()
        {
            Items = await service.RefreshDataAsync();

            Items = Items.OrderBy(c => c.PartyId).ToList();
            foreach (var item in Items)
            {
                item.PartyId = 0;
                item.LedgerType = null;
                if (string.IsNullOrEmpty(item.PANNo)) item.PANNo = " ";
                if (string.IsNullOrEmpty(item.GSTNo)) item.GSTNo = " ";
                if (string.IsNullOrEmpty(item.Address)) item.Address = " ";

                //item.LedgerMaster = null;
            }
            return await this.Sync(Items);
        }
        public async Task<List<DropListVM>> GetPartyListAsync()
        {
            var list = GetPartyNameList();
            if (list == null || list.Count <= 0)
                await SyncAsync().ConfigureAwait(false);
            return list;
        }


        private List<DropListVM> GetPartyNameList()
        {
            using (_context = new eStoreDbContext())
            {

                var list = _context.Parties.Select(c => new DropListVM { Value = c.PartyId, Label = c.PartyName }).ToList();

                return list;


            }

        }
    }
}
