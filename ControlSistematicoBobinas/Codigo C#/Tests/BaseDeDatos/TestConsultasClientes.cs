using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using LibControlSistematico;

namespace Tests.BaseDeDatos
{
    [TestClass]
    public class TestConsultasClientes
    {

        HacedorDeConsultas hacedorDeConsultas;

        [TestInitialize]
        public void Init()
        {
            hacedorDeConsultas = new HacedorDeConsultas("localhost", "3306", "1", "testDB");
        }

        [TestCleanup]
        public void Cleanup()
        {
            hacedorDeConsultas.vaciarBaseDeDatos();
        }


        private TestContext testContextInstance;

        /// <summary>
        ///Obtiene o establece el contexto de las pruebas que proporciona
        ///información y funcionalidad para la serie de pruebas actual.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }



        [TestMethod]
        public void agregarClienteALaBaseDeDatos()
        {
            hacedorDeConsultas.agregarCliente("Cliente1","direccion","localidad","CP","Provincia","IVA","CUIT");
            int cantidadClientes = hacedorDeConsultas.cantidadClientes();

            Assert.AreEqual(cantidadClientes, 1);
        }

        [TestMethod]
        public void agregarDosClienteALaBaseDeDatos()
        {
            hacedorDeConsultas.agregarCliente("Cliente1","direccion","localidad","CP","Provincia","IVA","CUIT");
            hacedorDeConsultas.agregarCliente("Cliente2","direccion","localidad","CP","Provincia","IVA","CUIT");
            int cantidadClientes = hacedorDeConsultas.cantidadClientes();

            Assert.AreEqual(cantidadClientes, 2);
        }

        [TestMethod]
        public void agregartresClienteALaBaseDeDatos()
        {
            hacedorDeConsultas.agregarCliente("Cliente1","direccion","localidad","CP","Provincia","IVA","CUIT");
            hacedorDeConsultas.agregarCliente("Cliente2","direccion","localidad","CP","Provincia","IVA","CUIT");
            hacedorDeConsultas.agregarCliente("Cliente3","direccion","localidad","CP","Provincia","IVA","CUIT");
            int cantidadClientes = hacedorDeConsultas.cantidadClientes();

            Assert.AreEqual(cantidadClientes, 3);
        }

        [TestMethod]
        public void agregar40ClienteALaBaseDeDatos()
        {
            for (int i = 0; i < 40; i++)
                hacedorDeConsultas.agregarCliente("ClienteModificado"+i, "direccion", "localidad", "CP", "Provincia", "IVA", "CUIT");

            int cantidadClientes = hacedorDeConsultas.cantidadClientes();

            Assert.AreEqual(cantidadClientes, 40);
        }

        [TestMethod]
        public void eliminarCliente()
        {
            hacedorDeConsultas.agregarCliente("Cliente1","direccion","localidad","CP","Provincia","IVA","CUIT");
            string indice = hacedorDeConsultas.getIndiceCliente("Cliente1");
            hacedorDeConsultas.borrarCliente(indice);
            int cantidadClientes = hacedorDeConsultas.cantidadClientes();

            Assert.AreEqual(cantidadClientes, 0);
        }

        [TestMethod]
        public void eliminar40Clientes()
        {
            for (int i = 0; i < 40; i++)
                hacedorDeConsultas.agregarCliente("ClienteModificado"+i, "direccion", "localidad", "CP", "Provincia", "IVA", "CUIT");

            string indice;

            for (int i = 0; i < 40; i++)
            {
                indice = hacedorDeConsultas.getIndiceCliente("ClienteModificado" + i);
                hacedorDeConsultas.borrarCliente(indice);
            }

            int cantidadClientes = hacedorDeConsultas.cantidadClientes();

            Assert.AreEqual(cantidadClientes, 0);
        }

        [TestMethod]
        public void actualizarCliente()
        {
            hacedorDeConsultas.agregarCliente("Cliente1","direccion","localidad","CP","Provincia","IVA","CUIT");
            string indice;
            indice = hacedorDeConsultas.getIndiceCliente("Cliente1");
            hacedorDeConsultas.updateCliente("ClienteModificado", "direccion", "localidad", "CP", "Provincia", "IVA", "CUIT",indice);
            indice = hacedorDeConsultas.getIndiceCliente("ClienteModificado");
            hacedorDeConsultas.borrarCliente(indice);

            int cantidadClientes = hacedorDeConsultas.cantidadClientes();

            Assert.AreEqual(cantidadClientes, 0);
        }

        [TestMethod]
        public void actualizar40Clientes()
        {
            string indice;
            for (int i = 0; i < 40; i++)
            {
                hacedorDeConsultas.agregarCliente("Cliente1","direccion","localidad","CP","Provincia","IVA","CUIT");
                indice = hacedorDeConsultas.getIndiceCliente("Cliente1");
                hacedorDeConsultas.updateCliente("Cliente"+i, "direccion", "localidad", "CP", "Provincia", "IVA", "CUIT",indice);
            }

            for (int i = 0; i < 40; i++)
            {
                indice = hacedorDeConsultas.getIndiceCliente("Cliente" + i);
                hacedorDeConsultas.borrarCliente(indice);
            }

            int cantidadClientes = hacedorDeConsultas.cantidadClientes();

            Assert.AreEqual(cantidadClientes, 0);
        }


