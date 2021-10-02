using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using eStore.Shared.Models.Payroll;
using eStoreMobile.Core.Database;
using Microsoft.EntityFrameworkCore;

namespace eStoreMobile.Core.DataViewModel
{
    public class AttendanceDataModel
    {
        public List<Attendance> Attendances { get; set; }
        private RestApi.RestService<Attendance> service;
        private eStoreDbContext _context;
        public AttendanceDataModel()
        {
            service = new RestApi.RestService<Attendance>(Constants.AttendanceUrl, "Attendance");
        }

        public async Task<List<Attendance>> GetAttendances(int storeid, bool local = true)
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
                Sync();
                return Attendances;
            }
        }

        public async void Sync()
        {
            using (_context = new eStoreDbContext())
            {
                if (Attendances == null || Attendances.Count <= 0)
                    Attendances = await service.RefreshDataAsync();

                await _context.Attendances.AddRangeAsync(Attendances);
                int record = await _context.SaveChangesAsync();
                Debug.WriteLine("No of Record added: " + record);
            }
        }


        public async Task<Attendance> GetAttendanceByIdAsync(int id) {
            using (_context = new eStoreDbContext())
            {
                var emp = await _context.Attendances.FindAsync(id);
                if (emp != null)
                {
                    return emp;
                }
                return null;

            }
        }
        public async Task<List<Attendance>> GetAttendanceByName(string StaffName) {

            using(_context=new eStoreDbContext())
            {
                return await _context.Attendances.Include(c => c.Employee).Where(c => c.Employee.StaffName == StaffName).ToListAsync();
            }
        }

        public async Task<int> SaveAttendance(Attendance Attendance, bool isNew = true, bool local = false)
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
                else
                    return 0;

            }

        }

        public int DeleteAttendance(int id)
        {

            using (_context = new eStoreDbContext())
            {
                var emp = _context.Attendances.Find(id);
                if (emp != null)
                {
                    _context.Attendances.Remove(emp);
                }
                return _context.SaveChanges();

            }
        }

        public bool IsExists(int id)
        {
            if (_context.Attendances.Find(id) != null)
                return true;
            return false;
        }

    }
}
