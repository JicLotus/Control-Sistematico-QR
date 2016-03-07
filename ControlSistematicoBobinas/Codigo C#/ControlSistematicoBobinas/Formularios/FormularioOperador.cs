using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Drawing.Printing;

using System.Linq;
using System.Text;
using System.Windows.Forms;

using LibControlSistematico;

using System.Text.RegularExpressions;
using System.IO.Ports;

using FormularioLectorCode;

namespace ControlSistematicoBobinas
{
    public partial class FormularioOperador : Form
    {
        public delegate void AddDataDelegate(String myString);
        public AddDataDelegate myDelegate;

        private observacionesGenerales frmObservaciones;

        SerialPort RS232 = new SerialPort("COM1", 9600, Parity.Odd, 7, StopBits.One);

        private ConectorBaseDeDatos consultador;
        private Form refPanelInicial;
        Dictionary<string, int> HashCliente;
        Dictionary<string, int> HashTipoPapel;
        private string dir;
        private string dir2;

        string nroBobinaActual;
        string finBob;
        string nombreMaquinista;
        int idMaquinista;
        int nroCopia;

        string observacionFinal;

        string turnoNombre, turnoId;
        turno turnoMaquinista;
        RelojBobina relojBobina;

        administradorRotuloBobina rotuloBobina;

        public FormularioOperador(ref ConectorBaseDeDatos hacedor, ref InputMaquinista panelInicial, string dirPrincipal, string dirSecundaria, turno turnoMaquinistaParam)
        {
            dir = dirPrincipal;
            dir2 = dirSecundaria;

            InitializeComponent();
            consultador = hacedor;
            this.inicializarControles();
            refPanelInicial = panelInicial;

            turnoMaquinista = turnoMaquinistaParam;
            nroCopia = 1;
            observacionFinal = "";
            checkBox1.Checked = true;
            finBob = DateTime.Now.ToString("H:mm");
            relojBobina = new RelojBobina();
            rotuloBobina = new administradorRotuloBobina();
        }

        public void setNombreIdMaquinista(int idMaquinistaParam, string nombreMaquinistaParam)
        {
            idMaquinista = idMaquinistaParam;
            nombreMaquinista = nombreMaquinistaParam;
        }

        private void inicializarControles()
        {
            DateTime Hoy = DateTime.Today;
            string fecha_actual = Hoy.ToString("dd-MM-yyyy");
            txtDate.Text = fecha_actual;

            try
            {
                if (consultador.cargaClientesCompleto())
                {
                    Guardar_Datos(dir + "clientes.txt", dir2 + "clientes.txt");
                    consultador.CargaTipoPapel();
                    Guardar_Datos(dir + "tipospapel.txt", dir2 + "tipospapel.txt");
                    cargarFormulariosPendientes();
                    nroBobinaActual = consultador.ultimoNumeroBobina();
                    GuardarCantsBobs(Convert.ToInt32(nroBobinaActual));
                }
                else { loggerError("Error al conectar a la base de datos. Carga de archivos clientes.txt y tipospapel.txt. Posible desactualizacion."); }
            }
            catch
            {
                loggerError("Error al conectar a la base de datos. Carga de archivos clientes.txt y tipospapel.txt. Posible desactualizacion.");
            }

            Cargar_datos();
            Completar_combo();

            txtFormat.ValidatingType = typeof(double);
            txtWeight.ValidatingType = typeof(double);
            txtCoil.ValidatingType = typeof(double);
            txtEspesor.ValidatingType = typeof(double);
        }

        private void FormularioOperador_Load(object sender, EventArgs e)
        {
            FormResizer objFormResizer = new FormResizer();
            objFormResizer.ResizeForm(this, 864, 1152);

            try
            {

                if (!RS232.IsOpen)
                {
                    this.myDelegate = new AddDataDelegate(AddDataMethod);
                    RS232.DataReceived += new SerialDataReceivedEventHandler(RS232_DataReceived);
                    RS232.Open();
                }

            }
            catch (Exception error)
            {
                //loggerError(error.Message);
            }

        }

        public void AddDataMethod(String myString)
        {
            if (radioButton1.Checked)
            {
                try
                {
                    string decimos = myString.Split('.')[0];

                    if (decimos.Length == 1) { myString = "00" + myString; }
                    else if (decimos.Length == 2) { myString = "0" + myString; }

                    txtWeight.Text = myString;
                    lblPeso.Text = myString;
                }
                catch (Exception e)
                {
                    loggerError(e.Message);
                }
            }
        }

        void RS232_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                //string read = RS232.ReadExisting();
                string read2 = RS232.ReadLine().ToString();
                //int read3 = RS232.ReadChar();

