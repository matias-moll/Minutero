using Dominio;
using MinuteroWeb.Views.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MinuteroWeb.Controllers
{
    public class MinutasController : Controller
    {
        public ActionResult Index(string usuarioLoggeado)
        {
            try
            {
                MinutasIndexViewModel minutasIndex = new MinutasIndexViewModel();

                EstadoViewModel estado = new EstadoViewModel();
                estado.encargado = Dominio.ManejoDeUsuarios.getEncargado().Trim();
                estado.codigoEncargado = Dominio.ManejoDeUsuarios.getCodigoEncargado().Trim();
                estado.usuarioLoggeado = usuarioLoggeado;

                GeneracionMinutaViewModel generacionMinuta = new GeneracionMinutaViewModel();
                minutasIndex.generacionMinuta = generacionMinuta;
                minutasIndex.estado = estado;

                fillViewModelURLs(minutasIndex);

                return View(minutasIndex);
            }
            catch (Exception ex)
            {
                return View("~/Views/Shared/Error.cshtml", ex);
            }
        }

        private void fillViewModelURLs(MinutasIndexViewModel minutasIndex)
        {
            minutasIndex.getMinutaURL = Url.Action("GetVistaPreviaMinuta", "Minutas", new { codigoEncargado = minutasIndex.estado.codigoEncargado });
            minutasIndex.getUsuariosCopadosURL = Url.Action("GetUsuariosCopados", "Minutas");
            minutasIndex.getUsuariosEnFaltaURL = Url.Action("GetUsuariosEnFalta", "Minutas");
            minutasIndex.sendMinutaURL = Url.Action("SendMinuta", "Minutas", new { codigoEncargado = minutasIndex.estado.codigoEncargado });
            minutasIndex.sendFantasmaURL = Url.Action("SendFantasma", "Minutas", new { codigoEncargado = minutasIndex.estado.codigoEncargado });
            minutasIndex.marcarSendLaterURL = Url.Action("MarcarSendLater", "Minutas", new { codigoUsuario = "codigoUsuarioValue" });
            minutasIndex.marcarStudyDayURL = Url.Action("MarcarStudyDay", "Minutas", new { codigoUsuario = "codigoUsuarioValue" });
            minutasIndex.marcarSickURL = Url.Action("MarcarSick", "Minutas", new { codigoUsuario = "codigoUsuarioValue" });
            minutasIndex.marcarLicenseURL = Url.Action("MarcarLicense", "Minutas", new { codigoUsuario = "codigoUsuarioValue" });
            minutasIndex.marcarHolidaysURL = Url.Action("MarcarHolidays", "Minutas", new { codigoUsuario = "codigoUsuarioValue" });
        }

        public JsonResult GetUsuariosCopados()
        {
            List<Usuario> usuariosCopados = Dominio.ManejoDeUsuarios.getUsuariosQueEnviaronSuMinuta();
            return Json(usuariosCopados, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetUsuariosEnFalta()
        {
            List<Usuario> usuariosEnFalta = Dominio.ManejoDeUsuarios.getUsuariosQueNoEnviaronSuMinuta();
            return Json(usuariosEnFalta, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetVistaPreviaMinuta(string codigoEncargado)
        {
            // Obtenemos el emisor y los destinatarios
            Dominio.Usuario encargado = Dominio.ManejoDeUsuarios.getUsuarioFromCodigo(codigoEncargado);

            List<Dominio.ItemMinuta> minuta = Dominio.ManejoDeMinutas.getMinutaActual();
            string linkToSharepoint = ""; //Dominio.ManejoDeConfiguraciones.getLinkToSharepoint();
            string resultado = Soporte.FormatMinuteHelper.formatMinute(minuta, encargado.nombreAMostrar, linkToSharepoint);
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void SendMinuta(string codigoEncargado)
        {
            // Obtenemos el emisor y los destinatarios
            Dominio.Usuario encargado = Dominio.ManejoDeUsuarios.getUsuarioFromCodigo(codigoEncargado);
            string emisor = encargado.mail;
            List<string> destinatarios = Dominio.ManejoDeSuscriptores.getSuscriptores();

            // Volvemos a armar la minuta para enviarla del lado del servidor para evitar pasarla desde el cliente.
            List<Dominio.ItemMinuta> minuta = Dominio.ManejoDeMinutas.getMinutaActual();
            string linkToSharepoint = ""; //Dominio.ManejoDeConfiguraciones.getLinkToSharepoint();
            string resultado = Soporte.FormatMinuteHelper.formatMinute(minuta, encargado.nombreAMostrar, linkToSharepoint);

            Soporte.MailHelper.SendMail(destinatarios, "Meeting Minutes", resultado);
        }

        [HttpPost]
        public void SendFantasma(string codigoEncargado)
        {
            List<Usuario> usuariosEnFalta = Dominio.ManejoDeUsuarios.getUsuariosQueNoEnviaronSuMinuta();
            // Mappeamos la lista de usuarios a lista de mails destinatarios del fantasma.
            Soporte.MailHelper.SendFantasma(usuariosEnFalta.Select(unUsuario => unUsuario.mail).ToList(),
                                            Server.MapPath("~/Images/MinutasOrDie.jpg"));

        }

        [HttpPost]
        public void MarcarSendLater(string codigoUsuario)
        {
            List<ItemMinuta> minuta = armarMinutaConItem(() => Dominio.ItemMinuta.getItemMinutaSendLater());

            Dominio.ManejoDeMinutas.grabarMinuta(minuta, codigoUsuario);
        }

        [HttpPost]
        public void MarcarStudyDay(string codigoUsuario)
        {
            List<ItemMinuta> minuta = armarMinutaConItem(() => Dominio.ItemMinuta.getItemStudyDay());

            Dominio.ManejoDeMinutas.grabarMinuta(minuta, codigoUsuario);
        }

        [HttpPost]
        public void MarcarSick(string codigoUsuario)
        {
            List<ItemMinuta> minuta = armarMinutaConItem(() => Dominio.ItemMinuta.getItemMinutaSick());

            Dominio.ManejoDeMinutas.grabarMinuta(minuta, codigoUsuario);
        }

        [HttpPost]
        public void MarcarLicense(string codigoUsuario)
        {
            List<ItemMinuta> minuta = armarMinutaConItem(() => Dominio.ItemMinuta.getItemMinutaLicense());

            Dominio.ManejoDeMinutas.grabarMinuta(minuta, codigoUsuario);
        }

        [HttpPost]
        public void MarcarHolidays(string codigoUsuario)
        {
            List<ItemMinuta> minuta = armarMinutaConItem(() => Dominio.ItemMinuta.getItemMinutaHolidays());

            Dominio.ManejoDeMinutas.grabarMinuta(minuta, codigoUsuario);
        }

        private static List<ItemMinuta> armarMinutaConItem(Func<ItemMinuta> obtenerItemMinutaMethod)
        {
            List<ItemMinuta> minuta = new List<ItemMinuta>();

            ItemMinuta itemMinutaSendLater = obtenerItemMinutaMethod();
            minuta.Add(itemMinutaSendLater);

            return minuta;
        }
    }
}
