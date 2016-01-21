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
        public GeneradorDeConsultas Generador_Consultas;
        private int regs_hoja;
        protected IntermediarioConexion intermediario;
        

        public HacedorDeConsultas(string ip, string puerto, string timeOut,string baseDeDatos)
        {
            regs_hoja = 62;
            Generador_Consultas = new GeneradorDeConsultas(regs_hoja);
            intermediario = new IntermediarioConexion(ip, puerto, timeOut,baseDeDatos);
        }

        public void setIp(string ip)
        {
            intermediario.setIp(ip);
        }

        public int cantidad_hojas(int indexTipoAnio, int indexTipoNombre, int indexTipoEstado, string day, string month, string year, string day2, string month2, string year2, int idNombre, int idEstado, int nroBobina, int indexTipoClienteParam, int indexTipoPapelParam, int indexcampoFechaParam, int idTipoPapelParam, int idClienteParam)
        {
            Generador_Consultas.setIndicesFiltro(indexTipoAnio, indexTipoNombre, indexTipoEstado, day, month, year, day2, month2, year2, idNombre, idEstado, nroBobina, indexTipoClienteParam, indexTipoPapelParam, indexcampoFechaParam, idTipoPapelParam, idClienteParam);
            return intermediario.cantidad_hojas(ref cantidad_registros_actuales, Generador_Consultas.cantidad_registros(), regs_hoja);
        }

        public int cantidad_reg_actuales()
        {
            return cantidad_registros_actuales;
        }

        public void inicializar_datos()
        {
            /*using (StreamWriter wr = new StreamWriter("./asdasd.txt", true))
            {
                wr.WriteLine(Generador_Consultas.inicializar_datos(this.cantidad_reg_actuales()));
            }*/
            intermediario.consultar(Generador_Consultas.inicializar_datos(this.cantidad_reg_actuales()));
        }

        public Boolean insertar_datos(string codigo_de_barras)
        {
            string columna = "index";
            string indice = intermediario.darDato(Generador_Consultas.dar_indice(codigo_de_barras, columna), columna);

            if (indice == "") return false;

            intermediario.consultar(Generador_Consultas.incertar_datos(indice));

            return true;
        }

        public void accion_pa(int contador_hoja)
        {
            intermediario.consultar(Generador_Consultas.accion_pagina(cantidad_reg_actuales(), contador_hoja));
        }


        public void CargaTipoPapel()
        {
            intermediario.consultar(Generador_Consultas.CargaTipoPapel());
        }

        public void ingresar_nuevo_producto(string codigo, string nombre)
        {
            intermediario.consultar(Generador_Consultas.ingresar_nuevo_producto(codigo, nombre));
        }

        public void ingresar_codigo(int indiceRegistro)
        {
            int estado, cliente;

            estado = Convert.ToInt32(intermediario.darDato(Generador_Consultas.estadoRegistro(indiceRegistro), "estado_id"));
            cliente = Convert.ToInt32(intermediario.darDato(Generador_Consultas.clienteRegistro(indiceRegistro), "cliente_id"));
            // Stock = 1, later quit the hard coding
            if (estado == 1 & cliente != 1)
            {
                intermediario.consultar(Generador_Consultas.cambiarEstado(2, indiceRegistro));
            }
        }


        public void CargarStock(int idClient, double Coil, string txtDate, double Format, string txtObservation, double Weight, int idTipo, string espesor, string finBob, string idMaquinista, string turno)
        {
            string id = this.getIdStockBaradero();
            intermediario.consultar(Generador_Consultas.agregarStock(idClient, Coil, txtDate, Format, txtObservation, Weight, idTipo, espesor, finBob, idMaquinista, id, turno));
        }

        public void consultarDirectamente(string consulta)
        {
            intermediario.consultar(consulta);
        }

        public string ultimoNumeroBobina()
        {
            return intermediario.darDato(Generador_Consultas.ultimoNumeroBobina(), "auto_increment");
        }

        public string consultarDirectamenteRegs(string consulta)
        {
            return intermediario.darDato(consulta, "auto_increment");
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
                    intermediario.consultar(Generador_Consultas.onemillion(r, cliente.ToString(), tipo.ToString(), maquinista.ToString(), estado.ToString(), finBob.ToString("H:mm")));
                }
            }
        }

        public void onemillion2()
        {
            for (int i = 0; i < 200000; i++)
            {
                intermediario.consultar(Generador_Consultas.onemillion2());
            }
        }

        public void updateDatosCargado(string id, string idCliente, string idTipo, string gramaje, string espesor, string peso, string observacion, string idEstado)
        {
            intermediario.consultar(Generador_Consultas.updateDatosCargado(id, idCliente, idTipo, gramaje, espesor, peso, observacion, idEstado));
        }

        public void updateProducto(string tipo, string metros, string id)
        {
            intermediario.consultar(Generador_Consultas.updateProducto(tipo, metros, id));
        }


        public void updateCliente(string cliente, string id, string txtDirec, string txtLocalidad, string txtCP, string txtProv, string txtIVA, string txtCUIT)
        {
            intermediario.consultar(Generador_Consultas.updateCliente(cliente, id, txtDirec, txtLocalidad, txtCP, txtProv, txtIVA, txtCUIT));
        }

        public void updateMaquinista(string maquinista, string ayudante, string id)
        {
            intermediario.consultar(Generador_Consultas.updateMaquinista(maquinista, ayudante, id));
        }

        public void newProducto(string tipo, string metros)
        {
            intermediario.consultar(Generador_Consultas.agregarProducto(tipo, metros));
        }

        public void newCliente(string cliente, string txtDirec, string txtLocalidad, string txtCP, string txtProv, string txtIVA, string txtCUIT)
        {
            intermediario.consultar(Generador_Consultas.agregarCliente(cliente, txtDirec, txtLocalidad, txtCP, txtProv, txtIVA, txtCUIT));
        }

        public void newMaquinista(string maquinista, string ayudante)
        {
            intermediario.consultar(Generador_Consultas.agregarMaquinista(maquinista, ayudante));
        }

        public string darPeso()
        {
            return intermediario.darDato(Generador_Consultas.pesoTotal(), "sum(lectorcodigo.reg_2014.peso)");
        }

        public void enroqueEstados(string idCliente, string idRemitoImpreso, string nroBobina)
        {

            intermediario.consultar(Generador_Consultas.enroqueEstados(idCliente, idRemitoImpreso, nroBobina));
        }

        public string getIdRemitoImpreso()
        {
            string consulta = Generador_Consultas.getIdRemitoImpreso();
            return intermediario.darDato(consulta, "Index");
        }

        public string getIdStockBaradero()
        {
            string consulta = Generador_Consultas.getIdStockBaradero();
            return intermediario.darDato(consulta, "Index");
        }

        public void newUsuario(string nombre, string pass, string priv)
        {
            intermediario.consultar(Generador_Consultas.agregarUsuario(nombre, pass, priv));
        }

        public void updateUsuario(string id, string nombre, string pass, string priv)
        {
            intermediario.consultar(Generador_Consultas.updateUsuario(id, nombre, pass, priv));
        }

        public void borrarBobina(string id)
        {
            intermediario.consultar(Generador_Consultas.borrarBobina(id));
        }

        public void borrarUsuario(string id)
        {
            intermediario.consultar(Generador_Consultas.borrarUsuario(id));
        }

        public void borrarCliente(string id)
        {
            intermediario.consultar(Generador_Consultas.borrarCliente(id));
        }

        public void borrarMaquinista(string id)
        {
            intermediario.consultar(Generador_Consultas.borrarMaquinista(id));
        }

        public void borrarProducto(string id)
        {
            intermediario.consultar(Generador_Consultas.borrarProducto(id));
        }


        public void setObservacionGeneral(string observacion, string fecha, string horario, string maquinista)
        {
            intermediario.consultar(Generador_Consultas.setObservacionGeneral(observacion, fecha, horario, maquinista));
        }

        public void getObservacionesDia()
        {
            intermediario.consultar(Generador_Consultas.getObservacionDia());
        }

        public void getObservacionesFechaSeleccionada()
        {
            intermediario.consultar(Generador_Consultas.getObservacionFechaSeleccionada());
        }


        ////////Usuario
        public void getUsers()
        {
            intermediario.consultar(Generador_Consultas.getUsers(this.cantidad_reg_actuales()));
        }

        public void accionPaginaUsuario(int contadorHoja)
        {
            intermediario.consultar(Generador_Consultas.accionPaginaUsuario(this.cantidad_reg_actuales(), contadorHoja));
        }

        public int cantidadHojasUsuarios()
        {
            return intermediario.cantidad_hojas(ref cantidad_registros_actuales, Generador_Consultas.cantidadUsuarios(), regs_hoja);
        }
        //////////Usuario


        ////Maquinistas
        public void cargaMaquinistasCompleto()
        {
            intermediario.consultar(Generador_Consultas.cargaMaquinistasCompleto());
        }

        public void getMaquinistas()
        {
            intermediario.consultar(Generador_Consultas.carga_maquinistas(this.cantidad_reg_actuales()));
        }

        public void accionPaginaMaquinistas(int contadorHoja)
        {
            intermediario.consultar(Generador_Consultas.accionPaginaMaquinista(this.cantidad_reg_actuales(), contadorHoja));
        }

        public int cantidadHojasMaquinistas()
        {
            return intermediario.cantidad_hojas(ref cantidad_registros_actuales, Generador_Consultas.cantidadMaquinistas(), regs_hoja);
        }
        //////


        /// Productos
        public void cargaTipoPapelCompleto()
        {
            intermediario.consultar(Generador_Consultas.cargaTipoPapelCompleto());
        }

        public void getProductos()
        {
            intermediario.consultar(Generador_Consultas.getProductos(this.cantidad_reg_actuales()));
        }

        public void accionPaginaProductos(int contadorHoja)
        {
            intermediario.consultar(Generador_Consultas.accionPaginaProductos(this.cantidad_reg_actuales(), contadorHoja));
        }

        public int cantidadHojasProductos()
        {
            return intermediario.cantidad_hojas(ref cantidad_registros_actuales, Generador_Consultas.cantidadProductos(), regs_hoja);
        }
        ///


        /// Clientes
        public bool cargaClientesCompleto()
        {
            try
            {
                intermediario.consultar(Generador_Consultas.cargaClientesCompleto());
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void getClientes()
        {
            intermediario.consultar(Generador_Consultas.getClientes(this.cantidad_reg_actuales()));
        }

        public void accionPaginaClientes(int contadorHoja)
        {
            intermediario.consultar(Generador_Consultas.accionPaginaClientes(this.cantidad_reg_actuales(), contadorHoja));
        }

        public int cantidadHojasClientes()
        {
            return intermediario.cantidad_hojas(ref cantidad_registros_actuales, Generador_Consultas.cantidadClientes(), regs_hoja);
        }
        /// 


        /// Historial
        public void getHistorial()
        {
            intermediario.consultar(Generador_Consultas.getPhonesHistory(this.cantidad_reg_actuales()));
        }

        public void accionPaginaHistorial(int contadorHoja)
        {
            intermediario.consultar(Generador_Consultas.accionPaginaHistorialCelular(this.cantidad_reg_actuales(), contadorHoja));
        }

        public int cantidadHojasHistorialCelular()
        {
            return intermediario.cantidad_hojas(ref cantidad_registros_actuales, Generador_Consultas.countPhonesHistory(), regs_hoja);
        }
        /// 


        /// Observaciones generales
        public void getObsGenerales()
        {
            intermediario.consultar(Generador_Consultas.getObservacionesGenerales(this.cantidad_reg_actuales()));
        }

        public void accionPaginaObsGeneral(int contadorHoja)
        {
            intermediario.consultar(Generador_Consultas.accionPaginaObsGenerales(this.cantidad_reg_actuales(), contadorHoja));
        }

        public int cantidadHojasObsGeneral()
        {
            return intermediario.cantidad_hojas(ref cantidad_registros_actuales, Generador_Consultas.countObservacionesGenerales(), regs_hoja);
        }
        ///

        public bool existeBobina(string nroBobina)
        {
            string resultado = intermediario.darDato(Generador_Consultas.existeBobina(nroBobina), "COUNT( numero_bobina )");
            int res = Convert.ToInt32(resultado);
            if (res >= 1) return true;
            return false;
        }
    }
}
