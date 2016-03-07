//****************************************************************************************************
//****************************************************************************************************
//                                   DIBUJADO Y EMPALME DE OPCIONES
//Author: Jose Ignacio Castelli
//****************************************************************************************************
//****************************************************************************************************

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using LibControlSistematico;

namespace ControlSistematicoBobinas
{
    public class aparienciaFormulario
    {
        DataGridView dataGridView1;

        private Dictionary<string, string> HC;
        private Dictionary<string, List<string>> HCSuper;
        private Dictionary<string, string> dicMaquinista;
        private Dictionary<string, string> DM;
        private Dictionary<string, string> HT;
        private Dictionary<string, string> HTipoMetros;
        private Dictionary<string, string> Hmaquinista;

        ComboBox cmbCliente, cmbTipo, cmbEstado;
        string id,tipo, cliente, observacion, gramaje, peso, espesor, fechaFabricacion, finBob, formato, idTurno,maquinista;
        string metros, idMaquinistaSeleccionado, idClienteSeleccionado, idTipoSeleccionado;
        private TextBox txtObservacion,txtEspesor;
        private Label lblEstado;
        private MaskedTextBox mskPeso,mskGramaje;

        private ToolStripMenuItem btnInforme,btnParteDiario, btnRemito, btnRemitoVistaPrevia;

        int indexTipoAnio, indexTipoMaquinista, indexTipoEstado, indexTipoCliente, indexTipoPapel;
        string day, month, year;
        string day2, month2, year2;
        private int idMaquinista, idEstado, idTipoPapel, idCliente, nroBobina;
        private double porcentajeRemito;
        private int tipocampoFecha;
        ArchivoIni config;

        Administrador frmMain;

        public aparienciaFormulario(ref DataGridView grillaParam, ArchivoIni configParam, ref Administrador frmMainParam) 
        {
            frmMain = frmMainParam;
            dataGridView1 = grillaParam;
            dataGridView1.Font = new Font("Tahoma", 11F, GraphicsUnit.Pixel);
            HC = new Dictionary<string, string>();
            DM = new Dictionary<string, string>();
            HT = new Dictionary<string, string>();
            HTipoMetros = new Dictionary<string, string>();
            Hmaquinista = new Dictionary<string, string>();
            HCSuper = new Dictionary<string, List<string>>();
            config = configParam;
            this.inicializarVariablesFiltro();
            dataGridView1.ReadOnly = false;
        }

        private void inicializarVariablesFiltro()
        {
            indexTipoAnio = config.getFiltroFecha();
            indexTipoMaquinista = config.getFiltroMaquinista();
            indexTipoEstado = config.getFiltroEstado();
            indexTipoCliente = config.getFiltroCliente();
            indexTipoPapel = config.getFiltroTipoPapel();

            string fecha = DateTime.Today.ToString("dd-MM-yyyy");
            day = fecha.Split('-')[0];
            month = fecha.Split('-')[1];
            year = fecha.Split('-')[2];
            day2 = fecha.Split('-')[0];
            month2 = fecha.Split('-')[1];
            year2 = fecha.Split('-')[2];

            idMaquinista = config.getIndexMaquinista();
            idEstado = config.getIndexEstado();
            idTipoPapel = config.getIndexTipoPapel();
            idCliente = config.getIndexCliente();

            porcentajeRemito = config.getPorcentajeRemito();
            tipocampoFecha = config.getTipoCampoFecha();
            nroBobina = config.getNroBobina();
        }

        public void accionPagina() 
        {
            dataGridView1.Columns["cliente_id"].Visible = false;
            dataGridView1.Columns["maquinista_id"].Visible = false;
            dataGridView1.Columns["producto_id"].Visible = false;
            dataGridView1.Columns["Maquinista"].Visible = true;
            dataGridView1.Columns["Seleccion"].Visible = true;
            dataGridView1.Columns["estado_id"].Visible = false;
            dataGridView1.Columns["celular"].Visible = false;
            dataGridView1.Columns["turno"].Visible = false;

            if ((Maquinista)indexTipoMaquinista == Maquinista.NOMBRE && (Fecha)indexTipoAnio == Fecha.DIA && (campoFecha)tipocampoFecha == campoFecha.FECHA_FABRICACION)
            {
                btnParteDiario.Enabled = true;
                dataGridView1.Columns["Maquinista"].Visible = false;
                dataGridView1.Columns["Seleccion"].Visible = false;
            }

        }
        
