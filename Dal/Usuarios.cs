using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    public class Usuarios
    {

        public static DataTable getEncargado()
        {
            string query = "select Encargado = dbo.getEncargado()";
            return DBAccess.executeQuery(query);
        }

        public static DataTable getCodigoEncargado()
        {
            string query = "select Encargado = dbo.getCodigoEncargado()";
            return DBAccess.executeQuery(query);
        }

        public static DataTable getUsuariosDelSistema()
        {
            // El retorno de esta view es: Codigo y Descripcion
            return DBAccess.executeQueryToGetView("usuariosDelSistema");
        }

        public static DataTable getUsuariosQueNoEnviaronSuMinuta()
        {
            // El retorno de esta view es: Usuario, NombreAMostrar, Mail
            return DBAccess.executeQueryToGetView("usuariosQueNoEnviaronSuMinutaHoy");
        }

        public static DataTable getUsuariosQueEnviaronSuMinuta()
        {
            // El retorno de esta view es: Usuario, NombreAMostrar, Mail
            return DBAccess.executeQueryToGetView("usuariosQueEnviaronSuMinutaHoy");
        }


        

        public static DataTable getUsuarioFromCodigo(string codigoUsuario)
        {
            string query = String.Format("execute getUsuarioFromCodigo \'{0}\'", codigoUsuario); 
            return DBAccess.executeQuery(query);
        }
    }
}
