using eStore.Shared.Models.Payroll;
using eStoreMobile.Core.Database;
using eStoreMobile.Core.Models.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

//Always Cache Employee Database in local for Admin work.
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

        public async Task<List<Employee>> GetEmployees(int storeid,bool local = false)
        {
            if ( local )
            { 
                using( _context=new eStoreDbContext())
                {
                     return await _context.Employees.Where(c => c.StoreId == storeid).ToListAsync();
                }
            }
            else
            {
                Employees = await service.RefreshDataAsync ();
               
                return Employees;
            }
        }

        public async Task<bool> Sync()
        {
            using (_context = new eStoreDbContext())
            {
                Employees = await _context.Employees.ToListAsync();

                if (Employees == null || Employees.Count <= 0)
                    Employees = await service.RefreshDataAsync();

                Employees = Employees.OrderBy(c => c.EmployeeId).ToList();
                foreach (var emp in Employees)
                {
                    emp.EmployeeId = 0;
                }
               // _ = await _context.Database.ExecuteSqlCommandAsync("Delete table Employees; ALTER TABLE Employees AUTO_INCREMENT = 0;");
                await _context.Employees.AddRangeAsync(Employees);
                int record = await _context.SaveChangesAsync();
                Debug.WriteLine("No of Record added: " + record);
                return record > 0;
            }
        }

        public async Task<List<DropListVM>> GetEmployeeListAsync(int storeId, bool working = true)
        {
            var list = GetEmployeeNameList(storeId, working);
            if(list==null || list.Count <= 0)
                await Sync().ConfigureAwait(false);
            return list;
        }


        public List<DropListVM> GetEmployeeNameList(int storeId, bool working = true)
        {
            using (_context = new eStoreDbContext())
            {
                //var list=await eStoreDatabase.Database.Table<Employee> ().Where (c => c.StoreId == storeId && c.IsWorking == working).ToListAsync ();
                if (working)
                {
                    var list = _context.Employees.Where(c => c.StoreId == storeId && c.IsWorking).Select(c => new DropListVM { Value = c.EmployeeId, Label = c.StaffName }).ToList();

                    return list;
                }
                else
                {
                    var list = _context.Employees.Where(c => c.StoreId == storeId).Select(c => new DropListVM { Value = c.EmployeeId, Label = c.StaffName }).ToList();
                    return list;
                }
            }

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

        public int DeleteEmployee(int id) {

            using(_context=new eStoreDbContext () )
            {
                var emp = _context.Employees.Find (id);
                if ( emp != null )
                { 
                    _context.Employees.Remove (emp); 
                }
                return _context.SaveChanges ();

            }
        }

        public bool IsExists(int id)
        {
            if ( _context.Employees.Find (id) != null )
                return true;
            return false;
        }



    }
}