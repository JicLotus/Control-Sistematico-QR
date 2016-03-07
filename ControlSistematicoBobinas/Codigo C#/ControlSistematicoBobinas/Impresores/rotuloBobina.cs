using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Data;
using System.Windows.Forms;

namespace ControlSistematicoBobinas
{
    public class rotuloBobina
    {
        private int PageWidth;
        private int PageHeight;
        private int LeftMargin;
        private int TopMargin;
        private int RightMargin;
        private int BottomMargin;
        private int pageW, pageH;
            
        private float paramAjuste = 1.7f;

        private float CurrentY,CurrentX;

        Image imagenQr;
        Image rectRedondeado;
        Image rect;
        Image rectRallado;
        Image rectRedondeadoRallado;

        public string tituloMaterialTipo,TituloCliente,tituloPesoNeto,maquinista, materialTipo, Formato, pesoNeto, nroBobina, Fecha, Gramaje, Cliente, Observaciones, Espesor, Hora, nroCopia, turno;

        public rotuloBobina(PrintDocument aPrintDocument, string materialtipo, string formato, string pesoneto, string nrobobina, string fecha, string gramaje, string espesor, string cliente, string observaciones, Image imagenqr,string maqParam, string turnoParam, string nroCopiaParam,string finBob) 
        {
            Fecha = "FECHA:  " + fecha;
            Hora = "HORA:  " + finBob;
            nroCopia = "Nº COPIA:  " + nroCopiaParam;

            maquinista = "MAQUINISTA: " + maqParam;
            Formato = "FORMATO(Mts):  " + formato;
            turno = "TURNO: " + turnoParam;
            Espesor = "ESPESOR:  " + espesor;
            nroBobina = "Nº BOBINA:  " + nrobobina;
            Gramaje = "GRAMAJE:  " + gramaje;

            tituloMaterialTipo = "CALIDAD:";
            materialTipo =  materialtipo;

            tituloPesoNeto = "PESO(Kg):";
            pesoNeto = pesoneto;

            TituloCliente = "CLIENTE:";
            Cliente = cliente;


            //string [] split = observaciones.Split(',');
            //string obsvFinal = "";
            
            /*foreach (string slt in split)
            {
                obsvFinal += slt + "\n";
            }*/

            Observaciones = "OBSERVACIONES:\n" + observaciones;
            imagenQr = imagenqr;

            rectRedondeado = ControlSistematicoBobinas.Properties.Resources.rectanguloRedondeado;
            rect = ControlSistematicoBobinas.Properties.Resources.rectangulo;
            rectRallado = ControlSistematicoBobinas.Properties.Resources.rectanguloRallado;
            rectRedondeadoRallado = ControlSistematicoBobinas.Properties.Resources.rectanguloRedondeadoRallado;

            if (!aPrintDocument.DefaultPageSettings.Landscape)
            {
                PageWidth = aPrintDocument.DefaultPageSettings.PaperSize.Width;
                PageHeight = aPrintDocument.DefaultPageSettings.PaperSize.Height;
            }
            else
            {
                PageHeight = aPrintDocument.DefaultPageSettings.PaperSize.Width;
                PageWidth = aPrintDocument.DefaultPageSettings.PaperSize.Height;
            }

            LeftMargin = aPrintDocument.DefaultPageSettings.Margins.Left;
            TopMargin = aPrintDocument.DefaultPageSettings.Margins.Top;
            RightMargin = aPrintDocument.DefaultPageSettings.Margins.Right;
            BottomMargin = aPrintDocument.DefaultPageSettings.Margins.Bottom;
            pageW= aPrintDocument.DefaultPageSettings.PaperSize.Width;
            pageH = aPrintDocument.DefaultPageSettings.PaperSize.Height;

        }

