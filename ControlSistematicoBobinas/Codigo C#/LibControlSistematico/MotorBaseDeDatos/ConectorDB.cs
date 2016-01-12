using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;


namespace LibControlSistematico
{

    public class ConectorDB
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;
        private string port;
        private string timeOut;
        private MySqlCommand CmdConsulta;

        public MySqlDataAdapter Adaptador;
        
        //Constructor
        public ConectorDB(string servidor, string basededatos, string usuario, string contrasenia,string puerto, string timeOut)
        {
            Initialize(servidor,basededatos,usuario,contrasenia,puerto,timeOut);
        }

        //Inicializamos Valores
        public void Initialize(string servidor, string basededatos, string usuario, string contrasenia,string puerto,string timeOutParam)
        {
                server = servidor;
                database = basededatos;
                uid = usuario;
                password = contrasenia;
                timeOut = timeOutParam;
                string connectionString;
                port = puerto;
                connectionString = "SERVER=" + server + ";PORT="+ puerto +";" + "DATABASE=" +
                database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";Connect Timeout="+timeOut+";Convert Zero Datetime=True";
            
                connection = new MySqlConnection(connectionString);       
        }


        public void setServerIP(string ip) 
        {
            string connectionString = "SERVER=" + ip + ";PORT=" + port + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";Connect Timeout="+timeOut+";Convert Zero Datetime=True";
            connection.ConnectionString = connectionString;
        }

        //Abrimos conexion DB
        public Boolean OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch
            {
                return false;
            }
        }

        //Cerramos Conexion DB
        public void CloseConnection()
        {
            connection.Close();
        }


        public Boolean HacerConsulta(String Consulta)
        {
            try { 
                CmdConsulta = new MySqlCommand(Consulta,connection);
                return true;   
            }
            catch(Exception error)
            {
                return false;
            }
        }

        public MySqlDataAdapter AdaptadorDataGrid()
        {
            MySqlDataAdapter Adaptador = new MySqlDataAdapter();
            Adaptador.SelectCommand = CmdConsulta;
            
            return Adaptador;
        }

        public MySqlDataReader LectorDatos()
        {
            MySqlDataReader Lector = CmdConsulta.ExecuteReader();
            Lector.Read();
            
            return Lector;
        }



    }
}
