using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using LibControlSistematico;
using FormularioLectorCode;

namespace ControlSistematicoBobinas
{
    public partial class ObservacionesDia : Form
    {
        ConectorBaseDeDatos consultador;

        public ObservacionesDia(ref ConectorBaseDeDatos hacedor)
        {
            InitializeComponent();
            consultador = hacedor;
            dataGridView1.Font = new Font("Tahoma", 20F, GraphicsUnit.Pixel);
            dataGridView1.DataSource = consultador.Dar_BSource();
            consultador.getObservacionesDia();
            this.ajustarResolucion();
        }

        private void ObservacionesDia_Load(object sender, EventArgs e)
        {

        }

        private void ajustarResolucion()
        {
            FormResizer objFormResizer = new FormResizer();

            int Width = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Size.Width;
            int Height = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Size.Height;
            string dim = Width.ToString() + "x" + Height.ToString();

            switch (dim)
            {
                case "800x600":
                    Height += 90;
                    Width += 315;
                    break;
                case "1024x768":
                    Height -= 60;
                    Width += 80;
                    break;
                case "1366x768":
                    Height -= 60;
                    Width -= 260;
                    break;
            };
            objFormResizer.ResizeForm(this, Height, Width);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
