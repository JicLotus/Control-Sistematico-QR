//****************************************************************************************************
//****************************************************************************************************
//                                   FORMULARIO DE FILTROS
//Author: Jose Ignacio Castelli
//****************************************************************************************************
//****************************************************************************************************


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using LibControlSistematico;
using System.Globalization;
using System.Threading;

namespace ControlSistematicoBobinas
{
    public partial class Filtros : Form
    {
        aparienciaFormulario refApariencia;

        public Filtros(ref aparienciaFormulario apariencia)
        {
            InitializeComponent();
            refApariencia = apariencia;
        }

        private void Filtros_Load(object sender, EventArgs e)
        {
            mskPorciento.ValidatingType = typeof(double);
            this.completarFiltros();
            this.configuracionHabiitacionControles();
        }

        private void Filtros_Shown(object sender, EventArgs e)
        {
        }

        public void configuracionHabiitacionControles()
        {
            this.habilitacionFiltrosPrincipales();
            this.habilitacionFiltroCliente();
            this.habilitacionFiltroEstado();
            this.habilitacionFiltroMaquinista();
            this.habilitacionFiltroTipoPapel();
        }

        private void completarFiltros()
        {
            this.completarCmbDatos();
            this.completarCmbTiposCarga();
            this.completarCajasInformacion();
            this.completarIndicesCmb();
        }

        private void completarIndicesCmb()
        {
            cmbMaquinista.SelectedIndex = cmbMaquinista.Items.IndexOf(refApariencia.getNombreMaquinista());
            cmbCliente.SelectedIndex = cmbCliente.Items.IndexOf(refApariencia.getNombreCliente());
            cmbEstado.SelectedIndex = cmbEstado.Items.IndexOf(refApariencia.getNombreEstado());
            cmbPapel.SelectedIndex = cmbPapel.Items.IndexOf(refApariencia.getNombreTipoPapel());
        }

        private void completarCmbDatos()
        {
            this.completarComboBox(refApariencia.getHashMaquinistas(), cmbMaquinista, true);
            this.completarComboBox(refApariencia.getHashClientes(), cmbCliente, false);
            this.completarComboBox(refApariencia.getHashClientes(), cmbEstado, false);
            this.completarComboBox(refApariencia.getHashTipoPapeles(), cmbPapel, true);
        }

        private void completarCmbTiposCarga()
        {
            cmbTipoEstado.SelectedIndex = refApariencia.getIndexTipoEstado();
            cmbTipoMaquinista.SelectedIndex = refApariencia.getIndexTipoMaquinista();
            cmbTipoPapel.SelectedIndex = refApariencia.getIndexTipoPapel();
            cmbTipoCliente.SelectedIndex = refApariencia.getIndexTipoCliente();
            cmbTipoPrincipal.SelectedIndex = refApariencia.getIndexTipoPrincipal();
        }

        private void completarCajasInformacion()
        {
            mskPorciento.Text = refApariencia.getPorcentajeRemito().ToString();
            txtNroBobina.Text = refApariencia.getNroBobina().ToString();

            /*string textoFechaDesde = refApariencia.getDateDay()+"/"+refApariencia.getDateMonth() +"/" + refApariencia.getDateYear();
            string textoFechaHasta = refApariencia.getHastaDay() + "/" + refApariencia.getHastaMonth() + "/" + refApariencia.getHastaYear();
            
            DateTime fechaDesde = Convert.ToDateTime(textoFechaDesde, new CultureInfo("es-ES"));
            DateTime fechaHasta = Convert.ToDateTime(textoFechaHasta, new CultureInfo("es-ES"));*/

            int day = Convert.ToInt32(refApariencia.getDateDay());
            int month = Convert.ToInt32(refApariencia.getDateMonth());
            int year = Convert.ToInt32(refApariencia.getDateYear());
            int day2 = Convert.ToInt32(refApariencia.getHastaDay());
            int month2 = Convert.ToInt32(refApariencia.getHastaMonth());
            int year2 = Convert.ToInt32(refApariencia.getHastaYear());
            DateTime fechaDesde = new DateTime(year, month, day);
            DateTime fechaHasta = new DateTime(year2, month2, day2);

            cmbDesde.Value = fechaDesde;
            cmbHasta.Value = fechaHasta;

            if ((campoFecha)refApariencia.getTipoCampoFecha() == campoFecha.FECHA_FABRICACION) fechaFabricacion.Checked = true;
            else fechaScaneo.Checked = true;
        }

