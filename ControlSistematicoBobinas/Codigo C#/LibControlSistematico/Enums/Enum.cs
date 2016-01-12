using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibControlSistematico
{
    public enum Fecha { DIA=0, MES, AÑO, DESDEHASTA, NROBOBINA};
    public enum Maquinista { TODOS=0, NOMBRE};
    public enum estado { TODOS = 0, NOMBRE, EXCEPTO};
    public enum Cliente { TODOS=0, NOMBRE };
    public enum TipoPapel { TODOS=0, NOMBRE};
    public enum tipoCarga { DATOS_CARGADOS=0, PRODUCTOS, MAQUINISTAS, CLIENTES, USUARIOS, PHONESHISTORY, OBSERVACIONESGENRALES}
    public enum privilegio { ADMIN=1, USER};
    public enum modo { NORMAL=0,ADMIN,OPERARIO};
    public enum campoFecha { FECHA_SCANEO=0, FECHA_FABRICACION };
    public enum Turno { _21A5 = 1, _5A13, _13A21 };

}
