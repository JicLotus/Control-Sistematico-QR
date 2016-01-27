using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibControlSistematico
{
    class ConsultasProductos
    {

        double registros_por_hoja;
        string baseDeDatos;

        public ConsultasProductos(double reg_por_hoja,string baseDeDatosParam)
        {
            registros_por_hoja = reg_por_hoja;
            baseDeDatos = baseDeDatosParam;
        }

        public string vaciarRegistros()
        {
            return "Truncate productos";
        }

        public string dar_indice(string codigo_de_barras, string columna)
        {
            return ("Select productos." + columna + " from productos where Codigo='" + codigo_de_barras + "' Limit 1;");
        }

        public string CargaTipoPapel()
        {
            return ("select * from productos limit 0,30;");
        }

        public string borrarProducto(string id)
        {
            return "Delete from `"  + baseDeDatos +  "`.`productos` WHERE `productos`.`Index`=" + id + " limit 1";
        }



        public string agregarProducto(string tipo, string metros)
        {
            return ("insert into lectorcodigo.productos (`Index`,`Tipo`,`Metros`) values(NULL, '" + tipo + "','" + metros + "')");
        }

        public string updateProducto(string tipo, string metros, string id)
        {
            return ("UPDATE  `"  + baseDeDatos +  "`.`productos` SET  `tipo` =  '" + tipo + "' , `metros` =  '" + metros + "' WHERE  `productos`.`index` =" + id + " LIMIT 1");
        }

        public string ingresar_nuevo_producto(string codigo, string nombre)
        {
            return ("INSERT INTO  `"  + baseDeDatos +  "`.`productos` (`index` ,`Codigo` ,`Nombre_Producto`)VALUES (NULL ,  '" + codigo + "',  '" + nombre + "');");
        }

        public string cargaTipoPapelCompleto()
        {
            return "Select * from `"  + baseDeDatos +  "`.`productos`";
        }

        public string cantidadProductos()
        {
            return ("select count(*) from productos limit 1;");
        }

        public string getProductos(int cantidad_registros)
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
            return "Select * from `"  + baseDeDatos +  "`.`productos` limit " + hoja_inicial + "," + limite + ";";
        }

        public string accionPaginaProductos(int cantidad_registros, int contador_hoja)
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
            return ("Select * from productos limit " + hoja_inicial + "," + limite + ";");
        }


        public string getIndiceNombre(string nombre)
        {
            return "Select id from productos where nombre='" + nombre + "' limit 1";
        }
    }
}
