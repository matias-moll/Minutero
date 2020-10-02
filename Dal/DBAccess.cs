using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    internal static class DBAccess
    {
        internal static DataTable executeQuery(string query)
        {
            DataTable table = new DataTable();

            using (var connection = DBConn.getDBConn())
            {
                connection.Open();

                using (var command = new SqlCommand(query, connection))
                {
                    // Loads the query results into the table
                    table.Load(command.ExecuteReader());
                }

                connection.Close();
            }

            return table;
        }

        internal static DataTable executeQueryToGetView(string viewName)
        {
            // Ejecutamos una query al nombre de la view dada.
            string query = "select * from " + viewName;
            return DBAccess.executeQuery(query);
        }

        internal static DataTable executeSP(string nameSP, SqlParameter parametro)
        {
            List<SqlParameter> parametros = new List<SqlParameter>() { parametro};
            return executeSP(nameSP, parametros);
        }

        internal static DataTable executeSP(string nameSP, List<SqlParameter> parametros)
        {
            DataTable table = new DataTable();

            using (var conexion = DBConn.getDBConn())
            {
                conexion.Open();
                using (var command = new SqlCommand(nameSP, conexion))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    parametros.ForEach(parametro => command.Parameters.Add(parametro));

                    // Loads the query results into the table
                    table.Load(command.ExecuteReader());
                }
                conexion.Close();
            }

            return table;
        }
    }
}