        public void llenarCmbCliente() 
        {
            try
            {
                cmbCliente.Items.Clear();
                cmbEstado.Items.Clear();
                HC.Clear();
                HCSuper.Clear();

                int n = dataGridView1.Rows.Count;
                int indexColumna = dataGridView1.Columns["CLIENTE"].Index;
                int indexCliente = dataGridView1.Columns["INDEX"].Index;
                int indexDireccion = dataGridView1.Columns["DIRECCION"].Index;
                int indexLoc = dataGridView1.Columns["LOCALIDAD"].Index;
                int indexCP = dataGridView1.Columns["C.P."].Index;
                int indexProv = dataGridView1.Columns["PROVINCIA"].Index;
                int indexIVA = dataGridView1.Columns["I.V.A."].Index;
                int indexCUIT = dataGridView1.Columns["CUIT"].Index;

                for (int i = 0; i < n - 1; i++)
                {
                    cmbCliente.Items.Add(dataGridView1[indexColumna, i].Value.ToString());
                    cmbEstado.Items.Add(dataGridView1[indexColumna, i].Value.ToString());
                    HC[dataGridView1[indexCliente, i].Value.ToString()] = dataGridView1[indexColumna, i].Value.ToString();
                    List<string> datosCliente = new List<string>();

                    datosCliente.Add(dataGridView1[indexColumna, i].Value.ToString());
                    datosCliente.Add(dataGridView1[indexDireccion, i].Value.ToString());
                    datosCliente.Add(dataGridView1[indexLoc, i].Value.ToString());
                    datosCliente.Add(dataGridView1[indexCP, i].Value.ToString());
                    datosCliente.Add(dataGridView1[indexProv, i].Value.ToString());
                    datosCliente.Add(dataGridView1[indexIVA, i].Value.ToString());
                    datosCliente.Add(dataGridView1[indexCUIT, i].Value.ToString());
                    HCSuper[dataGridView1[indexCliente, i].Value.ToString()] = datosCliente;
                }
            }
            catch (Exception e) { }
        }


