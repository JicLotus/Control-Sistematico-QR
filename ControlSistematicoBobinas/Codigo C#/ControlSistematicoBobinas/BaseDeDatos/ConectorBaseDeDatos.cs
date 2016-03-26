using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LibControlSistematico;

using System.Windows.Forms;
using System.Data;

namespace FormularioLectorCode
{
    public class ConectorBaseDeDatos : HacedorDeConsultas
    {
        private BindingSource bSource;
        public PuenteBSource puenteBSource;

        public ConectorBaseDeDatos(string ip,string puerto,string timeOut,string baseDeDatos) : base(ip,puerto,timeOut,baseDeDatos)
        {
                bSource = new BindingSource();
                puenteBSource = new PuenteBSource();
                HacedorDeConsultas estaClase = (HacedorDeConsultas)this;
                puenteBSource.setHacedor(ref estaClase);
                intermediario.setPuenteBSource(ref puenteBSource);
        }

        public override void setBSource(ref DataTable dataTableParam)
        {
            bSource.DataSource = dataTableParam;
        }

        public BindingSource Dar_BSource()
        {
            return bSource;
        }

    }
}