                read2 = read2.Replace("M", "");
                read2 = read2.Replace(" ", "");
                read2 = read2.Replace("KG", "");
                read2 = read2.Replace("", "");


                label1.Invoke(this.myDelegate, new Object[] { read2 });
            }
            catch (Exception error)
            {
                //loggerError(error.Message);
            }
        }

        private void Cargar_datos()
        {
            LectorArchivos archivo;

            archivo = new LectorArchivos(dir + "clientes.txt");
            HashCliente = archivo.LeerArchivo(false);

            archivo = new LectorArchivos(dir + "tipospapel.txt");
            HashTipoPapel = archivo.LeerArchivo(true);
        }

        private int obtenerCantidadBobs()
        {
            LectorArchivos archivo;
            archivo = new LectorArchivos(dir + "cantBobs.txt");
            int cantidadBobs = archivo.leerCantBobs();
            return cantidadBobs;
        }

        private void GuardarCantsBobs(int cantidad)
        {
            LectorArchivos archivo, archivo2;

            archivo = new LectorArchivos(dir + "cantBobs.txt");
            archivo.BorrarArchivo();
            archivo.GuardarArchivo(cantidad.ToString());

            archivo2 = new LectorArchivos(dir2 + "cantBobs.txt");
            archivo2.BorrarArchivo();
            archivo2.GuardarArchivo(cantidad.ToString());

        }


        //Retorna True cuando no hubo error de conexion y pudo cargar todas las bobinas
        //Retorna False en caso contrario
        private bool cargarFormulariosPendientes()
        {
            LectorArchivos archivo;
            List<String> laLista = new List<String>();

            try
            {
                archivo = new LectorArchivos(dir + "ConsultasSinEnviar.txt");
                laLista = archivo.leerArchivoConsulta();
                string nroBobina;

                foreach (string consulta in laLista)
                {
                    //Buscamos en la consulta el nro de bobina
                    if (consulta != "")
                    {
                        nroBobina = consulta.Split(',')[12].Replace("`TURNO`)VALUES (", "");
                        if (!consultador.existeBobina(nroBobina))
                        {
                            consultador.consultarDirectamente(consulta);
                        }
                    }
                }
                archivo.BorrarArchivo();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        private void Guardar_Consulta(string consulta)
        {
            LectorArchivos archivo;

            archivo = new LectorArchivos(dir + "ConsultasSinEnviar.txt");
            archivo.GuardarArchivo(consulta);
            LectorArchivos archivo2 = new LectorArchivos(dir2 + "ConsultasSinEnviar.txt");
            archivo2.GuardarArchivo(consulta);
        }


        private void Completar_combo()
        {

            try
            {

                foreach (KeyValuePair<string, int> cliente in HashCliente)
                {
                    if (cliente.Key != "Remito Impreso")
                    {
                        cmbCliente.Items.Add(cliente.Key);
                    }
                }

                foreach (KeyValuePair<string, int> tipo in HashTipoPapel)
                {
                    cmbTipe.Items.Add(tipo.Key);
                }
            }
            catch (Exception e)
            {
                loggerError(e.Message);
            }
        }

        private void BotonIngresarCodigo_Click_1(object sender, EventArgs e)
        {


            this.seleccionarObservaciones();
            string mensajeCorroborar = CorroborarDatos();

            DateTime Hoy = DateTime.Today;
            string fecha_actual = Hoy.ToString("dd-MM-yyyy");
            txtDate.Text = fecha_actual;
            int idClient, idTipo;
            double Coil, peso, Formato;

            if (mensajeCorroborar == "")
            {

                if (relojBobina.esCopia(0)) { nroCopia++; }
                else
                {
                    idClient = HashCliente[cmbCliente.Text];
                    idTipo = HashTipoPapel[cmbTipe.Text];

                    Coil = Convert.ToDouble(txtCoil.Text);
                    peso = Convert.ToDouble(txtWeight.Text);
                    Formato = Convert.ToDouble(txtFormat.Text);
                    finBob = DateTime.Now.ToString("H:mm");
                    nroCopia = 1;

                    if (this.cargarFormulariosPendientes() && (nroBobinaActual = consultador.ultimoNumeroBobina()) != "")
                    {
                        consultador.agregarBobina(idClient, Coil, txtDate.Text, Formato, observacionFinal, peso, idTipo, txtEspesor.Text, finBob, idMaquinista.ToString(), turnoId);
                        GuardarCantsBobs(Convert.ToInt32(nroBobinaActual) + 1);
                    }
                    else
                    {
                        Hoy = DateTime.Today;
                        fecha_actual = Hoy.ToString("yyyy-MM-dd");
                        nroBobinaActual = (obtenerCantidadBobs()).ToString();
                        string stockBaradero = HashCliente["Stock Baradero"].ToString();
                        finBob = DateTime.Now.ToString("H:mm");
                        Guardar_Consulta("INSERT INTO  `lectorcodigo`.`reg_2014` (`Numero_Bobina` ,`estado_id` ,`producto_id` ,`cliente_id` ,`maquinista_id` ,`PESO` ,`OBSERVACION` ,`GRAMAJE` ,`ESPESOR` , `FIN_BOB` , `FORMATO`, `FECHA_FABRICACION`,`TURNO`)VALUES (" + nroBobinaActual + " ,  '" + stockBaradero + "',  '" + idTipo.ToString() + "',  '" + idClient.ToString() + "',  '" + idMaquinista.ToString() + "',  '" + txtWeight.Text + "',  '" + observacionFinal + "',  '" + Coil.ToString() + "',  '" + txtEspesor.Text + "',  '" + finBob + "', '" + txtFormat.Text + "' ,  '" + fecha_actual + "', '" + turnoId + "');");
                        GuardarCantsBobs(Convert.ToInt32(nroBobinaActual) + 1);
                        loggerError("Error al general la consulta. Se la ha guardado en ConsultasSinEnviar.txt");
                    }
                }

                try
                {
                    //VIEJOO
                    //string textoQr = "Cliente=" + cmbCliente.Text + ";Numero Bobina=" + nroBobinaActual + ";Gramaje=" + txtCoil.Text + ";Espesor=" + txtEspesor.Text + ";Peso=" + txtWeight.Text + ";Fin_Bob=" + finBob + ";Formato=" + txtFormat.Text + ";Observacion=" + observacionFinal + ";Maquinista=" + nombreMaquinista + ";Fecha=" + fecha_actual + ";Tipo=" + cmbTipe.Text + ";" + HashCliente[cmbCliente.Text] + ";" + HashTipoPapel[cmbTipe.Text] + ";" + idMaquinista + ";" + turnoId;
                    //string textoQr = nroBobina + ";" + gramaje + ";" + espesor + ";" + peso + ";" + finBob + ";" + formato + ";" + observacion + ";" + fechaFabricacion + ";" + idCliente + ";" + idTipo + ";" + idMaquinista + ";" + turno;
                    string textoQr = nroBobinaActual + ";" + txtCoil.Text + ";" + txtEspesor.Text + ";" + txtWeight.Text + ";" + finBob + ";" + txtFormat.Text + ";" + observacionFinal + ";" + fecha_actual + ";" + HashCliente[cmbCliente.Text] + ";" + HashTipoPapel[cmbTipe.Text] + ";" + idMaquinista + ";" + turnoId;
                    rotuloBobina.imprimir(textoQr, cmbTipe.Text, txtFormat.Text, txtWeight.Text, nroBobinaActual, txtDate.Text, txtCoil.Text, txtEspesor.Text, cmbCliente.Text, observacionFinal.Replace(",", ",\n"), nombreMaquinista, turnoNombre, nroCopia.ToString(), finBob, false);
                }
                catch (Exception error)
                {
                    loggerError(error.Message);
                }

                this.borrarCompletado();

            }
            else
            {
                MessageBox.Show(mensajeCorroborar);
            }

            observacionFinal = "";
        }


        private void borrarCompletado()
        {
            txtObservation.Text = "";
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
            checkBox5.Checked = false;
            checkBox6.Checked = false;
            checkBox7.Checked = false;

        }

        //Expresion regular
        public bool IsNumber(string inputvalue)
        {
            Regex isnumber = new Regex("[^0-9]");
            return !isnumber.IsMatch(inputvalue);
        }

        private string CorroborarDatos()
        {
            string corroboracion = "";

            if (txtObservation.Text.Length > 34) corroboracion = "ERROR: El recuadro de OBSERVACIONES(8) debe contener menos caracteres.";
            else if (cmbTipe.Text == "") corroboracion = "ERROR: El recuadro MATERIAL TIPO(1) esta vacio.";
            else if (txtFormat.Text == "0,00") corroboracion = "ERROR: El recuadro FORMATO(2) esta vacio.";
            else if (txtWeight.Text == "000,0") corroboracion = "ERROR: El recuadro PESO NETO(3) esta vacio.";
            else if (Convert.ToDouble(txtWeight.Text) < 200) corroboracion = "ERROR: El recuadro PESO NETO(3) debe ser mayor a 200.";
            else if (txtCoil.Text == "00,0") corroboracion = "ERROR: El recuadro GRAMAJE(4) esta vacio.";
            else if (cmbCliente.Text == "") corroboracion = "ERROR: El recuadro CLIENTE(6) esta vacio.";
            else if (txtEspesor.Text == "00") corroboracion = "ERROR: El recuadro ESPESOR(7) esta vacio.";

            return corroboracion;
        }

        private void Guardar_Datos(string path, string path2)
        {
            LectorArchivos archivo;
            archivo = new LectorArchivos(path);
            archivo.BorrarArchivo();

            LectorArchivos archivo2;
            archivo2 = new LectorArchivos(path2);
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

        private void grpDatos_Enter(object sender, EventArgs e)
        {

        }

        private void FormularioOperador_FormClosed(object sender, FormClosedEventArgs e)
        {
            //RS232.Close();
            refPanelInicial.Show();
            this.Hide();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            txtWeight.ReadOnly = true;
            txtWeight.TabStop = false;
            lblPeso.Visible = true;
            //try { 
            //    RS232.Open();
            // }
            //catch(Exception){}
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            txtWeight.ReadOnly = false;
            txtWeight.TabStop = true;
            lblPeso.Visible = false;
            //if (RS232.IsOpen) { RS232.Close(); }
        }

        private void loggerError(string error)
        {
            LectorArchivos archivo;
            archivo = new LectorArchivos(dir + "ErrorFormularioOperador.log");

            LectorArchivos archivo2;
            archivo2 = new LectorArchivos(dir2 + "ErrorFormularioOperador.log");

            DateTime Hoy = DateTime.Today;
            string fecha_actual = Hoy.ToString("dd/MM/yyyy");
            string Hora = DateTime.Now.ToString("H:mm");

            archivo.GuardarArchivo(error + " Fecha: " + fecha_actual + " Hora:" + Hora);
            archivo2.GuardarArchivo(error + " Fecha: " + fecha_actual + " Hora:" + Hora);
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void seleccionarObservaciones()
        {
            if (checkBox1.Checked && txtObservation.Text != "") observacionFinal += txtObservation.Text + ",";
            if (checkBox2.Checked) observacionFinal += checkBox2.Text + ",";
            if (checkBox3.Checked) observacionFinal += checkBox3.Text + ",";
            if (checkBox4.Checked) observacionFinal += checkBox4.Text + ",";
            if (checkBox5.Checked) observacionFinal += checkBox5.Text + ",";
            if (checkBox6.Checked) observacionFinal += checkBox6.Text + ",";
            if (checkBox7.Checked) observacionFinal += checkBox7.Text;

            observacionFinal = observacionFinal.Replace(";", "");
            observacionFinal = observacionFinal.Replace(":", "");
        }

        private void txtWeight_Enter_1(object sender, EventArgs e)
        {
            this.BeginInvoke((MethodInvoker)delegate()
            {
                txtWeight.Select(0, 0);
            });
        }


        private void txtFormat_Enter(object sender, EventArgs e)
        {
            this.BeginInvoke((MethodInvoker)delegate()
            {
                txtFormat.Select(0, 0);
            });
        }

        private void txtCoil_Enter(object sender, EventArgs e)
        {
            this.BeginInvoke((MethodInvoker)delegate()
            {
                txtCoil.Select(0, 0);
            });
        }

        private void txtEspesor_Enter(object sender, EventArgs e)
        {
            this.BeginInvoke((MethodInvoker)delegate()
            {
                txtEspesor.Select(0, 0);
            });
        }

        private void btnSalirMaquinista_Click(object sender, EventArgs e)
        {
            //this.Close();
            refPanelInicial.Show();
            this.Hide();
        }

        private void FormularioOperador_VisibleChanged(object sender, EventArgs e)
        {
            DateTime Hoy = DateTime.Today;
            string fecha_actual = Hoy.ToString("dd-MM-yyyy");
            txtDate.Text = fecha_actual;
            relojBobina.setInicio();
        }

        private void btnObsGral_Click(object sender, EventArgs e)
        {
            frmObservaciones = new observacionesGenerales(ref consultador, nombreMaquinista);
            frmObservaciones.Show();
        }

        private void FormularioOperador_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F12)
            {
                ObservacionesDia frmObsDia = new ObservacionesDia(ref consultador);
                frmObsDia.Show();
            }
        }

        public void setTurno()
        {
            int horaActual = DateTime.Now.Hour;
            int minutosActuales = DateTime.Now.Minute;
            turnoId = turnoMaquinista.getIndiceTurno(idMaquinista, horaActual, minutosActuales);
            turnoNombre = turnoMaquinista.getTurno();
        }

    }
}
