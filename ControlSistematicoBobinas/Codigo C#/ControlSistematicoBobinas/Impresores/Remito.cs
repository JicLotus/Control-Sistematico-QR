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


namespace ControlSistematicoBobinas
{
    class Remito
    {
        private string celular;
        private string fecha;

        private string nombreCliente;
        private string localidad;
        private string provincia;

        private string iva;
        private string cuit;
        private string codigoPostal;
        private string direccion;

        private int PageWidth;
        private int PageHeight;
        private int LeftMargin;
        private int TopMargin;
        private int RightMargin;
        private int BottomMargin;
        private int pageW, pageH;

        private string digsCelu;

        private float CurrentX, CurrentY;

        Font font;
        Font font2;
        Font font3;
        Font font4;

        private DataGridView gw;
        double porcentajeRemito;

        public Remito(DataGridView gwParam,PrintDocument aPrintDocument, string celParam, List <string> datosCliente, double porcentaje)
        {
            celular = "Nro Celular:" + celParam;
            DateTime Hoy = DateTime.Today;
            string fecha_actual = Hoy.ToString("D");
            fecha = fecha_actual;        
          
            nombreCliente= datosCliente[0];
            direccion= datosCliente[1];
            localidad = datosCliente[2];
            codigoPostal = datosCliente[3];
            provincia =  datosCliente[4];
            iva = datosCliente[5];
            cuit = datosCliente[6];
            porcentajeRemito= porcentaje;
            
            font = new Font("Tahoma", 10, FontStyle.Regular, GraphicsUnit.Point);
            font2 = new Font("Tahoma", 8, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point);
            font3 = new Font("Tahoma", 8, FontStyle.Regular, GraphicsUnit.Point);
            font4 = new Font("Tahoma", 8, FontStyle.Bold, GraphicsUnit.Point);
            

            gw = gwParam;

            LeftMargin = aPrintDocument.DefaultPageSettings.Margins.Left;
            TopMargin = aPrintDocument.DefaultPageSettings.Margins.Top;
            RightMargin = aPrintDocument.DefaultPageSettings.Margins.Right;
            BottomMargin = aPrintDocument.DefaultPageSettings.Margins.Bottom;
            pageW = aPrintDocument.DefaultPageSettings.PaperSize.Width;
            pageH = aPrintDocument.DefaultPageSettings.PaperSize.Height;
        }


        private void DrawHeader(Graphics g)
        {
            CurrentX = (float)LeftMargin + 110;
            CurrentY = (float)TopMargin + 200;
            
            StringFormat PageStringFormat = new StringFormat();
            PageStringFormat.Trimming = StringTrimming.Word;
            PageStringFormat.FormatFlags = StringFormatFlags.NoWrap | StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
            PageStringFormat.Alignment = StringAlignment.Far;

           //g.DrawImage(FormularioLectorCode.Properties.Resources.imagenremito, 0, 0, FormularioLectorCode.Properties.Resources.imagenremito.Width / ((2999999 / 1000000)*(19/10))-35, FormularioLectorCode.Properties.Resources.imagenremito.Height / ((2999999 / 1000000)*(19/10))-25);

            g.DrawString(nombreCliente, font, new SolidBrush(Color.Black), CurrentX, CurrentY);
            CurrentY += 28;
            g.DrawString(direccion, font, new SolidBrush(Color.Black), CurrentX, CurrentY);
            CurrentY += 28;
            g.DrawString(localidad, font, new SolidBrush(Color.Black), CurrentX, CurrentY);

            CurrentY += 31;
            g.DrawString(iva, font, new SolidBrush(Color.Black), CurrentX, CurrentY);

            CurrentY -= 31;
            CurrentX += 210;
            g.DrawString(codigoPostal, font, new SolidBrush(Color.Black), CurrentX, CurrentY);
            CurrentX += 270;

            g.DrawString(provincia, font, new SolidBrush(Color.Black), CurrentX, CurrentY);
            g.DrawString(fecha, font, new SolidBrush(Color.Black), CurrentX-45, CurrentY-165);
            

            CurrentY += 31;
            g.DrawString(cuit, font, new SolidBrush(Color.Black), CurrentX, CurrentY);

            CurrentY += 50;

        }

