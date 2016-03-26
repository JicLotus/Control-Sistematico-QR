using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using LibControlSistematico;

namespace Tests
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

        [TestMethod]
        public void agregarBobinaALaBaseDeDatos(){
            hacedorDeConsultas.agregarBobina(1, 1.0, "1-1-2015", 0.1, "observacion", 123, 1, "espesor", "1:1", "1", "1a2");
            int cantidadBobinas = hacedorDeConsultas.cantidadBobinas();

            Assert.AreEqual(cantidadBobinas, 1);
        }

        [TestMethod]
        public void agregarDosBobinaALaBaseDeDatos()
        {
            hacedorDeConsultas.agregarBobina(1, 1.0, "1-1-2015", 0.1, "observacion", 123, 1, "espesor", "1:1", "1", "1a2");
            hacedorDeConsultas.agregarBobina(1, 1.0, "1-1-2015", 0.1, "observacion2", 123, 1, "espesor2", "1:1", "1", "1a2");
            int cantidadBobinas = hacedorDeConsultas.cantidadBobinas();

            Assert.AreEqual(cantidadBobinas, 2);
        }

        [TestMethod]
        public void agregartresBobinaALaBaseDeDatos()
        {
            hacedorDeConsultas.agregarBobina(1, 1.0, "1-1-2015", 0.1, "observacion", 123, 1, "espesor", "1:1", "1", "1a2");
            hacedorDeConsultas.agregarBobina(1, 1.0, "1-1-2015", 0.1, "observacion2", 123, 1, "espesor2", "1:1", "1", "1a2");
            hacedorDeConsultas.agregarBobina(1, 1.0, "1-1-2015", 0.1, "observacion3", 123, 1, "espesor3", "1:1", "1", "1a2");
            int cantidadBobinas = hacedorDeConsultas.cantidadBobinas();

            Assert.AreEqual(cantidadBobinas, 3);
        }

        [TestMethod]
        public void agregar40BobinaALaBaseDeDatos()
        {
            for (int i = 0; i < 40; i++)
                hacedorDeConsultas.agregarBobina(1, 1.0, "1-1-2015", 0.1, "observacion"+i, 123, 1, "espesor"+i, "1:1", "1", "1a2");

            int cantidadBobinas = hacedorDeConsultas.cantidadBobinas();

            Assert.AreEqual(cantidadBobinas, 40);
        }


        

        

        


        [TestMethod]
        public void fallaAgregarBobinaALaBaseDeDatos()
        {
            hacedorDeConsultas.agregarBobina(1, 1.0, "1-1-2015", 0.1, "observacion", 123, 1, "espesor", "1:1", "1", "1a2");
            int cantidadBobinas = hacedorDeConsultas.cantidadBobinas();

            Assert.AreNotEqual(cantidadBobinas, 2);
        }

        [TestMethod]
        public void FallaAgregarDosBobinaALaBaseDeDatos()
        {
            hacedorDeConsultas.agregarBobina(1, 1.0, "1-1-2015", 0.1, "observacion", 123, 1, "espesor", "1:1", "1", "1a2");
            hacedorDeConsultas.agregarBobina(1, 1.0, "1-1-2015", 0.1, "observacion2", 123, 1, "espesor2", "1:1", "1", "1a2");
            int cantidadBobinas = hacedorDeConsultas.cantidadBobinas();

            Assert.AreNotEqual(cantidadBobinas, 3);
        }

        [TestMethod]
        public void FallaAgregartresBobinaALaBaseDeDatos()
        {
            hacedorDeConsultas.agregarBobina(1, 1.0, "1-1-2015", 0.1, "observacion", 123, 1, "espesor", "1:1", "1", "1a2");
            hacedorDeConsultas.agregarBobina(1, 1.0, "1-1-2015", 0.1, "observacion2", 123, 1, "espesor2", "1:1", "1", "1a2");
            hacedorDeConsultas.agregarBobina(1, 1.0, "1-1-2015", 0.1, "observacion3", 123, 1, "espesor3", "1:1", "1", "1a2");
            int cantidadBobinas = hacedorDeConsultas.cantidadBobinas();

            Assert.AreNotEqual(cantidadBobinas, 4);
        }

        [TestMethod]
        public void FallaAgregar40BobinaALaBaseDeDatos()
        {
            for (int i = 0; i < 40; i++)
                hacedorDeConsultas.agregarBobina(1, 1.0, "1-1-2015", 0.1, "observacion"+i, 123, 1, "espesor"+i, "1:1", "1", "1a2");

            int cantidadBobinas = hacedorDeConsultas.cantidadBobinas();

            Assert.AreNotEqual(cantidadBobinas, 41);
        }


    }
}