        public void completarJoins() 
        {
            DataGridViewColumn cliente = new DataGridViewColumn();
            DataGridViewColumn maq = new DataGridViewColumn();
            DataGridViewColumn tipo = new DataGridViewColumn();
            DataGridViewColumn metros = new DataGridViewColumn();
            DataGridViewCheckBoxColumn seleccion = new DataGridViewCheckBoxColumn();
            DataGridViewCellStyle style = new DataGridViewCellStyle();
            
            //style.Font = new Font("Verdana", 8, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle = style;
            
            cliente.Name = "Cliente";
            maq.Name = "Maquinista";
            tipo.Name = "Tipo";
            metros.Name = "Metros";
            seleccion.Name = "Seleccion";
            
            seleccion.AutoSizeMode = dataGridView1.Columns[0].AutoSizeMode;
            cliente.CellTemplate = dataGridView1.Columns[0].CellTemplate;
            cliente.AutoSizeMode = dataGridView1.Columns[0].AutoSizeMode;
            maq.CellTemplate = dataGridView1.Columns[0].CellTemplate;
            maq.AutoSizeMode = dataGridView1.Columns[0].AutoSizeMode;
            tipo.CellTemplate = dataGridView1.Columns[0].CellTemplate;
            tipo.AutoSizeMode = dataGridView1.Columns[0].AutoSizeMode;
            metros.CellTemplate = dataGridView1.Columns[0].CellTemplate;
            metros.AutoSizeMode = dataGridView1.Columns[0].AutoSizeMode;

            
            dataGridView1.Columns.Insert(0, seleccion);
            dataGridView1.Columns.Insert(1, cliente);
            dataGridView1.Columns.Insert(9, tipo);
            dataGridView1.Columns.Insert(10, metros);
            dataGridView1.Columns.Insert(13, maq);


            foreach (DataGridViewColumn columna in dataGridView1.Columns)
            {
                columna.ReadOnly = true;
            }

            dataGridView1.Columns[0].ReadOnly = false;

            

            int nFilas = dataGridView1.Rows.Count;
            int indexCli = dataGridView1.Columns["CLIENTE"].Index;
            int indexMaq = dataGridView1.Columns["MAQUINISTA"].Index;
            int indexT = dataGridView1.Columns["TIPO"].Index;
            int indexM = dataGridView1.Columns["METROS"].Index;
            int indexIdCli = dataGridView1.Columns["CLIENTE_ID"].Index;
            int indexIdMaq = dataGridView1.Columns["MAQUINISTA_ID"].Index;
            int indexIdProd = dataGridView1.Columns["PRODUCTO_ID"].Index;
            int indexSeleccion = dataGridView1.Columns["SELECCION"].Index;


            for (int i = 0; i < nFilas - 1; i++)
            {
                dataGridView1[indexSeleccion, i].Value = false;

                string idCliente = dataGridView1[indexIdCli, i].Value.ToString();
                string idMaq = dataGridView1[indexIdMaq, i].Value.ToString();
                string idTipo = dataGridView1[indexIdProd, i].Value.ToString();
                string[] tipoMetros=null;

                if (HTipoMetros.ContainsKey(idTipo))
                {
                    tipoMetros = HTipoMetros[idTipo].Split(':');
                }
                else { HTipoMetros[idTipo] = "INVALIDO:INVALIDO"; }

                try { 
                    dataGridView1[indexCli, i].Value = HC[idCliente];
                }
                catch(Exception e) {
                    HC[idCliente] = "INEXISTENTE";
                    dataGridView1[indexCli, i].Value = HC[idCliente];
                }
                
                try{
                    dataGridView1[indexMaq, i].Value = Hmaquinista[idMaq];
                }
                catch (Exception e) {
                    Hmaquinista[idMaq] = "INEXISTENTE";
                    dataGridView1[indexMaq, i].Value = Hmaquinista[idMaq];
                }
                
                try{
                    dataGridView1[indexT, i].Value = tipoMetros[0];
                }
                catch (Exception e) {
                    dataGridView1[indexT, i].Value = "INVALIDO";
                }
                
                try{
                    dataGridView1[indexM, i].Value = tipoMetros[1];
                }
                catch (Exception e) { dataGridView1[indexM, i].Value = "INVALIDO"; }
            }


        }

        
        public void accionDatosCargados() 
        {

            bool vacioGrillaSeleccionada;
            try
            {
                if (dataGridView1.CurrentRow.Cells[0].Value.ToString() != "")
                {
                    vacioGrillaSeleccionada = false;
                }
                else 
                {
                    vacioGrillaSeleccionada = true;
                }
            }
            catch (Exception error) 
            {
                vacioGrillaSeleccionada = true;
            }

            if (!vacioGrillaSeleccionada)
            {

                try
                {
                    id = dataGridView1.CurrentRow.Cells["Numero_Bobina"].Value.ToString();
                    tipo = dataGridView1.CurrentRow.Cells["TIPO"].Value.ToString();
                    metros = dataGridView1.CurrentRow.Cells["METROS"].Value.ToString();
                    cliente = dataGridView1.CurrentRow.Cells["CLIENTE"].Value.ToString();
                    observacion = dataGridView1.CurrentRow.Cells["OBSERVACION"].Value.ToString();
                    gramaje = dataGridView1.CurrentRow.Cells["GRAMAJE"].Value.ToString();
                    peso = dataGridView1.CurrentRow.Cells["PESO"].Value.ToString();
                    espesor = dataGridView1.CurrentRow.Cells["ESPESOR"].Value.ToString();

                    finBob = dataGridView1.CurrentRow.Cells["FIN_BOB"].Value.ToString();
                    formato = dataGridView1.CurrentRow.Cells["FORMATO"].Value.ToString();
                    idTurno = dataGridView1.CurrentRow.Cells["TURNO"].Value.ToString();
                    maquinista = dataGridView1.CurrentRow.Cells["MAQUINISTA"].Value.ToString();

                    //INDICES
                    idMaquinistaSeleccionado = dataGridView1.CurrentRow.Cells["MAQUINISTA_ID"].Value.ToString();
                    idClienteSeleccionado = dataGridView1.CurrentRow.Cells["CLIENTE_ID"].Value.ToString();
                    idTipoSeleccionado = dataGridView1.CurrentRow.Cells["PRODUCTO_ID"].Value.ToString();
                    ////////

                    cmbTipo.SelectedIndex = cmbTipo.Items.IndexOf(tipo);
                    cmbCliente.SelectedIndex = cmbCliente.Items.IndexOf(cliente);
                    txtObservacion.Text = observacion;
                    mskGramaje.Text = gramaje;
                    mskPeso.Text = peso;
                    txtEspesor.Text = espesor;
                }
                catch (Exception error) { }

                try
                {
                    fechaFabricacion = dataGridView1.CurrentRow.Cells["FECHA_FABRICACION"].Value.ToString();
                    fechaFabricacion = fechaFabricacion.Split('/')[0] + "/" + fechaFabricacion.Split('/')[1] + "/" + fechaFabricacion.Split('/')[2].Split(' ')[0];
                }
                catch (Exception error)
                {
                    fechaFabricacion = dataGridView1.CurrentRow.Cells["FECHA_SCANEO"].Value.ToString();
                    fechaFabricacion = fechaFabricacion.Split('/')[0] + "/" + fechaFabricacion.Split('/')[1] + "/" + fechaFabricacion.Split('/')[2].Split(' ')[0];
                }


                try
                {
                    string estadoid = dataGridView1.CurrentRow.Cells["estado_id"].Value.ToString();
                    lblEstado.Text = HC[estadoid];
                    cmbEstado.SelectedIndex = cmbEstado.Items.IndexOf(lblEstado.Text);
                }
                catch (Exception error)
                {
                    lblEstado.Text = "INEXISTENTE";
                    cmbEstado.SelectedIndex = cmbEstado.Items.IndexOf(lblEstado.Text);
                }
            }
            else
            {
                lblEstado.Text = "";
                cmbTipo.SelectedIndex = -1;
                cmbCliente.SelectedIndex = -1;
                cmbEstado.SelectedIndex = -1;
                txtObservacion.Text = "";
                mskGramaje.Text = "";
                mskPeso.Text = "";
                txtEspesor.Text = "";
            }

        }

