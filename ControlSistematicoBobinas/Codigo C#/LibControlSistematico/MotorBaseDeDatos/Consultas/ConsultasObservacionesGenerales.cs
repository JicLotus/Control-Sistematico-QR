using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibControlSistematico
{
    class ConsultasObservacionesGenerales
    {

        double registros_por_hoja;
        string baseDeDatos;

        private string clausula_where = "";
        private Fecha indexTipoAnio;
        private string day, month, year;
        private string day2, month2, year2;

        public ConsultasObservacionesGenerales(double reg_por_hoja,string baseDeDatosParam)
        {
            registros_por_hoja = reg_por_hoja;
            baseDeDatos = baseDeDatosParam;
        }

        public string vaciarRegistros()
        {
            return "Truncate observaciones_generales";
        }

        public string getObservacionDia()
        {
            DateTime Hoy = DateTime.Today;

            return "select * from `"  + baseDeDatos +  "`.`observaciones_generales` where (day(Fecha)=" + Hoy.Day + " and month(Fecha)=" + Hoy.Month + " and year(Fecha)=" + Hoy.Year + ")";
        }

        
        public string getObservacionFechaSeleccionada(string day,string month,string year)
        {
            string consultaInicial = "select * from `"  + baseDeDatos +  "`.`observaciones_generales` ";
            string where = "where (day(Fecha)=" + day + " and month(Fecha)=" + month + " and year(Fecha)=" + year + ")";

            return (consultaInicial + where);
        }

        public string agregarObservacionGeneral(string observacion, string fecha, string horario, string maquinista)
        {
            return "INSERT INTO `"  + baseDeDatos +  "`.`observaciones_generales` (`Index`, `Observacion`, `Fecha`, `Horario`, `Maquinista`) VALUES (NULL, '" + observacion + "', '" + fecha + "', '" + horario + "', '" + maquinista + "');";
        }

        public string countObservacionesGenerales()
        {

            switch (indexTipoAnio)
            {
                case Fecha.DIA:
                    clausula_where = "where (day(Fecha)=" + day + " and month(Fecha)=" + month + " and year(Fecha)=" + year + ")";
                    break;
                case Fecha.MES:
                    clausula_where = "where (month(Fecha)=" + month + " and year(Fecha)=" + year + ")";
                    break;
                case Fecha.AÑO:
                    clausula_where = "where (year(Fecha)=" + year + ")";
                    break;
                case Fecha.DESDEHASTA:
                    clausula_where = "where (Fecha BETWEEN '" + year + "/" + month + "/" + day + "' and '" + year2 + "/" + month2 + "/" + day2 + "'" + ")";
                    break;
            }

            return "Select count(*) from `" + baseDeDatos + "`.`observaciones_generales` " + clausula_where + " limit 1";
        }

        public string getObservacionesGenerales(int cantidad_registros)
        {
            double hoja_inicial = 0;
            double limite = registros_por_hoja;
            if (cantidad_registros > registros_por_hoja)
            {
                hoja_inicial = (cantidad_registros - (registros_por_hoja));
                if (hoja_inicial < 0)
                {
                    limite = (cantidad_registros - (registros_por_hoja));
                    hoja_inicial = 0;
                }
            }
            return "Select * from `" + baseDeDatos + "`.`observaciones_generales` " + clausula_where + " limit " + hoja_inicial + "," + limite + ";";
        }

        public void setIndicesFiltro(int indexTipoAnioParam, string dayParam, string monthParam, string yearParam, string dayParam2, string monthParam2, string yearParam2)
        {
            indexTipoAnio = (Fecha)indexTipoAnioParam;
            day = dayParam;
            month = monthParam;
            year = yearParam;
            day2 = dayParam2;
            month2 = monthParam2;
            year2 = yearParam2;
        }

        public string accionPaginaObsGenerales(int cantidad_registros, int contador_hoja)
        {
            double hoja_inicial = 0;
            double limite = registros_por_hoja;
            if (cantidad_registros > registros_por_hoja)
            {
                hoja_inicial = (cantidad_registros - (registros_por_hoja * contador_hoja));
                if (hoja_inicial < 0)
                {
                    limite = (cantidad_registros - (registros_por_hoja * (contador_hoja - 1)));
                    hoja_inicial = 0;
                }
            }
            return ("Select * from observaciones_generales " + clausula_where + "  limit " + hoja_inicial + "," + limite + ";");
        }

        public string cantidadObservaciones()
        {
            return "Select count(*) from `" + baseDeDatos + "`.`observaciones_generales` limit 1";
        }

        public string getIndiceObservacion(string nombre)
        {
            return "Select observaciones_generales.index from observaciones_generales where observacion='" + nombre + "' limit 1";
        }

        public string borrarObservacionGeneral(string id)
        {
            return "Delete from observaciones_generales where observaciones_generales.index=" + id + " limit 1";
        }

        public string updateObservacionGeneral(string observacion, string fecha, string horario, string maquinista,string id)
        {
            return "UPDATE  `"+ baseDeDatos +"`.`observaciones_generales` SET  `Observacion` =  '"+ observacion +"' WHERE  `observaciones_generales`.`Index` =" + id;
        }

    }
}