        private void completarComboBox(Dictionary<string, string> hash, ComboBox cmb, Boolean key)
        {
            BindingSource bindingSource1 = new BindingSource();

            if (key) bindingSource1.DataSource = hash.Keys.ToList();
            else bindingSource1.DataSource = hash.Values.ToList();

            cmb.DataSource = bindingSource1.DataSource;

        }

        private void cmbTipoPrincipal_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.habilitacionFiltrosPrincipales();
        }

        private void habilitacionFiltrosPrincipales()
        {
            if (cmbTipoPrincipal.SelectedIndex == Convert.ToInt32(Fecha.DESDEHASTA))
            {
                cmbHasta.Enabled = true;
                cmbDesde.Enabled = true;
                txtNroBobina.Enabled = false;
            }
            else if (cmbTipoPrincipal.SelectedIndex == Convert.ToInt32(Fecha.NROBOBINA))
            {
                txtNroBobina.Enabled = true;
                cmbDesde.Enabled = false;
                cmbHasta.Enabled = false;
            }
            else
            {
                cmbDesde.Enabled = true;
                cmbHasta.Enabled = false;
                txtNroBobina.Enabled = false;
            }
        }

        private void habilitacionFiltroMaquinista()
        {
            if (cmbTipoMaquinista.SelectedIndex == Convert.ToInt32(Maquinista.TODOS))
            {
                cmbMaquinista.Enabled = false;
            }
            else cmbMaquinista.Enabled = true;

        }

        private void habilitacionFiltroCliente()
        {
            if (cmbTipoCliente.SelectedIndex == Convert.ToInt32(Cliente.TODOS))
            {
                cmbCliente.Enabled = false;
            }
            else cmbCliente.Enabled = true;

        }

        private void habilitacionFiltroEstado()
        {
            if (cmbTipoEstado.SelectedIndex == Convert.ToInt32(estado.TODOS))
            {
                cmbEstado.Enabled = false;
            }
            else cmbEstado.Enabled = true;
        }

        private void habilitacionFiltroTipoPapel()
        {
            if (cmbTipoPapel.SelectedIndex == Convert.ToInt32(TipoPapel.TODOS))
            {
                cmbPapel.Enabled = false;
            }
            else cmbPapel.Enabled = true;
        }

        private void cmbTipoEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.habilitacionFiltroEstado();
        }

        private void cmbTipoMaquinista_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.habilitacionFiltroMaquinista();
        }

        private void cmbTipoPapel_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.habilitacionFiltroTipoPapel();
        }

        private void cmbTipoCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.habilitacionFiltroCliente();
        }

        private void AplicarCambios_Click(object sender, EventArgs e)
        {
            int CampoFecha = (int)campoFecha.FECHA_SCANEO;
            if (fechaFabricacion.Checked == true) CampoFecha = (int)campoFecha.FECHA_FABRICACION;

            refApariencia.setPorcentajeRemito(mskPorciento.Text);
            refApariencia.setNroBobina(txtNroBobina.Text);
            refApariencia.setFechaDede(cmbDesde.Value);
            refApariencia.setFechaHasta(cmbHasta.Value);
            refApariencia.setCampoFecha(CampoFecha);

            refApariencia.setIdMaquinista(cmbMaquinista.Text);
            refApariencia.setIdCliente(cmbCliente.Text);
            refApariencia.setIdEstado(cmbEstado.Text);
            refApariencia.setIdTipoPapel(cmbPapel.Text);

            refApariencia.setTipoEstado(cmbTipoEstado.SelectedIndex);
            refApariencia.setTipoMaquinista(cmbTipoMaquinista.SelectedIndex);
            refApariencia.setTipoPapel(cmbTipoPapel.SelectedIndex);
            refApariencia.setTipoCliente(cmbTipoCliente.SelectedIndex);
            refApariencia.setTipoPrincipal(cmbTipoPrincipal.SelectedIndex);

            refApariencia.guardarFiltros();

            refApariencia.actualizarDatos();

            this.Close();
        }

        private void frmPaginas_Enter(object sender, EventArgs e)
        {

        }


    }
}