        [TestMethod]
        public void fallaAgregarClienteALaBaseDeDatos()
        {
            hacedorDeConsultas.agregarCliente("Cliente1","direccion","localidad","CP","Provincia","IVA","CUIT");
            int cantidadClientes = hacedorDeConsultas.cantidadClientes();

            Assert.AreNotEqual(cantidadClientes, 2);
        }

        [TestMethod]
        public void FallaAgregarDosClienteALaBaseDeDatos()
        {
            hacedorDeConsultas.agregarCliente("Cliente1","direccion","localidad","CP","Provincia","IVA","CUIT");
            hacedorDeConsultas.agregarCliente("Cliente2","direccion","localidad","CP","Provincia","IVA","CUIT");
            int cantidadClientes = hacedorDeConsultas.cantidadClientes();

            Assert.AreNotEqual(cantidadClientes, 3);
        }

        [TestMethod]
        public void FallaAgregartresClienteALaBaseDeDatos()
        {
            hacedorDeConsultas.agregarCliente("Cliente1","direccion","localidad","CP","Provincia","IVA","CUIT");
            hacedorDeConsultas.agregarCliente("Cliente2","direccion","localidad","CP","Provincia","IVA","CUIT");
            hacedorDeConsultas.agregarCliente("Cliente3","direccion","localidad","CP","Provincia","IVA","CUIT");
            int cantidadClientes = hacedorDeConsultas.cantidadClientes();

            Assert.AreNotEqual(cantidadClientes, 4);
        }

        [TestMethod]
        public void FallaAgregar40ClienteALaBaseDeDatos()
        {
            for (int i = 0; i < 40; i++)
                hacedorDeConsultas.agregarCliente("ClienteModificado"+i, "direccion", "localidad", "CP", "Provincia", "IVA", "CUIT");

            int cantidadClientes = hacedorDeConsultas.cantidadClientes();

            Assert.AreNotEqual(cantidadClientes, 41);
        }

        [TestMethod]
        public void FallaEliminarCliente()
        {
            hacedorDeConsultas.agregarCliente("Cliente1","direccion","localidad","CP","Provincia","IVA","CUIT");
            string indice = hacedorDeConsultas.getIndiceCliente("Cliente1");
            hacedorDeConsultas.borrarCliente(indice);
            int cantidadClientes = hacedorDeConsultas.cantidadClientes();

            Assert.AreNotEqual(cantidadClientes, 1);
        }

        [TestMethod]
        public void FallaEliminar40Clientes()
        {
            for (int i = 0; i < 40; i++)
                hacedorDeConsultas.agregarCliente("ClienteModificado"+i, "direccion", "localidad", "CP", "Provincia", "IVA", "CUIT");

            string indice;

            for (int i = 0; i < 40; i++)
            {
                indice = hacedorDeConsultas.getIndiceCliente("ClienteModificado" + i);
                hacedorDeConsultas.borrarCliente(indice);
            }

            int cantidadClientes = hacedorDeConsultas.cantidadClientes();

            Assert.AreNotEqual(cantidadClientes, 40);
        }

        [TestMethod]
        public void FallaActualizarCliente()
        {
            hacedorDeConsultas.agregarCliente("Cliente1","direccion","localidad","CP","Provincia","IVA","CUIT");
            string indice = hacedorDeConsultas.getIndiceCliente("Cliente1");
            hacedorDeConsultas.updateCliente("ClienteModificado", "direccion", "localidad", "CP", "Provincia", "IVA", "CUIT",indice);
            indice = hacedorDeConsultas.getIndiceCliente("ClienteModificado");
            hacedorDeConsultas.borrarCliente(indice);

            int cantidadClientes = hacedorDeConsultas.cantidadClientes();

            Assert.AreNotEqual(cantidadClientes, 1);
        }

        [TestMethod]
        public void FallaActualizar40Clientes()
        {
            string indice;
            for (int i = 0; i < 40; i++)
            {
                hacedorDeConsultas.agregarCliente("Cliente1","direccion","localidad","CP","Provincia","IVA","CUIT");
                indice = hacedorDeConsultas.getIndiceCliente("Cliente1");
                hacedorDeConsultas.updateCliente("Cliente"+i, "direccion", "localidad", "CP", "Provincia", "IVA", "CUIT",indice);
            }

            for (int i = 0; i < 40; i++)
            {
                indice = hacedorDeConsultas.getIndiceCliente("Cliente" + i);
                hacedorDeConsultas.borrarCliente(indice);
            }

            int cantidadClientes = hacedorDeConsultas.cantidadClientes();

            Assert.AreNotEqual(cantidadClientes, 40);
        }

    }
}
