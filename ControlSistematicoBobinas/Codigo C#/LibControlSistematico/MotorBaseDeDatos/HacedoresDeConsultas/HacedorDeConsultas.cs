using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LibControlSistematico;
using System.Globalization;
using System.IO;


namespace LibControlSistematico
{
    public class HacedorDeConsultas
    {

        public int cantidad_registros_actuales;
        
        private ConsultasBobinas consultasBobinas;
        private ConsultasClientes consultasClientes;
        private ConsultasHistorialCelular consultasHistorialCelular;
        private ConsultasMaquinistas consultasMaquinistas;
        private ConsultasObservacionesGenerales consultasObservacionesGenerales;
        private ConsultasProductos consultasProductos;
        private ConsultasUsuarios consultasUsuarios;

        private int regs_hoja;
        protected IntermediarioConexion intermediario;
        

        public HacedorDeConsultas(string ip, string puerto, string timeOut,string baseDeDatos)
        {
            this.inicializarConsultas(baseDeDatos);
            intermediario = new IntermediarioConexion(ip, puerto, timeOut,baseDeDatos);
        }

        private void inicializarConsultas(string baseDeDatos) 
        {
            regs_hoja = 62;
            consultasBobinas = new ConsultasBobinas(regs_hoja, baseDeDatos);
            consultasClientes = new ConsultasClientes(regs_hoja, baseDeDatos);
            consultasHistorialCelular = new ConsultasHistorialCelular(regs_hoja, baseDeDatos);
            consultasMaquinistas = new ConsultasMaquinistas(regs_hoja, baseDeDatos);
            consultasObservacionesGenerales = new ConsultasObservacionesGenerales(regs_hoja, baseDeDatos);
            consultasProductos = new ConsultasProductos(regs_hoja, baseDeDatos);
            consultasUsuarios = new ConsultasUsuarios(regs_hoja, baseDeDatos);
        }

        public void setIp(string ip)
        {
            intermediario.setIp(ip);
        }

        public int cantidad_reg_actuales()
        {
            return cantidad_registros_actuales;
        }

        public void consultarDirectamente(string consulta)
        {
            intermediario.consultar(consulta);
        }

        public string consultarDirectamenteRegs(string consulta)
        {
            return intermediario.darDato(consulta, "auto_increment");
        }

        public void vaciarBaseDeDatos()
        {
            intermediario.consultar(consultasBobinas.vaciarRegistros());
            intermediario.consultar(consultasClientes.vaciarRegistros());
            intermediario.consultar(consultasMaquinistas.vaciarRegistros());
            intermediario.consultar(consultasProductos.vaciarRegistros());
            intermediario.consultar(consultasUsuarios.vaciarRegistros());
            intermediario.consultar(consultasHistorialCelular.vaciarRegistros());
            intermediario.consultar(consultasObservacionesGenerales.vaciarRegistros());
        }


        ////////Usuario


        public void agregarUsuario(string nombre, string pass, string priv)
        {
            intermediario.consultar(consultasUsuarios.agregarUsuario(nombre, pass, priv));
        }

        public void updateUsuario(string id, string nombre, string pass, string priv)
        {
            intermediario.consultar(consultasUsuarios.updateUsuario(id, nombre, pass, priv));
        }

        public void borrarUsuario(string id)
        {
            intermediario.consultar(consultasUsuarios.borrarUsuario(id));
        }

        public void getUsers()
        {
            intermediario.consultar(consultasUsuarios.getUsers(this.cantidad_reg_actuales()));
        }

        public void accionPaginaUsuario(int contadorHoja)
        {
            intermediario.consultar(consultasUsuarios.accionPaginaUsuario(this.cantidad_reg_actuales(), contadorHoja));
        }

        public int cantidadHojasUsuarios()
        {
            return intermediario.cantidad_hojas(ref cantidad_registros_actuales, consultasUsuarios.cantidadUsuarios(), regs_hoja);
        }

        public int cantidadUsuarios()
        {
            return Int32.Parse(intermediario.darDato(consultasUsuarios.cantidadUsuarios(), "COUNT(*)"));
        }

        public string getIndiceUsuario(string nombre)
        {
            return intermediario.darDato(consultasUsuarios.getIndiceNombre(nombre), "id");
        }
        //////////Usuario


        ////Maquinistas

        public void agregarMaquinista(string maquinista, string ayudante)
        {
            intermediario.consultar(consultasMaquinistas.agregarMaquinista(maquinista, ayudante));
        }

