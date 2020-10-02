using System.Collections.Generic;
using System.Data;

namespace Dominio
{
    public class ManejoDeMinutas
    {
        public static List<ItemMinuta> getItemsMinutaDeAyerPorUsuario(string usuario)
        {
            // Llamamos a la capa de acceso a datos y convertimos la respuesta a una lista de objetos de nuestro dominio.
            DataTable dtItemsMinuta = Dal.Minutas.getItemsMinutaDeAyerPorUsuario(usuario);
            List<ItemMinuta> itemsMinuta = getListaFromDataTable(dtItemsMinuta);

            return itemsMinuta;
        }

        public static List<ItemMinuta> getLastValidItemsMinutaPorUsuario(string usuario)
        {
            DataTable dtItemsMinuta = Dal.Minutas.getLastValidItemsMinutaPorUsuario(usuario);
            List<ItemMinuta> itemsMinuta = getListaFromDataTable(dtItemsMinuta);

            return itemsMinuta;
        }

        public static List<ItemMinuta> getItemsMinutaDeHoyPorUsuario(string usuario)
        {
            DataTable dtItemsMinuta = Dal.Minutas.getItemsMinutaDeAyerPorUsuario(usuario);
            List<ItemMinuta> itemsMinuta = getListaFromDataTable(dtItemsMinuta);

            return itemsMinuta;
        }

        public static List<ItemMinuta> getMinutaActual()
        {
            DataTable dtItemsMinuta = Dal.Minutas.getMinutaActual();
            List<ItemMinuta> itemsMinuta = getListaFromDataTable(dtItemsMinuta);

            return itemsMinuta;
        }

        public static void grabarMinuta(List<ItemMinuta> itemsMinuta, string usuario)
        {
            int idMinuta = Dal.Minutas.grabarMinuta(usuario);

            itemsMinuta.ForEach(item => item.Save(idMinuta));
        }

        private static List<ItemMinuta> getListaFromDataTable(DataTable dtItemsMinuta)
        {
            List<ItemMinuta> itemsMinuta = new List<ItemMinuta>();
            Usuario usuario;

            foreach (DataRow drItem in dtItemsMinuta.Rows)
            {
                usuario = new Usuario(drItem["Usuario"].ToString(), drItem["NombreAMostrar"].ToString());
                itemsMinuta.Add(new ItemMinuta(usuario,
                                               drItem["Tipo"].ToString(),
                                               drItem["Descripcion"].ToString()));
            }
            return itemsMinuta;
        }
    }
}
