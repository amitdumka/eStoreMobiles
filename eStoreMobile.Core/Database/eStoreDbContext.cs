using Microsoft.EntityFrameworkCore;
using eStore.Shared.Models.Users;
using eStore.Shared.Models.Payroll;
using eStore.Shared.Models.Stores;
using eStore.Shared.Models.Sales;
using eStoreMobile.Core.Models;
using eStore.Shared.Models.Common;
using eStore.Shared.Models.Banking;
using eStore.Shared.Models.Accounts.Expenses;
using eStore.Shared.Models.Purchases;
using eStore.Shared.Models.Accounts;
using eStoreMobile.Core.Models.Invoicing;
using Invoice = eStoreMobile.Core.Models.Invoicing.Invoice;
using InvoicePayment = eStoreMobile.Core.Models.Invoicing.InvoicePayment;

namespace eStoreMobile.Core.Database
{
    public class eStoreDbContext: DbContext
    {
        private string DatabasePath { get; set; }
        public eStoreDbContext()
        {
            DatabasePath = Constants.DatabasePath;
            SQLitePCL.Batteries_V2.Init();
            
            //this.Database.Migrate();
            this.Database.EnsureCreated();
            //this.Database.GetAppliedMigrations();
            

        }
        public eStoreDbContext(string databasePath)
        {
            DatabasePath = databasePath;
            SQLitePCL.Batteries_V2.Init();
            this.Database.EnsureCreated();
        }
        public DbSet<StockList> StockLists { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet <Attendance> Attendances { get; set; }
        public DbSet<Salesman> Salesmen { get; set; }
        public DbSet<TranscationMode> TranscationModes { get; set; }
        
        public DbSet<TaxName> Taxes { get; set; }
        public DbSet<Bank> Banks { get; set; }
        public DbSet<BankAccount> AccountNumbers { get; set; }
        public DbSet<EletricityBill> EletricityBills { get; set; }
        public DbSet<ElectricityConnection> ElectricityConnections { get; set; }

        public DbSet<ProductItem> ProductItems { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DbSet<Party> Parties { get; set; }
        public DbSet<LedgerMaster> LedgerMasters { get; set; }
        public DbSet<LedgerType> LedgerTypes { get; set; }

        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoicePayment> InvoicePayments { get; set; }
        public DbSet<InvoiceItem> InvoiceItems { get; set; }
       
        //public ICollection<BankAccountInfo> BankAccounts { get; set; }
        //public ICollection<Areas.Uploader.Models.BankSetting> BankSettings { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={DatabasePath}");
        }
        
    }
}
