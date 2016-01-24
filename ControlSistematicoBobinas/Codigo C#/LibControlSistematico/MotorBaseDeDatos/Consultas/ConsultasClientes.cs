using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibControlSistematico
{
    class ConsultasClientes
    {

        double registros_por_hoja;
        string baseDeDatos;

        public ConsultasClientes(double reg_por_hoja,string baseDeDatosParam)
        {
            registros_por_hoja = reg_por_hoja;
            baseDeDatos = baseDeDatosParam;
        }

        public string vaciarRegistros()
        {
            return "Truncate clientes";
        }

        public string borrarCliente(string id)
        {
            return "Delete from `"  + baseDeDatos +  "`.`clientes` WHERE `clientes`.`Index`=" + id + " limit 1";
        }

        public string getIdRemitoImpreso()
        {
            return ("select clientes.Index from `"  + baseDeDatos +  "`.`clientes` where clientes.Cliente = 'Remito Impreso' limit 1");
        }

        public string getIdStockBaradero()
        {
            return ("select clientes.Index from `"  + baseDeDatos +  "`.`clientes` where clientes.Cliente = 'Stock Baradero' limit 1");
        }

        public string agregarCliente(string cliente, string txtDirec, string txtLocalidad, string txtCP, string txtProv, string txtIVA, string txtCUIT)
        {
            return ("insert into lectorcodigo.clientes (`Index`,`Cliente`,`Direccion`,`Localidad`,`C.P.`,`Provincia`,`I.V.A.`,`CUIT`) values(NULL,'" + cliente + "','" + txtDirec + "','" + txtLocalidad + "','" + txtCP + "','" + txtProv + "','" + txtIVA + "','" + txtCUIT + "')");
        }

        public string updateCliente(string cliente, string id, string txtDirec, string txtLocalidad, string txtCP, string txtProv, string txtIVA, string txtCUIT)
        {
            return ("Update lectorcodigo.clientes set cliente='" + cliente + "', Direccion='" + txtDirec + "', Localidad='" + txtLocalidad + "', `C.P.`='" + txtCP + "', Provincia='" + txtProv + "', `I.V.A.`='" + txtIVA + "', CUIT='" + txtCUIT + "' where clientes.index =" + id + " limit 1");
        }

        public string cargaClientesCompleto()
        {
            return "Select * from `"  + baseDeDatos +  "`.`clientes`";
        }

        public string cantidadClientes()
        {
            return ("select count(*) from clientes limit 1;");
        }


        public string getClientes(int cantidad_registros)
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
            return "Select * from `"  + baseDeDatos +  "`.`clientes` limit " + hoja_inicial + "," + limite + ";";
        }

        public string accionPaginaClientes(int cantidad_registros, int contador_hoja)
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
            return ("Select * from clientes limit " + hoja_inicial + "," + limite + ";");
        }

    }
}