        public void llenarComboMaquinista() 
        {
            try
            {
                int n = dataGridView1.Rows.Count;
                int indexColumna = dataGridView1.Columns["MAQUINISTA"].Index;
                int indexAyudante = dataGridView1.Columns["AYUDANTE"].Index;
                int indexINDEX = dataGridView1.Columns["INDEX"].Index;
                dicMaquinista = new Dictionary<string, string>();
                DM.Clear();
                Hmaquinista.Clear();
                for (int i = 0; i < n - 1; i++)
                {
                    string maquinista = dataGridView1[indexColumna, i].Value.ToString();
                    string ayudante = dataGridView1[indexAyudante, i].Value.ToString();
                    string index = dataGridView1[indexINDEX, i].Value.ToString();
                    dicMaquinista[maquinista] = ayudante;
                    
                    DM[maquinista] = index;
                    Hmaquinista[index] = maquinista;
                }
            }
            catch (Exception e) { }
        }

        /*
         * 
         * 
         */
        public void llenarComboTipo() 
        {

            try{
                cmbTipo.Items.Clear();
                HT.Clear();
                HTipoMetros.Clear();

                int n = dataGridView1.Rows.Count;
                int indexColumna = dataGridView1.Columns["TIPO"].Index;
                int indexI = dataGridView1.Columns["INDEX"].Index;
                int indexM = dataGridView1.Columns["METROS"].Index;

                for (int i = 0; i < n - 1; i++)
                {
                    string tipo = dataGridView1[indexColumna, i].Value.ToString();
                    string index = dataGridView1[indexI, i].Value.ToString();
                    string metros = dataGridView1[indexM, i].Value.ToString();
                    cmbTipo.Items.Add(tipo);
                    HT[tipo] = index;
                    HTipoMetros[index] = tipo + ":" + metros;
                }


                if (cmbTipo.SelectedIndex >= 0)
                {
                    cmbTipo.SelectedIndex = 0;
                }
            }
            catch (Exception e) { }

        }

