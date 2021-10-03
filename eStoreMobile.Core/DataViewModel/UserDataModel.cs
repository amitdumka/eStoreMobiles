using eStore.Shared.Models.Users;
using eStoreMobile.Core.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace eStoreMobile.Core.DataViewModel
{
    public class UserDataModel
    {
        public List<User> Users { get; set; }
        private RestApi.RestService<User> service;
        private eStoreDbContext _context;

        public UserDataModel()
        {
            service = new RestApi.RestService<User> (Constants.UserUrl, "User");
        }

        public async Task<List<User>> GetUserListAsync(bool local = true)
        {
            if ( local )
            {
                using(_context = new eStoreDbContext())
                {
                    Users = await _context.Users.ToListAsync();
                }

                
            }
            if (Users == null || Users.Count <= 0)
                Users = await service.RefreshDataAsync();
            return Users;

        }

        public async Task<bool> VerifyLoginAsync(string username, string password)
        {
            
            using(_context = new eStoreDbContext())
            {
                if ((await _context.Users.CountAsync()) > 0)
                {
                    var user = await _context.Users.Where(c => c.UserName == username && c.Password == password).FirstOrDefaultAsync();
                    if (user != null)
                        return true;
                    else
                        return false;
                }
                else
                {
                    return await VerifyLoginRemoteAsync(username, password);
                }
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

            using(_context= new eStoreDbContext())
            {
                await _context.Users.AddRangeAsync(Users);
                int record = await _context.SaveChangesAsync();
                Debug.WriteLine("No of Record added: " + record);
            }
            
           
        }

        public async Task<int> SaveUser(User user, bool isNew = true, bool isLocal=true)
        {
            int count = 0;
            using (_context=new eStoreDbContext())
            {
                if (isNew) {
                   await _context.Users.AddAsync(user);
                }
                else {
                    _context.Users.Update(user);
                }
                count = await _context.SaveChangesAsync();
            }
            if (!isLocal)
            {
                _ = service.SaveAsync(user, isNew);
            }
            return count;
        }


    }
}