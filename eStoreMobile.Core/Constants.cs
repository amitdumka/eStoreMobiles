using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eStoreMobile.Core
{
    public class Constants
    {
        public const string BackendUrl  = "https://zumo-abcd1234.azurewebsites.net";
        //Database
        public const string DatabaseFilename = "estoreSQLite.db3";

        public const SQLite.SQLiteOpenFlags Flags =
            // open the database in read/write mode
            SQLite.SQLiteOpenFlags.ReadWrite |
            // create the database if it doesn't exist
            SQLite.SQLiteOpenFlags.Create |
            // enable multi-threaded database access
            SQLite.SQLiteOpenFlags.SharedCache;
        public static string DatabasePath
        {
            get
            {
                var basePath = Environment.GetFolderPath (Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine (basePath, DatabaseFilename);
            }
        }

        //Urls
        public static readonly string BaseAddress = "https://www.aprajitaretails.in/api";
        public static readonly string UserUrl = $"{BaseAddress}/users";
        public static readonly string StockListurl = $"{BaseAddress}/stockLists";
        public static readonly string StoreUrl = $"{BaseAddress}/stores";
        public static readonly string EmployeeUrl = $"{BaseAddress}/employees";
        public static readonly string DailySaleUrl = $"{BaseAddress}/dailysale";
        public static readonly string AttendanceUrl = $"{BaseAddress}/attendances";

        public static readonly string EnumUrl = $"{BaseAddress}/enumvalue";
        public static readonly string RoleUrl = $"{BaseAddress}/roles";

    }

}