        public void establecerControlesCargados(){
            
            dataGridView1.Columns["cliente_id"].Visible = false;
            dataGridView1.Columns["maquinista_id"].Visible = false;
            dataGridView1.Columns["producto_id"].Visible = false;
            dataGridView1.Columns["Maquinista"].Visible = true;
            dataGridView1.Columns["Seleccion"].Visible = true;
            dataGridView1.Columns["estado_id"].Visible = false;
            dataGridView1.Columns["celular"].Visible = false;
            dataGridView1.Columns["turno"].Visible = false;

            btnParteDiario.Enabled = false;
            //btnRemito.Enabled = false;
            //btnRemitoVistaPrevia.Enabled = false;
            
            if ((Maquinista)indexTipoMaquinista == Maquinista.NOMBRE && (Fecha)indexTipoAnio == Fecha.DIA && (campoFecha)tipocampoFecha == campoFecha.FECHA_FABRICACION)
            {
                btnParteDiario.Enabled = true;
                dataGridView1.Columns["Maquinista"].Visible = false;
                dataGridView1.Columns["Seleccion"].Visible = false;
            }

            //if ((estado)indexTipoEstado == estado.NOMBRE) 
            //{
                btnRemito.Enabled = true;
                btnRemitoVistaPrevia.Enabled = true;
            //}


            try{
                if (HC[idEstado.ToString()] =="Remito Impreso" && (estado)indexTipoEstado == estado.NOMBRE) 
                {
                    btnRemito.Enabled = false;
                    btnRemitoVistaPrevia.Enabled = false;
                }
            }
            catch (Exception e) { }

        }

        public Boolean estaNombreMaquinistaSeleccionado()
        {
            return idMaquinista >= 0;
        }

        public Boolean estaEstadoSeleccionado()
        {
            return idEstado >= 0 && indexTipoEstado>0;
        }

        /**************************************************
         *************************************************
         *******************GETERS************************
         *************************************************
        **************************************************/

        public string getId() { return id; }

        
        public string getTurno()
        {
            return idTurno;
        }

        public string getObservacion() 
        {
            return observacion;
        }

        public string getFechaFabricacion()
        {
            return fechaFabricacion;
        }

        public string getGramaje()
        {
            return gramaje;
        }

        public string getEspesor()
        {
            return espesor;
        }

        public string getPeso()
        {
            return peso;
        }

        public string getFinBob()
        {
            return finBob;
        }

        public string getFormato()
        {
            return formato;
        }

        public string getNombreMaquinistaSeleccionado() 
        {
            return maquinista;
        }

        public string getNombreClienteSeleccionado()
        {
            return cliente;
        }

        public string getMetrosSeleccionados()
        {
            return metros;
        }

        public string getIdMaquinistaSeleccionado()
        {
            return idMaquinistaSeleccionado;
        }

        public string getIdClienteSeleccionado() 
        {
            return idClienteSeleccionado;
        }

        public string getIdTipoSeleccionado()
        {
            return idTipoSeleccionado;
        }


        public string getTipoEspecial()
        {
            return tipo;
        }

        public string getNombreMaquinista()
        {
            try
            {
                return Hmaquinista[idMaquinista.ToString()];
            }
            catch (Exception e) { return ""; }
        }

        public string getNombreCliente()
        {
            try
            {
                return HC[idCliente.ToString()];
            }
            catch (Exception e) { return ""; }
        }

        public string getNombreTipoPapel() 
        {
            try
            {
                return HTipoMetros[idTipoPapel.ToString()].Split(':')[0];
            }
            catch (Exception e) { return ""; }
        }