        public void updateMaquinista(string maquinista, string ayudante, string id)
        {
            intermediario.consultar(consultasMaquinistas.updateMaquinista(maquinista, ayudante, id));
        }

        public void borrarMaquinista(string id)
        {
            intermediario.consultar(consultasMaquinistas.borrarMaquinista(id));
        }

        public void cargaMaquinistasCompleto()
        {
            intermediario.consultar(consultasMaquinistas.cargaMaquinistasCompleto());
        }

        public void getMaquinistas()
        {
            intermediario.consultar(consultasMaquinistas.carga_maquinistas(this.cantidad_reg_actuales()));
        }

        public void accionPaginaMaquinistas(int contadorHoja)
        {
            intermediario.consultar(consultasMaquinistas.accionPaginaMaquinista(this.cantidad_reg_actuales(), contadorHoja));
        }

        public int cantidadHojasMaquinistas()
        {
            return intermediario.cantidad_hojas(ref cantidad_registros_actuales, consultasMaquinistas.cantidadMaquinistas(), regs_hoja);
        }

        public int cantidadMaquinistas()
        {
            return Int32.Parse(intermediario.darDato(consultasMaquinistas.cantidadMaquinistas(), "COUNT(*)"));
        }

        public string getIndiceMaquinista(string nombre)
        {
            return intermediario.darDato(consultasMaquinistas.getIndiceNombre(nombre), "Index");
        }


        //////


        ///Productos
        /// 

        public void ingresar_nuevo_producto(string codigo, string nombre)
        {
            intermediario.consultar(consultasProductos.ingresar_nuevo_producto(codigo, nombre));
        }

        public void agregarProducto(string tipo, string metros)
        {
            intermediario.consultar(consultasProductos.agregarProducto(tipo, metros));
        }

        public void CargaTipoPapel()
        {
            intermediario.consultar(consultasProductos.CargaTipoPapel());
        }

        public void updateProducto(string tipo, string metros, string id)
        {
            intermediario.consultar(consultasProductos.updateProducto(tipo, metros, id));
        }

        public void cargaTipoPapelCompleto()
        {
            intermediario.consultar(consultasProductos.cargaTipoPapelCompleto());
        }


        public void getProductos()
        {
            intermediario.consultar(consultasProductos.getProductos(this.cantidad_reg_actuales()));
        }

        public void accionPaginaProductos(int contadorHoja)
        {
            intermediario.consultar(consultasProductos.accionPaginaProductos(this.cantidad_reg_actuales(), contadorHoja));
        }

        public int cantidadHojasProductos()
        {
            return intermediario.cantidad_hojas(ref cantidad_registros_actuales, consultasProductos.cantidadProductos(), regs_hoja);
        }


        public void borrarProducto(string id)
        {
            intermediario.consultar(consultasProductos.borrarProducto(id));
        }

        public int cantidadProductos()
        {
            return Int32.Parse(intermediario.darDato(consultasProductos.cantidadProductos(), "COUNT(*)"));
        }

        public string getIndiceProducto(string nombre)
        {
            return intermediario.darDato(consultasProductos.getIndiceNombre(nombre), "index");
        }

        ///


        ////Clientes
        public string getIdRemitoImpreso()
        {
            string consulta = consultasClientes.getIdRemitoImpreso();
            return intermediario.darDato(consulta, "Index");
        }

        public string getIdStockBaradero()
        {
            string consulta = consultasClientes.getIdStockBaradero();
            return intermediario.darDato(consulta, "Index");
        }
        
        public void borrarCliente(string id)
        {
            intermediario.consultar(consultasClientes.borrarCliente(id));
        }

        public void updateCliente(string cliente, string txtDirec, string txtLocalidad, string txtCP, string txtProv, string txtIVA, string txtCUIT, string id)
        {
            intermediario.consultar(consultasClientes.updateCliente(cliente, id, txtDirec, txtLocalidad, txtCP, txtProv, txtIVA, txtCUIT));
        }


        public void agregarCliente(string cliente, string txtDirec, string txtLocalidad, string txtCP, string txtProv, string txtIVA, string txtCUIT)
        {
            intermediario.consultar(consultasClientes.agregarCliente(cliente, txtDirec, txtLocalidad, txtCP, txtProv, txtIVA, txtCUIT));
        }

