using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibControlSistematico
{
    class ConsultasUsuarios
    {

        double registros_por_hoja;
        string baseDeDatos;

        public ConsultasUsuarios(double reg_por_hoja,string baseDeDatosParam)
        {
            registros_por_hoja = reg_por_hoja;
            baseDeDatos = baseDeDatosParam;
        }

        public string vaciarRegistros()
        {
            return "Truncate usuarios";
        }

        public string borrarUsuario(string id)
        {
            return "Delete from usuarios where id=" + id + " limit 1";
        }


        public string agregarUsuario(string nombre, string pass, string priv)
        {
            return "INSERT INTO  `"  + baseDeDatos +  "`.`usuarios` (`id` ,`Nombre` ,`Password` ,`Privilegio`)VALUES (NULL ,  '" + nombre + "',  '" + pass + "',  '" + priv + "');";
        }

        public string updateUsuario(string id, string nombre, string pass, string priv)
        {
            return "UPDATE  `"  + baseDeDatos +  "`.`usuarios` SET  `Nombre` =  '" + nombre + "',`Password` =  '" + pass + "',`Privilegio` =  '" + priv + "' WHERE  `usuarios`.`id` =" + id + ";";
        }

        public string cantidadUsuarios()
        {
            return "Select count(*) from `"  + baseDeDatos +  "`.`usuarios` limit 1";
        }

        public string getUsers(int cantidad_registros)
        {
            string hoja_inicial = "0";
            if (cantidad_registros > registros_por_hoja)
            {
                hoja_inicial = (cantidad_registros - registros_por_hoja).ToString();
            }

            return ("Select id,Nombre,Password,Privilegio from usuarios limit " + hoja_inicial + "," + registros_por_hoja + ";");
        }

        public string accionPaginaUsuario(int cantidad_registros, int contador_hoja)
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
            return ("Select id,Nombre,Password,Privilegio from usuarios limit " + hoja_inicial + "," + limite + ";");
        }

        public string getIndiceNombre(string nombre)
        {
            return "Select id from usuarios where nombre='" + nombre + "' limit 1";
        }

    }
}
