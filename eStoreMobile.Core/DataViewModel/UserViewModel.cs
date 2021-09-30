using eStore.Shared.Models.Users;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
            Users = await service.RefreshDataAsync ();
            return Users;
        }

        public async Task<bool> VerifyLoginAsync(string username, string password)
        {
            try
            {
                if ( Users == null || Users.Count > 0 )
                {
                    Users = await GetUserListAsync ();
                }
                if ( Users != null && Users.Count > 0 )
                {
                    var user = Users.Where (c => c.UserName == username && c.Password == password).FirstOrDefault ();

                    if ( user != null )
                        return true;
                    else
                        return false;
                }
                else
                    return false;
            }
            catch ( Exception ex )
            {
                Debug.WriteLine (ex.Message);
                return false;
            }
        }
    }
}