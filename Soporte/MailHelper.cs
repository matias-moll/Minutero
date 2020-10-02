using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Soporte
{
    public static class MailHelper
    {
        public static void SendMail(List<string> destinatarios, string titulo, string contenidoMail)
        {
            try
            {
                // Creamos el objeto mensaje de mail.
                MailMessage msg = new MailMessage();

                agregarEmisorYDestinatarios("minutero@hexacta.com", destinatarios, msg);

                // Cargamos el subject del mail y el cuerpo, sacados de la pantalla.
                msg.Subject = titulo;
                msg.Body = contenidoMail;
                msg.IsBodyHtml = true;

                SmtpClient client = configureCredentialsAndServer();

                client.Send(msg);
            }
            catch (Exception ex)
            {
                Exception excep = new Exception("Se produjo un error al intentar enviar la minuta", ex);
                throw excep;
            }
        }

        public static void SendFantasma(List<string> destinatarios, string pathToTheImage)
        {
            try
            {
                // Creamos el objeto mensaje de mail.
                MailMessage msg = new MailMessage();

                agregarEmisorYDestinatarios("minutero@hexacta.com", destinatarios, msg);

                // Cargamos el subject del mail y el cuerpo
                msg.Subject = "Minutas or Die";
                msg.IsBodyHtml = true;

                // Agregamos una view alternativa con la imagen inline.
                var inlineLogo = new LinkedResource(pathToTheImage);
                inlineLogo.ContentId = Guid.NewGuid().ToString();

                string body = string.Format(@"<img src=""cid:{0}"" />", inlineLogo.ContentId);

                var view = AlternateView.CreateAlternateViewFromString(body, null, "text/html");
                view.LinkedResources.Add(inlineLogo);
                msg.AlternateViews.Add(view);


                SmtpClient client = configureCredentialsAndServer();

                client.Send(msg);
            }
            catch (Exception ex)
            {
                Exception excep = new Exception("Se produjo un error al intentar enviar a nuestro fantasma", ex);
                throw excep;
            }
        }

        private static void agregarEmisorYDestinatarios(string emisor, List<string> destinatarios, MailMessage msg)
        {
            // Agregamos todos los destinatarios del mail.
            foreach (string destinatario in destinatarios)
                msg.To.Add(new MailAddress(destinatario));

            // Cargamos el emisor
            msg.From = new MailAddress(emisor);
        }

        private static SmtpClient configureCredentialsAndServer()
        {
            // Recuperamos a partir de los aprametros el servidor a utilizar.
            string servidorCorreo = "in-v3.mailjet.com";
            int puerto = 25; 
            SmtpClient client = new SmtpClient(servidorCorreo, puerto);
            client.EnableSsl = true;

            NetworkCredential credentials = new NetworkCredential("5908a6a5bc66857052d07f340d1a01d3", "93c3c91b1065086715584b41b5594df6");
            client.Credentials = credentials;

            return client;
        }
    }
}
