using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;

namespace WpfApp1
{
    class db
    {
        SqlConnection connection = new SqlConnection(@"Data Source =DESKTOP-2J8B4QQ\COFFNICK; Initial Catalog = applications; Integrated Security = True;MultipleActiveResultSets=True;TrustServerCertificate=True");

        public void openCon()// открытие соеденеия с бд
        {
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
       
        }

        public void closeCon()// закрытие соеденеия с бд
        {
            if(connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }
        public SqlConnection getConnection()// получения соеденеия с бд
        {
            return connection;
        }
    }
}
