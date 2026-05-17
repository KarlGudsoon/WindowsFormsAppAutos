using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PermisosVehiculos.Data
{
    public class DatabaseHelper
    {
        private static readonly string ConnectionString =
            "Server=localhost;Database=db_circulacion;Uid=root;Pwd=;";

        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }
    }
}