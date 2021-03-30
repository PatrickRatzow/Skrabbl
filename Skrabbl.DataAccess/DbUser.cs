using Skrabbl.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using System.Linq;
using System.Diagnostics;

namespace Skrabbl.DataAccess
{
   public class DbUser : IUser
    {
  
        private string connectionString;

        public DbUser() {
            connectionString = @"Data Source=DESKTOP-RJ2KJ7K\SQLEXPRESS;Initial Catalog = Test; Integrated Security=true";
        }
        // Test
        public DbUser(String connectionStringTest) {
            connectionString = connectionStringTest;
        }

        public bool Connect() {
            bool connect = false;
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                connection.Open();
                if (connection.State == System.Data.ConnectionState.Open) {
                    connect = true;
                }
            }
            return connect;
        }


        public List<User> GetAllUsers()
        {
            string userSqlSelect = "select * from _user";
            List<User> users = null;
            using (SqlConnection userConnection = new SqlConnection(connectionString)) {
                users = userConnection.Query<User>(userSqlSelect).ToList();
                foreach (var user in users) {
                   Debug.WriteLine(user.username);
                }
                
            
            }
            return users;
        }

        public User GetUserById(int inputId)
        {
            string getUserByIdSql = "select * from _user where userId = @inputUserId";
            User foundUser = null;
            using (SqlConnection userConnection = new SqlConnection(connectionString))
                foundUser = userConnection.QueryFirstOrDefault<User>(getUserByIdSql, new { inputUserId = inputId });
            return foundUser;
        }
        
    }

}
