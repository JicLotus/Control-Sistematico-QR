using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibControlSistematico
{
    public class turno
    {
        int indiceMaquinistaActual = 0;
        string turnoActual;

        public string getTurno() 
        {
            string nombreTurnoActual="";

            if (turnoActual=="1")
            {
                nombreTurnoActual="21 a 5";
            }
            else if (turnoActual == "2")
            {
                nombreTurnoActual = "5 a 13";
            }
            else if (turnoActual == "3")
            {
                nombreTurnoActual = "13 a 21";
            }

            return nombreTurnoActual;
        }

        public string getIndiceTurno(int indiceMaquinista, int horaActual, int minutosActuales) 
        {
            //Turno turnoActual = Turno._21A5;

            if (indiceMaquinista != indiceMaquinistaActual) 
            {
                
                if ((horaActual >= 21 & horaActual <= 23) || (horaActual >= 0 & horaActual < 5) || (horaActual == 20 & minutosActuales >= 40))
                {
                    turnoActual = "1";
                }
                
                if (((horaActual >= 5 & horaActual < 13) || (horaActual == 4 & minutosActuales >= 40)))
                {
                    turnoActual = "2";
                }

                if (((horaActual >= 13 & horaActual < 21) || (horaActual == 12 & minutosActuales >= 40)) & turnoActual != "1")
                {
                    turnoActual = "3";
                }

            }

            indiceMaquinistaActual = indiceMaquinista;
            return turnoActual;
        }

        public string getTurnoInducido(string indiceTurno) 
        {
            turnoActual = indiceTurno;
            return this.getTurno();
        }

    }
}
