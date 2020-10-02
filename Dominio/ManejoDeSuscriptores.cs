using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public static class ManejoDeSuscriptores
    {
        public static List<string> getSuscriptores()
        {
            DataTable dataTableSuscriptores = Dal.Suscriptores.getSuscriptores();
            if (dataTableSuscriptores.Rows.Count > 0)
                return getListaSuscriptoresFromDataTable(dataTableSuscriptores);
            else
                throw new Exception("No se encontró ningún suscriptor para enviarle la minuta.");
        }

        private static List<string> getListaSuscriptoresFromDataTable(DataTable dataTableSuscriptores)
        {
            List<string> suscriptores = new List<string>();
            foreach (DataRow drSuscriptore in dataTableSuscriptores.Rows)
                suscriptores.Add(drSuscriptore["Mail"].ToString());
            
            return suscriptores;
        }

    }
}
