using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibControlSistematico
{
    class ConsultasHistorialCelular
    {
        
        double registros_por_hoja;
        string baseDeDatos;
        private string clausula_where = "";
        private Fecha indexTipoAnio;
        private string day, month, year;
        private string day2, month2, year2;

        public ConsultasHistorialCelular(double reg_por_hoja,string baseDeDatosParam)
        {
            registros_por_hoja = reg_por_hoja;
            baseDeDatos = baseDeDatosParam;
        }

        public string vaciarRegistros()
        {
            return "Truncate historial_celular";
        }

        public string onemillion2()
        {
            return ("INSERT INTO  `"  + baseDeDatos +  "`.`historial_celular` (`Index` ,`Fecha` ,`Usuario` ,`Nro_Bobina` ,`Estado`)VALUES (NULL ,  '2014-11-13',  'asssada',  '11211',  'sacsacasc');");
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

        public string countPhonesHistory()
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

            return "Select count(*) from `"  + baseDeDatos +  "`.`historial_celular` limit 1";
        }

        public string getPhonesHistory(int cantidad_registros)
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
            return "Select * from `"  + baseDeDatos +  "`.`historial_celular` " + clausula_where+ " limit " + hoja_inicial + "," + limite + ";";
        }

        public string accionPaginaHistorialCelular(int cantidad_registros, int contador_hoja)
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
            return ("Select * from historial_celular " + clausula_where + " limit " + hoja_inicial + "," + limite + ";");
        }


    }
}
