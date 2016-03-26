using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using LibControlSistematico;

using FormularioLectorCode;

namespace ControlSistematicoBobinas
{
    public partial class InputBox : Form
    {

        private Form refPanelInicial;
        ConectorBaseDeDatos consultador;
        string negativo;
        ArchivoIni config;

        public InputBox(string title, ref ConectorBaseDeDatos consult, string negado, ref Form PanelInicial, ArchivoIni configParam)
        {
            this.Text = title;
            negativo = negado;
            consultador = consult;

            InitializeComponent();

            refPanelInicial = PanelInicial;
            config = configParam;
        }

        private void InputBox_Load(object sender, EventArgs e)
        {
            FormResizer objFormResizer = new FormResizer();
            objFormResizer.ResizeForm(this, 864, 1152);
        }

        private void button1_Click(object sender, EventArgs e)
        {


        }

        private void InputBox_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (txtContra.Text == "" || txtUser.Text == "") {
                    MessageBox.Show(negativo);
                    return;
                }

                usuario user = new usuario(ref consultador, txtUser.Text, dataGridView1);

                if (txtContra.Text == user.getPass())
                {
                    Form frmAdmin = new Administrador(ref consultador, ref refPanelInicial, user.getPrivilegio(), config);
                    frmAdmin.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show(negativo);
                }

            }
            catch (Exception ee)
            {
                //MessageBox.Show(ee.Message);
                MessageBox.Show("Error! No se pudo conectar con el servidor.");
            }
        }

        private void InputBox_FormClosed_1(object sender, FormClosedEventArgs e)
        {
            refPanelInicial.Show();
        }

    }
}
