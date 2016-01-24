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

        public string setObservacionGeneral(string observacion, string fecha, string horario, string maquinista)
        {
            return "INSERT INTO `"  + baseDeDatos +  "`.`observaciones_generales` (`Index`, `Observacion`, `Fecha`, `Horario`, `Maquinista`) VALUES (NULL, '" + observacion + "', '" + fecha + "', '" + horario + "', '" + maquinista + "');";
        }

        public string countObservacionesGenerales()
        {
            return "Select count(*) from `"  + baseDeDatos +  "`.`observaciones_generales` limit 1";
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
            return "Select * from `"  + baseDeDatos +  "`.`observaciones_generales` limit " + hoja_inicial + "," + limite + ";";
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
            return ("Select * from observaciones_generales limit " + hoja_inicial + "," + limite + ";");
        }

    }
}
