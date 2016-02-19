using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using LibControlSistematico;

namespace Tests.BaseDeDatos
{
    [TestClass]
    public class TestConsultasBobinas
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
        public void agregarBobinaALaBaseDeDatos(){
        
            hacedorDeConsultas.agregarBobina("1", "direccion", "localidad", "CP", "Provincia", "IVA", "CUIT");
            int cantidadBobinas = hacedorDeConsultas.cantidadBobinas();

            Assert.AreEqual(cantidadBobinas, 1);
        }

        [TestMethod]
        public void agregarDosBobinaALaBaseDeDatos()
        {
            hacedorDeConsultas.agregarBobina("Bobina1", "direccion", "localidad", "CP", "Provincia", "IVA", "CUIT");
            hacedorDeConsultas.agregarBobina("Bobina2", "direccion", "localidad", "CP", "Provincia", "IVA", "CUIT");
            int cantidadBobinas = hacedorDeConsultas.cantidadBobinas();

            Assert.AreEqual(cantidadBobinas, 2);
        }

        [TestMethod]
        public void agregartresBobinaALaBaseDeDatos()
        {
            hacedorDeConsultas.agregarBobina("Bobina1", "direccion", "localidad", "CP", "Provincia", "IVA", "CUIT");
            hacedorDeConsultas.agregarBobina("Bobina2", "direccion", "localidad", "CP", "Provincia", "IVA", "CUIT");
            hacedorDeConsultas.agregarBobina("Bobina3", "direccion", "localidad", "CP", "Provincia", "IVA", "CUIT");
            int cantidadBobinas = hacedorDeConsultas.cantidadBobinas();

            Assert.AreEqual(cantidadBobinas, 3);
        }

        [TestMethod]
        public void agregar40BobinaALaBaseDeDatos()
        {
            for (int i = 0; i < 40; i++)
                hacedorDeConsultas.agregarBobina("BobinaModificado" + i, "direccion", "localidad", "CP", "Provincia", "IVA", "CUIT");

            int cantidadBobinas = hacedorDeConsultas.cantidadBobinas();

            Assert.AreEqual(cantidadBobinas, 40);
        }

        [TestMethod]
        public void eliminarBobina()
        {
            hacedorDeConsultas.agregarBobina("Bobina1", "direccion", "localidad", "CP", "Provincia", "IVA", "CUIT");
            string indice = hacedorDeConsultas.getIndiceBobina("Bobina1");
            hacedorDeConsultas.borrarBobina(indice);
            int cantidadBobinas = hacedorDeConsultas.cantidadBobinas();

            Assert.AreEqual(cantidadBobinas, 0);
        }

        [TestMethod]
        public void eliminar40Bobinas()
        {
            for (int i = 0; i < 40; i++)
                hacedorDeConsultas.agregarBobina("BobinaModificado" + i, "direccion", "localidad", "CP", "Provincia", "IVA", "CUIT");

            string indice;

            for (int i = 0; i < 40; i++)
            {
                indice = hacedorDeConsultas.getIndiceBobina("BobinaModificado" + i);
                hacedorDeConsultas.borrarBobina(indice);
            }

            int cantidadBobinas = hacedorDeConsultas.cantidadBobinas();

            Assert.AreEqual(cantidadBobinas, 0);
        }

        [TestMethod]
        public void actualizarBobina()
        {
            hacedorDeConsultas.agregarBobina("Bobina1", "direccion", "localidad", "CP", "Provincia", "IVA", "CUIT");
            string indice;
            indice = hacedorDeConsultas.getIndiceBobina("Bobina1");
            hacedorDeConsultas.updateBobina("BobinaModificado", "direccion", "localidad", "CP", "Provincia", "IVA", "CUIT", indice);
            indice = hacedorDeConsultas.getIndiceBobina("BobinaModificado");
            hacedorDeConsultas.borrarBobina(indice);

            int cantidadBobinas = hacedorDeConsultas.cantidadBobinas();

            Assert.AreEqual(cantidadBobinas, 0);
        }

        [TestMethod]
        public void actualizar40Bobinas()
        {
            string indice;
            for (int i = 0; i < 40; i++)
            {
                hacedorDeConsultas.agregarBobina("Bobina1", "direccion", "localidad", "CP", "Provincia", "IVA", "CUIT");
                indice = hacedorDeConsultas.getIndiceBobina("Bobina1");
                hacedorDeConsultas.updateBobina("Bobina" + i, "direccion", "localidad", "CP", "Provincia", "IVA", "CUIT", indice);
            }

            for (int i = 0; i < 40; i++)
            {
                indice = hacedorDeConsultas.getIndiceBobina("Bobina" + i);
                hacedorDeConsultas.borrarBobina(indice);
            }

            int cantidadBobinas = hacedorDeConsultas.cantidadBobinas();

            Assert.AreEqual(cantidadBobinas, 0);
        }


