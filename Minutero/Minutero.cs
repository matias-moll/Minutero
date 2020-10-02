using Dominio;
using Minutero.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using TNGS.NetControls;

namespace Minutero
{
    public partial class Minutero : Form
    {
        #region Miembros

        private int minutos;
        int diaMinutaEnviada;
        private string m_usuarioActual;
        List<ItemMinuta> minutaDeAyer;
        List<ItemMinuta> minutaDeHoy;
        List<TextEdit> workedOn;
        List<TextEdit> workingOn;
        List<TextEdit> impediments;
        private NotifyIcon niSysTray;

        private string usuarioActual
        {
            get { return m_usuarioActual.Trim(); }
            set { m_usuarioActual =  value; }
        }

        #endregion

        // Constructor
        public Minutero()
        {
            InitializeComponent();
            workedOn = new List<TextEdit>();
            workingOn = new List<TextEdit>();
            impediments = new List<TextEdit>();
        }

        // Eventos
        private void Minutero_Load(object sender, EventArgs e)
        {
            // Inicializamos miembros
            minutos = 0;
            tmrNotifications.Enabled = true;
            diaMinutaEnviada = 0;

            // Agregamos los textedits a las listas para manejo generico.
            agregarEditsAListaWorkedOn();
            agregarEditsAListaWorkingOn();
            impediments.Add(teImpediments1);
            impediments.Add(teImpediments2);

            estadoInicial();
        }

        private void proponerWorkedOnUsandoMinutaDeAyer(List<ItemMinuta> itemsMinuta)
        {
            // Copiamos los items de working on de ayer a la lista de pantalla de worked on de hoy.
            for (int iterador = 0; iterador < itemsMinuta.Count(); iterador++)
                workedOn[iterador].Text = itemsMinuta[iterador].descripcion;
        }

        private void Mainframe_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                // Lo minimizaron. Nos ocultamos
                Visible = false;
                setUpTaskBarIcon();
                return;
            }

