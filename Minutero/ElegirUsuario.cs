using Dominio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;

namespace Minutero
{
    internal partial class ElegirUsuario : Form
    {
        public ElegirUsuario()
        {
            InitializeComponent();
        }

        private void ElegirUsuario_Load(object sender, EventArgs e)
        {
            List<Usuario> usuarios = ManejoDeUsuarios.getListaUsuarios();
            usuarios = usuarios.OrderBy(usuario => usuario.nombreAMostrar).ToList();
            cbUsuarios.DataSource = usuarios;
            cbUsuarios.ValueMember = "codigo";
            cbUsuarios.DisplayMember = "nombreAMostrar";
        }

        // Operaciones del formulario

        /// <summary>
        /// Cancela la modificacion
        /// </summary>
        private void cmdCancela_Click(object sender, EventArgs e)
        {
            Close();
            DialogResult= DialogResult.Cancel;
        }

        /// <summary>
        /// Confirmaron la operacion
        /// </summary>
        private void cmdConfirma_Click(object sender, EventArgs e)
        {
            Soporte.UsuarioActualHelper.SaveUsuarioActual(cbUsuarios.SelectedValue.ToString());
             
            // Terminamos 
            Close();
            DialogResult= DialogResult.OK;
        }


        // Propiedades del formulario

        public string Usuario
        {
            get { return cbUsuarios.SelectedValue.ToString(); }
            set { cbUsuarios.SelectedValue = value; }
        }
     
    }
}