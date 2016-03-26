using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

using MySql.Data.MySqlClient;
using LibControlSistematico;

using System.IO;
using System.Net;
using System.Text;

namespace LibControlSistematico
{
    public class IntermediarioConexion
    {

        public MySqlDataReader LectorDatos;
        public ConectorDB Conexion;
        public MySqlDataAdapter Adaptador;
        public DataTable DatosDeTabla;
        public PuenteBSource puenteBSource;

        public IntermediarioConexion(string ip, string puerto, string timeOut,string baseDeDatos)
        {
            //string hostname = "celulosabaradero2.dyndns.org";
            string ipe;
            try
            {
                IPHostEntry host;
                host = Dns.GetHostEntry(ip);
                ipe = host.AddressList[0].ToString();
            }
            catch (Exception e)
            {
                ipe = ip;
            }

            //Conexion = new ConectorDB(host.AddressList[0].ToString(), "lectorcodigo", "root", "",puerto);
            //Conexion = new ConectorDB("192.168.1.118", "lectorcodigo", "root", "",puerto);
            //Conexion = new ConectorDB("localhost", "lectorcodigo", "root", "",puerto);
            //Conexion = new ConectorDB("181.15.203.42", "lectorcodigo", "root", "",puerto); //ipserverCaseros
            //Conexion = new ConectorDB("192.168.1.190", "lectorcodigo", "root", "",puerto); //ipserverCaseros
            //Conexion = new ConectorDB("192.168.1.155", "lectorcodigo", "root", "",puerto); //ipLanBaradero
            //Conexion = new ConectorDB("181.14.173.208", "lectorcodigo", "root", "",puerto);//ipserverBaradero
            //Conexion = new ConectorDB(ip, "lectorcodigo", "root", "", puerto);

            Conexion = new ConectorDB(ipe, baseDeDatos, "root", "", puerto, timeOut);

            DatosDeTabla = new DataTable();
            
        }


        public void setPuenteBSource(ref PuenteBSource puenteBSourceParam)
        {
            puenteBSource = puenteBSourceParam;
            puenteBSource.setDataSource(ref DatosDeTabla);
        }


        public DataTable getDataTable() 
        {
            return DatosDeTabla;
        }

        public void setIp(string ip)
        {
            Conexion.setServerIP(ip);
        }

        public int getRows()
        {
            return DatosDeTabla.Rows.Count;
        }

        public void consultar(string consulta)
        {
            Conexion.OpenConnection();

            this.limpiarDataTable();

            Conexion.HacerConsulta(consulta);
            Adaptador = Conexion.AdaptadorDataGrid();
            Adaptador.Fill(DatosDeTabla);
            Adaptador.Update(DatosDeTabla);
            Conexion.CloseConnection();
        }



        private void limpiarDataTable()
        {
            DatosDeTabla.Clear();  //viejo clear, conviene hacer reset

            DatosDeTabla.Columns.Clear();

            DatosDeTabla = null;
            //bSource.DataSource = null;
            
            DatosDeTabla = new DataTable();
            if (puenteBSource!=null)
            {
                puenteBSource.borrarDataSource();
                puenteBSource.setDataSource(ref DatosDeTabla);
            }
            //bSource.DataSource = DatosDeTabla;
        }

        public string darDato(string consulta, string columna)
        {
            try
            {
                string nombre_producto;
                Conexion.OpenConnection();

                Conexion.HacerConsulta(consulta);

                LectorDatos = Conexion.LectorDatos();

                nombre_producto = LectorDatos[columna].ToString();
                Conexion.CloseConnection();

                return nombre_producto;
            }
            catch
            {
                Conexion.CloseConnection();
                return "";
            }

        }

        public int cantidad_hojas(ref int cantidad_registros_actuales, string consulta, double registros_por_hoja)
        {
            double cantidad_hojas_decimos;
            int cantidad_hojas;
            Conexion.OpenConnection();

            Conexion.HacerConsulta(consulta);
            LectorDatos = Conexion.LectorDatos();
            cantidad_registros_actuales = Convert.ToInt32(LectorDatos[0]);

            cantidad_hojas_decimos = cantidad_registros_actuales / registros_por_hoja;

            cantidad_hojas = Convert.ToInt32(Math.Round(cantidad_hojas_decimos));
            double resto = cantidad_hojas_decimos - cantidad_hojas;
            //En caso de que el redondeo no se haga al ser menor a ,5
            if (resto > 0) { cantidad_hojas++; }
            Conexion.CloseConnection();
            return cantidad_hojas;
        }


    }
}
