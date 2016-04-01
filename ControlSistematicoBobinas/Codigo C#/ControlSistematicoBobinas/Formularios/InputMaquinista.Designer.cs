namespace ControlSistematicoBobinas
{
    partial class InputMaquinista
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
            this.lblpregunta = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.cmbMaquinista = new System.Windows.Forms.ComboBox();
            this.data = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.data)).BeginInit();
            this.SuspendLayout();
            // 
            // lblpregunta
            // 
            this.lblpregunta.AutoSize = true;
            this.lblpregunta.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblpregunta.Location = new System.Drawing.Point(210, 136);
            this.lblpregunta.Name = "lblpregunta";
            this.lblpregunta.Size = new System.Drawing.Size(961, 73);
            this.lblpregunta.TabIndex = 1;
            this.lblpregunta.Text = "Ingrese nombre del Maquinista:";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(224, 439);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(698, 78);
            this.button1.TabIndex = 3;
            this.button1.Text = "Ingresar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cmbMaquinista
            // 
            this.cmbMaquinista.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMaquinista.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbMaquinista.FormattingEnabled = true;
            this.cmbMaquinista.Location = new System.Drawing.Point(224, 270);
            this.cmbMaquinista.Name = "cmbMaquinista";
            this.cmbMaquinista.Size = new System.Drawing.Size(698, 81);
            this.cmbMaquinista.TabIndex = 4;
            // 
            // data
            // 
            this.data.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.data.Location = new System.Drawing.Point(1, 12);
            this.data.Name = "data";
            this.data.Size = new System.Drawing.Size(116, 42);
            this.data.TabIndex = 39;
            this.data.Visible = false;
            // 
            // InputMaquinista
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(967, 661);
            this.Controls.Add(this.data);
            this.Controls.Add(this.cmbMaquinista);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lblpregunta);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "InputMaquinista";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Nombre Operador";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.InputMaquinista_FormClosed);
            this.Load += new System.EventHandler(this.InputMaquinista_Load);
            this.Shown += new System.EventHandler(this.InputMaquinista_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.data)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblpregunta;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox cmbMaquinista;
        private System.Windows.Forms.DataGridView data;
    }
}