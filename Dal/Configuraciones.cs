using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    public static class Configuraciones
    {
        public static DataTable getValorParametro(string descripcionParametro)
        {
            SqlParameter parametroDescripcionParametro = new SqlParameter("@descripcionParametro", SqlDbType.VarChar, 50);
            parametroDescripcionParametro.Value = descripcionParametro;

            return DBAccess.executeSP("getValorParametro", parametroDescripcionParametro);
        }
    }
}
