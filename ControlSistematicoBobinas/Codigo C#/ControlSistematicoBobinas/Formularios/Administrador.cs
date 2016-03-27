using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Drawing.Printing;

using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

using LibControlSistematico;
using FormularioLectorCode;
using System.IO;
using System.Drawing.Imaging;
using System.IO.Compression;

namespace ControlSistematicoBobinas
{
    public partial class Administrador : Form
    {
        private Form refPanelInicial;
        private string id;
        private int contador_hoja;
        private ConectorBaseDeDatos consultador;
        grillaPartesDiarios MyDataGridViewPrinter;
        private tipoCarga solapaActual;
        private privilegio priv;
        private bool abrePrimeraVez = true;
        private aparienciaFormulario apariencia;
        private administradorRotuloBobina rotuloBobina;
        private administradorRemito remito;

        public Administrador(ref ConectorBaseDeDatos hacedor, ref Form panelInicial, privilegio privParam, ArchivoIni configParam)
        {
            InitializeComponent();

            Administrador panel = this;
            apariencia = new aparienciaFormulario(ref dataGridView1, configParam, ref panel);
            apariencia.setVariablesControlesMain(ref mskGramaje, ref mskPeso, ref lblEstado, ref txtEspesor, ref txtObservacion, ref cmbTipo, ref cmbCliente, ref informeTotalToolStripMenuItem, ref parteDiarioMaquinistaToolStripMenuItem, ref remitoToolStripMenuItem1, ref remitoToolStripMenuItem, ref cmbEstado);
            consultador = hacedor;
            contador_hoja = 0;
            refPanelInicial = panelInicial;
            priv = privParam;
            mskPeso.ValidatingType = typeof(double);
            mskGramaje.ValidatingType = typeof(double);
            this.ajustarResolucion();
            rotuloBobina = new administradorRotuloBobina();
            remito = new administradorRemito();



        }

        void completarFiltroFechaHistorialObservaciones()
        {
            int day = Convert.ToInt32(apariencia.getDateDay2());
            int month = Convert.ToInt32(apariencia.getDateMonth2());
            int year = Convert.ToInt32(apariencia.getDateYear2());
            int day2 = Convert.ToInt32(apariencia.getHastaDay2());
            int month2 = Convert.ToInt32(apariencia.getHastaMonth2());
            int year2 = Convert.ToInt32(apariencia.getHastaYear2());
            DateTime fechaDesde = new DateTime(year, month, day);
            DateTime fechaHasta = new DateTime(year2, month2, day2);

            cmbDesde.Value = fechaDesde;
            cmbHasta.Value = fechaHasta;
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


        private void inicializar_datos()
        {
            try
            {
                consultador.inicializar_datos();
            }
            catch (Exception error)
            {
                MessageBox.Show("Se produjo el siguiente error: " + error.Message);
            }
        }

        private void Administrador_Shown(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.DataSource = consultador.Dar_BSource();
                //this.Text = "Lector Codigo [Datos Cargados]";
                //this.actualizarTotalHojas();
                solapaActual = tipoCarga.DATOS_CARGADOS;
                this.establecer_controles();

                if (priv == privilegio.ADMIN)
                {
                    btnCancelar.Visible = true;
                    usuariosToolStripMenuItem.Visible = true;
                }
                else { btnCancelar.Visible = false; usuariosToolStripMenuItem.Visible = false; }

            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
                //MessageBox.Show("Error! No se pudo conectar con el servidor.");
                this.Close();
            }
        }

        private void completarJoins()
        {
            apariencia.completarJoins();
        }

