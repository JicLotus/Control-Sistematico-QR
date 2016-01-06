namespace ControlSistematicoBobinas.Formularios
{
    partial class PanelInicial
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PanelInicial));
            this.label1 = new System.Windows.Forms.Label();
            this.cmbSrv = new System.Windows.Forms.ComboBox();
            this.btOperador = new System.Windows.Forms.Button();
            this.BtAdmin = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(64, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 15);
            this.label1.TabIndex = 8;
            this.label1.Text = "Servidor:";
            // 
            // cmbSrv
            // 
            this.cmbSrv.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSrv.FormattingEnabled = true;
            this.cmbSrv.Location = new System.Drawing.Point(66, 47);
            this.cmbSrv.Name = "cmbSrv";
            this.cmbSrv.Size = new System.Drawing.Size(162, 24);
            this.cmbSrv.TabIndex = 7;
            // 
            // btOperador
            // 
            this.btOperador.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btOperador.Location = new System.Drawing.Point(67, 173);
            this.btOperador.Name = "btOperador";
            this.btOperador.Size = new System.Drawing.Size(162, 48);
            this.btOperador.TabIndex = 6;
            this.btOperador.Text = "Formulario";
            this.btOperador.UseVisualStyleBackColor = true;
            // 
            // BtAdmin
            // 
            this.BtAdmin.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtAdmin.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtAdmin.Location = new System.Drawing.Point(67, 97);
            this.BtAdmin.Name = "BtAdmin";
            this.BtAdmin.Size = new System.Drawing.Size(162, 48);
            this.BtAdmin.TabIndex = 5;
            this.BtAdmin.Text = "Administrador";
            this.BtAdmin.UseVisualStyleBackColor = true;
            // 
            // PanelInicial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(293, 249);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbSrv);
            this.Controls.Add(this.btOperador);
            this.Controls.Add(this.BtAdmin);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PanelInicial";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.PanelInicial_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbSrv;
        private System.Windows.Forms.Button btOperador;
        private System.Windows.Forms.Button BtAdmin;
    }
}