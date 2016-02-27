using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LectorCode;
using System.Windows.Forms;

namespace FormularioLectorCode
{
    public class usuario
    {
        private string nombre="";
        private string pass="";
        private privilegio priv=privilegio.USER;

        private DataGridView gw;
        private HacedorDeConsultas consultador;

        public usuario(ref HacedorDeConsultas consult, string nombreParam, DataGridView gww) 
        {
            consultador = consult;

            gw = gww;
            gw.DataSource = consultador.Dar_BSource();
            consultador.getUsers();
            nombre = nombreParam;
            this.cargarDatos();
            
        }

        public void cargarDatos() 
        {   
            int n = gw.Rows.Count;
            int indexNombre = gw.Columns["NOMBRE"].Index;
            int indexContr = gw.Columns["PASSWORD"].Index;
            int indexPriv = gw.Columns["PRIVILEGIO"].Index;
            
            for (int i = 0; i < n - 1; i++)
            {
                string nombreConsulta = gw[indexNombre, i].Value.ToString().ToUpper();
                
                if (nombreConsulta == nombre.ToUpper())
                {
                    string passConsulta = gw[indexContr, i].Value.ToString();
                    privilegio privConsulta = (privilegio)gw[indexPriv, i].Value;
                    pass = passConsulta; priv = privConsulta;
                    break;
                }
            }
        }

        public string getPass() 
        {
            return pass;
        }

        public privilegio getPrivilegio() 
        {
            return priv;
        }

    }
}