        private void actualizarTotalHojas()
        {
            switch (solapaActual)
            {
                case tipoCarga.DATOS_CARGADOS:
                    TotalHojas.Text = consultador.cantidad_hojas(apariencia.getIndexTipoPrincipal(), apariencia.getIndexTipoMaquinista(), apariencia.getIndexTipoEstado(), apariencia.getDateDay(), apariencia.getDateMonth(), apariencia.getDateYear(), apariencia.getHastaDay(), apariencia.getHastaMonth(), apariencia.getHastaYear(), apariencia.getIdMaquinista(), apariencia.getIdEstado(), apariencia.getNroBobina(), apariencia.getIndexTipoCliente(), apariencia.getIndexTipoPapel(), apariencia.getTipoCampoFecha(), apariencia.getIdTipoPapel(), apariencia.getIdCliente()).ToString();
                    cantidadBobinas.Text = (consultador.cantidad_reg_actuales()).ToString();
                    this.inicializar_datos();
                    break;
                case tipoCarga.USUARIOS:
                    TotalHojas.Text = consultador.cantidadHojasUsuarios().ToString();
                    consultador.getUsers();
                    break;
                case tipoCarga.CLIENTES:
                    TotalHojas.Text = consultador.cantidadHojasClientes().ToString();
                    consultador.getClientes();
                    break;
                case tipoCarga.MAQUINISTAS:
                    TotalHojas.Text = consultador.cantidadHojasMaquinistas().ToString();
                    consultador.getMaquinistas();
                    break;
                case tipoCarga.OBSERVACIONESGENRALES:
                    TotalHojas.Text = consultador.cantidadHojasObsGeneral(apariencia.getIndexTipoPrincipal2(), apariencia.getDateDay2(), apariencia.getDateMonth2(), apariencia.getDateYear2(), apariencia.getHastaDay2(), apariencia.getHastaMonth2(), apariencia.getHastaYear2()).ToString();
                    consultador.getObsGenerales();
                    break;
                case tipoCarga.PHONESHISTORY:
                    TotalHojas.Text = consultador.cantidadHojasHistorialCelular(apariencia.getIndexTipoPrincipal2(), apariencia.getDateDay2(), apariencia.getDateMonth2(), apariencia.getDateYear2(), apariencia.getHastaDay2(), apariencia.getHastaMonth2(), apariencia.getHastaYear2()).ToString();
                    consultador.getHistorial();
                    break;
                case tipoCarga.PRODUCTOS:
                    TotalHojas.Text = consultador.cantidadHojasProductos().ToString();
                    consultador.getProductos();
                    break;
            }


            if (Convert.ToInt32(TotalHojas.Text) == 0) { TotalHojas.Text = "1"; }

            if (Convert.ToInt32(TotalHojas.Text) == 1)
            {
                BotonAtras.Enabled = false; BotonSiguiente.Enabled = false;
            }
            else
            {
                BotonAtras.Enabled = true; BotonSiguiente.Enabled = true;
            }
            this.ponerse_primer_fila();
        }


        private void ponerse_primer_fila()
        {
            try
            {
                //Se establece en la primer fila y se la manda a dibujar
                this.dataGridView1.CurrentCell = this.dataGridView1[1, 0];
                this.accion_dato();
            }
            catch (Exception error)
            {
                MessageBox.Show("Se produjo el siguiente error: " + error.Message);
            }
        }

        private void Accion_Pagina()
        {
            dataGridView1.Columns.Clear();
            lblhojaactual.Text = (contador_hoja + 1).ToString();

            switch (solapaActual)
            {
                case tipoCarga.DATOS_CARGADOS:
                    consultador.accion_pa(contador_hoja + 1);
                    dataGridView1.Sort(this.dataGridView1.Columns["Numero_Bobina"], ListSortDirection.Descending);
                    this.completarJoins();
                    apariencia.accionPagina();
                    break;
                case tipoCarga.USUARIOS:
                    consultador.accionPaginaUsuario(contador_hoja + 1);
                    dataGridView1.Sort(this.dataGridView1.Columns["id"], ListSortDirection.Descending);
                    break;
                case tipoCarga.CLIENTES:
                    consultador.accionPaginaClientes(contador_hoja + 1);
                    dataGridView1.Sort(this.dataGridView1.Columns["Index"], ListSortDirection.Descending);
                    break;
                case tipoCarga.MAQUINISTAS:
                    consultador.accionPaginaMaquinistas(contador_hoja + 1);
                    dataGridView1.Sort(this.dataGridView1.Columns["Index"], ListSortDirection.Descending);
                    break;
                case tipoCarga.OBSERVACIONESGENRALES:
                    consultador.accionPaginaObsGeneral(contador_hoja + 1);
                    dataGridView1.Sort(this.dataGridView1.Columns["Index"], ListSortDirection.Descending);
                    break;
                case tipoCarga.PHONESHISTORY:
                    consultador.accionPaginaHistorial(contador_hoja + 1);
                    dataGridView1.Sort(this.dataGridView1.Columns["Index"], ListSortDirection.Descending);
                    break;
                case tipoCarga.PRODUCTOS:
                    dataGridView1.Sort(this.dataGridView1.Columns["Index"], ListSortDirection.Descending);
                    consultador.accionPaginaProductos(contador_hoja + 1);
                    break;
            }



        }

        public void Volver_pagina_inicial()
        {
            contador_hoja = 0;
            lblhojaactual.Text = "1";

            this.actualizarTotalHojas();
        }

        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.Columns.Clear();
            solapaActual = tipoCarga.CLIENTES;
            this.establecer_controles();
        }

