using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace LibControlSistematico
{
    public class PuenteBSource
    {

        DataTable datosDeTabla;
        HacedorDeConsultas hacedor;
    
        public PuenteBSource() 
        {
        
        
        }

        
        public void setDataSource(ref DataTable datosDetablaParam) 
        {
            datosDeTabla = datosDetablaParam;
            hacedor.setBSource(ref datosDeTabla);
        }

        public void borrarDataSource()
        {
            datosDeTabla = null;
        }

        public void setHacedor(ref HacedorDeConsultas hacedorParam)
        {
            hacedor = hacedorParam;
        }

        public DataTable getDataSource() 
        {
            return datosDeTabla;
        }

        public int getRows() 
        {
            return datosDeTabla.Rows.Count;
        }

    }
}
