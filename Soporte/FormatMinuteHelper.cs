using Dominio;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soporte
{
    public static class FormatMinuteHelper
    {

        public static string formatMinute(List<ItemMinuta> itemsMinuta, string nombreEncargado = "", string linkToSharepoint = "")
        {
            string minutaConFormato = "";

            // Si estamos armando la minuta final tenemos el nombre del encargado -> necesitamos la cabecera de la minuta
            if(nombreEncargado != "")
                minutaConFormato = getCabeceraMinuta(nombreEncargado, linkToSharepoint);

            // Agrupamos por usuario
            List<IGrouping<string, ItemMinuta>> itemsMinutaAgrupadoPorUsuario = itemsMinuta.GroupBy(itemMinuta => 
                                                                                            itemMinuta.usuario.nombreAMostrar).ToList();
            // Agregamos el html para cada usuario
            itemsMinutaAgrupadoPorUsuario.ForEach(itemAgrupado => minutaConFormato += getHTMLPorCadaUsuario(itemAgrupado));

            return minutaConFormato;
        }

        private static string getHTMLPorCadaUsuario(IGrouping<string, ItemMinuta> itemAgrupado)
        {
            // Agregamos el nombre del usuario y comienzo de la lista
            string itemsMinutaPorCadaUsuario = String.Format("<br> <b> <span style=\"color:#2E74B5;\"> {0}  </span> </b> <ul>", itemAgrupado.Key);

            // Agrupamos por tipo de item de minuta
            List<IGrouping<ItemMinuta.tipoItem, ItemMinuta>> itemsDeUsuarioAgrupadoPorTipo = itemAgrupado.GroupBy(item => 
                                                                                                          item.tipo).ToList();
            // Agregamos el html para cada tipo de item de minuta
            itemsDeUsuarioAgrupadoPorTipo.ForEach(itemAgrupadoPorTipo => 
                                                  itemsMinutaPorCadaUsuario += getHTMLPorCadaTipoDeItem(itemAgrupadoPorTipo, itemsDeUsuarioAgrupadoPorTipo.Count));
            
            // Cerramos la lista
            itemsMinutaPorCadaUsuario += "</ul>";

            return itemsMinutaPorCadaUsuario;
        }

        private static string getHTMLPorCadaTipoDeItem(IGrouping<ItemMinuta.tipoItem, ItemMinuta> itemAgrupadoPorTipo, int cantidadTiposDeItem)
        {
            string itemsMinutaPorCadaTipo;

            if (!Dominio.ItemMinuta.esTipoItemEspecial(itemAgrupadoPorTipo.First()) || cantidadTiposDeItem > 1)
            {
                // Agregamos el tipo
                itemsMinutaPorCadaTipo = String.Format("<li> {0} <ul>", ItemMinuta.getDescripcionForKey(itemAgrupadoPorTipo.Key));

                // Agregamos todas las descripciones
                foreach (var itemMinuta in itemAgrupadoPorTipo)
                    itemsMinutaPorCadaTipo += String.Format("<li> {0} </li>", itemMinuta.descripcion);


                // Cerramos la lista de items de ese tipo.
                itemsMinutaPorCadaTipo += "</ul> </li>";
            }
            else
            {
                // Si entramos en este caso es uno de los tipos especiales (holidays, later, sick) entonces no hay otro nivel de indentacion.
                itemsMinutaPorCadaTipo = String.Format("<li> {0} </li>", itemAgrupadoPorTipo.First().descripcion);
            }

            return itemsMinutaPorCadaTipo;
        }

        private static string getCabeceraMinuta(string nombreEncargado, string pathToSharepoint)
        {
            string fechaActual = DateTime.Today.ToString("MMMM dd", CultureInfo.InvariantCulture);
            string sufijoFecha = getSuffix(DateTime.Today);
            string saludoInicial = "Good morning, <br /> <br /> Please find below today’s minutes. <br /> <br /> ";
            string linkToSharepointMsg = String.Format("<a href=\"{0}\" > {0} </a> <br /> <br />", pathToSharepoint);
            string fechaConFormato = String.Format(" <span style=\"font-size:16.0pt;color:#2E74B5;\"> {0} <sup> {1} </sup> </span> ",
                                                    fechaActual,sufijoFecha);

            string cabeceraMinuta = String.Format(" {0} {1} Have a great day, <br /> {2} <br /> <br /> {3} <br /> <br />",
                                                  saludoInicial, linkToSharepointMsg, nombreEncargado, fechaConFormato);

            return cabeceraMinuta;
        }

        private static string getSuffix(DateTime date)
        {
            string suffix = "";

            if (new[] { 11, 12, 13 }.Contains(date.Day))
            {
                suffix = "th";
            }
            else if (date.Day % 10 == 1)
            {
                suffix = "st";
            }
            else if (date.Day % 10 == 2)
            {
                suffix = "nd";
            }
            else if (date.Day % 10 == 3)
            {
                suffix = "rd";
            }
            else
            {
                suffix = "th";
            }
            return suffix;
        }

    }
}
