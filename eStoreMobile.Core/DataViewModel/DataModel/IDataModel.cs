using System.Collections.Generic;
using System.Threading.Tasks;

namespace eStoreMobile.Core.DataViewModel
{
    public interface IDataModel<T>
    {
        public Task<List<T>> GetItems(int storeid, bool local = false);
        public Task<bool> Sync();
        public Task<T> GetById(int id);
        public Task<bool> Save(T item, bool isNew = true, bool local = false);
        public Task<bool> Delete(int id);
        public Task<bool> IsExists(int id);
    }
}
