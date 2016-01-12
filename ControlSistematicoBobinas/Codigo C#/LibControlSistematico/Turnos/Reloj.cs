using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibControlSistematico
{
    public class Reloj
    {
        public DateTime inicio, detener;

        public bool iniciado;

        public Reloj() 
        {
            iniciado = false;
        }

        public double tiempoTranscurrido(double offSet)
        {
            detener = DateTime.Now.AddSeconds(offSet);
            TimeSpan transcurrido = detener.Subtract(inicio);
            inicio = DateTime.Now;
            return transcurrido.TotalSeconds;
        }

        public void iniciar() 
        {
            if (iniciado == false)
            { 
                inicio = DateTime.Now;
                iniciado = true;
            }
        }

        public bool prendido() 
        {
            return iniciado;
        }

        public void setInicio() 
        {
            if (iniciado == false) { 
                inicio = DateTime.Now;
            }
        }


    }
}
