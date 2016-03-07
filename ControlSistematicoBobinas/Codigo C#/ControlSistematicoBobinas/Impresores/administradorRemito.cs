using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ControlSistematicoBobinas
{
    class administradorRemito
    {
        PrintDocument MyPrintDocument;
        Remito impresorRemito;


        public administradorRemito() 
        {
            MyPrintDocument = new PrintDocument();
            //Agregamos el metodo de dibujado para el documento del rotulo
            this.MyPrintDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.MyPrintDocument_PrintPage);
            //
        }


        public void imprimir(DataGridView dataGridView1, string celParam, List<string> laLista, double porcentajeRemito, bool mostrarRotulo)
        {
            
            if (setupThePrinting(dataGridView1,celParam,laLista,porcentajeRemito))
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


        private bool setupThePrinting(DataGridView dataGridView1, string celParam, List<string> laLista, double porcentajeRemito)
        {
            PrintDialog MyPrintDialog = new PrintDialog();
            MyPrintDialog.AllowCurrentPage = false;
            MyPrintDialog.AllowPrintToFile = false;
            MyPrintDialog.AllowSelection = false;
            MyPrintDialog.AllowSomePages = false;
            MyPrintDialog.PrintToFile = false;
            MyPrintDialog.ShowHelp = false;
            MyPrintDialog.ShowNetwork = false;

            if (laLista == null) return false;

            MyPrintDocument.DocumentName = "Remito";
            MyPrintDocument.PrinterSettings = MyPrintDialog.PrinterSettings;
            MyPrintDocument.DefaultPageSettings = MyPrintDialog.PrinterSettings.DefaultPageSettings;
            MyPrintDocument.DefaultPageSettings.Margins = new Margins(50, 10, 40, 40);

            impresorRemito = new Remito(dataGridView1, MyPrintDocument, celParam, laLista, porcentajeRemito);

            return true;
        }

        public bool drawRotulo(System.Drawing.Printing.PrintPageEventArgs e)
        {
            return impresorRemito.DrawDataGridView(e.Graphics);
        }

        public void MyPrintDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            bool more = this.drawRotulo(e);
            if (more == true)
                e.HasMorePages = false;
        }


    }
}
