using Skrabbl.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skrabbl.API.Services
{
    public interface IUserService
    {
        Task<User> CreateUser(string _userName, string _password, string _email);
        Task<User> GetUser(string _username, string _password);

       
    }
}
