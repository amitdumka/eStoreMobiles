using Microsoft.EntityFrameworkCore;
using eStore.Shared.Models.Users;
using eStore.Shared.Models.Payroll;
using eStore.Shared.Models.Stores;
using eStore.Shared.Models.Sales;

namespace eStoreMobile.Core.Database
{
    public class eStoreDbContext: DbContext
    {
        private string DatabasePath { get; set; }
        public eStoreDbContext()
        {
            DatabasePath = Constants.DatabasePath;
            SQLitePCL.Batteries_V2.Init();
            this.Database.EnsureCreated();

        }
        public eStoreDbContext(string databasePath)
        {
            DatabasePath = databasePath;
            SQLitePCL.Batteries_V2.Init();
            this.Database.EnsureCreated();
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet <Attendance> Attendances { get; set; }
        public DbSet<Salesman> Salesmen { get; set; }
        public DbSet<DailySale> DailySales { get; set; }
        public DbSet<DuesList> Dues { get; set; }
        public DbSet<DueRecoverd> DueRecoverds { get; set; }
    
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={DatabasePath}");
        }
        
    }
}
