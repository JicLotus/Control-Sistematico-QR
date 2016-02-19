using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using LibControlSistematico;

namespace Tests.BaseDeDatos
{
    /// <summary>
    /// Descripción resumida de TestConsultasProductos
    /// </summary>
    [TestClass]
    public class TestConsultasProductos
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
        public void agregarProductoALaBaseDeDatos()
        {
            hacedorDeConsultas.agregarProducto("Producto1", "123");
            int cantidadProductos = hacedorDeConsultas.cantidadProductos();

            Assert.AreEqual(cantidadProductos, 1);
        }

        [TestMethod]
        public void agregarDosProductoALaBaseDeDatos()
        {
            hacedorDeConsultas.agregarProducto("Producto1", "123");
            hacedorDeConsultas.agregarProducto("Producto2", "123");
            int cantidadProductos = hacedorDeConsultas.cantidadProductos();

            Assert.AreEqual(cantidadProductos, 2);
        }

        [TestMethod]
        public void agregartresProductoALaBaseDeDatos()
        {
            hacedorDeConsultas.agregarProducto("Producto1", "123");
            hacedorDeConsultas.agregarProducto("Producto2", "123");
            hacedorDeConsultas.agregarProducto("Producto3", "123");
            int cantidadProductos = hacedorDeConsultas.cantidadProductos();

            Assert.AreEqual(cantidadProductos, 3);
        }

        [TestMethod]
        public void agregar40ProductoALaBaseDeDatos()
        {
            for (int i = 0; i < 40; i++)
                hacedorDeConsultas.agregarProducto("Producto" + i, "123");

            int cantidadProductos = hacedorDeConsultas.cantidadProductos();

            Assert.AreEqual(cantidadProductos, 40);
        }

        [TestMethod]
        public void eliminarProducto()
        {
            hacedorDeConsultas.agregarProducto("Producto1", "123");
            string indice = hacedorDeConsultas.getIndiceProducto("Producto1");
            hacedorDeConsultas.borrarProducto(indice);
            int cantidadProductos = hacedorDeConsultas.cantidadProductos();

            Assert.AreEqual(cantidadProductos, 0);
        }

        [TestMethod]
        public void eliminar40Productos()
        {
            for (int i = 0; i < 40; i++)
                hacedorDeConsultas.agregarProducto("Producto" + i, "123");

            string indice;

            for (int i = 0; i < 40; i++)
            {
                indice = hacedorDeConsultas.getIndiceProducto("Producto" + i);
                hacedorDeConsultas.borrarProducto(indice);
            }

            int cantidadProductos = hacedorDeConsultas.cantidadProductos();

            Assert.AreEqual(cantidadProductos, 0);
        }

        [TestMethod]
        public void actualizarProducto()
        {
            hacedorDeConsultas.agregarProducto("Producto1", "123");
            string indice = hacedorDeConsultas.getIndiceProducto("Producto1");
            hacedorDeConsultas.updateProducto("elnombremodificado", "frfrf",indice);
            indice = hacedorDeConsultas.getIndiceProducto("elnombremodificado");
            hacedorDeConsultas.borrarProducto(indice);

            int cantidadProductos = hacedorDeConsultas.cantidadProductos();

            Assert.AreEqual(cantidadProductos, 0);
        }

        [TestMethod]
        public void actualizar40Productos()
        {
            string indice;
            for (int i = 0; i < 40; i++)
            {
                hacedorDeConsultas.agregarProducto("Producto1", "123");
                indice = hacedorDeConsultas.getIndiceProducto("Producto1");
                hacedorDeConsultas.updateProducto("elnombremodificado" + i, "frfrf", indice);
            }

            for (int i = 0; i < 40; i++)
            {
                indice = hacedorDeConsultas.getIndiceProducto("elnombremodificado" + i);
                hacedorDeConsultas.borrarProducto(indice);
            }

            int cantidadProductos = hacedorDeConsultas.cantidadProductos();

            Assert.AreEqual(cantidadProductos, 0);
        }


