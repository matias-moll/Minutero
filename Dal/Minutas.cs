using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Dal
{
    public static class Minutas
    {
        public static DataTable getItemsMinutaDeAyerPorUsuario(string usuario)
        {
            SqlParameter parametroUsuario = new SqlParameter("@usuario", SqlDbType.VarChar, 20);
            parametroUsuario.Value = usuario;

            return DBAccess.executeSP("getItemsMinutaDeAyerPorUsuario", parametroUsuario);
        }

        public static DataTable getLastValidItemsMinutaPorUsuario(string usuario)
        {
            SqlParameter parametroUsuario = new SqlParameter("@usuario", SqlDbType.VarChar, 20);
            parametroUsuario.Value = usuario;

            return DBAccess.executeSP("getLastValidItemsMinutaPorUsuario", parametroUsuario);
        }

        public static DataTable getMinutaActual()
        {
            // Ejecutamos la query que trae el resultado de la view para la minuta del dia
            return DBAccess.executeQuery("select * from itemsMinutasDeHoy");
        }

        public static int grabarMinuta(string usuario)
        {
            SqlParameter parametroUsuario = new SqlParameter("@usuario", SqlDbType.VarChar, 15);
            parametroUsuario.Value = usuario;

            SqlParameter parametroIdMinuta = new SqlParameter("@idMinuta", SqlDbType.Int);
            parametroIdMinuta.Direction = ParameterDirection.Output;

            List<SqlParameter> parametros = new List<SqlParameter>() { parametroUsuario, parametroIdMinuta };

            DBAccess.executeSP("grabarMinuta", parametros);

            return Convert.ToInt32(parametroIdMinuta.Value);
        }

        public static void grabarItemMinuta(int idMinuta, string descripcion, string tipo)
        {
            SqlParameter parametroIdMinuta = new SqlParameter("@idMinuta", SqlDbType.Int);
            parametroIdMinuta.Value = idMinuta;

            SqlParameter parametroDescripcion = new SqlParameter("@descripcion", SqlDbType.VarChar, 100);
            parametroDescripcion.Value = descripcion;

            SqlParameter parametroTipo = new SqlParameter("@tipo", SqlDbType.VarChar, 15);
            parametroTipo.Value = tipo;

            List<SqlParameter> parametros = new List<SqlParameter>() { parametroIdMinuta, parametroDescripcion, parametroTipo };

            DBAccess.executeSP("grabarItemMinuta", parametros);
        }
    }
}
