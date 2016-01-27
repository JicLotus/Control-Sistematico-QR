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

        public string countPhonesHistory()
        {
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
            return "Select * from `"  + baseDeDatos +  "`.`historial_celular` limit " + hoja_inicial + "," + limite + ";";
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
            return ("Select * from historial_celular limit " + hoja_inicial + "," + limite + ";");
        }


    }
}
