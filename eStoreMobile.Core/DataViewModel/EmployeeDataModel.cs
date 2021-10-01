using eStore.Shared.Models.Payroll;
using eStoreMobile.Core.Database;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace eStoreMobile.Core.DataViewModel
{
    
    public class EmployeeDataModel
    {
        public List<Employee> Employees { get; set; }
        private RestApi.RestService<Employee> service;
        private eStoreDbContext _context;

        public EmployeeDataModel()
        {
            service = new RestApi.RestService<Employee> (Constants.EmployeeUrl, "Employee");
        }

        public async Task<List<Employee>> GetEmployees(int storeid,bool local = true)
        {
            if ( local )
            { 
                using( _context=new eStoreDbContext())
                {
                    await _context.Employees.Where(c => c.StoreId == storeid).ToListAsync();
                }
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
            using (_context = new eStoreDbContext())
            {
                if (Employees == null || Employees.Count <= 0)
                    Employees = await service.RefreshDataAsync();

                await _context.Employees.AddRangeAsync(Employees);
                int record = await _context.SaveChangesAsync();
                Debug.WriteLine("No of Record added: " + record);
            }
        }


        public async Task<IQueryable<(int EmployeeId, string StaffName)>> GetEmployeeNameList(int storeId,bool working=true)
        {
            using (_context = new eStoreDbContext()) {
                //var list=await eStoreDatabase.Database.Table<Employee> ().Where (c => c.StoreId == storeId && c.IsWorking == working).ToListAsync ();
                if (working)
                {
                    var list =  _context.Employees.Where(c => c.StoreId == storeId && c.IsWorking).Select(c => (c.EmployeeId, c.StaffName));
                        return list;
                }
                else
                {
                    var list =  _context.Employees.Where(c => c.StoreId == storeId ).Select(c => (c.EmployeeId, c.StaffName));
                    return list;
                } }

        }

        public void GetEmployeeById(int id) { }
        public void GetEmployeeByName(string firstName, string lastName) { }

        public async Task<int> SaveEmployee(Employee employee, bool isNew=true, bool local=false)
        {
            if ( local )
            {
                using(_context = new eStoreDbContext())
                {
                    if (isNew)
                    {
                         await _context.Employees.AddAsync(employee);
                    }
                    else
                    {
                         _context.Employees.Update(employee);
                    }
                    return await _context.SaveChangesAsync();
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