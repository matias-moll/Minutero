using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public static class ManejoDeUsuarios
    {
        public static string getEncargado()
        {
            DataTable dataTableEncargado = Dal.Usuarios.getEncargado();
            if (dataTableEncargado.Rows.Count > 0)
                return dataTableEncargado.Rows[0]["Encargado"].ToString();
            else
                throw new Exception("No se encontró encargado. Es obligatorio que haya un encargado activo siempre");
        }

        public static Usuario getUsuarioFromCodigo(string codigoUsuario)
        {
            DataTable dataTableUsuario = Dal.Usuarios.getUsuarioFromCodigo(codigoUsuario);
            if (dataTableUsuario.Rows.Count > 0)
                return mapUsuario(dataTableUsuario.Rows[0], includeMail:true);
            else
                throw new Exception(String.Format("No se encontró el usuario correspondiente al codigo: {0}.", codigoUsuario));
        }

        public static string getCodigoEncargado()
        {
            DataTable dataTableEncargado = Dal.Usuarios.getCodigoEncargado();
            if (dataTableEncargado.Rows.Count > 0)
                return dataTableEncargado.Rows[0]["Encargado"].ToString();
            else
                throw new Exception("No se encontró encargado. Es obligatorio que haya un encargado activo siempre");
        }

        public static List<Usuario> getListaUsuarios()
        {
            DataTable dataTableUsuarios = Dal.Usuarios.getUsuariosDelSistema();

            return getListaUsuariosFromDataTable(dataTableUsuarios);
        }

        public static List<Usuario> getUsuariosQueNoEnviaronSuMinuta()
        {
            DataTable dataTableUsuarios = Dal.Usuarios.getUsuariosQueNoEnviaronSuMinuta();

            return getListaUsuariosFromDataTable(dataTableUsuarios, includeMail: true);
        }

        public static List<Usuario> getUsuariosQueEnviaronSuMinuta()
        {
            DataTable dataTableUsuarios = Dal.Usuarios.getUsuariosQueEnviaronSuMinuta();

            return getListaUsuariosFromDataTable(dataTableUsuarios, includeMail: true);
        }

        // Este metodo parsea el datatable y retorna una lista de objetos Usuario. Algunas interfaces traen el mail y otras no, por eso el param opcional.
        private static List<Usuario> getListaUsuariosFromDataTable(DataTable dataTableUsuarios, bool includeMail = false)
        {
            List<Usuario> usuarios = new List<Usuario>();
            foreach (DataRow dataRowUsuario in dataTableUsuarios.Rows)
            {
                Usuario usuario = mapUsuario(dataRowUsuario, includeMail);
                usuarios.Add(usuario);
            } 

            return usuarios;
        }

        private static Usuario mapUsuario(DataRow dataRowUsuario, bool includeMail)
        {
            Usuario usuario = new Usuario(dataRowUsuario["Usuario"].ToString(), dataRowUsuario["NombreAMostrar"].ToString());
            usuario.mail = includeMail ? dataRowUsuario["Mail"].ToString() : "";
            return usuario;
        }
    }
}
