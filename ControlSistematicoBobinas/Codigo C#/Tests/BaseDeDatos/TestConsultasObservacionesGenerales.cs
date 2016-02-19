using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;


using LibControlSistematico;


namespace Tests.BaseDeDatos
{
    [TestClass]
    public class TestConsultasObservacionesGenerales
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
        public void agregarObservacionesGeneralALaBaseDeDatos()
        {
            hacedorDeConsultas.agregarObservacionGeneral("ObservacionesGeneral1", "1-3-2016","1:1:1","jorge");
            int cantidadObservacionesGenerales = hacedorDeConsultas.cantidadObservacionesGenerales();

            Assert.AreEqual(cantidadObservacionesGenerales, 1);
        }

        [TestMethod]
        public void agregarDosObservacionesGeneralALaBaseDeDatos()
        {
            hacedorDeConsultas.agregarObservacionGeneral("ObservacionesGeneral1", "1-3-2016","1:1:1","jorge");
            hacedorDeConsultas.agregarObservacionGeneral("ObservacionesGeneral2", "1-3-2016","1:1:1","jorge");
            int cantidadObservacionesGenerales = hacedorDeConsultas.cantidadObservacionesGenerales();

            Assert.AreEqual(cantidadObservacionesGenerales, 2);
        }

        [TestMethod]
        public void agregartresObservacionesGeneralALaBaseDeDatos()
        {
            hacedorDeConsultas.agregarObservacionGeneral("ObservacionesGeneral1", "1-3-2016","1:1:1","jorge");
            hacedorDeConsultas.agregarObservacionGeneral("ObservacionesGeneral2", "1-3-2016","1:1:1","jorge");
            hacedorDeConsultas.agregarObservacionGeneral("ObservacionesGeneral3", "1-3-2016","1:1:1","jorge");
            int cantidadObservacionesGenerales = hacedorDeConsultas.cantidadObservacionesGenerales();

            Assert.AreEqual(cantidadObservacionesGenerales, 3);
        }

        [TestMethod]
        public void agregar40ObservacionesGeneralALaBaseDeDatos()
        {
            for (int i = 0; i < 40; i++)
                hacedorDeConsultas.agregarObservacionGeneral("ObservacionesGeneral" + i, "1-3-2016","1:1:1","jorge");

            int cantidadObservacionesGenerales = hacedorDeConsultas.cantidadObservacionesGenerales();

            Assert.AreEqual(cantidadObservacionesGenerales, 40);
        }

        [TestMethod]
        public void eliminarObservacionesGeneral()
        {
            hacedorDeConsultas.agregarObservacionGeneral("ObservacionesGeneral1", "1-3-2016","1:1:1","jorge");
            string indice = hacedorDeConsultas.getIndiceObservacion("ObservacionesGeneral1");
            hacedorDeConsultas.borrarObservacionGeneral(indice);
            int cantidadObservacionesGenerales = hacedorDeConsultas.cantidadObservacionesGenerales();

            Assert.AreEqual(cantidadObservacionesGenerales, 0);
        }

        [TestMethod]
        public void eliminar40ObservacionesGenerales()
        {
            for (int i = 0; i < 40; i++)
                hacedorDeConsultas.agregarObservacionGeneral("ObservacionesGeneral" + i, "1-3-2016","1:1:1","jorge");

            string indice;

            for (int i = 0; i < 40; i++)
            {
                indice = hacedorDeConsultas.getIndiceObservacion("ObservacionesGeneral" + i);
                hacedorDeConsultas.borrarObservacionGeneral(indice);
            }

            int cantidadObservacionesGenerales = hacedorDeConsultas.cantidadObservacionesGenerales();

            Assert.AreEqual(cantidadObservacionesGenerales, 0);
        }

        [TestMethod]
        public void actualizarObservacionesGeneral()
        {
            hacedorDeConsultas.agregarObservacionGeneral("ObservacionesGeneral1", "1-3-2016","1:1:1","jorge");
            string indice;
            indice = hacedorDeConsultas.getIndiceObservacion("ObservacionesGeneral1");
            hacedorDeConsultas.updateObservacionGeneral("observacionModificada", "1-3-2016","1:1:1","jorge",indice);
            indice = hacedorDeConsultas.getIndiceObservacion("observacionModificada");
            hacedorDeConsultas.borrarObservacionGeneral(indice);

            int cantidadObservacionesGenerales = hacedorDeConsultas.cantidadObservacionesGenerales();

            Assert.AreEqual(cantidadObservacionesGenerales, 0);
        }

        [TestMethod]
        public void actualizar40ObservacionesGenerales()
        {
            string indice;
            for (int i = 0; i < 40; i++)
            {
                hacedorDeConsultas.agregarObservacionGeneral("ObservacionesGeneral1", "1-3-2016","1:1:1","jorge");
                indice = hacedorDeConsultas.getIndiceObservacion("ObservacionesGeneral1");
                hacedorDeConsultas.updateObservacionGeneral("observacionModificada" +  i, "1-3-2016","1:1:1","jorge",indice);
            }

            for (int i = 0; i < 40; i++)
            {
                indice = hacedorDeConsultas.getIndiceObservacion("observacionModificada" + i);
                hacedorDeConsultas.borrarObservacionGeneral(indice);
            }

            int cantidadObservacionesGenerales = hacedorDeConsultas.cantidadObservacionesGenerales();

            Assert.AreEqual(cantidadObservacionesGenerales, 0);
        }


