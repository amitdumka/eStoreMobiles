using eStore.Shared.Models.Payroll;
using eStoreMobile.Core.Database;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
//Only Logged in User record can only be store locally otherwise not it will be from web.
namespace eStoreMobile.Core.DataViewModel
{
    public class AttendanceDataModel
    {
        public List<Attendance> Attendances { get; set; }
        private RestApi.RestService<Attendance> service;
        private eStoreDbContext _context;
        private int empId;
        private EmpType Role;
        private bool isAdmin;

        public AttendanceDataModel(int id, EmpType role)
        {
            empId = id;
            Role = role;
            service = new RestApi.RestService<Attendance>(Constants.AttendanceUrl, "Attendance");
            isAdmin = IsAdmin();
        }

        public bool IsAdmin()
        {
            if (Role == EmpType.Accounts || Role == EmpType.StoreManager || Role == EmpType.Owner)
                return true;
            else
                return false;
        }

        public async Task<List<Attendance>> GetAttendances(int storeid, bool local = false)
        {
            if (isAdmin)
            {
                if (local)
                {
                    using (_context = new eStoreDbContext())
                    {
                        return await _context.Attendances.Where(c => c.StoreId == storeid).ToListAsync();
                    }
                }
                else
                {
                    Attendances = await service.RefreshDataAsync();
                    return Attendances;
                }
            }
            else
            {
                if (local)
                {
                    using (_context = new eStoreDbContext())
                    {
                        return await _context.Attendances.Where(c => c.StoreId == storeid && c.EmployeeId == empId).ToListAsync();
                    }
                }
                else
                {
                    if (empId > 0)
                    {
                        Attendances = (await service.RefreshDataAsync());
                        Attendances = Attendances.Where(c => c.EmployeeId == empId).ToList();

                    }
                    else
                        Attendances = null;

                    return Attendances;
                }
            }
        }

        //Do not use
        public async void Sync()
        {
            try
            {
                using (_context = new eStoreDbContext())
                {
                    if (Attendances == null || Attendances.Count <= 0)
                        Attendances = await service.RefreshDataAsync();
                    Attendances = Attendances.OrderBy(c => c.EmployeeId).ToList();
                    foreach (var item in Attendances)
                    {
                        item.Employee.EmployeeId = 0;
                        item.EmployeeId = 0;
                        _context.Employees.Add(item.Employee);
                        item.AttendanceId = 0;
                        _context.Attendances.Add(item);
                    }
                    int record = _context.SaveChanges();
                    Debug.WriteLine("No of Record added: " + record);
                }
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public async Task<Attendance> GetAttendanceByIdAsync(int id, bool local = false)
        {
            Attendance att = null;
            if (local)
            {
                using (_context = new eStoreDbContext())
                {

                    if (isAdmin || (empId > 0))
                    {
                        att = await _context.Attendances.FindAsync(id);

                    }



                }
            }
            else
            {
                if (isAdmin || (empId > 0))
                {
                    att = await service.GetByIdAsync(id);
                }
            }
            if (att.EmployeeId != empId && !isAdmin)
                return null;
            else return att;

        }

        public async Task<List<Attendance>> GetAttendanceByName(string StaffName, bool local = false)
        {
            
            
                List<Attendance> att=null;
                if (local && isAdmin)
                {
                    using (_context = new eStoreDbContext())
                    {
                        att = await _context.Attendances.Include(c => c.Employee).Where(c => c.Employee.StaffName == StaffName).ToListAsync();
                    }
                }
                else if (!local && isAdmin)
                {
                    string param = $"{{queryByField:'StaffName', fieldValue:'{StaffName}'}}";
                    att = await service.FindAsync(param);
                }
                return att;
            
        }

        public async Task<int> SaveAttendance(Attendance Attendance, bool isNew = true, bool local = false)
        {
            if (isAdmin || (empId > 0 && empId == Attendance.EmployeeId))
            {
                if (local)
                {


                    using (_context = new eStoreDbContext())
                    {
                        if (isNew)
                        {
                            await _context.Attendances.AddAsync(Attendance);
                        }
                        else
                        {
                            _context.Attendances.Update(Attendance);
                        }

                        return await _context.SaveChangesAsync();
                    }

                }
                else
                {
                    if (await service.SaveAsync(Attendance, isNew))
                        return 1;

                }
            }
            return -1;//Error;

        }

        public async Task<bool> DeleteAttendanceAsync(int id, bool local=false)
        {
            if (isAdmin || (empId == id))
            {
                if (local)
                {
                    using (_context = new eStoreDbContext())
                    {
                        var emp = _context.Attendances.Find(id);
                        if (emp != null)
                        {
                            _context.Attendances.Remove(emp);
                        }

                    }
                    return (_context.SaveChanges()>0?true:false);
                }
                else
                {
                    return await service.DeleteAsync(id);
                }

            }
            return false;
        }


        public bool IsExists(int id)
        {
            if (_context.Attendances.Find(id) != null)
                return true;
            return false;
        }
    }
}