        [TestMethod]
        public void fallaAgregarProductoALaBaseDeDatos()
        {
            hacedorDeConsultas.agregarProducto("Producto1", "123");
            int cantidadProductos = hacedorDeConsultas.cantidadProductos();

            Assert.AreNotEqual(cantidadProductos, 2);
        }

        [TestMethod]
        public void FallaAgregarDosProductoALaBaseDeDatos()
        {
            hacedorDeConsultas.agregarProducto("Producto1", "123");
            hacedorDeConsultas.agregarProducto("Producto2", "123");
            int cantidadProductos = hacedorDeConsultas.cantidadProductos();

            Assert.AreNotEqual(cantidadProductos, 3);
        }

        [TestMethod]
        public void FallaAgregartresProductoALaBaseDeDatos()
        {
            hacedorDeConsultas.agregarProducto("Producto1", "123");
            hacedorDeConsultas.agregarProducto("Producto2", "123");
            hacedorDeConsultas.agregarProducto("Producto3", "123");
            int cantidadProductos = hacedorDeConsultas.cantidadProductos();

            Assert.AreNotEqual(cantidadProductos, 4);
        }

        [TestMethod]
        public void FallaAgregar40ProductoALaBaseDeDatos()
        {
            for (int i = 0; i < 40; i++)
                hacedorDeConsultas.agregarProducto("Producto" + i, "123");

            int cantidadProductos = hacedorDeConsultas.cantidadProductos();

            Assert.AreNotEqual(cantidadProductos, 41);
        }

        [TestMethod]
        public void FallaEliminarProducto()
        {
            hacedorDeConsultas.agregarProducto("Producto1", "123");
            string indice = hacedorDeConsultas.getIndiceProducto("Producto1");
            hacedorDeConsultas.borrarProducto(indice);
            int cantidadProductos = hacedorDeConsultas.cantidadProductos();

            Assert.AreNotEqual(cantidadProductos, 1);
        }

        [TestMethod]
        public void FallaEliminar40Productos()
        {
            for (int i = 0; i < 40; i++)
                hacedorDeConsultas.agregarProducto("Producto" + i, "123");

            string indice;

            for (int i = 0; i < 40; i++)
            {
                indice = hacedorDeConsultas.getIndiceProducto("Producto" + i);
                hacedorDeConsultas.borrarProducto(indice);
            }

            int cantidadProductos = hacedorDeConsultas.cantidadProductos();

            Assert.AreNotEqual(cantidadProductos, 40);
        }

        [TestMethod]
        public void FallaActualizarProducto()
        {
            hacedorDeConsultas.agregarProducto("Producto1", "123");
            string indice = hacedorDeConsultas.getIndiceProducto("Producto1");
            hacedorDeConsultas.updateProducto("elnombremodificado", "frfrf",indice);
            indice = hacedorDeConsultas.getIndiceProducto("elnombremodificado");
            hacedorDeConsultas.borrarProducto(indice);

            int cantidadProductos = hacedorDeConsultas.cantidadProductos();

            Assert.AreNotEqual(cantidadProductos, 1);
        }

        [TestMethod]
        public void FallaActualizar40Productos()
        {
            string indice;
            for (int i = 0; i < 40; i++)
            {
                hacedorDeConsultas.agregarProducto("Producto1", "123");
                indice = hacedorDeConsultas.getIndiceProducto("Producto1");
                hacedorDeConsultas.updateProducto("elnombremodificado" + i, "frfrf", indice);
            }

            for (int i = 0; i < 40; i++)
            {
                indice = hacedorDeConsultas.getIndiceProducto("elnombremodificado" + i);
                hacedorDeConsultas.borrarProducto(indice);
            }

            int cantidadProductos = hacedorDeConsultas.cantidadProductos();

            Assert.AreNotEqual(cantidadProductos, 40);
        }

         
    }
}
