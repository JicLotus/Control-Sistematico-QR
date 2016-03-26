using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using LibControlSistematico;

namespace Tests
{
    /// <summary>
    /// Descripción resumida de TestConsultasMaquinistas
    /// </summary>
    [TestClass]
    public class TestConsultasMaquinistas
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



        [TestMethod]
        public void agregarMaquinistaALaBaseDeDatos()
        {
            hacedorDeConsultas.agregarMaquinista("Maquinista1", "Ayudante");
            int cantidadMaquinistas = hacedorDeConsultas.cantidadMaquinistas();

            Assert.AreEqual(cantidadMaquinistas, 1);
        }

        [TestMethod]
        public void agregarDosMaquinistaALaBaseDeDatos()
        {
            hacedorDeConsultas.agregarMaquinista("Maquinista1", "Ayudante");
            hacedorDeConsultas.agregarMaquinista("Maquinista2", "Ayudante");
            int cantidadMaquinistas = hacedorDeConsultas.cantidadMaquinistas();

            Assert.AreEqual(cantidadMaquinistas, 2);
        }

        [TestMethod]
        public void agregartresMaquinistaALaBaseDeDatos()
        {
            hacedorDeConsultas.agregarMaquinista("Maquinista1", "Ayudante");
            hacedorDeConsultas.agregarMaquinista("Maquinista2", "Ayudante");
            hacedorDeConsultas.agregarMaquinista("Maquinista3", "Ayudante");
            int cantidadMaquinistas = hacedorDeConsultas.cantidadMaquinistas();

            Assert.AreEqual(cantidadMaquinistas, 3);
        }

        [TestMethod]
        public void agregar40MaquinistaALaBaseDeDatos()
        {
            for (int i = 0; i < 40; i++)
                hacedorDeConsultas.agregarMaquinista("Maquinista" + i, "Ayudante");

            int cantidadMaquinistas = hacedorDeConsultas.cantidadMaquinistas();

            Assert.AreEqual(cantidadMaquinistas, 40);
        }

        [TestMethod]
        public void eliminarMaquinista()
        {
            hacedorDeConsultas.agregarMaquinista("Maquinista1", "Ayudante");
            string indice = hacedorDeConsultas.getIndiceMaquinista("Maquinista1");
            hacedorDeConsultas.borrarMaquinista(indice);
            int cantidadMaquinistas = hacedorDeConsultas.cantidadMaquinistas();

            Assert.AreEqual(cantidadMaquinistas, 0);
        }

        [TestMethod]
        public void eliminar40Maquinistas()
        {
            for (int i = 0; i < 40; i++)
                hacedorDeConsultas.agregarMaquinista("Maquinista" + i, "Ayudante");

            string indice;

            for (int i = 0; i < 40; i++)
            {
                indice = hacedorDeConsultas.getIndiceMaquinista("Maquinista" + i);
                hacedorDeConsultas.borrarMaquinista(indice);
            }

            int cantidadMaquinistas = hacedorDeConsultas.cantidadMaquinistas();

            Assert.AreEqual(cantidadMaquinistas, 0);
        }

        [TestMethod]
        public void actualizarMaquinista()
        {
            hacedorDeConsultas.agregarMaquinista("Maquinista1", "Ayudante");
            string indice = hacedorDeConsultas.getIndiceMaquinista("Maquinista1");
            hacedorDeConsultas.updateMaquinista("elnombremodificado", "frfrf", indice);
            indice = hacedorDeConsultas.getIndiceMaquinista("elnombremodificado");
            hacedorDeConsultas.borrarMaquinista(indice);

            int cantidadMaquinistas = hacedorDeConsultas.cantidadMaquinistas();

            Assert.AreEqual(cantidadMaquinistas, 0);
        }

        [TestMethod]
        public void actualizar40Maquinistas()
        {
            string indice;
            for (int i = 0; i < 40; i++)
            {
                hacedorDeConsultas.agregarMaquinista("Maquinista1", "Ayudante");
                indice = hacedorDeConsultas.getIndiceMaquinista("Maquinista1");
                hacedorDeConsultas.updateMaquinista("elnombremodificado" + i, "frfrf", indice);
            }

            for (int i = 0; i < 40; i++)
            {
                indice = hacedorDeConsultas.getIndiceMaquinista("elnombremodificado" + i);
                hacedorDeConsultas.borrarMaquinista(indice);
            }

            int cantidadMaquinistas = hacedorDeConsultas.cantidadMaquinistas();

            Assert.AreEqual(cantidadMaquinistas, 0);
        }


