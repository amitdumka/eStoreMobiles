using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eStoreMobile.Core
{
    public class Constants
    {
        public static readonly string BaseAddress = "https://www.aprajitaretails.in/api";
        public static readonly string UserUrl = $"{BaseAddress}/users";
        public static readonly string StoreUrl = $"{BaseAddress}/stores";
        public static readonly string EmployeeUrl = $"{BaseAddress}/emplotees";
        public static readonly string DailySaleUrl = $"{BaseAddress}/dailysale";
        public static readonly string AttendanceUrl = $"{BaseAddress}/attendances";

        public static readonly string EnumUrl = $"{BaseAddress}/enumvalue";
        public static readonly string RoleUrl = $"{BaseAddress}/roles";

    }
}