        private bool DrawRows(Graphics g)
        {
            int n = gw.Rows.Count;


            Dictionary<Tuple<string, string>, List<Tuple<string, string>>> datosRemito = new Dictionary<Tuple<string, string>, List<Tuple<string, string>>>();

            int nroBobina = gw.Columns["NUMERO_BOBINA"].Index;
            int indexpeso = gw.Columns["PESO"].Index;
            int indextipo = gw.Columns["TIPO"].Index;
            int indexmetros = gw.Columns["METROS"].Index;
            int indexcelu = gw.Columns["CELULAR"].Index;
            int indexSeleccion = gw.Columns["SELECCION"].Index;

            List<string> lcelu = new List<string>();
            

            for (int i = 0; i < n - 1; i++)
            {
                if ((bool)gw[indexSeleccion, i].Value)
                {

                    string bobina = gw[nroBobina, i].Value.ToString();
                    string peso = gw[indexpeso, i].Value.ToString();
                    string tipo = gw[indextipo, i].Value.ToString();
                    string metros = gw[indexmetros, i].Value.ToString();
                    string celular = gw[indexcelu, i].Value.ToString();

                    if (!lcelu.Contains(celular) & celular != "0") { lcelu.Add(celular); }

                    Tuple<string, string> tupla = new Tuple<string, string>(tipo, metros);
                    Tuple<string, string> tupla2 = new Tuple<string, string>(bobina, peso);

                    if (!datosRemito.ContainsKey(tupla))
                    {
                        datosRemito[tupla] = new List<Tuple<string, string>>();
                    }

                    datosRemito[tupla].Add(tupla2);
                }
            }

            string celulares = "Nro Celulares: ";

            foreach (string celu in lcelu)
            {
                celulares += (celu + "\n");
            }

            g.DrawString(celulares, font, new SolidBrush(Color.Black), 600, 150);


            CurrentY = 470;
            CurrentX = 170;
            double pesoTotal=0;
            double pesoParcial = 0;
            float currentYParcial;
            float currentXParcial;

            
            foreach (Tuple<string, string>key in datosRemito.Keys) 
            {
                if (CurrentY >= 880 & CurrentX == 170) { CurrentY = 470; CurrentX = 380; }
                g.DrawString("Kgs. De Papel " + key.Item1 + " " + key.Item2 + " Mts", font3, new SolidBrush(Color.Black), CurrentX, CurrentY);
                currentXParcial = CurrentX - 55;
                currentYParcial = CurrentY;
                CurrentY += 20;
                g.DrawString("N° bobina", font2, new SolidBrush(Color.Black), CurrentX-40, CurrentY);
                g.DrawString("Peso(kgs)", font2, new SolidBrush(Color.Black), CurrentX + 30, CurrentY);
                CurrentY += 20;

                foreach (Tuple<string,string>valor in datosRemito[key])
                {
                    if (CurrentY >= 880 & CurrentX == 170) { CurrentY = 470; CurrentX = 380; }
                    g.DrawString(valor.Item1.ToString(), font3, new SolidBrush(Color.Black), CurrentX-20, CurrentY);
                    double pesobobina=Convert.ToDouble(valor.Item2);
                    g.DrawString(pesobobina.ToString(), font3, new SolidBrush(Color.Black), CurrentX + 25, CurrentY);
                    pesoParcial += Convert.ToDouble(valor.Item2.ToString());
                    CurrentY += 15;
                }
                if (CurrentY >= 880 & CurrentX == 170) { CurrentY = 470; CurrentX = 380; }
                pesoParcial = pesoParcial * porcentajeRemito;
                g.DrawString(pesoParcial.ToString(), font4, new SolidBrush(Color.Black), currentXParcial, currentYParcial);
                pesoTotal += pesoParcial;
                pesoParcial = 0;
            }


            g.DrawString(pesoTotal.ToString() + " kgs", font, new SolidBrush(Color.Black), 630, 1070);

            return true;
        }

        public bool DrawDataGridView(Graphics g)
        {
            try
            {
                DrawHeader(g);
                bool bContinue = DrawRows(g);
                
                return bContinue;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Operation failed: " + ex.Message.ToString(), Application.ProductName + " - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }


    }
}
