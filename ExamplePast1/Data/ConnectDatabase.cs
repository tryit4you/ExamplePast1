using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Diagnostics;

namespace ExamplePast1.Data
{
    public class ConnectDatabase
    {
        public static SqlConnection connect;
        private string _serverName { get; set; }
        private string _dataName { get; set; }
        private string _userId { get; set; }
        private string _password { get; set; }
        private string _port { get; set; }

        public ConnectDatabase()
        {
            
            string sqlConnect = string.Format($"Data Source={_serverName};Persist Security Info=True;User ID={_userId};Password={_password}");
        }
        public ConnectDatabase(string serverName,string userId,string password)
        {
            _serverName = serverName;
            _userId = userId;
            _password = password;
            //_port = port;
        }
       public bool Connect()
        {
            try
            {

                string sqlConnect = string.Format($"Data Source={_serverName};Persist Security Info=True;User ID={_userId};Password={_password}");
                connect = new SqlConnection(sqlConnect);
                connect.Open();
                return true;
            }catch(SqlException ex)
            {
                Debug.Write("Lỗi kết nối đến server:" + ex.Message);
                return false;
            }
        }
    }
}