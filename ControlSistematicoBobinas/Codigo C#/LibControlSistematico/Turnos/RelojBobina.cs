using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibControlSistematico
{
    public class RelojBobina : Reloj
    {

        double tiempoTranscurrido=0;
        const double tiempoEntreBobina = 600.0;
        //const Double tiempoEntreBobina = 60.0;

        public RelojBobina() 
        {
        
        }

        public bool esCopia(double offSet) 
        {
            tiempoTranscurrido= this.tiempoTranscurrido(offSet);
            bool EsCopia = (tiempoTranscurrido < tiempoEntreBobina) & this.prendido();
            if (!this.prendido()) this.iniciar();
            return EsCopia;
        }
    }
}
