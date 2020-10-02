namespace MinuteroWeb.Views.Models
{
    public class MinutasIndexViewModel
    {
        public EstadoViewModel estado { get; set; }
        public GeneracionMinutaViewModel generacionMinuta { get; set; }
        public string getMinutaURL { get; set; }
        public string getUsuariosCopadosURL { get; set; }
        public string getUsuariosEnFaltaURL { get; set; }
        public string sendMinutaURL { get; set; }
        public string sendFantasmaURL { get; set; }
        public string marcarSendLaterURL { get; set; }
        public string marcarSickURL { get; set; }
        public string marcarLicenseURL { get; set; }
        public string marcarHolidaysURL { get; set; }
        public string marcarStudyDayURL { get; set; }


        public bool usuarioLoggeadoEsEncargado
        {
            get
            {
                return this.estado.usuarioLoggeadoEsEncargado;
            }
        }
    }
}