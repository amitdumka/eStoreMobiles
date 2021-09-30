using eStore.Shared.Models.Payroll;
using eStoreMobile.Core.Database;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace eStoreMobile.Core.DataViewModel
{
    public class EmployeeDataModel
    {
        public List<Employee> Employees { get; set; }
        private RestApi.RestService<Employee> service;
        private eStoreDatabase _context;

        public EmployeeDataModel()
        {
            service = new RestApi.RestService<Employee> (Constants.EmployeeUrl, "Employee");
        }

        public async Task<List<Employee>> GetEmployees(int storeid,bool local = true)
        {
            if ( local )
            {
                if ( _context == null )
                    _context = await eStoreDatabase.Instance;
                return await eStoreDatabase.Database.Table<Employee> ().Where(c=>c.StoreId==storeid).ToListAsync ();
            }
            else
            {
                Employees = await service.RefreshDataAsync ();
                Sync ();
                return Employees;
            }
        }

        public async void Sync()
        {
            if ( Employees == null || Employees.Count <= 0 )
                Employees = await service.RefreshDataAsync ();
            if ( _context == null )
                _context = await eStoreDatabase.Instance;
            int record = await eStoreDatabase.Database.InsertAllAsync (Employees);
            Debug.WriteLine ("No of Record added: " + record);
        }


        public async System.Threading.Tasks.Task<List<Employee>> GetEmployeeNameList(int storeId,bool working=true)
        {
            if ( _context == null )
                _context = await eStoreDatabase.Instance;
            //var list=await eStoreDatabase.Database.Table<Employee> ().Where (c => c.StoreId == storeId && c.IsWorking == working).ToListAsync ();
            if ( working )
            {
                var list = await eStoreDatabase.Database.QueryAsync<Employee> ($"SELECT EmployeeId, FirstName, LastName FROM [Employee] WHERE [StoreId] = {storeId} and [IsWorking]=0");
                return list;
            }
            else
            {
                var list = await eStoreDatabase.Database.QueryAsync<Employee> ($"SELECT EmployeeId, FirstName, LastName FROM [Employee] WHERE [StoreId] = {storeId}");
                return list;
            }

        }

        public void GetEmployeeById(int id) { }
        public void GetEmployeeByName(string firstName, string lastName) { }
        public async Task<int> SaveEmployee(Employee employee, bool isNew=true, bool local=false)
        {
            if ( local )
            {
                if ( _context == null )
                    _context = await eStoreDatabase.Instance;
                if ( isNew )
                {
                   return await eStoreDatabase.Database.InsertAsync (employee);
                }
                else
                {
                   return await eStoreDatabase.Database.UpdateAsync (employee);
                }
            }
            else
            {
                if ( await service.SaveAsync (employee, isNew) )
                    return 1;
                else
                    return 0;

            }

        }




    }
}