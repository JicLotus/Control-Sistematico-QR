
using Gma.QrCodeNet.Encoding.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ControlSistematicoBobinas
{
    public class administradorRotuloBobina
    {
        PrintDocument MyPrintDocument;
        ControlSistematicoBobinas.rotuloBobina impresorRotulo;
        QrCodeImgControl qrCodeImgControl1;
        

        public administradorRotuloBobina() 
        {
            MyPrintDocument = new PrintDocument();
            this.configImgQr();
            //Agregamos el metodo de dibujado para el documento del rotulo
            this.MyPrintDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.MyPrintDocument_PrintPage);
            //
        }

        public void configImgQr() 
        {
            qrCodeImgControl1 = new QrCodeImgControl();
            qrCodeImgControl1.Width = 400;
            qrCodeImgControl1.Height = 400;
            qrCodeImgControl1.BackgroundImageLayout = ImageLayout.Tile;   
        }

        public void imprimir(string textoQr, string cmbTipe, string txtFormat, string txtWeight, string nroBobinaActual, string txtDate, string txtCoil, string txtEspesor, string cmbCliente, string observacionFinal, string nombreMaquinista, string turnoNombre, string nroCopia, string finBob, bool mostrarRotulo)
        {
            armarCodeQr(textoQr);

            if (setupThePrinting(cmbTipe, txtFormat, txtWeight, nroBobinaActual, txtDate, txtCoil, txtEspesor, cmbCliente, observacionFinal, nombreMaquinista, turnoNombre, nroCopia,finBob))
            {
                PrintPreviewDialog MyPrintPreviewDialog = new PrintPreviewDialog();
                MyPrintPreviewDialog.Document = MyPrintDocument;

                if (mostrarRotulo) 
                {
                    MyPrintPreviewDialog.ShowDialog();
                }
                else
                { 
                    //MessageBox.Show(MyPrintDocument.PrinterSettings.PrinterName.ToString());
                    MyPrintDocument.Print();
                }

            }

        }

        private void armarCodeQr(string textoQr)
        {
            DateTime Hoy = DateTime.Today;
            string fecha_actual = Hoy.ToString("yyyy-MM-dd");

            qrCodeImgControl1.Text = textoQr;
            
        }

        private bool setupThePrinting(string cmbTipe,string txtFormat,string txtWeight,string nroBobinaActual,string txtDate,string txtCoil,string txtEspesor, string cmbCliente,string observacionFinal,string nombreMaquinista,string turnoNombre, string nroCopia,string finBob)
        {
            PrintDialog MyPrintDialog = new PrintDialog();
            MyPrintDialog.AllowCurrentPage = false;
            MyPrintDialog.AllowPrintToFile = false;
            MyPrintDialog.AllowSelection = false;
            MyPrintDialog.AllowSomePages = false;
            MyPrintDialog.PrintToFile = false;
            MyPrintDialog.ShowHelp = false;
            MyPrintDialog.ShowNetwork = false;


            MyPrintDocument.DocumentName = "ROTULO";
            MyPrintDocument.PrinterSettings = MyPrintDialog.PrinterSettings;
            MyPrintDocument.DefaultPageSettings = MyPrintDialog.PrinterSettings.DefaultPageSettings;
            MyPrintDocument.DefaultPageSettings.Margins = new Margins(10, 100, 70, 120);

            impresorRotulo = new ControlSistematicoBobinas.rotuloBobina(MyPrintDocument, cmbTipe, txtFormat, txtWeight, nroBobinaActual, txtDate, txtCoil, txtEspesor, cmbCliente, observacionFinal, qrCodeImgControl1.Image, nombreMaquinista, turnoNombre, nroCopia,finBob);

            return true;
        }

        public bool drawRotulo(System.Drawing.Printing.PrintPageEventArgs e)
        {
            return impresorRotulo.drawRotulo(e.Graphics);
        }

        public void MyPrintDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            bool more = this.drawRotulo(e);
            if (more == true)
                e.HasMorePages = false;
        }

    }
}
