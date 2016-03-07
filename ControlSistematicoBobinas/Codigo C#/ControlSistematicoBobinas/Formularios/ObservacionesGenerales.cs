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
    public partial class observacionesGenerales : Form
    {
        private string nombreMaquinista;
        private  ConectorBaseDeDatos consultador;

        public observacionesGenerales(ref ConectorBaseDeDatos hacedor, string maquinistaParam)
        {
            InitializeComponent();
            consultador = hacedor;
            nombreMaquinista = maquinistaParam;
            this.ajustarResolucion();
        }

        private void obervacionesGenerales_Load(object sender, EventArgs e)
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
            DateTime finObs = DateTime.Now;
            DateTime Hoy = DateTime.Today;
            string fecha_actual = Hoy.ToString("yyyy-MM-dd");
            consultador.agregarObservacionGeneral(textBox1.Text, fecha_actual, finObs.ToString("H:mm"), nombreMaquinista);
            this.Close();
        }

    }
}
