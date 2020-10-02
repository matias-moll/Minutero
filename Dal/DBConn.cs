using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    public static class DBConn
    {

        private static string stringDeConexion;

        static DBConn()
        {
            string pathBase = AppDomain.CurrentDomain.BaseDirectory;
            pathBase = pathBase.Substring(0, pathBase.IndexOf("\\Minutero"));
            string pathToConfig = pathBase + "\\Config\\Connection.txt";
            string[] lines = System.IO.File.ReadAllLines(pathToConfig);
            stringDeConexion = lines[0];
        }

        public static SqlConnection getDBConn()
        {
            return new SqlConnection(stringDeConexion);
        }
    }
}
