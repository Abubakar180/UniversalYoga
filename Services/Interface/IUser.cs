using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UniversalYoga.Models;

namespace UniversalYoga.Services.Interface
{
    public interface IUser
    {
        Task<bool> RegisterUser(clsUser user);
        Task<clsUser> LoginUser(string email);
        Task<List<clsUser>> GetInfo(string email);
        Task<List<clsUser>> GetAllUsersAsync();
    }
}
