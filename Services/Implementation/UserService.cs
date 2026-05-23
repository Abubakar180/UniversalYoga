using Firebase.Database;
using Firebase.Database.Query;
using UniversalYoga.Helpers;
using UniversalYoga.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Auth;
using UniversalYoga.Models;

namespace UniversalYoga.Services.Implementation
{
    public class UserService : IUser
    {
        FirebaseClient firebase = new FirebaseClient(FirebaseWebApi.DatabaseLink, new FirebaseOptions
        {
            AuthTokenAsyncFactory = () => Task.FromResult(FirebaseWebApi.DatabaseSecret)
        });
        public async Task<bool> RegisterUser(clsUser user)
        {
            var result = await firebase.Child(nameof(clsUser)).PostAsync(new clsUser()
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Contact = user.Contact,
                Address = user.Address,
            });

            if (result.Object != null)
            {
                return true;
            }
            else
            {
                return false;

            }
        }
        public async Task<bool> DeleteUser(string email)
        {
            //get the user's info where email exists 
            var task = (await firebase.Child(nameof(clsUser)).OnceAsync<clsUser>())
                .Where(a => a.Object.Email == email)
                .FirstOrDefault();

            if (task != null)
            {
                //DeleteAsync method is used to delete the user info from firebase DB using Key of Row
                await firebase.Child(nameof(clsUser)).Child(task.Key).DeleteAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
        //returns the user's info where user's email matches
        public async Task<clsUser> LoginUser(string email)
        {
            var GetPerson = (await firebase.Child(nameof(clsUser)).OnceAsync<clsUser>())
                .Where(a => a.Object.Email == email).FirstOrDefault();

            if (GetPerson != null)
            {

                var content = GetPerson.Object as clsUser;
                return content;

            }
            else
            {
                return null;
            }
        }
        public async Task<List<clsUser>> GetInfo(string email)
        {
            var GetInfo = (await firebase.Child(nameof(clsUser)).OnceAsync<clsUser>()).Where(a => a.Object.Email == email).Select(item => new clsUser
            {
                Email = item.Object.Email,
                LastName = item.Object.LastName,
                FirstName = item.Object.FirstName,
                Contact = item.Object.Contact,
                Address = item.Object.Address,
                key = item.Key,
            }); 

            if (GetInfo != null)
            {
                return new List<clsUser>(GetInfo);
            }
            else
            {
                return null;
            }
        }
        public async Task<List<clsUser>> GetAllUsersAsync()
        {
            return (await firebase.Child(nameof(clsUser)).OnceAsync<clsUser>()).Select(f => new clsUser
            {
                Email = f.Object.Email,
                LastName = f.Object.LastName,
                FirstName = f.Object.FirstName,
                Contact = f.Object.Contact,
                Address = f.Object.Address,
                key = f.Key,
            }).ToList();
        }
    }
}