        private void maquinistasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.Columns.Clear();
            solapaActual = tipoCarga.MAQUINISTAS;
            this.establecer_controles();
        }

        private void IngresoProductos_Click(object sender, EventArgs e)
        {
            dataGridView1.Columns.Clear();
            solapaActual = tipoCarga.PRODUCTOS;
            this.establecer_controles();
        }

        private void codigosCargadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.Columns.Clear();
            solapaActual = tipoCarga.DATOS_CARGADOS;
            this.establecer_controles();
        }

        private void usuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.Columns.Clear();
            solapaActual = tipoCarga.USUARIOS;
            this.establecer_controles();
        }

        private void establecer_controles()
        {
            switch (solapaActual)
            {
                case tipoCarga.DATOS_CARGADOS:
                    llenarComboCliente();
                    llenarComboMaquinista();
                    llenarComboTipo();
                    this.establecerControlesDatosCargados();
                    break;
                case tipoCarga.CLIENTES:
                    this.establecerControlesClientes();
                    break;
                case tipoCarga.MAQUINISTAS:
                    this.establecerControlesMaquinistas();
                    break;
                case tipoCarga.PRODUCTOS:
                    this.establecerControlesProductos();
                    break;
                case tipoCarga.USUARIOS:
                    this.establecerControlesUsuarios();
                    break;
                case tipoCarga.OBSERVACIONESGENRALES:
                    this.establecerControlesObservacionesGenerales();
                    
                    break;
                case tipoCarga.PHONESHISTORY:
                    this.establecerControlesHistorialCelulares();
                    break;
            }

            this.ponerse_primer_fila();
        }

        public void establecerControlesDatosCargados()
        {
            dataGridView1.Columns.Clear();

            this.frmVisibles(false, false, false, true, true, false, true, true, true,false);
            propiedades_visualizar("Lector Codigo [Datos Cargados]", false, false);
            this.Volver_pagina_inicial();
            dataGridView1.Sort(this.dataGridView1.Columns["Numero_Bobina"], ListSortDirection.Descending);

            this.completarJoins();

            apariencia.establecerControlesCargados();
            lblPeso.Text = consultador.darPeso();
        }

        private void establecerControlesUsuarios()
        {
            this.frmVisibles(false, false, false, false, false, true, false, false, true,false);
            propiedades_visualizar("Lector Codigo [Usuarios Cargados]", true, true);
            this.Volver_pagina_inicial();
            dataGridView1.Sort(this.dataGridView1.Columns["id"], ListSortDirection.Descending);
            foreach (DataGridViewColumn columna in dataGridView1.Columns)
            {
                columna.ReadOnly = true;
            }
        }

        private void establecerControlesClientes()
        {
            this.frmVisibles(true, false, false, false, false, false, false, false, true,false);
            propiedades_visualizar("Lector Codigo [Clientes Cargados]", true, true);
            this.Volver_pagina_inicial();
            dataGridView1.Sort(this.dataGridView1.Columns["Index"], ListSortDirection.Descending);
            foreach (DataGridViewColumn columna in dataGridView1.Columns)
            {
                columna.ReadOnly = true;
            }
        }

        private void establecerControlesMaquinistas()
        {
            this.frmVisibles(false, false, true, false, false, false, false, false, true,false);
            propiedades_visualizar("Lector Codigo [Maquinistas Cargados]", true, true);
            this.Volver_pagina_inicial();
            dataGridView1.Sort(this.dataGridView1.Columns["Index"], ListSortDirection.Descending);
            foreach (DataGridViewColumn columna in dataGridView1.Columns)
            {
                columna.ReadOnly = true;
            }
        }

        private void establecerControlesProductos()
        {
            this.frmVisibles(false, true, false, false, false, false, false, false, true,false);
            propiedades_visualizar("Lector Codigo [Productos Cargados]", true, true);
            this.Volver_pagina_inicial();
            dataGridView1.Sort(this.dataGridView1.Columns["Index"], ListSortDirection.Descending);
            foreach (DataGridViewColumn columna in dataGridView1.Columns)
            {
                columna.ReadOnly = true;
            }
        }

        public void accion_dato()
        {
            switch (solapaActual)
            {
                case tipoCarga.DATOS_CARGADOS:
                    this.accionDatosCargados();
                    break;
                case tipoCarga.CLIENTES:
                    this.accionClientes();
                    break;
                case tipoCarga.MAQUINISTAS:
                    this.accionMaquinistas();
                    break;
                case tipoCarga.PRODUCTOS:
                    this.accionProductos();
                    break;
                case tipoCarga.USUARIOS:
                    this.accionUsuarios();
                    break;
            }
        }

        private void accionClientes()
        {
            id = dataGridView1.CurrentRow.Cells["Index"].Value.ToString();
            txtCliente.Text = dataGridView1.CurrentRow.Cells["CLIENTE"].Value.ToString();
            txtDirec.Text = dataGridView1.CurrentRow.Cells["DIRECCION"].Value.ToString();
            txtLocalidad.Text = dataGridView1.CurrentRow.Cells["LOCALIDAD"].Value.ToString();
            txtCP.Text = dataGridView1.CurrentRow.Cells["C.P."].Value.ToString();
            txtProv.Text = dataGridView1.CurrentRow.Cells["Provincia"].Value.ToString();
            txtIVA.Text = dataGridView1.CurrentRow.Cells["I.V.A."].Value.ToString();
            txtCUIT.Text = dataGridView1.CurrentRow.Cells["CUIT"].Value.ToString();
        }


        private void accionMaquinistas()
        {
            id = dataGridView1.CurrentRow.Cells["Index"].Value.ToString();
            txtMaquinista.Text = dataGridView1.CurrentRow.Cells["MAQUINISTA"].Value.ToString();
            txtAyudante.Text = dataGridView1.CurrentRow.Cells["AYUDANTE"].Value.ToString();
        }

        private void accionProductos()
        {
            id = dataGridView1.CurrentRow.Cells["Index"].Value.ToString();
            txtProductoTipo.Text = dataGridView1.CurrentRow.Cells["TIPO"].Value.ToString();
            txtMetros.Text = dataGridView1.CurrentRow.Cells["METROS"].Value.ToString();
        }

        private void accionUsuarios()
        {
            id = dataGridView1.CurrentRow.Cells["ID"].Value.ToString();
            txtUser.Text = dataGridView1.CurrentRow.Cells["NOMBRE"].Value.ToString();
            txtPass.Text = dataGridView1.CurrentRow.Cells["PASSWORD"].Value.ToString();
            txtPrivilegio.Text = dataGridView1.CurrentRow.Cells["PRIVILEGIO"].Value.ToString();
        }

        private void accionDatosCargados()
        {
            apariencia.accionDatosCargados();
            id = apariencia.getId();
        }

        private void llenarComboMaquinista()
        {
            consultador.cargaMaquinistasCompleto();
            apariencia.llenarComboMaquinista();
        }

        private void llenarComboCliente()
        {
            consultador.cargaClientesCompleto();
            apariencia.llenarCmbCliente();
        }

        private void llenarComboTipo()
        {
            consultador.cargaTipoPapelCompleto();
            apariencia.llenarComboTipo();
        }

        private void propiedades_visualizar(string barra, bool btnEliminarParam, bool btnNuevoParam)
        {
            this.Text = barra;
            btnNuevo.Visible = btnNuevoParam;
        }

        private void frmVisibles(bool frmClienteParam, bool frmProductosParam, bool frmMaquinistasParam, bool frmDatosCargadosParam, bool vistaImprimirParam, bool frmUsuariosParam, bool frmDatosParam, bool btnFiltrosParam, bool frmEdicionParam,bool grpFiltroFechaParam)
        {
            frmCliente.Visible = frmClienteParam;
            frmProductos.Visible = frmProductosParam;
            frmMaquinistas.Visible = frmMaquinistasParam;
            frmDatosCargados.Visible = frmDatosCargadosParam;
            frmUsuarios.Visible = frmUsuariosParam;
            frmDatos.Visible = frmDatosParam;
            imprimirToolStripMenuItem.Visible = vistaImprimirParam;
            vistaPreviaToolStripMenuItem.Visible = vistaImprimirParam;
            filtrosToolStripMenuItem.Visible = btnFiltrosParam;
            frmEdicion.Visible = frmEdicionParam;
            grpFiltroFecha.Visible = grpFiltroFechaParam;
        }

        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            this.accion_dato();
        }

        private void dataGridView1_KeyUp(object sender, KeyEventArgs e)
        {
            this.accion_dato();
        }


        public void MyPrintDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            bool more = MyDataGridViewPrinter.DrawDataGridView(e.Graphics);
            if (more == true)
                e.HasMorePages = false;
        }


        private bool SetupThePrinting() // procedimientos para configurar la impresion
        {
            PrintDialog MyPrintDialog = new PrintDialog();
            MyPrintDialog.AllowCurrentPage = false;
            MyPrintDialog.AllowPrintToFile = false;
            MyPrintDialog.AllowSelection = false;
            MyPrintDialog.AllowSomePages = false;
            MyPrintDialog.PrintToFile = false;
            MyPrintDialog.ShowHelp = false;
            MyPrintDialog.ShowNetwork = false;

            MyPrintDocument.DocumentName = "PARTE DE MAQUINA DE PAPEL";
            MyPrintDocument.PrinterSettings = MyPrintDialog.PrinterSettings;
            MyPrintDocument.DefaultPageSettings = MyPrintDialog.PrinterSettings.DefaultPageSettings;
            MyPrintDocument.DefaultPageSettings.Margins = new Margins(0, 50, 40, 40);

            bool conTitulo = false;
            Font fuente = new Font("Tahoma", 12, FontStyle.Bold, GraphicsUnit.Point);
            string titulo = "PARTE DE MAQUINA DE PAPEL";
            if (contador_hoja == 0) conTitulo = true;

            try
            {
                string ayudante = apariencia.getAyudante();
                string textoMaquinista = apariencia.getNombreMaquinista();
                string textoFecha = apariencia.getDateDay() + "/" + apariencia.getDateMonth() + "/" + apariencia.getDateYear();


                consultador.getObservacionesFechaSeleccionada();
                int indexObs = dataGridView1.Columns["OBSERVACION"].Index;
                int indexFecha = dataGridView1.Columns["FECHA"].Index;
                int indexHorario = dataGridView1.Columns["HORARIO"].Index;
                int n = dataGridView1.Rows.Count;
                string textoObservacionesGenerales = "";

                for (int i = 0; i < n - 1; i++)
                {
                    string obs = dataGridView1[indexObs, i].Value.ToString();
                    string fecha = dataGridView1[indexFecha, i].Value.ToString().Split(' ')[0];
                    string horario = dataGridView1[indexHorario, i].Value.ToString();
                    textoObservacionesGenerales += ((obs.Replace("\n", "") + " " + horario) + "\n");
                }

                dataGridView1.Columns.Clear();
                solapaActual = tipoCarga.DATOS_CARGADOS;
                this.establecer_controles();


                MyDataGridViewPrinter = new grillaPartesDiarios(dataGridView1, MyPrintDocument, true, conTitulo, titulo, fuente, Color.Black, false, textoFecha, textoMaquinista, ayudante, "", lblPeso.Text, textoObservacionesGenerales);
            }
            catch (Exception e) { MessageBox.Show("Debe seleccionar un maquinista."); }

            return true;
        }


        private void parteDiarioMaquinistaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SetupThePrinting())
            {
                PrintPreviewDialog MyPrintPreviewDialog = new PrintPreviewDialog();
                MyPrintPreviewDialog.Document = MyPrintDocument;

                if (MessageBox.Show("Desea ver previamente el parte diario a imprimir?", "Guardado Registro", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    MyPrintPreviewDialog.ShowDialog();
                }
                else { MyPrintDocument.Print(); }
            }
        }

        private void BotonSiguiente_Click(object sender, EventArgs e)
        {
            //BotonSiguiente.Enabled = false;
            //BotonAtras.Enabled = true;
            if ((contador_hoja + 1).ToString() == TotalHojas.Text) { return; }
            contador_hoja++;
            this.Accion_Pagina();
            //BotonSiguiente.Enabled = true;
        }

        private void BotonAtras_Click(object sender, EventArgs e)
        {
            //BotonAtras.Enabled = false;
            if (contador_hoja == 0)
            {
                //BotonAtras.Enabled = false;
            }
            else
            {
                contador_hoja--;
                this.Accion_Pagina();
                //BotonAtras.Enabled = true;
            }
        }


        private void Administrador_FormClosed(object sender, FormClosedEventArgs e)
        {
            refPanelInicial.Show();
        }

        private void cmbDate_ValueChanged(object sender, EventArgs e)
        {
            if (!abrePrimeraVez)
            {
                this.establecerControlesDatosCargados();
            }
            else { abrePrimeraVez = false; }
        }

        private void cmbNombre_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.establecerControlesDatosCargados();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Desea modificar el registro seleccionado?", "Guardado Registro", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (id != "")
                {
                    dataGridView1.Columns.Clear();
                    switch (solapaActual)
                    {
                        case tipoCarga.DATOS_CARGADOS:
                            consultador.updateDatosCargado(id, apariencia.getUpdateIdCliente(), apariencia.getUpdateIdTipo(), mskGramaje.Text, txtEspesor.Text, mskPeso.Text, txtObservacion.Text, apariencia.getUpdateIdEstado());
                            break;
                        case tipoCarga.CLIENTES:
                            consultador.updateCliente(txtCliente.Text, id, txtDirec.Text, txtLocalidad.Text, txtCP.Text, txtProv.Text, txtIVA.Text, txtCUIT.Text);
                            break;

                        case tipoCarga.MAQUINISTAS:
                            consultador.updateMaquinista(txtMaquinista.Text, txtAyudante.Text, id);
                            break;

                        case tipoCarga.PRODUCTOS:
                            consultador.updateProducto(txtProductoTipo.Text, txtMetros.Text, id);
                            break;

                        case tipoCarga.USUARIOS:
                            consultador.updateUsuario(id, txtUser.Text, txtPass.Text, txtPrivilegio.Text);
                            break;
                    }
                    this.establecer_controles();
                }
            }
        }

        private void btnNuevo_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("Desea agregar un nuevo registro?", "Nuevo Registro", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                dataGridView1.Columns.Clear();
                switch (solapaActual)
                {
                    case tipoCarga.CLIENTES:
                        consultador.agregarCliente(txtCliente.Text, txtDirec.Text, txtLocalidad.Text, txtCP.Text, txtProv.Text, txtIVA.Text, txtCUIT.Text);
                        break;

                    case tipoCarga.MAQUINISTAS:
                        consultador.agregarMaquinista(txtMaquinista.Text, txtAyudante.Text);
                        break;

                    case tipoCarga.PRODUCTOS:
                        consultador.agregarProducto(txtProductoTipo.Text, txtMetros.Text);
                        break;

                    case tipoCarga.USUARIOS:
                        consultador.agregarUsuario(txtUser.Text, txtPass.Text, txtPrivilegio.Text);
                        break;

                }
                this.establecer_controles();
            }
        }


        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Desea eliminar el registro seleccionado?", "Eliminar Registro", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (id != "")
                {
                    dataGridView1.Columns.Clear();
                    switch (solapaActual)
                    {
                        case tipoCarga.DATOS_CARGADOS:
                            consultador.borrarBobina(id);
                            break;

                        case tipoCarga.CLIENTES:
                            consultador.borrarCliente(id);
                            break;

                        case tipoCarga.MAQUINISTAS:
                            consultador.borrarMaquinista(id);
                            break;

                        case tipoCarga.PRODUCTOS:
                            consultador.borrarProducto(id);
                            break;

                        case tipoCarga.USUARIOS:
                            consultador.borrarUsuario(id);
                            break;

                    }
                    this.establecer_controles();
                }
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            consultador.onemillion();
        }

        private void remitoImprimir(bool mostrarRemito)
        {
            List<string> laLista = null;
            if (apariencia.getIdEstado() != -1)
            {
                laLista = apariencia.getListaInfoCliente(apariencia.getIdEstado().ToString());
            }

            remito.imprimir(dataGridView1, "", laLista, apariencia.getPorcentajeRemito(), mostrarRemito);
        }

        private void remitoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (apariencia.getPorcentajeRemito() <= 100)
            {
                if (apariencia.estaEstadoSeleccionado())
                {

                    this.remitoImprimir(false);

                    string idRemitoImpreso = consultador.getIdRemitoImpreso();

                    if (idRemitoImpreso != "")
                    {
                        try
                        {
                            int indexSeleccion = dataGridView1.Columns["SELECCION"].Index;
                            int indexNroBobina = dataGridView1.Columns["NUMERO_BOBINA"].Index;
                            int asdasd = dataGridView1.Columns["ESTADO_ID"].Index;
                            int nFilas = dataGridView1.Rows.Count;
                            Dictionary<string, string> hashBobinaEstado = new Dictionary<string, string>();

                            for (int i = 0; i < nFilas - 1; i++)
                            {
                                if ((bool)dataGridView1[indexSeleccion, i].Value)
                                {

                                    string NROBobina = dataGridView1[indexNroBobina, i].Value.ToString();
                                    string idEstadoBobina = dataGridView1[asdasd, i].Value.ToString();
                                    hashBobinaEstado[NROBobina] = idEstadoBobina;
                                }
                            }

                            foreach (string bobina in hashBobinaEstado.Keys)
                            {
                                consultador.enroqueEstados(hashBobinaEstado[bobina], idRemitoImpreso, bobina);
                            }

                            dataGridView1.Columns.Clear();
                            solapaActual = tipoCarga.DATOS_CARGADOS;
                            this.establecer_controles();
                        }
                        catch (Exception a) { }
                    }
                }
                else
                {
                    MessageBox.Show("Error al imprimir remito. Asegurese de tener un estado seleccionado.");
                }
            }
            else
            {
                MessageBox.Show("El porentaje del remito debe ser menor igual a 100.");
            }

        }

        private void filtrosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filtros frmFiltros = new Filtros(ref apariencia);
            frmFiltros.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (solapaActual == tipoCarga.DATOS_CARGADOS)
            {
                DataGridViewCheckBoxCell ch1 = new DataGridViewCheckBoxCell();
                //ch1 = (DataGridViewCheckBoxCell)dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0];
                ch1 = (DataGridViewCheckBoxCell)dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0];

                if (ch1.Value == null)
                    ch1.Value = false;
                switch (ch1.Value.ToString())
                {
                    case "True":
                        ch1.Value = false;
                        break;
                    case "False":
                        ch1.Value = true;
                        break;
                }
            }
        }

        private void frmDatos_Enter(object sender, EventArgs e)
        {

        }

        private void historialScaneoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.Columns.Clear();
            solapaActual = tipoCarga.PHONESHISTORY;
            this.establecer_controles();
        }

        private void observacionesGeneralesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.Columns.Clear();
            solapaActual = tipoCarga.OBSERVACIONESGENRALES;
            this.establecer_controles();
        }

        private void establecerControlesHistorialCelulares()
        {
            this.frmVisibles(false, false, false, false, false, false, false, false, false,true);
            propiedades_visualizar("Lector Codigo [Historial Celulares]", false, false);

            this.Volver_pagina_inicial();
            foreach (DataGridViewColumn columna in dataGridView1.Columns)
            {
                columna.ReadOnly = true;
            }
            dataGridView1.Sort(this.dataGridView1.Columns["Index"], ListSortDirection.Descending);
            completarFiltroFechaHistorialObservaciones();
        }

        private void establecerControlesObservacionesGenerales()
        {
            this.frmVisibles(false, false, false, false, false, false, false, false, false,true);
            propiedades_visualizar("Lector Codigo [Observaciones Generales]", false, false);

            this.Volver_pagina_inicial();
            foreach (DataGridViewColumn columna in dataGridView1.Columns)
            {
                columna.ReadOnly = true;
            }
            dataGridView1.Sort(this.dataGridView1.Columns["Index"], ListSortDirection.Descending);
            completarFiltroFechaHistorialObservaciones();
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            consultador.onemillion2();
        }

        private void remitoToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (apariencia.getPorcentajeRemito() <= 100)
            {
                if (apariencia.estaEstadoSeleccionado())
                {
                    if (apariencia.getIdEstado() != -1)
                    {
                        this.remitoImprimir(true);
                    }
                }
                else
                {
                    MessageBox.Show("Error al imprimir remito. Asegurese de tener un estado seleccionado.");
                }
            }

        }

        private void Administrador_Load(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {
        }

        private void rotuloBobinaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string cliente, nroBobina, gramaje, espesor, peso, finBob, formato, observacion, nombreMaquinista, fechaFabricacion;
            string tipo, idCliente, idTipo, idMaquinista, turno;

            int indexCliente = dataGridView1.Columns["CLIENTE"].Index;
            int indexNroBobina = dataGridView1.Columns["NUMERO_BOBINA"].Index;
            int indexGramaje = dataGridView1.Columns["GRAMAJE"].Index;
            int indexEspesor = dataGridView1.Columns["ESPESOR"].Index;
            int indexPeso = dataGridView1.Columns["PESO"].Index;
            int indexFinBob = dataGridView1.Columns["FIN_BOB"].Index;
            int indexFormato = dataGridView1.Columns["FORMATO"].Index;
            int indexObservacion = dataGridView1.Columns["OBSERVACION"].Index;
            int indexNombreMaquinista = dataGridView1.Columns["MAQUINISTA"].Index;
            int indexFechaFabricacion;
            try
            {
                indexFechaFabricacion = dataGridView1.Columns["FECHA_FABRICACION"].Index;
            }
            catch (Exception error)
            {
                indexFechaFabricacion = dataGridView1.Columns["FECHA_SCANEO"].Index;
            }

            int indexTipo = dataGridView1.Columns["TIPO"].Index;
            int indexMetros = dataGridView1.Columns["METROS"].Index;
            int indexIdCliente = dataGridView1.Columns["CLIENTE_ID"].Index;
            int indexIdTipo = dataGridView1.Columns["PRODUCTO_ID"].Index;
            int indexIdMaquinista = dataGridView1.Columns["MAQUINISTA_ID"].Index;
            int indexTurno = dataGridView1.Columns["TURNO"].Index;

            int nFilas = dataGridView1.Rows.Count;
            turno Turno = new turno();

            for (int i = 0; i < nFilas - 1; i++)
            {
                cliente = dataGridView1[indexCliente, i].Value.ToString();
                nroBobina = dataGridView1[indexNroBobina, i].Value.ToString();
                gramaje = dataGridView1[indexGramaje, i].Value.ToString();
                espesor = dataGridView1[indexEspesor, i].Value.ToString();
                peso = dataGridView1[indexPeso, i].Value.ToString();
                finBob = dataGridView1[indexFinBob, i].Value.ToString();
                formato = dataGridView1[indexFormato, i].Value.ToString();
                observacion = dataGridView1[indexObservacion, i].Value.ToString();
                nombreMaquinista = dataGridView1[indexNombreMaquinista, i].Value.ToString();
                fechaFabricacion = dataGridView1[indexFechaFabricacion, i].Value.ToString();
                fechaFabricacion = fechaFabricacion.Split('/')[2].Split(' ')[0] + "-" + fechaFabricacion.Split('/')[1] + "-" + fechaFabricacion.Split('/')[0];
                tipo = dataGridView1[indexTipo, i].Value.ToString() + " " + dataGridView1[indexMetros, i].Value.ToString() + "Mts";
                idCliente = dataGridView1[indexIdCliente, i].Value.ToString();
                idTipo = dataGridView1[indexIdTipo, i].Value.ToString();
                idMaquinista = dataGridView1[indexIdMaquinista, i].Value.ToString();
                turno = dataGridView1[indexTurno, i].Value.ToString();

                //string textoQr = "Cliente=" + cliente + ";Numero Bobina=" + nroBobina + ";Gramaje=" + gramaje + ";Espesor=" + espesor + ";Peso=" + peso + ";Fin_Bob=" + finBob + ";Formato=" + formato + ";Observacion=" + observacion + ";Maquinista=" + nombreMaquinista + ";Fecha=" + fechaFabricacion + ";Tipo=" + tipo + ";" + idCliente + ";" + idTipo + ";" + idMaquinista + ";" + turno;
                string textoQr = nroBobina + ";" + gramaje + ";" + espesor + ";" + peso + ";" + finBob + ";" + formato + ";" + observacion + ";" + fechaFabricacion + ";" + idCliente + ";" + idTipo + ";" + idMaquinista + ";" + turno;

                string nombreTurno = Turno.getTurnoInducido(turno);
                rotuloBobina.imprimir(textoQr, tipo, formato, peso, nroBobina, fechaFabricacion, gramaje, espesor, cliente, observacion.Replace(",", ",\n"), nombreMaquinista, nombreTurno, "2", finBob, false);
            }

        }

        private void rotuloBobinaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string cliente = apariencia.getNombreClienteSeleccionado();
            string nroBobina = apariencia.getId();
            string gramaje = apariencia.getGramaje();
            string espesor = apariencia.getEspesor();
            string peso = apariencia.getPeso();
            string finBob = apariencia.getFinBob();
            string formato = apariencia.getFormato();
            string observacion = apariencia.getObservacion();
            string nombreMaquinista = apariencia.getNombreMaquinistaSeleccionado();
            string fechaFabricacion = apariencia.getFechaFabricacion();
            fechaFabricacion = fechaFabricacion.Split('/')[2].Split(' ')[0] + "-" + fechaFabricacion.Split('/')[1] + "-" + fechaFabricacion.Split('/')[0];
            string tipo = apariencia.getTipoEspecial() + " " + apariencia.getMetrosSeleccionados() + "Mts";
            string idCliente = apariencia.getIdClienteSeleccionado();
            string idTipo = apariencia.getIdTipoSeleccionado();
            string idMaquinista = apariencia.getIdMaquinistaSeleccionado();
            string turno = apariencia.getTurno();


            // QR VIEJO
            //string textoQr = "Cliente=" + cliente + ";Numero Bobina=" + nroBobina + ";Gramaje=" + gramaje + ";Espesor=" + espesor + ";Peso=" + peso + ";Fin_Bob=" + finBob + ";Formato=" + formato + ";Observacion=" + observacion + ";Maquinista=" + nombreMaquinista + ";Fecha=" + fechaFabricacion + ";Tipo=" + tipo + ";" + idCliente + ";" + idTipo + ";" + idMaquinista + ";" + turno;
            // QR VIEJO

            //string textoQr = "=;Numero Bobina=" + nroBobina + ";Gramaje=" + gramaje + ";Espesor=" + espesor + ";Peso=" + peso + ";Fin_Bob=" + finBob + ";Formato=" + formato + ";Observacion=" + observacion + ";=" + ";Fecha=" + fechaFabricacion + ";=" + ";" + idCliente + ";" + idTipo + ";" + idMaquinista + ";" + turno;
            string textoQr = nroBobina + ";" + gramaje + ";" + espesor + ";" + peso + ";" + finBob + ";" + formato + ";" + observacion + ";" + fechaFabricacion + ";" + idCliente + ";" + idTipo + ";" + idMaquinista + ";" + turno;

            turno Turno = new turno();
            string nombreTurno = Turno.getTurnoInducido(turno);

            rotuloBobina.imprimir(textoQr, tipo, formato, peso, nroBobina, fechaFabricacion, gramaje, espesor, cliente, observacion.Replace(",", ",\n"), nombreMaquinista, nombreTurno, "2", finBob, true);
        }

        private void BotonAtras_Click_1(object sender, EventArgs e)
        {

        }

        private void cmbTipoPrincipal_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void AplicarCambios_Click(object sender, EventArgs e)
        {
            apariencia.setFechaDesde2(cmbDesde.Value);
            apariencia.setFechaHasta2(cmbHasta.Value);
            apariencia.setTipoPrincipal2(cmbTipoPrincipal.SelectedIndex);
            this.establecer_controles();
        }


    }

}
