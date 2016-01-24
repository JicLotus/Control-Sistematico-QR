using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibControlSistematico
{
    class ConsultasMaquinistas
    {

        double registros_por_hoja;
        string baseDeDatos;

        public ConsultasMaquinistas(double reg_por_hoja,string baseDeDatosParam)
        {
            registros_por_hoja = reg_por_hoja;
            baseDeDatos = baseDeDatosParam;
        }

        public string vaciarRegistros()
        {
            return "Truncate maquinista";
        }

        public string borrarMaquinista(string id)
        {
            return "Delete from `"  + baseDeDatos +  "`.`maquinista` WHERE `maquinista`.`Index`=" + id + " limit 1";
        }

        public string agregarMaquinista(string maquinista, string ayudante)
        {
            return ("insert into lectorcodigo.maquinista (`Index`,`Maquinista`,`Ayudante`) values(NULL, '" + maquinista + "','" + ayudante + "')");
        }


        public string updateMaquinista(string maquinista, string ayudante, string id)
        {
            return ("UPDATE lectorcodigo.maquinista set maquinista ='" + maquinista + "' , ayudante = '" + ayudante + "' where maquinista.index =" + id + " limit 1");
        }

        public string cargaMaquinistasCompleto()
        {
            return "Select * from `"  + baseDeDatos +  "`.`maquinista`";
        }

        public string cantidadMaquinistas()
        {
            return "Select count(*) from `"  + baseDeDatos +  "`.`maquinista` limit 1";
        }

        public string carga_maquinistas(int cantidad_registros)
        {
            string hoja_inicial = "0";
            if (cantidad_registros > registros_por_hoja)
            {
                hoja_inicial = (cantidad_registros - registros_por_hoja).ToString();
            }
            return ("select * from maquinista limit " + hoja_inicial + "," + registros_por_hoja + ";");
        }

        public string accionPaginaMaquinista(int cantidad_registros, int contador_hoja)
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
            return ("Select * from maquinista limit " + hoja_inicial + "," + limite + ";");
        }

    }
}
