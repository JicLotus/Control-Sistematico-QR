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
    public partial class PanelInicial : Form
    {
        public ConectorBaseDeDatos consultador;
        InputMaquinista frmOperador;
        private ArchivoIni recolectorDatos;

        bool primeraVez = true;
        public PanelInicial()
        {
            InitializeComponent();
            recolectorDatos = new ArchivoIni();
            recolectorDatos.LeerArchivo(true);
            consultador = new ConectorBaseDeDatos(recolectorDatos.getIp(0), recolectorDatos.getPuerto(), recolectorDatos.getMysqlTimeOut(),"lectorCode");

            Form panelInicial = this;
            frmOperador = new InputMaquinista(ref consultador, ref panelInicial, recolectorDatos.getPathGuardadoInfo1(), recolectorDatos.getPathGuardadoInfo2());

            cmbSrv.Items.Add(recolectorDatos.getIpName());
            cmbSrv.Items.Add(recolectorDatos.getIp2Name());

        }

        private void changeIp()
        {
            consultador.setIp(recolectorDatos.getIp(cmbSrv.SelectedIndex));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.abrirPanelInputAdmin();
        }

        private void btOperador_Click(object sender, EventArgs e)
        {
            this.abrirPanelOperador();
        }

        private void PanelInicial_Load(object sender, EventArgs e)
        {
            FormResizer objFormResizer = new FormResizer();
            objFormResizer.ResizeForm(this, 864, 1152);

        }


        private void abrirPanelOperador()
        {
            try
            {
                frmOperador.Show();
            }
            catch (Exception ee)
            {
                Form panelInicial = this;
                frmOperador = new InputMaquinista(ref consultador, ref panelInicial, recolectorDatos.getPathGuardadoInfo1(), recolectorDatos.getPathGuardadoInfo2());
                frmOperador.Show();
            }

            this.Hide();
        }

        private void abrirPanelInputAdmin()
        {
            try
            {
                string title = "Ingreso Usuario";
                string negado = "Usuario o contraseña incorrecta.";

                Form panelInicial = this;
                recolectorDatos.LeerArchivo(true);
                Form InputBox = new InputBox(title, ref consultador, negado, ref panelInicial, recolectorDatos);

                InputBox.Show();
                this.Hide();
            }
            catch
            {
                MessageBox.Show("No se pudo conectar con la base de datos.");
            }
        }

        private void PanelInicial_Shown(object sender, EventArgs e)
        {
            switch (recolectorDatos.getModoApertura())
            {
                case modo.ADMIN:
                    this.abrirPanelInputAdmin();
                    break;
                case modo.OPERARIO:
                    this.abrirPanelOperador();
                    break;
                case modo.NORMAL:
                    break;
            };

            primeraVez = false;

            cmbSrv.SelectedIndex = 0;
        }

        private void PanelInicial_VisibleChanged(object sender, EventArgs e)
        {
            if (!primeraVez)
                if (recolectorDatos.getModoApertura() == modo.ADMIN | recolectorDatos.getModoApertura() == modo.OPERARIO) { this.Close(); }
        }

        private void cmbSrv_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.changeIp();
        }

    }
}
