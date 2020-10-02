using Dominio;
using MinuteroWeb.Views.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MinuteroWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            LoginViewModel login = new LoginViewModel();
            login.fraseDelDia = "- No estaba dormido... ESTABA EBRIO!!!     - Carita Triste!     ";

            return View(login);
        }

        public ActionResult Login(string userName)
        {
            // Validamos usuario
            if (!esUsuarioValido(userName))
                return View("~/Views/Shared/Error.cshtml", new UsuarioInvalidoException());

            // Redirigimos al index ya loggeados.
            string url = Url.Action("Index", "Minutas", new { usuarioLoggeado = userName });
            return Redirect(url);
        }

        private static bool esUsuarioValido(string userName)
        {
            return ManejoDeUsuarios.getListaUsuarios().Exists(usuario => usuario.codigo.Trim().Equals(userName));
        }
    }
}
