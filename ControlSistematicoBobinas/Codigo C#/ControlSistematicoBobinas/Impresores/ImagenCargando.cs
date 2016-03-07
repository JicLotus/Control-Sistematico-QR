using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;

using System.Drawing;

namespace ControlSistematicoBobinas
{
    class ImagenCargando
    {
        PictureBox imagen_de_carga = new PictureBox();

        public ImagenCargando()
        {

            Form formulario = new Form();
            formulario.Visible = true;
            formulario.Opacity = 0.83;
            
            formulario.FormBorderStyle = 0;
            //formulario.BackColor = Color.Transparent;

            /*
            imagen_de_carga.Image = ControlSistematicoBobinas.Properties.Resources.gifdecarga;
            
            imagen_de_carga.Location = new Point(100, 100);

            imagen_de_carga.Visible = true;
            imagen_de_carga.Show();

            formulario.Controls.Add(imagen_de_carga);*/
        }

    }
}