        [TestMethod]
        public void fallaAgregarBobinaALaBaseDeDatos()
        {
            hacedorDeConsultas.agregarBobina("Bobina1", "direccion", "localidad", "CP", "Provincia", "IVA", "CUIT");
            int cantidadBobinas = hacedorDeConsultas.cantidadBobinas();

            Assert.AreNotEqual(cantidadBobinas, 2);
        }

        [TestMethod]
        public void FallaAgregarDosBobinaALaBaseDeDatos()
        {
            hacedorDeConsultas.agregarBobina("Bobina1", "direccion", "localidad", "CP", "Provincia", "IVA", "CUIT");
            hacedorDeConsultas.agregarBobina("Bobina2", "direccion", "localidad", "CP", "Provincia", "IVA", "CUIT");
            int cantidadBobinas = hacedorDeConsultas.cantidadBobinas();

            Assert.AreNotEqual(cantidadBobinas, 3);
        }

        [TestMethod]
        public void FallaAgregartresBobinaALaBaseDeDatos()
        {
            hacedorDeConsultas.agregarBobina("Bobina1", "direccion", "localidad", "CP", "Provincia", "IVA", "CUIT");
            hacedorDeConsultas.agregarBobina("Bobina2", "direccion", "localidad", "CP", "Provincia", "IVA", "CUIT");
            hacedorDeConsultas.agregarBobina("Bobina3", "direccion", "localidad", "CP", "Provincia", "IVA", "CUIT");
            int cantidadBobinas = hacedorDeConsultas.cantidadBobinas();

            Assert.AreNotEqual(cantidadBobinas, 4);
        }

        [TestMethod]
        public void FallaAgregar40BobinaALaBaseDeDatos()
        {
            for (int i = 0; i < 40; i++)
                hacedorDeConsultas.agregarBobina("BobinaModificado" + i, "direccion", "localidad", "CP", "Provincia", "IVA", "CUIT");

            int cantidadBobinas = hacedorDeConsultas.cantidadBobinas();

            Assert.AreNotEqual(cantidadBobinas, 41);
        }

        [TestMethod]
        public void FallaEliminarBobina()
        {
            hacedorDeConsultas.agregarBobina("Bobina1", "direccion", "localidad", "CP", "Provincia", "IVA", "CUIT");
            string indice = hacedorDeConsultas.getIndiceBobina("Bobina1");
            hacedorDeConsultas.borrarBobina(indice);
            int cantidadBobinas = hacedorDeConsultas.cantidadBobinas();

            Assert.AreNotEqual(cantidadBobinas, 1);
        }

        [TestMethod]
        public void FallaEliminar40Bobinas()
        {
            for (int i = 0; i < 40; i++)
                hacedorDeConsultas.agregarBobina("BobinaModificado" + i, "direccion", "localidad", "CP", "Provincia", "IVA", "CUIT");

            string indice;

            for (int i = 0; i < 40; i++)
            {
                indice = hacedorDeConsultas.getIndiceBobina("BobinaModificado" + i);
                hacedorDeConsultas.borrarBobina(indice);
            }

            int cantidadBobinas = hacedorDeConsultas.cantidadBobinas();

            Assert.AreNotEqual(cantidadBobinas, 40);
        }

        [TestMethod]
        public void FallaActualizarBobina()
        {
            hacedorDeConsultas.agregarBobina("Bobina1", "direccion", "localidad", "CP", "Provincia", "IVA", "CUIT");
            string indice = hacedorDeConsultas.getIndiceBobina("Bobina1");
            hacedorDeConsultas.updateBobina("BobinaModificado", "direccion", "localidad", "CP", "Provincia", "IVA", "CUIT", indice);
            indice = hacedorDeConsultas.getIndiceBobina("BobinaModificado");
            hacedorDeConsultas.borrarBobina(indice);

            int cantidadBobinas = hacedorDeConsultas.cantidadBobinas();

            Assert.AreNotEqual(cantidadBobinas, 1);
        }

        [TestMethod]
        public void FallaActualizar40Bobinas()
        {
            string indice;
            for (int i = 0; i < 40; i++)
            {
                hacedorDeConsultas.agregarBobina("Bobina1", "direccion", "localidad", "CP", "Provincia", "IVA", "CUIT");
                indice = hacedorDeConsultas.getIndiceBobina("Bobina1");
                hacedorDeConsultas.updateBobina("Bobina" + i, "direccion", "localidad", "CP", "Provincia", "IVA", "CUIT", indice);
            }

            for (int i = 0; i < 40; i++)
            {
                indice = hacedorDeConsultas.getIndiceBobina("Bobina" + i);
                hacedorDeConsultas.borrarBobina(indice);
            }

            int cantidadBobinas = hacedorDeConsultas.cantidadBobinas();

            Assert.AreNotEqual(cantidadBobinas, 40);
        }


    }
}
