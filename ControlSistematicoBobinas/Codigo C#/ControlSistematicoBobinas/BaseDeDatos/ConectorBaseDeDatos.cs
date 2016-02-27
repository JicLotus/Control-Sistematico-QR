using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LibControlSistematico;

using System.Windows.Forms;


namespace FormularioLectorCode
{
    public class ConectorBaseDeDatos : HacedorDeConsultas
    {
        private BindingSource bSource;


        public ConectorBaseDeDatos(string ip,string puerto,string timeOut,string baseDeDatos) : base(ip,puerto,timeOut,baseDeDatos)
        {
            bSource = new BindingSource();
            bSource.DataSource = intermediario.getDataTable();
        }
        
        public BindingSource Dar_BSource()
        {
            return bSource;
        }

    }
}
