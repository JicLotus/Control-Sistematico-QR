using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LibControlSistematico
{
    public class ConsultasBobinas
    {
        private double registros_por_hoja;

        private Fecha indexTipoAnio;
        private Maquinista indexTipoNombre;
        private estado indexTipoEstado;
        private Cliente indexTipoCliente;
        private TipoPapel indexTipoPapel;
        private campoFecha tipocampoFecha;
        private string day, month, year;
        private string day2, month2, year2;
        private string CAMPOFECHA;
        private int idNombre, idEstado, nroBobina, idTipoPapel, idCliente;
        private string baseDeDatos;

        private string clausula_where="";

        public ConsultasBobinas(double reg_por_hoja,string baseDeDatosParam)
        {
            registros_por_hoja = reg_por_hoja;
            baseDeDatos = baseDeDatosParam;
        }


        public string getDay() { return day; }
        public string getMonth() { return month; }
        public string getYear() { return year; }


        public string inicializar_datos(int cantidad_registros)
        {
            string hoja_inicial = "0";
            if (cantidad_registros > registros_por_hoja)
            {
                hoja_inicial = (cantidad_registros - registros_por_hoja).ToString();
            }
            //return ("Select * from productos, " + selector_tabla.tabla_actual() + " where " + selector_tabla.tabla_actual() + ".index = productos.index limit " + hoja_inicial + "," + registros_por_hoja + ";");
            //return ("Select clientes.cliente,reg_2014.Numero_Bobina,reg_2014.gramaje,reg_2014.espesor,reg_2014.peso,productos.tipo,reg_2014.observacion From reg_2014 INNER JOIN clientes ON reg_2014.cliente_id = clientes.index INNER JOIN estado ON reg_2014.estado_id = estado.id INNER JOIN maquinista ON reg_2014.maquinista_id = maquinista.index INNER JOIN productos ON reg_2014.producto_id = productos.index limit " + hoja_inicial + "," + registros_por_hoja + ";");       

            //**LA GRAN CONSULTA EXPIRADA
            //***************************
            //FECHA: 07/09/204
            //***************************
            //return ("Select clientes.Cliente, reg_2014.Numero_Bobina, reg_2014.Gramaje, reg_2014.Espesor, reg_2014.Peso, reg_2014.Fin_Bob, reg_2014.Formato, productos.Tipo, productos.Metros ,reg_2014.Observacion, maquinista.Maquinista, reg_2014.estado_id, reg_2014.celular From reg_2014  left JOIN maquinista ON reg_2014.maquinista_id = maquinista.index left JOIN productos ON reg_2014.producto_id = productos.index left JOIN clientes ON reg_2014.cliente_id = clientes.index " + clausula_where + " limit " + hoja_inicial + "," + registros_por_hoja + ";");
            //***************************
            //***************************

            return ("Select "+ CAMPOFECHA +", reg_2014.cliente_id, reg_2014.Numero_Bobina, reg_2014.Gramaje, reg_2014.Espesor, reg_2014.Peso, reg_2014.Fin_Bob, reg_2014.Formato, reg_2014.producto_id ,reg_2014.Observacion, reg_2014.maquinista_id, reg_2014.estado_id, reg_2014.celular,reg_2014.turno From reg_2014  " + clausula_where + " limit " + hoja_inicial + "," + registros_por_hoja + ";");
        }
        


        public string incertar_datos(string indice)
        {
            DateTime Hoy = DateTime.Today;
            string fecha_actual = Hoy.ToString("yyyy-MM-dd");
            return ("INSERT INTO  `"  + baseDeDatos +  "`.`reg_2014` (`id` ,`index` ,`Fecha`)VALUES (NULL , '" + indice + "',  '" + fecha_actual + "');");
        }



        public string accion_pagina(int cantidad_registros,int contador_hoja)
        {
            double hoja_inicial = 0;
            double limite = registros_por_hoja;
            if (cantidad_registros > registros_por_hoja)
            {
                hoja_inicial = (cantidad_registros - (registros_por_hoja * contador_hoja));
                
                if (hoja_inicial < 0) 
                {
                    limite = (cantidad_registros - (registros_por_hoja * (contador_hoja-1)));
                    hoja_inicial = 0;
                }

            }
            //return ("Select * from productos, " + selector_tabla.tabla_actual() + " where " + selector_tabla.tabla_actual() + ".index = productos.index Limit " + hoja_inicial + "," + limite + ";");
            //return ("Select clientes.cliente,reg_2014.Numero_Bobina,reg_2014.gramaje,reg_2014.espesor,reg_2014.peso,productos.tipo,reg_2014.observacion, estado.ESTADO From reg_2014 INNER JOIN clientes ON reg_2014.cliente_id = clientes.index INNER JOIN estado ON reg_2014.estado_id = estado.id INNER JOIN maquinista ON reg_2014.maquinista_id = maquinista.index INNER JOIN productos ON reg_2014.producto_id = productos.index Limit " + hoja_inicial + "," + limite + ";");

            /**LA GRAN CONSULTA EXPIRADA
            ***************************
            FECHA: 07/09/204
            ***************************
            return ("Select clientes.Cliente, reg_2014.Numero_Bobina, reg_2014.Gramaje, reg_2014.Espesor, reg_2014.Peso, reg_2014.Fin_Bob, reg_2014.Formato, productos.Tipo, productos.Metros ,reg_2014.Observacion, maquinista.Maquinista, reg_2014.estado_id, reg_2014.celular From reg_2014 left JOIN maquinista ON reg_2014.maquinista_id = maquinista.index left JOIN productos ON reg_2014.producto_id = productos.index left JOIN clientes ON reg_2014.cliente_id = clientes.index " + clausula_where + " limit " + hoja_inicial + "," + limite + ";");
            ***************************
            ***************************/
            return ("Select " + CAMPOFECHA + ", reg_2014.cliente_id, reg_2014.Numero_Bobina, reg_2014.Gramaje, reg_2014.Espesor, reg_2014.Peso, reg_2014.Fin_Bob, reg_2014.Formato, reg_2014.producto_id ,reg_2014.Observacion, reg_2014.maquinista_id, reg_2014.estado_id, reg_2014.celular, reg_2014.turno From reg_2014  " + clausula_where + " limit " + hoja_inicial + "," + limite + ";");
        }

        public string cantidad_registros()
        {
            switch (tipocampoFecha)
            {
                case campoFecha.FECHA_FABRICACION:
                    CAMPOFECHA = "reg_2014.FECHA_FABRICACION";
                    break;
                case campoFecha.FECHA_SCANEO:
                    CAMPOFECHA = "reg_2014.FECHA_SCANEO";
                    break;
            }
            
            switch (indexTipoAnio)
            {
                case Fecha.DIA:
                    DateTime diaPosterior = new DateTime(Convert.ToInt32(year),Convert.ToInt32(month),Convert.ToInt32(day));
                    diaPosterior = diaPosterior.AddDays(1);
                    //clausula_where = "where (day(" + CAMPOFECHA + ")>=" + day + " and month(" + CAMPOFECHA + ")=" + month + " and year(" + CAMPOFECHA + ")=" + year + " and hour(reg_2014.Fin_Bob)>=5 and hour(reg_2014.Fin_Bob)<=21";
                    
                    if ((campoFecha)tipocampoFecha == campoFecha.FECHA_SCANEO)
                    {
                        clausula_where = "where (day(" + CAMPOFECHA + ")=" + day + " and month(" + CAMPOFECHA + ")=" + month + " and year(" + CAMPOFECHA + ")=" + year;
                    }
                    else
                    {
                        clausula_where = "where (((day(" + CAMPOFECHA + ")=" + day + " and month(" + CAMPOFECHA + ")=" + month + " and year(" + CAMPOFECHA + ")=" + year + " and hour(reg_2014.Fin_Bob)>=4 and hour(reg_2014.Fin_Bob)<=21 and (turno=2 or turno=3)) or (day(" + CAMPOFECHA + ")=" + day + " and month(" + CAMPOFECHA + ")=" + month + " and year(" + CAMPOFECHA + ")=" + year + " and hour(reg_2014.Fin_Bob)>=20 and turno=1) or (day(" + CAMPOFECHA + ")=" + (diaPosterior.Day).ToString() + " and month(" + CAMPOFECHA + ")=" + diaPosterior.Month.ToString() + " and year(" + CAMPOFECHA + ")=" + diaPosterior.Year.ToString() + " and hour(reg_2014.Fin_Bob)<=5) and turno=1)";
                    }

                    break;
                case Fecha.MES:
                    clausula_where = "where (month(" + CAMPOFECHA + ")=" + month + " and year(" + CAMPOFECHA + ")=" + year;
                    break;
                case Fecha.AÑO:
                    clausula_where = "where (year(" + CAMPOFECHA + ")=" + year;
                    break;
                case Fecha.DESDEHASTA:
                    //clausula_where = "where (day(" + CAMPOFECHA + ")>=" + day + " and month(" + CAMPOFECHA + ")>=" + month + " and year(" + CAMPOFECHA + ")>=" + year + " and day(" + CAMPOFECHA + ")<=" + day2 + " and month(" + CAMPOFECHA + ")<=" + month2 + " and year(" + CAMPOFECHA + ")<=" + year2;
                    clausula_where = "where (" + CAMPOFECHA + " BETWEEN '" + year + "/" + month + "/" + day + "' and '" + year2 + "/" + month2 + "/" + day2 + "'";
                    break;
                case Fecha.NROBOBINA:
                    clausula_where = "where (reg_2014.Numero_Bobina=" + nroBobina;
                    break;
            }

            switch (indexTipoCliente)
            {
                case Cliente.TODOS:
                    clausula_where += "";
                    break;
                case Cliente.NOMBRE:
                    clausula_where += " and reg_2014.cliente_id="+ idCliente;
                    break;
            }

            switch (indexTipoPapel)
            {
                case TipoPapel.TODOS:
                    clausula_where += "";
                    break;
                case TipoPapel.NOMBRE:
                    clausula_where += " and reg_2014.producto_id="+ idTipoPapel;
                    break;
            }

            switch (indexTipoNombre)
            {
                case Maquinista.TODOS:
                    clausula_where += "";
                    break;
                case Maquinista.NOMBRE:
                    clausula_where += " and reg_2014.maquinista_id = " + idNombre;
                    break;
            }

            switch (indexTipoEstado)
            {
                case estado.TODOS:
                    clausula_where+= " )";
                    break;
                case estado.NOMBRE:
                    clausula_where+= " and reg_2014.estado_id = " + idEstado + " )";
                    break;
                case estado.EXCEPTO:
                    clausula_where += " and reg_2014.estado_id != " + idEstado + " )";
                    break;
            }

            return ("Select count(*) from reg_2014 " + clausula_where + " Limit 1");
        }
        
        public string dar_tablas()
        {
            return ("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES where TABLE_SCHEMA='lectorcodigo'");
        }

        public void setIndicesFiltro(int indexTipoAnioParam, int indexTipoNombreParam, int indexTipoEstadoParam, string dayParam, string monthParam, string yearParam, string dayParam2, string monthParam2, string yearParam2, int idNombreParam, int idEstadoParam, int nroBobinaParam, int indexTipoClienteParam,int indexTipoPapelParam, int tipocampoFechaParam,int idTipoPapelParam,int idClienteParam)
        {
            indexTipoAnio = (Fecha)indexTipoAnioParam;
            indexTipoNombre = (Maquinista)indexTipoNombreParam;
            indexTipoEstado = (estado)indexTipoEstadoParam;
            idEstado = idEstadoParam;
            day = dayParam;
            month = monthParam;
            year = yearParam;
            day2 = dayParam2;
            month2 = monthParam2;
            year2 = yearParam2;
            idNombre = idNombreParam;
            nroBobina = nroBobinaParam;
            indexTipoCliente = (Cliente)indexTipoClienteParam;
            indexTipoPapel = (TipoPapel)indexTipoPapelParam;
            tipocampoFecha = (campoFecha)tipocampoFechaParam;
            idTipoPapel = idTipoPapelParam;
            idCliente= idClienteParam;
        }


        public string estadoRegistro(int indiceRegistro)
        {
            return ("Select estado_id from reg_2014 Where id=" + indiceRegistro + ";");
        }

        public string clienteRegistro(int indiceRegistro)
        {
            return ("Select cliente_id from reg_2014 Where id=" + indiceRegistro + ";");
        }

        public string onemillion(string fecha, string cliente, string tipo, string maquinista, string estado, string finBob)
        {
            return ("INSERT INTO  `"  + baseDeDatos +  "`.`reg_2014` (`Numero_Bobina` ,`estado_id` ,`producto_id` ,`cliente_id` ,`maquinista_id` ,`Peso` ,`Observacion` ,`Gramaje` ,`Espesor` ,`Fin_Bob` ,`Formato` ,`FECHA_FABRICACION` ,`celular`)VALUES (NULL ,  '" + estado + "',  '" + tipo + "',  '" + cliente + "',  '" + maquinista + "',  '123.12',  'asdasdagetyhj 2342 vgefg grh..mm',  '12',  '123',  '" + finBob + "',  '123',  '" + fecha + "', '213131');");
        }


        public string cambiarEstado(int estado, int indiceRegistro)
        {
            return ("Update `"  + baseDeDatos +  "`.`reg_2014` SET `estado_id` =  " + estado + " WHERE  `reg_2014`.`id` =" + indiceRegistro + ";");
        }

        public string agregarStock(int idClient, double Coil, string txtDate, double Format, string txtObservation, double Weight, int idTipo, string Espesor, string FinBob, string idMaquinista, string idStockBaradero, string turno)
        {
            DateTime Hoy = DateTime.Today;
            string fecha_actual = Hoy.ToString("yyyy-MM-dd");
            string FB = DateTime.Now.ToString("H:mm");

            return ("INSERT INTO  `"  + baseDeDatos +  "`.`reg_2014` (`Numero_Bobina` ,`estado_id` ,`producto_id` ,`cliente_id` ,`maquinista_id` ,`PESO` ,`OBSERVACION` ,`GRAMAJE` ,`ESPESOR` , `FIN_BOB` , `FORMATO`, `FECHA_FABRICACION`,`TURNO`)VALUES (NULL ,  '" + idStockBaradero + "',  '" + idTipo.ToString() + "',  '" + idClient.ToString() + "',  '" + idMaquinista + "',  '" + Weight.ToString().Replace(",", ".") + "',  '" + txtObservation + "',  '" + Coil.ToString().Replace(",", ".") + "',  '" + Espesor.Replace(",", ".") + "',  '" + FB + "', '" + Format.ToString().Replace(",", ".") + "' ,  '" + fecha_actual + "' ,  '" + turno + "');");
        }

        public string updateDatosCargado(string id, string idCliente, string idTipo, string gramaje, string espesor, string peso, string observacion,string idEstado) 
        {
            return ("update lectorcodigo.reg_2014 set reg_2014.producto_id='" + idTipo + "', reg_2014.cliente_id ='" + idCliente + "', reg_2014.gramaje ='" + gramaje.Replace(",", ".") + "', reg_2014.espesor='" + espesor + "', reg_2014.peso='" + peso.Replace(",", ".") + "', reg_2014.observacion='" + observacion + "', reg_2014.estado_id='"+idEstado +"' where reg_2014.numero_bobina =" + id + " limit 1");
        }

        public string pesoTotal() 
        {
            return("select sum(lectorcodigo.reg_2014.peso) from lectorcodigo.reg_2014 " + clausula_where + " limit 1");
        }


        public string enroqueEstados(string idCliente, string idEstadoRemitoImpreso, string nroBobina) 
        {
            return ("UPDATE  `"  + baseDeDatos +  "`.`reg_2014` SET  `estado_id` ='"+ idEstadoRemitoImpreso +"',`cliente_id` ='"+idCliente+"' WHERE  `reg_2014`.`Numero_Bobina` ='"+ nroBobina + "'");
        }

        
        public string borrarBobina(string nroBobina)
        {
            return "Delete from reg_2014 where Numero_Bobina=" + nroBobina + " limit 1";
        }

        
        public string ultimoNumeroBobina()
        {
            return "select auto_increment from `information_schema`.tables where TABLE_SCHEMA = 'lectorcodigo' and TABLE_NAME = 'reg_2014'";
        }

        public string existeBobina(string nroBobina) 
        {
            return "SELECT COUNT( numero_bobina ) FROM lectorcodigo.reg_2014 WHERE reg_2014.numero_bobina =" + nroBobina;
        }


        public string vaciarRegistros()
        {
            return "Truncate reg_2014";
        }

        public string cantidadBobinas()
        {
            return ("select count(*) from reg_2014 limit 1;");
        }

    }
}
