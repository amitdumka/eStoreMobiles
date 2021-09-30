using eStore.Shared.Models.Users;
using eStoreMobile.Core.Database;
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
        private eStoreDatabase _context;

        public UserViewModel()
        {
            service = new RestApi.RestService<User> (Constants.UserUrl, "User");
        }

        public async Task<List<User>> GetUserListAsync(bool local = true)
        {
            if ( local )
            {
                if ( _context == null )
                    _context = await eStoreDatabase.Instance;
                Users = await eStoreDatabase.Database.Table<User> ().ToListAsync ();
            }

            if ( Users == null || Users.Count <= 0 )
                Users = await service.RefreshDataAsync ();
            return Users;
        }

        public async Task<bool> VerifyLoginAsync(string username, string password)
        {
            if ( _context == null )
                _context = await eStoreDatabase.Instance;
            if ( ( await eStoreDatabase.Database.Table<User> ().CountAsync () ) > 0 )
            {
                var user = await eStoreDatabase.Database.Table<User> ().Where (c => c.UserName == username && c.Password == password).FirstOrDefaultAsync ();
                if ( user != null )
                    return true;
                else
                    return false;
            }
            else
            {
                return await VerifyLoginRemoteAsync (username, password);
            }
        }

        public async Task<bool> VerifyLoginRemoteAsync(string username, string password)
        {
            try
            {
                if ( Users == null || Users.Count <= 0 )
                {
                    Users = await GetUserListAsync (false);
                }
                Sync ();
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

        public async void Sync()
        {
            if(Users==null|| Users.Count<=0 )
                Users = await service.RefreshDataAsync ();
            if ( _context == null )
                _context = await eStoreDatabase.Instance;
            int record= await eStoreDatabase.Database.InsertAllAsync(Users);
            Debug.WriteLine ("No of Record added: " + record);
        }
    }
}