using System;

namespace Dominio
{
    public class ItemMinuta
    {
        public static string SickDescription = "Sick";
        public static string LicenseDescription = "On License";
        public static string StudyDayDescription = "Study Day";
        public static string HolidaysDescription = "On Holidays";
        public static string SendLaterDescription = "Will send it later";

        public enum tipoItem { WorkedOn = 0, WorkingOn = 1, Impediments = 2 };

        public Usuario usuario { get; set; }
        public tipoItem tipo { get; set; }
        public string descripcion { get; set; }

        public ItemMinuta(Usuario usuario, tipoItem tipo, string descripcion)
        {
            this.usuario = usuario;
            this.tipo = tipo;
            this.descripcion = descripcion;
        }

        public ItemMinuta(Usuario usuario, string tipo, string descripcion)
            : this(usuario, (ItemMinuta.tipoItem)Enum.Parse(typeof(ItemMinuta.tipoItem), tipo, true), descripcion)
        {
        }

        public static string getDescripcionForKey(tipoItem tipo)
        {
            if (tipo == tipoItem.WorkingOn)
                return "Working on";
            else if (tipo == tipoItem.WorkedOn)
                return "Worked on";
            else return tipo.ToString();
        }

        public static ItemMinuta getItemMinutaSendLater()
        {
            return new ItemMinuta(new Usuario(), ItemMinuta.tipoItem.WorkedOn, SendLaterDescription);
        }

        public static ItemMinuta getItemStudyDay()
        {
            return new ItemMinuta(new Usuario(), ItemMinuta.tipoItem.WorkedOn, StudyDayDescription);
        }

        public static ItemMinuta getItemMinutaSick()
        {
            return new ItemMinuta(new Usuario(), ItemMinuta.tipoItem.WorkedOn, SickDescription);
        }

        public static ItemMinuta getItemMinutaLicense()
        {
            return new ItemMinuta(new Usuario(), ItemMinuta.tipoItem.WorkedOn, LicenseDescription);
        }

        public static ItemMinuta getItemMinutaHolidays()
        {
            return new ItemMinuta(new Usuario(), ItemMinuta.tipoItem.WorkedOn, HolidaysDescription);
        }

        public static bool esTipoItemEspecial(ItemMinuta itemMinuta)
        {
            return itemMinuta.descripcion.Contains(SickDescription) ||
                   itemMinuta.descripcion.Contains(StudyDayDescription) ||
                   itemMinuta.descripcion.Contains(LicenseDescription) ||
                   itemMinuta.descripcion.Contains(HolidaysDescription) ||
                   itemMinuta.descripcion.Contains(SendLaterDescription);
        }

        internal void Save(int idMinuta)
        {
            Dal.Minutas.grabarItemMinuta(idMinuta, this.descripcion, this.tipo.ToString());
        }

    }
}
