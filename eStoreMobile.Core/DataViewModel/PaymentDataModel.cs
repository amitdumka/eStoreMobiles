using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using eStore.Shared.Models.Accounts;
using eStoreMobile.Core.Database;

namespace eStoreMobile.Core.DataViewModel
{
    public class PaymentDataModel:IDataModel<Payment>
    {
        public List<Payment> Payments { get; set; }
        private RestApi.RestService<Payment> service;
        private eStoreDbContext _context;


        public PaymentDataModel()
        {
            service = new RestApi.RestService<Payment>(Constants.EmployeeUrl, "Payment");
        }

        public List<Payment> GetItems(int storeid, bool local = false)
        {
            throw new NotImplementedException();
        }

        public bool Sync()
        {
            throw new NotImplementedException();
        }

        public void GetById(int id)
        {
            throw new NotImplementedException();
        }

        public int Save(Payment item, bool isNew = true, bool local = false)
        {
            throw new NotImplementedException();
        }

        public int DeleteEmployee(int id)
        {
            throw new NotImplementedException();
        }

        public bool IsExists(int id)
        {
            throw new NotImplementedException();
        }
    }
    public interface IDataModel<T>
    {
        // public List<T> ObjectName { get; set; }
        // private RestApi.RestService<T> service;
        // private eStoreDbContext _context;

        //public <T>DataModel()
        //{
        //    service = new RestApi.RestService<Employee>(Constants.EmployeeUrl, "Employee");
        //}

        public  List<T> GetItems(int storeid, bool local = false);

        public bool Sync();

        public void GetById(int id);


        public int  Save(T item, bool isNew = true, bool local = false);

        public int DeleteEmployee(int id);


        public bool IsExists(int id);
        



    }
}
