using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MinuteroWeb.Controllers
{
    class UsuarioInvalidoException : Exception
    {
        public UsuarioInvalidoException()
            : base("El usuario ingresado no es un usuario valido en la base de datos.")
        {
        }
    }
}
