using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MinuteroWeb.Views.Models
{
    public class EstadoViewModel
    {
        public string usuarioLoggeado { get; set; }
        public string encargado { get; set; }
        public string codigoEncargado { get; set; }

        public bool usuarioLoggeadoEsEncargado
        {
            get
            {
                return this.codigoEncargado == this.usuarioLoggeado;
            }
        }
    }
}