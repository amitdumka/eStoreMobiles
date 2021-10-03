using System;
using System.Collections.Generic;
using System.Text;

namespace eStoreMobile.Constants
{
    public static class ApplicationContext
    {
        public static string UserName { get; set; }
        public static EmpType Role { get; set; }
        public static bool IsLoggedIn { get; set; }
        
        public static int EmpId { get; set; }
        public static int StoreId { get; set; }
        public static string StoreName { get; set; }

    }
}
