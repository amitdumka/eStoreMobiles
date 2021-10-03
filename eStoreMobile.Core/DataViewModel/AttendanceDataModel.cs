using eStore.Shared.Models.Payroll;
using eStoreMobile.Core.Database;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

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
            service = new RestApi.RestService<Attendance> (Constants.AttendanceUrl, "Attendance");
            isAdmin = IsAdmin ();
        }

        public bool IsAdmin()
        {
            if ( Role == EmpType.Accounts || Role == EmpType.StoreManager || Role == EmpType.Owner )
                return true;
            else
                return false;
        }

        public async Task<List<Attendance>> GetAttendances(int storeid, bool local = true)
        {
            if ( isAdmin )
            {
                if ( local )
                {
                    using ( _context = new eStoreDbContext () )
                    {
                        return await _context.Attendances.Where (c => c.StoreId == storeid).ToListAsync ();
                    }
                }
                else
                {
                    Attendances = await service.RefreshDataAsync ();
                    Sync ();
                    return Attendances;
                }
            }
            else
            {
                if ( local )
                {
                    using ( _context = new eStoreDbContext () )
                    {
                        return await _context.Attendances.Where (c => c.StoreId == storeid && c.EmployeeId == empId).ToListAsync ();
                    }
                }
                else
                {
                    if ( empId > 0 )
                    {
                        Attendances = ( await service.RefreshDataAsync () );
                        Attendances = Attendances.Where (c => c.EmployeeId == empId).ToList ();
                        Sync ();
                    }
                    else
                        Attendances = null;

                    return Attendances;
                }
            }
        }

        public async void Sync()
        {
            try
            {
                using ( _context = new eStoreDbContext () )
                {
                    if ( Attendances == null || Attendances.Count <= 0 )
                        Attendances = await service.RefreshDataAsync ();
                    Attendances = Attendances.OrderBy (c => c.EmployeeId).ToList ();

                    foreach ( var item in Attendances )
                    {
                        item.Employee.EmployeeId = 0;
                        item.EmployeeId = 0;
                        _context.Employees.Add (item.Employee);
                        item.AttendanceId = 0;
                        _context.Attendances.Add (item);
                    }
                   
                   
                    int record = _context.SaveChanges ();
                   


                    Debug.WriteLine ("No of Record added: " + record);
                }
            }
            catch ( System.Exception ex )
            {

                Debug.WriteLine (ex.Message);
            }
            
        }

        public async Task<Attendance> GetAttendanceByIdAsync(int id)
        {

            using ( _context = new eStoreDbContext () )
            {
                Attendance att = null;
                if(isAdmin||( empId > 0 && empId == id ) )
                {
                    att = await _context.Attendances.FindAsync (id);
                }
               
                
                return null;
            }
        }

        public async Task<List<Attendance>> GetAttendanceByName(string StaffName)
        {
            using ( _context = new eStoreDbContext () )
            {
                if ( isAdmin )
                    return await _context.Attendances.Include (c => c.Employee).Where (c => c.Employee.StaffName == StaffName).ToListAsync ();
                else
                    return null;
            }
        }

        public async Task<int> SaveAttendance(Attendance Attendance, bool isNew = true, bool local = false)
        {
            if ( isAdmin || ( empId > 0 && empId == Attendance.EmployeeId ) )
            {
                if ( local )
                {


                    using ( _context = new eStoreDbContext () )
                    {
                        if ( isNew )
                        {
                            await _context.Attendances.AddAsync (Attendance);
                        }
                        else
                        {
                            _context.Attendances.Update (Attendance);
                        }

                        return await _context.SaveChangesAsync ();
                    }

                }
                else
                {
                    if ( await service.SaveAsync (Attendance, isNew) )
                        return 1;
                    
                }
            }
            return -1;//Error;
                
        }

        public int DeleteAttendance(int id)
        {
            if(isAdmin||(empId==id))
            using ( _context = new eStoreDbContext () )
            {
                var emp = _context.Attendances.Find (id);
                if ( emp != null )
                {
                    _context.Attendances.Remove (emp);
                }
               
            }
            return _context.SaveChanges ();
        }

        public bool IsExists(int id)
        {
            if ( _context.Attendances.Find (id) != null )
                return true;
            return false;
        }
    }
}