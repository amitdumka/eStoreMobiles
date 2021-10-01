using System;
using eStore.Shared.Models.Payroll;
using eStore.Shared.Models.Sales;
using eStore.Shared.Models.Stores;
using eStore.Shared.Models.Users;
using SQLite;

namespace eStoreMobile.Core.Database
{
    //Note: Will Think of using SqlLite direct database if EF Core doesnt work as is required.
    [Obsolete]
    public class eStoreDatabase
    {
        public static SQLiteAsyncConnection Database;

        public eStoreDatabase()
        {
            Database = new SQLiteAsyncConnection (Constants.DatabasePath, Constants.Flags);
        }

        public static readonly AsyncLazy<eStoreDatabase> Instance = new AsyncLazy<eStoreDatabase> (async () =>
        {
            var instance = new eStoreDatabase ();
            CreateTableResult result = await Database.CreateTableAsync<User> ();
           // result = await Database.CreateTableAsync<Employee> ();
           // result = await Database.CreateTableAsync<Attendance> ();
           // result = await Database.CreateTableAsync<DailySale> ();
           // result = await Database.CreateTableAsync<DuesList> ();
           //// result = await Database.CreateTableAsync<Salesman> ();
           // result = await Database.CreateTableAsync<Store> ();

            return instance;
        });
    }


}