        public string getNombreEstado()
        {
            try
            {
                return HC[idEstado.ToString()];
            }
            catch (Exception e) { return ""; }
        }

        
        /*
         * Retorna lista en donde contiene toda la data del idEstado(cliente)
         * Devuelve null en caso de error
         */
        public List<string> getListaInfoCliente(string idEstado)
        {
            List<string> laLista;

            try
            {
                laLista = HCSuper[idEstado];
            }
            catch (Exception e) { laLista = null; }

            return laLista;
        }


        public double getPorcentajeRemito() 
        {
            return (Convert.ToDouble(porcentajeRemito) / 100);
        }

        public string getDateDay(){
            return day;
        }

        public string getDateMonth()
        {
            return month;
        }

        public string getDateYear()
        {
            return year;
        }

        public string getHastaDay()
        {
            return day2;
        }

        public string getHastaMonth()
        {
            return month2;
        }

        public string getHastaYear()
        {
            return year2;
        }

        public int getIndexTipoPrincipal()
        {
            return indexTipoAnio;
        }

        public int getIndexTipoMaquinista()
        {
            return indexTipoMaquinista;
        }

        public int getIndexTipoEstado()
        {
           return indexTipoEstado;
        }

        public int getIndexTipoCliente()
        {
            return indexTipoCliente;
        }

        public int getIndexTipoPapel()
        {
            return indexTipoPapel;
        }

        public int getIdMaquinista() 
        {
            return idMaquinista;
        }

        public int getIdEstado() 
        {
            return idEstado;
        }

        public int getIdTipoPapel() 
        {
            return idTipoPapel;
        }

        public int getIdCliente() 
        {
            return idCliente;
        }

        public int getTipoCampoFecha() 
        {
            return tipocampoFecha;
        }

        public int getNroBobina() 
        {
            return nroBobina;   
        }
        /*
         * 
         * 
         */
        public string getAyudante()
        {
            string ayudante;

            try
            {
                ayudante = dicMaquinista[getNombreMaquinista()];
            }
            catch (Exception e) { ayudante = ""; }

            return ayudante;
        }


        /*
         * Retorna IddelCliente
         * Devuelve "" en caso de excepcion
         */
        public string getUpdateIdCliente()
        {
            string id;

            try
            {
                id = HC.Keys.OfType<String>().FirstOrDefault(s => HC[s] == cmbCliente.SelectedItem.ToString());
            }
            catch (Exception e) { id = ""; }

            return id;
        }

        public string getUpdateIdEstado()
        {
            string id;

            try
            {
                id = HC.Keys.OfType<String>().FirstOrDefault(s => HC[s] == cmbEstado.SelectedItem.ToString());
            }
            catch (Exception e) { id = ""; }

            return id;
        }

        /*
         * Retorna idTipo de papel
         * Retorna "" en caso de excepcion 
        */

        public string getUpdateIdTipo()
        {
            string idTipo;
            try
            {
                idTipo = HT[cmbTipo.SelectedItem.ToString()];
            }
            catch (Exception e) { idTipo = ""; }

            return idTipo;
        }

        public Dictionary<string, string> getHashMaquinistas()
        {
            return DM;
        }

        public Dictionary<string, string> getHashClientes()
        {
            return HC;
        }

        public Dictionary<string, string> getHashTipoPapeles()
        {
            return HT;
        }

        /**************************************************
         *************************************************
         *******************SETERS************************
         *************************************************
        **************************************************/

        public void setPorcentajeRemito(string mskPorciento)
        {
            porcentajeRemito = Convert.ToDouble(mskPorciento);
        }

        public void setNroBobina(string txtNroBobina)
        {
            nroBobina = Convert.ToInt32(txtNroBobina);
        }

        public void setFechaDede(DateTime textoFechaDesde)
        {
            day = textoFechaDesde.Day.ToString();
            month = textoFechaDesde.Month.ToString();
            year = textoFechaDesde.Year.ToString();
        }

        public void setFechaHasta(DateTime textoFechaHasta)
        {
            day2 = textoFechaHasta.Day.ToString();
            month2 = textoFechaHasta.Month.ToString();
            year2 = textoFechaHasta.Year.ToString();
        }
        
