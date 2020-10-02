namespace Minutero
{
    partial class ElegirUsuario
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ElegirUsuario));
            this.xpnlBase = new TNGS.NetControls.XPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.gbCancela = new TNGS.NetControls.GlassButton();
            this.gbConfirma = new TNGS.NetControls.GlassButton();
            this.label11 = new System.Windows.Forms.Label();
            this.cbUsuarios = new System.Windows.Forms.ComboBox();
            this.xpnlBase.SuspendLayout();
            this.SuspendLayout();
            // 
            // xpnlBase
            // 
            this.xpnlBase.BackColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.xpnlBase.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(227)))), ((int)(((byte)(242)))));
            this.xpnlBase.BorderColor = System.Drawing.Color.Black;
            this.xpnlBase.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.xpnlBase.Controls.Add(this.cbUsuarios);
            this.xpnlBase.Controls.Add(this.panel1);
            this.xpnlBase.Controls.Add(this.label1);
            this.xpnlBase.Controls.Add(this.gbCancela);
            this.xpnlBase.Controls.Add(this.gbConfirma);
            this.xpnlBase.Controls.Add(this.label11);
            this.xpnlBase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xpnlBase.Location = new System.Drawing.Point(0, 0);
            this.xpnlBase.Name = "xpnlBase";
            this.xpnlBase.Size = new System.Drawing.Size(476, 165);
            this.xpnlBase.SkinFixed = true;
            this.xpnlBase.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(11, 38);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(450, 2);
            this.panel1.TabIndex = 208;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(182, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 15);
            this.label1.TabIndex = 207;
            this.label1.Text = "Cambio de Usuario";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // gbCancela
            // 
            this.gbCancela.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.gbCancela.BlackBorder = true;
            this.gbCancela.ButtonMode = TNGS.NetControls.GlassButton.GBMode.Flat;
            this.gbCancela.CircleButton = false;
            this.gbCancela.FlatColor = System.Drawing.Color.Red;
            this.gbCancela.FlatDefaultColor = TNGS.NetControls.ColorRuts.ColoresDefault.None;
            this.gbCancela.FlatFontSize = 9;
            this.gbCancela.FlatTextColor = System.Drawing.Color.White;
            this.gbCancela.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.gbCancela.Location = new System.Drawing.Point(293, 125);
            this.gbCancela.Name = "gbCancela";
            this.gbCancela.Size = new System.Drawing.Size(82, 27);
            this.gbCancela.TabIndex = 2;
            this.gbCancela.Text = "Cancelar";
            this.gbCancela.W8Color = System.Drawing.Color.Red;
            this.gbCancela.Click += new System.EventHandler(this.cmdCancela_Click);
            // 
            // gbConfirma
            // 
            this.gbConfirma.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.gbConfirma.BlackBorder = true;
            this.gbConfirma.ButtonMode = TNGS.NetControls.GlassButton.GBMode.Flat;
            this.gbConfirma.CircleButton = false;
            this.gbConfirma.FlatColor = System.Drawing.Color.LimeGreen;
            this.gbConfirma.FlatDefaultColor = TNGS.NetControls.ColorRuts.ColoresDefault.None;
            this.gbConfirma.FlatFontSize = 9;
            this.gbConfirma.FlatTextColor = System.Drawing.Color.White;
            this.gbConfirma.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.gbConfirma.Location = new System.Drawing.Point(381, 125);
            this.gbConfirma.Name = "gbConfirma";
            this.gbConfirma.Size = new System.Drawing.Size(82, 27);
            this.gbConfirma.TabIndex = 1;
            this.gbConfirma.Text = "Aceptar";
            this.gbConfirma.W8Color = System.Drawing.Color.LimeGreen;
            this.gbConfirma.Click += new System.EventHandler(this.cmdConfirma_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(70, 63);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(54, 15);
            this.label11.TabIndex = 201;
            this.label11.Text = "Usuario:";
            // 
            // cbUsuarios
            // 
            this.cbUsuarios.FormattingEnabled = true;
            this.cbUsuarios.Location = new System.Drawing.Point(150, 60);
            this.cbUsuarios.Name = "cbUsuarios";
            this.cbUsuarios.Size = new System.Drawing.Size(225, 23);
            this.cbUsuarios.TabIndex = 209;
            // 
            // ElegirUsuario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(476, 165);
            this.Controls.Add(this.xpnlBase);
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ElegirUsuario";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Cambo de Radio";
            this.Load += new System.EventHandler(this.ElegirUsuario_Load);
            this.xpnlBase.ResumeLayout(false);
            this.xpnlBase.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private TNGS.NetControls.XPanel xpnlBase;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label1;
        private TNGS.NetControls.GlassButton gbCancela;
        private TNGS.NetControls.GlassButton gbConfirma;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cbUsuarios;
    }
}

