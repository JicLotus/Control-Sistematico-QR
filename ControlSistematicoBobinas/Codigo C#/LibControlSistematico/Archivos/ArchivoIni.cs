using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LibControlSistematico
{
    public class ArchivoIni : Archivo
    {
        private const string pathConfig = "C:/conf.ini";

        //CASEROS
        //private string direccionIp = "192.168.1.190:Caseros LAN";
        //private string direccionIp2 = "celulosabaradero2.dyndns.org:Baradero";
        //private string mysqlTimeOut = "1000";

        //BARADERO
        private string direccionIp = "192.168.1.155:Baradero LAN";
        private string direccionIp2 = "celulosabaradero.dyndns.org:Caseros";
        private string mysqlTimeOut = "1";
        
        private string nameIp, nameIp2;
        private string puerto="3306";
        private string pathGuardadoInfo1 = "D:/";
        private string pathGuardadoInfo2 = "C:/";

        // 0=Normal(abre el panel inicial)
        // 1=Admin(abre directamente el panel Administrador)
        // 2=Operador(abre directamente el panel del Operario)
        private modo modoApertura=modo.NORMAL;

        //FILTROS
        private Fecha filtroFecha = Fecha.DIA;
        private Maquinista filtroMaquinista = Maquinista.TODOS;
        private estado filtroEstado = estado.TODOS;
        private TipoPapel filtroTipoPapel = TipoPapel.TODOS;
        private Cliente filtroCliente = Cliente.TODOS;
        private campoFecha CampoFecha = campoFecha.FECHA_SCANEO;

        private string nroBobina = "0";
        private string Estado = "0";
        private string maquinista  = "0";
        private string cliente = "0";
        private string tipoPapel = "0";
        private string porcentaje = "100,00";
        //FIN FILTROS

        //
        private int lineasArchivoDefault = 19;
        //

        public ArchivoIni()
        {
            pathFile = pathConfig;

            nameIp = direccionIp.Split(':')[1];
            nameIp2 = direccionIp2.Split(':')[1];
        }

        public override Dictionary<string, int> LeerArchivo(bool cargaTipo)
        {
            try
            {
                if (File.Exists(pathFile))
                {
                    using (StreamReader sr = new StreamReader(pathFile))
                    {
                        string s = sr.ReadLine();
                        int cantidadLineas=0;
                        while (s != null)
                        {
                            cantidadLineas++;
                            string[] split = s.Split('=');

                            switch (split[0])
                            {
                                case "IP":
                                    direccionIp = split[1];
                                    nameIp = direccionIp.Split(':')[1];
                                    break;
                                case "IP2":
                                    direccionIp2 = split[1];
                                    nameIp2 = direccionIp2.Split(':')[1];
                                    break;
                                case "PUERTO":
                                    puerto = split[1];
                                    break;
                                case "PATH1":
                                    pathGuardadoInfo1 = split[1];
                                    break;
                                case "PATH2":
                                    pathGuardadoInfo2 = split[1];
                                    break;
                                case "MODO":
                                    modoApertura = (modo)(Convert.ToInt32(split[1]));
                                    break;
                                case "MYSQLTIMEOUT":
                                    mysqlTimeOut = split[1];
                                    break;
                                case "FILTROFECHA":
                                    filtroFecha = (Fecha)(Convert.ToInt32(split[1]));
                                    break;
                                case "NROBOBINA":
                                    nroBobina = split[1];
                                    break;
                                case "FILTROESTADO":
                                    filtroEstado = (estado)(Convert.ToInt32(split[1]));
                                    break;
                                case "IDEESTADO":
                                    Estado = split[1];
                                    break;
                                case "FILTROMAQUINISTA":
                                    filtroMaquinista = (Maquinista)(Convert.ToInt32(split[1]));
                                    break;
                                case "IDMAQUINISTA":
                                    maquinista = split[1];
                                    break;
                                case "FILTROTIPOPAPEL":
                                    filtroTipoPapel = (TipoPapel)(Convert.ToInt32(split[1]));
                                    break;
                                case "IDTIPOPAPEL":
                                    tipoPapel = split[1];
                                    break;
                                case "FILTROCLIENTE":
                                    filtroCliente = (Cliente)(Convert.ToInt32(split[1]));
                                    break;
                                case "IDCLIENTE":
                                    cliente = split[1];
                                    break;
                                case "CAMPOFECHA":
                                    CampoFecha = (campoFecha)(Convert.ToInt32(split[1]));
                                    break;
                                case "PORCENTAJEREMITO":
                                    porcentaje = split[1];
                                    break;
                            }
                            s = sr.ReadLine();
                        }

                        if (cantidadLineas != lineasArchivoDefault)
                        {
                            this.BorrarArchivo();
                            this.armarArchivoDefault();
                            this.guardarFiltros(filtroFecha, nroBobina, filtroEstado, Estado, filtroMaquinista, maquinista, filtroTipoPapel, tipoPapel, filtroCliente, cliente, CampoFecha, porcentaje);
                        }
                    }
                }
                else
                {
                    this.armarArchivoDefault();
                    this.guardarFiltros(filtroFecha, nroBobina, filtroEstado, Estado, filtroMaquinista, maquinista, filtroTipoPapel, tipoPapel, filtroCliente, cliente, CampoFecha, porcentaje);
                }
            }
            catch (Exception e) 
            {
                this.BorrarArchivo();
                this.armarArchivoDefault();
                this.guardarFiltros(filtroFecha, nroBobina, filtroEstado, Estado, filtroMaquinista, maquinista, filtroTipoPapel, tipoPapel, filtroCliente, cliente, CampoFecha, porcentaje);
            }
            return null;
        }


        public void armarArchivoDefault() 
        {
            this.GuardarArchivo("IP=" + direccionIp);
            this.GuardarArchivo("IP2=" + direccionIp2);
            this.GuardarArchivo("PUERTO=" + puerto);
            this.GuardarArchivo("PATH1=" + pathGuardadoInfo1);
            this.GuardarArchivo("PATH2=" + pathGuardadoInfo2);
            this.GuardarArchivo("MODO=" + (Convert.ToInt32(modoApertura)).ToString());
            this.GuardarArchivo("MYSQLTIMEOUT=" + mysqlTimeOut);
        }

        public void guardarFiltros(Fecha filtroFechaParam,string nroBobinaParam,estado filtroEstadoParam,string EstadoParam ,Maquinista filtroMaquinistaParam,string maquinistaParam,TipoPapel filtroTipoPapelParam,string tipoPapelParam,Cliente filtroClienteParam,string clienteParam,campoFecha CampoFechaParam,string porcentajeParam)
        {
            //FILTROS
            this.GuardarArchivo("FILTROFECHA=" + (Convert.ToInt32(filtroFechaParam)).ToString());
            this.GuardarArchivo("NROBOBINA=" + nroBobinaParam);
            this.GuardarArchivo("FILTROESTADO=" + (Convert.ToInt32(filtroEstadoParam)).ToString());
            this.GuardarArchivo("IDEESTADO=" + EstadoParam);
            this.GuardarArchivo("FILTROMAQUINISTA=" + (Convert.ToInt32(filtroMaquinistaParam)).ToString());
            this.GuardarArchivo("IDMAQUINISTA=" + maquinistaParam);
            this.GuardarArchivo("FILTROTIPOPAPEL=" + (Convert.ToInt32(filtroTipoPapelParam)).ToString());
            this.GuardarArchivo("IDTIPOPAPEL=" + tipoPapelParam);
            this.GuardarArchivo("FILTROCLIENTE=" + (Convert.ToInt32(filtroClienteParam)).ToString());
            this.GuardarArchivo("IDCLIENTE=" + clienteParam);
            this.GuardarArchivo("CAMPOFECHA=" + (Convert.ToInt32(CampoFechaParam)).ToString());
            this.GuardarArchivo("PORCENTAJEREMITO=" + porcentajeParam);
        }

        public string getIp(int index) 
        {
            string selectedIp="";

            if (index==0)
                selectedIp = direccionIp.Split(':')[0];
            else selectedIp = direccionIp2.Split(':')[0];

            return selectedIp;
        }

        public string getIpName() 
        {
            return nameIp;
        }

        public string getIp2Name()
        {
            return nameIp2;
        }

        public string getPuerto() 
        {
            return puerto;
        }

        public modo getModoApertura()
        {
            return modoApertura;
        }

        public string getPathGuardadoInfo1()
        {
            return pathGuardadoInfo1;
        }

        public string getPathGuardadoInfo2()
        {
            return pathGuardadoInfo2;
        }

        public int getFiltroFecha(){
            return Convert.ToInt32(filtroFecha);
        }
        
        public int getNroBobina(){
            return Convert.ToInt32(nroBobina);
        }

        public int getFiltroEstado(){
            return Convert.ToInt32(filtroEstado);
        }

        public int getIndexEstado(){
            return Convert.ToInt32(Estado);
        }
    
        public int getFiltroMaquinista(){
            return Convert.ToInt32(filtroMaquinista);
        }    

        public int getIndexMaquinista(){
            return Convert.ToInt32(maquinista);
        }
        
        public int getFiltroTipoPapel(){
            return Convert.ToInt32(filtroTipoPapel);
        }

        public int getIndexTipoPapel(){
            return Convert.ToInt32(tipoPapel);
        }
        
        public int getFiltroCliente(){
            return Convert.ToInt32(filtroCliente);
        }

        public int getIndexCliente(){
            return Convert.ToInt32(cliente);
        }
         
        public int getTipoCampoFecha(){
            return Convert.ToInt32(CampoFecha);
        }
        
        public double getPorcentajeRemito()
        {
            return Convert.ToDouble(porcentaje);
        }

        public string getMysqlTimeOut() 
        {
            return mysqlTimeOut;
        }

    }
}
