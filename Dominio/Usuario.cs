using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Usuario
    {
        public string codigo { get; set; }
        public string nombreAMostrar { get; set; }
        public bool esEncargado { get; set; }
        public string mail { get; set; }

        public Usuario(string codigo, string nombreAMostrar, string mail = "", bool esEncargado = false)
        {
            this.codigo = codigo;
            this.nombreAMostrar = nombreAMostrar;
            this.mail = mail;
            this.esEncargado = esEncargado;
        }

        public Usuario()
        {

        }
    }
}