        [TestMethod]
        public void fallaAgregarObservacionesGeneralALaBaseDeDatos()
        {
            hacedorDeConsultas.agregarObservacionGeneral("ObservacionesGeneral1", "1-3-2016","1:1:1","jorge");
            int cantidadObservacionesGenerales = hacedorDeConsultas.cantidadObservacionesGenerales();

            Assert.AreNotEqual(cantidadObservacionesGenerales, 2);
        }

        [TestMethod]
        public void FallaAgregarDosObservacionesGeneralALaBaseDeDatos()
        {
            hacedorDeConsultas.agregarObservacionGeneral("ObservacionesGeneral1", "1-3-2016","1:1:1","jorge");
            hacedorDeConsultas.agregarObservacionGeneral("ObservacionesGeneral2", "1-3-2016","1:1:1","jorge");
            int cantidadObservacionesGenerales = hacedorDeConsultas.cantidadObservacionesGenerales();

            Assert.AreNotEqual(cantidadObservacionesGenerales, 3);
        }

        [TestMethod]
        public void FallaAgregartresObservacionesGeneralALaBaseDeDatos()
        {
            hacedorDeConsultas.agregarObservacionGeneral("ObservacionesGeneral1", "1-3-2016","1:1:1","jorge");
            hacedorDeConsultas.agregarObservacionGeneral("ObservacionesGeneral2", "1-3-2016","1:1:1","jorge");
            hacedorDeConsultas.agregarObservacionGeneral("ObservacionesGeneral3", "1-3-2016","1:1:1","jorge");
            int cantidadObservacionesGenerales = hacedorDeConsultas.cantidadObservacionesGenerales();

            Assert.AreNotEqual(cantidadObservacionesGenerales, 4);
        }

        [TestMethod]
        public void FallaAgregar40ObservacionesGeneralALaBaseDeDatos()
        {
            for (int i = 0; i < 40; i++)
                hacedorDeConsultas.agregarObservacionGeneral("ObservacionesGeneral" + i, "1-3-2016","1:1:1","jorge");

            int cantidadObservacionesGenerales = hacedorDeConsultas.cantidadObservacionesGenerales();

            Assert.AreNotEqual(cantidadObservacionesGenerales, 41);
        }

        [TestMethod]
        public void FallaEliminarObservacionesGeneral()
        {
            hacedorDeConsultas.agregarObservacionGeneral("ObservacionesGeneral1", "1-3-2016","1:1:1","jorge");
            string indice = hacedorDeConsultas.getIndiceObservacion("ObservacionesGeneral1");
            hacedorDeConsultas.borrarObservacionGeneral(indice);
            int cantidadObservacionesGenerales = hacedorDeConsultas.cantidadObservacionesGenerales();

            Assert.AreNotEqual(cantidadObservacionesGenerales, 1);
        }

        [TestMethod]
        public void FallaEliminar40ObservacionesGenerales()
        {
            for (int i = 0; i < 40; i++)
                hacedorDeConsultas.agregarObservacionGeneral("ObservacionesGeneral" + i, "1-3-2016","1:1:1","jorge");

            string indice;

            for (int i = 0; i < 40; i++)
            {
                indice = hacedorDeConsultas.getIndiceObservacion("ObservacionesGeneral" + i);
                hacedorDeConsultas.borrarObservacionGeneral(indice);
            }

            int cantidadObservacionesGenerales = hacedorDeConsultas.cantidadObservacionesGenerales();

            Assert.AreNotEqual(cantidadObservacionesGenerales, 40);
        }

        [TestMethod]
        public void FallaActualizarObservacionesGeneral()
        {
            hacedorDeConsultas.agregarObservacionGeneral("ObservacionesGeneral1", "1-3-2016","1:1:1","jorge");
            string indice = hacedorDeConsultas.getIndiceObservacion("ObservacionesGeneral1");
            hacedorDeConsultas.updateObservacionGeneral("observacionModificada", "1-3-2016","1:1:1","jorge",indice);
            indice = hacedorDeConsultas.getIndiceObservacion("observacionModificada");
            hacedorDeConsultas.borrarObservacionGeneral(indice);

            int cantidadObservacionesGenerales = hacedorDeConsultas.cantidadObservacionesGenerales();

            Assert.AreNotEqual(cantidadObservacionesGenerales, 1);
        }

        [TestMethod]
        public void FallaActualizar40ObservacionesGenerales()
        {
            string indice;
            for (int i = 0; i < 40; i++)
            {
                hacedorDeConsultas.agregarObservacionGeneral("ObservacionesGeneral1", "1-3-2016","1:1:1","jorge");
                indice = hacedorDeConsultas.getIndiceObservacion("ObservacionesGeneral1");
                hacedorDeConsultas.updateObservacionGeneral("observacionModificada" +  i, "1-3-2016","1:1:1","jorge",indice);
            }

            for (int i = 0; i < 40; i++)
            {
                indice = hacedorDeConsultas.getIndiceObservacion("observacionModificada" + i);
                hacedorDeConsultas.borrarObservacionGeneral(indice);
            }

            int cantidadObservacionesGenerales = hacedorDeConsultas.cantidadObservacionesGenerales();

            Assert.AreNotEqual(cantidadObservacionesGenerales, 40);
        }

    }
}