        public bool cargaClientesCompleto()
        {
            try
            {
                intermediario.consultar(consultasClientes.cargaClientesCompleto());
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void getClientes()
        {
            intermediario.consultar(consultasClientes.getClientes(this.cantidad_reg_actuales()));
        }

        public void accionPaginaClientes(int contadorHoja)
        {
            intermediario.consultar(consultasClientes.accionPaginaClientes(this.cantidad_reg_actuales(), contadorHoja));
        }

        public int cantidadHojasClientes()
        {
            return intermediario.cantidad_hojas(ref cantidad_registros_actuales, consultasClientes.cantidadClientes(), regs_hoja);
        }


        public int cantidadClientes()
        {
            return Int32.Parse(intermediario.darDato(consultasClientes.cantidadClientes(), "COUNT(*)"));
        }

        public string getIndiceCliente(string nombre)
        {
            return intermediario.darDato(consultasClientes.getIndiceNombre(nombre), "index");
        }
        /// 


        /// Historial
        public void onemillion2()
        {
            for (int i = 0; i < 200000; i++)
            {
                intermediario.consultar(consultasHistorialCelular.onemillion2());
            }
        }

        
        public void getHistorial()
        {
            intermediario.consultar(consultasHistorialCelular.getPhonesHistory(this.cantidad_reg_actuales()));
        }

        public void accionPaginaHistorial(int contadorHoja)
        {
            intermediario.consultar(consultasHistorialCelular.accionPaginaHistorialCelular(this.cantidad_reg_actuales(), contadorHoja));
        }

        public int cantidadHojasHistorialCelular()
        {
            return intermediario.cantidad_hojas(ref cantidad_registros_actuales, consultasHistorialCelular.countPhonesHistory(), regs_hoja);
        }
        ///


        /// Observaciones generales
        /// 

        public void updateObservacionGeneral(string observacion, string fecha, string horario, string maquinista,string id)
        {
            intermediario.consultar(consultasObservacionesGenerales.updateObservacionGeneral(observacion, fecha, horario, maquinista,id));
        }

        public void borrarObservacionGeneral(string id)
        {
            intermediario.consultar(consultasObservacionesGenerales.borrarObservacionGeneral(id));
        }

        public string getIndiceObservacion(string nombre)
        {
            return intermediario.darDato(consultasObservacionesGenerales.getIndiceObservacion(nombre), "index");
        }

        public int cantidadObservacionesGenerales()
        {
            return Int32.Parse(intermediario.darDato(consultasObservacionesGenerales.cantidadObservaciones(), "COUNT(*)"));
        }

        public void agregarObservacionGeneral(string observacion, string fecha, string horario, string maquinista)
        {
            intermediario.consultar(consultasObservacionesGenerales.agregarObservacionGeneral(observacion, fecha, horario, maquinista));
        }

        public void getObservacionesDia()
        {
            intermediario.consultar(consultasObservacionesGenerales.getObservacionDia());
        }

        public void getObservacionesFechaSeleccionada()
        {
            intermediario.consultar(consultasObservacionesGenerales.getObservacionFechaSeleccionada(consultasBobinas.getDay(), consultasBobinas.getMonth(), consultasBobinas.getYear()));
        }

        public void getObsGenerales()
        {
            intermediario.consultar(consultasObservacionesGenerales.getObservacionesGenerales(this.cantidad_reg_actuales()));
        }

        public void accionPaginaObsGeneral(int contadorHoja)
        {
            intermediario.consultar(consultasObservacionesGenerales.accionPaginaObsGenerales(this.cantidad_reg_actuales(), contadorHoja));
        }

        public int cantidadHojasObsGeneral()
        {
            return intermediario.cantidad_hojas(ref cantidad_registros_actuales, consultasObservacionesGenerales.countObservacionesGenerales(), regs_hoja);
        }
        ///


        /////Bobinas

        public int cantidad_hojas(int indexTipoAnio, int indexTipoNombre, int indexTipoEstado, string day, string month, string year, string day2, string month2, string year2, int idNombre, int idEstado, int nroBobina, int indexTipoClienteParam, int indexTipoPapelParam, int indexcampoFechaParam, int idTipoPapelParam, int idClienteParam)
        {
            consultasBobinas.setIndicesFiltro(indexTipoAnio, indexTipoNombre, indexTipoEstado, day, month, year, day2, month2, year2, idNombre, idEstado, nroBobina, indexTipoClienteParam, indexTipoPapelParam, indexcampoFechaParam, idTipoPapelParam, idClienteParam);
            return intermediario.cantidad_hojas(ref cantidad_registros_actuales, consultasBobinas.cantidad_registros(), regs_hoja);
        }

        public void inicializar_datos()
        {
            /*using (StreamWriter wr = new StreamWriter("./asdasd.txt", true))
            {
                wr.WriteLine(consultasBobinas.inicializar_datos(this.cantidad_reg_actuales()));
            }*/
            intermediario.consultar(consultasBobinas.inicializar_datos(this.cantidad_reg_actuales()));
        }

        public Boolean insertar_datos(string codigo_de_barras)
        {
            string columna = "index";
            string indice = intermediario.darDato(consultasProductos.dar_indice(codigo_de_barras, columna), columna);

            if (indice == "") return false;

            intermediario.consultar(consultasBobinas.incertar_datos(indice));

            return true;
        }

        public void accion_pa(int contador_hoja)
        {
            intermediario.consultar(consultasBobinas.accion_pagina(cantidad_reg_actuales(), contador_hoja));
        }

        public void ingresar_codigo(int indiceRegistro)
        {
            int estado, cliente;

            estado = Convert.ToInt32(intermediario.darDato(consultasBobinas.estadoRegistro(indiceRegistro), "estado_id"));
            cliente = Convert.ToInt32(intermediario.darDato(consultasBobinas.clienteRegistro(indiceRegistro), "cliente_id"));
            // Stock = 1, later quit the hard coding
            if (estado == 1 & cliente != 1)
            {
                intermediario.consultar(consultasBobinas.cambiarEstado(2, indiceRegistro));
            }
        }

        public void agregarBobina(int idClient, double Coil, string txtDate, double Format, string txtObservation, double Weight, int idTipo, string espesor, string finBob, string idMaquinista, string turno)
        {
            string id = this.getIdStockBaradero();
            intermediario.consultar(consultasBobinas.agregarStock(idClient, Coil, txtDate, Format, txtObservation, Weight, idTipo, espesor, finBob, idMaquinista, id, turno));
        }


        public string ultimoNumeroBobina()
        {
            return intermediario.darDato(consultasBobinas.ultimoNumeroBobina(), "auto_increment");
        }

        public void onemillion()
        {
            string a = "01/10/2013";

            DateTime finBob = DateTime.Now;
            DateTime asd = Convert.ToDateTime(a, new CultureInfo("es-ES"));
            string r;
            Random random = new Random();

            for (int i = 0; i < 200000; i++)
            {
                int cliente = random.Next(5); if (cliente == 0) cliente = 1;
                int tipo = random.Next(4); if (tipo == 0) tipo = 1;
                int maquinista = random.Next(3); if (maquinista == 0) maquinista = 1;
                int estado = random.Next(5); if (estado == 0) estado = 1;

                asd = asd.AddDays(1);
                r = asd.ToString("yyyy-MM-dd");

                for (int j = 0; j < 48; j++)
                {
                    finBob = finBob.AddMinutes(30);
                    intermediario.consultar(consultasBobinas.onemillion(r, cliente.ToString(), tipo.ToString(), maquinista.ToString(), estado.ToString(), finBob.ToString("H:mm")));
                }
            }
        }

        public bool existeBobina(string nroBobina)
        {
            string resultado = intermediario.darDato(consultasBobinas.existeBobina(nroBobina), "COUNT( numero_bobina )");
            int res = Convert.ToInt32(resultado);
            if (res >= 1) return true;
            return false;
        }

        public void updateDatosCargado(string id, string idCliente, string idTipo, string gramaje, string espesor, string peso, string observacion, string idEstado)
        {
            intermediario.consultar(consultasBobinas.updateDatosCargado(id, idCliente, idTipo, gramaje, espesor, peso, observacion, idEstado));
        }

        public string darPeso()
        {
            return intermediario.darDato(consultasBobinas.pesoTotal(), "sum(lectorcodigo.reg_2014.peso)");
        }

        public void enroqueEstados(string idCliente, string idRemitoImpreso, string nroBobina)
        {

            intermediario.consultar(consultasBobinas.enroqueEstados(idCliente, idRemitoImpreso, nroBobina));
        }

        public void borrarBobina(string id)
        {
            intermediario.consultar(consultasBobinas.borrarBobina(id));
        }
        ////

    }
}
