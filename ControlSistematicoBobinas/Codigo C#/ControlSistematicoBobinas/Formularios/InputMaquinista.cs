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
    public partial class InputMaquinista : Form
    {
        ConectorBaseDeDatos consultador;
        private Form refPanelInicial;

        Dictionary<string, int> HashMaquinista;

        bool cerrarForm = false;
        private string dir;
        private string dir2;

        FormularioOperador frmOperador;
        turno turnoMaquinista;

        public InputMaquinista(ref ConectorBaseDeDatos consult, ref Form PanelInicial, string dirPrincipal, string dirSecundario)
        {
            InitializeComponent();
            consultador = consult;
            refPanelInicial = PanelInicial;
            frmOperador = null;

            dir = dirPrincipal;
            dir2 = dirSecundario;
            turnoMaquinista = new turno();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (cmbMaquinista.Text != "")
            {
                try
                {
                    frmOperador.setNombreIdMaquinista(HashMaquinista[cmbMaquinista.Text], cmbMaquinista.Text);
                    frmOperador.setTurno();
                    frmOperador.Show();
                    this.Hide();
                }
                catch (Exception ee)
                {
                    InputMaquinista panelInicial = this;
                    frmOperador = new FormularioOperador(ref consultador, ref panelInicial, dir, dir2, turnoMaquinista);
                    frmOperador.setNombreIdMaquinista(HashMaquinista[cmbMaquinista.Text], cmbMaquinista.Text);
                    frmOperador.setTurno();
                    frmOperador.Show();
                    this.Hide();
                }
            }
            else { MessageBox.Show("Seleccione el nombre del Maquinista."); }
        }

        private void InputMaquinista_Load(object sender, EventArgs e)
        {
            FormResizer objFormResizer = new FormResizer();
            objFormResizer.ResizeForm(this, 864, 1152);
        }

        private void InputMaquinista_FormClosed(object sender, FormClosedEventArgs e)
        {
            refPanelInicial.Show();
        }

        private void cargarDatos()
        {
            LectorArchivos archivo;

            archivo = new LectorArchivos(dir + "maquinistas.txt");
            HashMaquinista = archivo.LeerArchivo(false);
        }

        private void Completar_combo()
        {

            try
            {
                foreach (KeyValuePair<string, int> cliente in HashMaquinista)
                {
                    cmbMaquinista.Items.Add(cliente.Key);
                }
            }
            catch
            {
                MessageBox.Show("Debe tener Maquinistas ingresados previamente!");
                cerrarForm = true;
            }
        }


        private void loggerError(string error)
        {
            LectorArchivos archivo, archivo2;
            archivo = new LectorArchivos(dir + "./ErrorFormularioOperador.log");
            archivo2 = new LectorArchivos(dir2 + "./ErrorFormularioOperador.log");

            DateTime Hoy = DateTime.Today;
            string fecha_actual = Hoy.ToString("dd/MM/yyyy");
            string Hora = DateTime.Now.ToString("H:mm");

            archivo.GuardarArchivo(error + " Fecha: " + fecha_actual + " Hora:" + Hora);
            archivo2.GuardarArchivo(error + " Fecha: " + fecha_actual + " Hora:" + Hora);
        }

        private void Guardar_Datos(string path, string path2)
        {
            LectorArchivos archivo, archivo2;
            archivo = new LectorArchivos(path);
            archivo2 = new LectorArchivos(path2);
            archivo.BorrarArchivo();
            archivo2.BorrarArchivo();

            data.DataSource = consultador.Dar_BSource();

            try
            {
                foreach (DataGridViewRow row in this.data.Rows)
                {
                    string valor = "";
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (cell.Value.ToString() != "")
                        {
                            valor = valor + cell.Value.ToString() + ";";
                        }
                    }
                    archivo.GuardarArchivo(valor);
                    archivo2.GuardarArchivo(valor);
                }
            }
            catch
            {
            }
        }

        private void InputMaquinista_Shown(object sender, EventArgs e)
        {
            if (cerrarForm)
            {
                refPanelInicial.Show();
                this.Close();
            }

            try
            {
                consultador.cargaMaquinistasCompleto();
                Guardar_Datos(dir + "maquinistas.txt", dir2 + "maquinistas.txt");
            }
            catch { loggerError("Error al conectar a la base de datos. Carga de archivos clientes.txt y tipospapel.txt. Posible desactualizacion."); }

            cargarDatos();
            Completar_combo();
            InputMaquinista panelInicial = this;
            frmOperador = new FormularioOperador(ref consultador, ref panelInicial, dir, dir2, turnoMaquinista);

        }

        private void InputMaquinista_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void InputMaquinista_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

    }
}