        public void setCampoFecha(int CampoFechaParam)
        {
            tipocampoFecha = CampoFechaParam;
        }
        
        /*
        * Retorna id del Nombre del maquinista
        * Devuelve -1 en caso de excepcion
        */
        public void setIdMaquinista(string nombreMaquinista)
        {
            try
            {
                idMaquinista = Convert.ToInt32(DM[nombreMaquinista]);
            }
            catch (Exception e) { idMaquinista = -1; }
        }

        public void setIdCliente(string cliente)
        {
            try
            {
                var key = HC.Keys.OfType<String>().FirstOrDefault(s => HC[s] == cliente);
                idCliente = Convert.ToInt32(key);
            }
            catch (Exception e) { idCliente = -1; }
        }

        /*
         * Guarda id del estado de la bobina en la variable idEstado
         * GUarda -1 en caso de que no se encuentre
         */
        public void setIdEstado(string textoEstado)
        {
            try
            {
                var key = HC.Keys.OfType<String>().FirstOrDefault(s => HC[s] == textoEstado);
                idEstado = Convert.ToInt32(key);
            }
            catch (Exception e) { idEstado = -1; }
        }
        
        public void setIdTipoPapel(string TipoPapel)
        {
            try{
                idTipoPapel = Convert.ToInt32(HT[TipoPapel]);
            }
            catch (Exception e) { idTipoPapel = -1; }
        }

        public void setTipoEstado(int cmbTipoEstado)
        {
            indexTipoEstado = Convert.ToInt32(cmbTipoEstado);
        }
        
        public void setTipoMaquinista(int cmbTipoMaquinista)
        {
            indexTipoMaquinista = Convert.ToInt32(cmbTipoMaquinista);
        }
        
        public void setTipoPapel(int cmbTipoPapel)
        {
            indexTipoPapel = Convert.ToInt32(cmbTipoPapel);
        }
        
        public void setTipoCliente(int cmbTipoCliente)
        {
            indexTipoCliente = Convert.ToInt32(cmbTipoCliente);
        }
            
        public void setTipoPrincipal(int cmbTipoPrincipal)
        {
            indexTipoAnio = Convert.ToInt32(cmbTipoPrincipal);
        }

        public void setVariablesControlesMain(ref MaskedTextBox mskGramajeParam, ref MaskedTextBox mskPesoParam, ref Label lblEstadoParam, ref TextBox txtEspParam, ref TextBox txtObsParam, ref ComboBox cmbTipoParam, ref ComboBox cmbClienteParam, ref ToolStripMenuItem btnInformeTotalParam, ref ToolStripMenuItem btnParteDiarioParam, ref ToolStripMenuItem btnRemitoParam, ref ToolStripMenuItem btnRemitoVistaPreviaParam, ref ComboBox cmbEstadoParam)
        {
            cmbTipo = cmbTipoParam;
            cmbCliente = cmbClienteParam;
            cmbEstado = cmbEstadoParam;
            txtObservacion = txtObsParam;
            txtEspesor = txtEspParam;
            lblEstado = lblEstadoParam;
            mskPeso = mskPesoParam;
            mskGramaje = mskGramajeParam;
            btnInforme = btnInformeTotalParam;
            btnParteDiario = btnParteDiarioParam;
            btnRemito = btnRemitoParam;
            btnRemitoVistaPrevia = btnRemitoVistaPreviaParam;
        }

        public void guardarFiltros()
        {
            config.BorrarArchivo();
            config.armarArchivoDefault();
            
            config.guardarFiltros((Fecha)indexTipoAnio, nroBobina.ToString(), (estado)indexTipoEstado, idEstado.ToString(), (Maquinista)indexTipoMaquinista, idMaquinista.ToString(), (TipoPapel)indexTipoPapel, idTipoPapel.ToString(), (Cliente)indexTipoCliente, idCliente.ToString(), (campoFecha)tipocampoFecha, porcentajeRemito.ToString());
        }

        public void actualizarDatos() 
        {
            frmMain.establecerControlesDatosCargados();
        }

    }
}