        public void dibujarRotulo(Graphics g)
        {
            CurrentX = (float)LeftMargin;
            CurrentY = (float)TopMargin;

            StringFormat PageStringFormat = new StringFormat();
            PageStringFormat.Trimming = StringTrimming.Word;
            PageStringFormat.FormatFlags = StringFormatFlags.NoWrap | StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
            PageStringFormat.Alignment = StringAlignment.Far;
            Font PageStringFont = new Font("Tahoma", 18, FontStyle.Regular, GraphicsUnit.Point);
            Font PageStringFont2 = new Font("Tahoma", 12, FontStyle.Regular, GraphicsUnit.Point);
            Font fuentePrimordial = new Font("Tahoma", 35, FontStyle.Regular | FontStyle.Bold, GraphicsUnit.Point);
            Font fuenteSecundaria = new Font("Tahoma", 17, FontStyle.Regular | FontStyle.Bold, GraphicsUnit.Point);

            //IMPRESION IMAGENES FONDO
            g.DrawImage(rect, CurrentX, CurrentY, rect.Width, rect.Height*paramAjuste);
            CurrentX += (float)rect.Width;
            g.DrawImage(rect, CurrentX, CurrentY, rect.Width - 20, rect.Height * paramAjuste);
            CurrentX += (float)rect.Width - 20;
            g.DrawImage(rect, CurrentX, CurrentY, rect.Width, rect.Height * paramAjuste);

            CurrentX = (float)LeftMargin;
            CurrentY += rect.Height * paramAjuste;

            g.DrawImage(rectRallado, CurrentX, CurrentY, rectRallado.Width + 122, rectRallado.Height * paramAjuste);
            CurrentX += rectRallado.Width + 122;
            g.DrawImage(rectRallado, CurrentX, CurrentY, rectRallado.Width + 126, rectRallado.Height * paramAjuste);

            CurrentY += (float)rectRallado.Height * paramAjuste + 30;
            CurrentX = (float)LeftMargin;

            g.DrawImage(rectRedondeadoRallado, CurrentX, CurrentY, rectRedondeadoRallado.Width, rectRedondeadoRallado.Height * paramAjuste);
            CurrentX += (float)rectRedondeadoRallado.Width;
            //g.DrawImage(rectRedondeado, CurrentX, CurrentY, 286 + imagenQr.Width, rectRedondeado.Height * paramAjuste);
            CurrentX+= (float)(286 + imagenQr.Width)/(float)(1.8);
            CurrentY += (rectRedondeado.Height / 5) * paramAjuste + 10;


            //*************************************
            //IMAGEN QR
            //*************************************
            g.DrawImage(imagenQr, CurrentX -360, CurrentY + 50, imagenQr.Width, imagenQr.Height);


            //*************************************
            //IMPRESION IMAGENES FONDO
            //*************************************
            CurrentX = LeftMargin + 15;

            g.DrawString(Fecha, PageStringFont, new SolidBrush(Color.Black), CurrentX, TopMargin + 35);
            g.DrawString(Hora, PageStringFont, new SolidBrush(Color.Black), CurrentX + 260, TopMargin + 35);
            g.DrawString(nroCopia, PageStringFont, new SolidBrush(Color.Black), CurrentX + 500, TopMargin + 35);

            g.DrawString(maquinista, PageStringFont, new SolidBrush(Color.Black), CurrentX , (CurrentY - 205) / paramAjuste);
            g.DrawString(Formato, PageStringFont, new SolidBrush(Color.Black), CurrentX + 400, (CurrentY - 205)/paramAjuste);

            g.DrawString(turno, PageStringFont, new SolidBrush(Color.Black), CurrentX, (CurrentY - 60) / paramAjuste);
            g.DrawString(Espesor, PageStringFont, new SolidBrush(Color.Black), CurrentX + 400, (CurrentY - 60)/paramAjuste);


            g.DrawString(nroBobina, PageStringFont, new SolidBrush(Color.Black), CurrentX, (CurrentY+93)/paramAjuste);

            g.DrawString(Gramaje, PageStringFont, new SolidBrush(Color.Black), CurrentX+400, (CurrentY+93)/paramAjuste);

            //*************************************
            //           MATERIALTIPO
            //*************************************
            g.DrawString(tituloMaterialTipo, PageStringFont, new SolidBrush(Color.Black), CurrentX+60, CurrentY - 50);
            g.DrawString(materialTipo, fuenteSecundaria, new SolidBrush(Color.Black), CurrentX, CurrentY - 10);

            //OBSERVACIONES
            g.DrawString(Observaciones, PageStringFont2, new SolidBrush(Color.Black), CurrentX+300, CurrentY - 115);

            CurrentY += (g.MeasureString(tituloMaterialTipo, PageStringFont).Height )*paramAjuste +50;

            //*************************************
            //           PESO
            //*************************************

            g.DrawString(tituloPesoNeto, PageStringFont, new SolidBrush(Color.Black), CurrentX+60, CurrentY-40);
            CurrentY += (g.MeasureString(tituloPesoNeto, PageStringFont).Height)*paramAjuste;

            g.DrawString(pesoNeto, fuentePrimordial, new SolidBrush(Color.Black), CurrentX+60, CurrentY-60);
            CurrentY += (g.MeasureString(pesoNeto, fuentePrimordial).Height) * paramAjuste;


            //*************************************
            //           CLIENTE
            //*************************************

            g.DrawString(TituloCliente, PageStringFont, new SolidBrush(Color.Black), CurrentX+60, CurrentY-70);
            CurrentY += g.MeasureString(TituloCliente, PageStringFont).Height;

            g.DrawString(Cliente, fuenteSecundaria, new SolidBrush(Color.Black), CurrentX , CurrentY - 60);
            CurrentY += g.MeasureString(Cliente, fuenteSecundaria).Height;

        }


        public bool drawRotulo(Graphics g)
        {
            try
            {
                dibujarRotulo(g);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Operation failed: " + ex.Message.ToString(), Application.ProductName + " - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

    }
}
