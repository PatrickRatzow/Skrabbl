using Skrabbl.DataAccess;
using Skrabbl.Model;
using System;
using System.Collections.Generic;

namespace Skrabbl.TestDataAccess
{
    public class Program
    {
        static void Main(string[] args)
        {
            DbUser dbUser = new DbUser();
            List<User> dbUserList = dbUser.GetAllUsers();
            Console.WriteLine(dbUser);
            foreach (User user in dbUserList) {
                Console.WriteLine(user.username);
            }
            User user22 = dbUser.GetUserById(3);
            Console.WriteLine(user22.username);
          
        }
    }
}
