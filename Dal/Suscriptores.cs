using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    public static class Suscriptores
    {
        public static DataTable getSuscriptores()
        {
            // El retorno de esta view es: Codigo y Descripcion
            return DBAccess.executeQueryToGetView("suscriptoresMinuta");
        }
    }
}
