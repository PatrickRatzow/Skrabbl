using Skrabbl.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Skrabbl.DataAccess
{
    interface IUser
    {
        List<User> GetAllUsers();
        User GetUserById(int inputId);

    }
}