            if (WindowState == FormWindowState.Normal)
            {
                // Lo normalizaron. Nos mostramos
                Visible = true;
                killTaskBarIcon();
                return;
            }
        }

        private void tmrNotifications_Tick(object sender, EventArgs e)
        {
            // Si la minuta ya fue enviada hoy salimos
            if (diaMinutaEnviada == DateTime.Today.Day) return;

            // Si no son las 9 am no se avisa nada, todos duermen todavia!
            if (DateTime.Now.Hour < 9) return;

            // Si no pasaron 20 minutos del ultimo aviso salimos
            if (++minutos < 20) return;

            // Recargamos la lista
            tmrNotifications.Enabled = false;
            // Si la ventana no esta minimizada tenemos que crear otro icon para mostrar la notificacion.
            if (WindowState == FormWindowState.Normal)
                createNotifyIconShowNotificationAndDestroyIt();
            else
                niSysTray.ShowBalloonTip(10000);

            minutos = 0;
            tmrNotifications.Enabled = true;

        }

        private void niSysTray_DoubleClick(object sender, EventArgs e)
        {
            Visible = true;
            WindowState = FormWindowState.Normal;
        }

        private void textEdits_DoubleClick(object sender, EventArgs e)
        {
            ((TextEdit)sender).SelectionStart = 0;
            ((TextEdit)sender).SelectionLength = ((TextEdit)sender).Text.Length;
        }

        private void Minutero_FormClosing(object sender, FormClosingEventArgs e)
        {
            killTaskBarIcon();
        }

        // Operaciones
        private void gbCopy_Click(object sender, EventArgs e)
        {
            for (int iterador = 0; iterador < 8; iterador++)
                workingOn[iterador].Text = workedOn[iterador].Text;
        }

        private void gbPreview_Click(object sender, EventArgs e)
        {
            cargarMinutaDeHoy();
            wbVistaPrevia.DocumentText = Soporte.FormatMinuteHelper.formatMinute(minutaDeHoy);
            gbSend.Enabled = true;

        }

        private void gbSelectUser_Click(object sender, EventArgs e)
        {
            ElegirUsuario frmElegirUsuario = new ElegirUsuario();
            frmElegirUsuario.ShowDialog(this);

            if (frmElegirUsuario.DialogResult == System.Windows.Forms.DialogResult.OK)
                estadoInicial();
        }

        private void gbSend_Click(object sender, EventArgs e)
        {
            try
            {
                Dominio.ManejoDeMinutas.grabarMinuta(minutaDeHoy, usuarioActual);
                MessageBox.Show("¡Su minuta fue enviada exitosamente!");

                diaMinutaEnviada = DateTime.Today.Day;

                estadoInicial();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tipo de error: " + ex.GetType() + " " + ex.Message);
            }
        }

        private void gbReset_Click(object sender, EventArgs e)
        {
            estadoInicial();
        }


        // Metodos 
        private void estadoInicial()
        {
            limpiarControles();

            usuarioActual = Soporte.UsuarioActualHelper.GetUsuarioActual();

            //Muestra el nombre completo del usuario seleccionado.
            if (!string.IsNullOrWhiteSpace(usuarioActual))
            {
                lblSelectedUser.Text = ManejoDeUsuarios.getUsuarioFromCodigo(usuarioActual).nombreAMostrar;
            }

            // Se obtiene la ultima minuta valida.
            minutaDeAyer = Dominio.ManejoDeMinutas.getLastValidItemsMinutaPorUsuario(usuarioActual);

            wbMinutaDeAyer.DocumentText = Soporte.FormatMinuteHelper.formatMinute(minutaDeAyer);

            proponerWorkedOnUsandoMinutaDeAyer(minutaDeAyer.Where(itemMinuta => itemMinuta.tipo == ItemMinuta.tipoItem.WorkingOn).ToList());
            teImpediments1.Text = "None";
            gbSend.Enabled = false;
        }

        private void limpiarControles()
        {
            limpiarEdits(workedOn);
            limpiarEdits(workingOn);
            limpiarEdits(impediments);

            wbVistaPrevia.DocumentText = "";
        }

        private void limpiarEdits(List<TextEdit> listToClean)
        {
            listToClean.ForEach(edit => edit.Text = "");
        }

        private void cargarMinutaDeHoy()
        {
            string codigoUsuarioActual = Soporte.UsuarioActualHelper.GetUsuarioActual();

            Usuario usuarioActual = Dominio.ManejoDeUsuarios.getUsuarioFromCodigo(codigoUsuarioActual);

            minutaDeHoy = new List<ItemMinuta>();

            filtrarVacios(workedOn).ForEach(itemWorkedOn =>
                     minutaDeHoy.Add(new ItemMinuta(usuarioActual, ItemMinuta.tipoItem.WorkedOn, itemWorkedOn.Text)));
            filtrarVacios(workingOn).ForEach(itemWorkingOn =>
                    minutaDeHoy.Add(new ItemMinuta(usuarioActual, ItemMinuta.tipoItem.WorkingOn, itemWorkingOn.Text)));
            filtrarVacios(impediments).ForEach(itemImpediment =>
                    minutaDeHoy.Add(new ItemMinuta(usuarioActual, ItemMinuta.tipoItem.Impediments, itemImpediment.Text)));
        }

        private List<TextEdit> filtrarVacios(List<TextEdit> listaAFiltrar)
        {
            return listaAFiltrar.Where(edit => edit.Text.Trim() != "").ToList();
        }

        private void setUpTaskBarIcon()
        {
            niSysTray = new NotifyIcon();
            this.niSysTray.DoubleClick += new System.EventHandler(this.niSysTray_DoubleClick);
            this.niSysTray.BalloonTipClicked += new System.EventHandler(this.niSysTray_DoubleClick);
            cargarNotifyIcon(niSysTray);
        }

        private void cargarNotifyIcon(NotifyIcon notifyIcon)
        {
            notifyIcon.Text = "notifyIcon1";
            notifyIcon.Visible = true;
            notifyIcon.Icon = Resources.TrayIcon;
            notifyIcon.BalloonTipTitle = "Minuta";
            notifyIcon.BalloonTipText = "No te olvides de la Minuta!";
            notifyIcon.BalloonTipIcon = ToolTipIcon.Warning;
            notifyIcon.Text = "Minutero";
        }

        private void killTaskBarIcon()
        {
            if (niSysTray != null)
            {
                niSysTray.Visible = false;
                niSysTray.Dispose();
            }
        }

        private void createNotifyIconShowNotificationAndDestroyIt()
        {
            NotifyIcon tempIconForNotification = new NotifyIcon();
            tempIconForNotification = new NotifyIcon();
            cargarNotifyIcon(tempIconForNotification);
            tempIconForNotification.ShowBalloonTip(10000);
            tempIconForNotification.BalloonTipClosed += (sender, e) => { var thisIcon = (NotifyIcon)sender; thisIcon.Visible = false; thisIcon.Dispose(); };
        }

        private void agregarEditsAListaWorkingOn()
        {
            workingOn.Add(teWorkingOn1);
            workingOn.Add(teWorkingOn2);
            workingOn.Add(teWorkingOn3);
            workingOn.Add(teWorkingOn4);
            workingOn.Add(teWorkingOn5);
            workingOn.Add(teWorkingOn6);
            workingOn.Add(teWorkingOn7);
            workingOn.Add(teWorkingOn8);
        }

        private void agregarEditsAListaWorkedOn()
        {
            workedOn.Add(teWorkedOn1);
            workedOn.Add(teWorkedOn2);
            workedOn.Add(teWorkedOn3);
            workedOn.Add(teWorkedOn4);
            workedOn.Add(teWorkedOn5);
            workedOn.Add(teWorkedOn6);
            workedOn.Add(teWorkedOn7);
            workedOn.Add(teWorkedOn8);
        }

    }
}
