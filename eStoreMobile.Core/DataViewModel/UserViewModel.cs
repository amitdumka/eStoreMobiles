
using eStore.Shared.Models.Users;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eStoreMobile.Core.DataViewModel
{
    public class UserViewModel
    {
        public List<User> Users { get; set; }
        private RestApi.RestService<User> service;

        public UserViewModel()
        {
            service = new RestApi.RestService<User> (Constants.UserUrl, "User");
        }

        public async Task<List<User>> GetUserListAsync()
        {
            Users =  await service.RefreshDataAsync ();
            return Users;
        }


        public bool VerifyLoginAsync(string username, string password)
        {
            Debug.WriteLine ("got Username=" + username);
            if ( Users != null && Users.Count > 0 )
            {
                _ = GetUserListAsync ();
            }

            var user = Users.Where (c => c.UserName == username && c.Password == password).FirstOrDefault ();
            Debug.WriteLine ("\nGot User" + user.FullName);
            if ( user != null )
                return true;
            else
                return false;

        }

    }
}