        [TestMethod]
        public void fallaAgregarMaquinistaALaBaseDeDatos()
        {
            hacedorDeConsultas.agregarMaquinista("Maquinista1", "Ayudante");
            int cantidadMaquinistas = hacedorDeConsultas.cantidadMaquinistas();

            Assert.AreNotEqual(cantidadMaquinistas, 2);
        }

        [TestMethod]
        public void FallaAgregarDosMaquinistaALaBaseDeDatos()
        {
            hacedorDeConsultas.agregarMaquinista("Maquinista1", "Ayudante");
            hacedorDeConsultas.agregarMaquinista("Maquinista2", "Ayudante");
            int cantidadMaquinistas = hacedorDeConsultas.cantidadMaquinistas();

            Assert.AreNotEqual(cantidadMaquinistas, 3);
        }

        [TestMethod]
        public void FallaAgregartresMaquinistaALaBaseDeDatos()
        {
            hacedorDeConsultas.agregarMaquinista("Maquinista1", "Ayudante");
            hacedorDeConsultas.agregarMaquinista("Maquinista2", "Ayudante");
            hacedorDeConsultas.agregarMaquinista("Maquinista3", "Ayudante");
            int cantidadMaquinistas = hacedorDeConsultas.cantidadMaquinistas();

            Assert.AreNotEqual(cantidadMaquinistas, 4);
        }

        [TestMethod]
        public void FallaAgregar40MaquinistaALaBaseDeDatos()
        {
            for (int i = 0; i < 40; i++)
                hacedorDeConsultas.agregarMaquinista("Maquinista" + i, "Ayudante");

            int cantidadMaquinistas = hacedorDeConsultas.cantidadMaquinistas();

            Assert.AreNotEqual(cantidadMaquinistas, 41);
        }

        [TestMethod]
        public void FallaEliminarMaquinista()
        {
            hacedorDeConsultas.agregarMaquinista("Maquinista1", "Ayudante");
            string indice = hacedorDeConsultas.getIndiceMaquinista("Maquinista1");
            hacedorDeConsultas.borrarMaquinista(indice);
            int cantidadMaquinistas = hacedorDeConsultas.cantidadMaquinistas();

            Assert.AreNotEqual(cantidadMaquinistas, 1);
        }

        [TestMethod]
        public void FallaEliminar40Maquinistas()
        {
            for (int i = 0; i < 40; i++)
                hacedorDeConsultas.agregarMaquinista("Maquinista" + i, "Ayudante");

            string indice;

            for (int i = 0; i < 40; i++)
            {
                indice = hacedorDeConsultas.getIndiceMaquinista("Maquinista" + i);
                hacedorDeConsultas.borrarMaquinista(indice);
            }

            int cantidadMaquinistas = hacedorDeConsultas.cantidadMaquinistas();

            Assert.AreNotEqual(cantidadMaquinistas, 40);
        }

        [TestMethod]
        public void FallaActualizarMaquinista()
        {
            hacedorDeConsultas.agregarMaquinista("Maquinista1", "Ayudante");
            string indice = hacedorDeConsultas.getIndiceMaquinista("Maquinista1");
            hacedorDeConsultas.updateMaquinista("elnombremodificado", "frfrf", indice);
            indice = hacedorDeConsultas.getIndiceMaquinista("elnombremodificado");
            hacedorDeConsultas.borrarMaquinista(indice);

            int cantidadMaquinistas = hacedorDeConsultas.cantidadMaquinistas();

            Assert.AreNotEqual(cantidadMaquinistas, 1);
        }

        [TestMethod]
        public void FallaActualizar40Maquinistas()
        {
            string indice;
            for (int i = 0; i < 40; i++)
            {
                hacedorDeConsultas.agregarMaquinista("Maquinista1", "Ayudante");
                indice = hacedorDeConsultas.getIndiceMaquinista("Maquinista1");
                hacedorDeConsultas.updateMaquinista("elnombremodificado" + i, "frfrf", indice);
            }

            for (int i = 0; i < 40; i++)
            {
                indice = hacedorDeConsultas.getIndiceMaquinista("elnombremodificado" + i);
                hacedorDeConsultas.borrarMaquinista(indice);
            }

            int cantidadMaquinistas = hacedorDeConsultas.cantidadMaquinistas();

            Assert.AreNotEqual(cantidadMaquinistas, 40);
        }
  
    }
}
