using System;
using System.IO;

namespace Soporte
{
    public static class UsuarioActualHelper
    {
        public static string GetUsuarioActual()
        {
            string pathToConfig = getPathToArchivoUsuario();

            // Recuperamos el usuario
            string[] lines = System.IO.File.ReadAllLines(pathToConfig);
            return lines[0];
        }

        public static void SaveUsuarioActual(string usuario)
        {
            string pathToConfig = getPathToArchivoUsuario();

            // Truncamos el contenido y guardamos el nuevo usuario.
            FileInfo fi = new FileInfo(pathToConfig);
            using (TextWriter txtWriter = new StreamWriter(fi.Open(FileMode.Truncate)))
            {
                txtWriter.Write(usuario);
            }
        }

        private static string getPathToArchivoUsuario()
        {
            // Recuperamos el path al archivo
            string pathBase = AppDomain.CurrentDomain.BaseDirectory;
            pathBase = pathBase.Substring(0, pathBase.IndexOf("\\Minutero") + "\\Minutero".Length);
            
            string pathToConfig = pathBase + "\\Usuario.txt";

            // Validamos.
            if (!System.IO.File.Exists(pathToConfig))
                throw new Exception("No se encontró el archivo de configuración de usuario");

            return pathToConfig;
        }
    }
}
