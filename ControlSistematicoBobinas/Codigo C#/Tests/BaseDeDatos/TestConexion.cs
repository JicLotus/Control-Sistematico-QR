using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using LibControlSistematico;


namespace TestLectorCodigo
{
    [TestClass]
    public class TestConexion
    {

        [TestMethod]
        public void ConexionDBExitosa()
        {
            ConectorDB conector;

            conector = new ConectorDB("127.0.0.1", "testDB", "root", "", "3306","1");
            
            Assert.IsTrue(conector.OpenConnection());
            conector.CloseConnection();
        }

        [TestMethod]
        public void ConexionDBFallidaPorIP()
        {
            ConectorDB conector;
            conector = new ConectorDB("localhostt", "testDB", "root", "", "", "1");

            Assert.IsFalse(conector.OpenConnection());
        }

        [TestMethod]
        public void ConexionDBFallidaPorPuerto()
        {
            ConectorDB conector;
            conector = new ConectorDB("localhost", "testDB", "root", "", "123", "1");

            Assert.IsFalse(conector.OpenConnection());
        }

        [TestMethod]
        public void ConexionDBFallidaPorBaseDeDatos()
        {
            ConectorDB conector;
            conector = new ConectorDB("localhostt", "testDbbB", "root", "", "", "1");

            Assert.IsFalse(conector.OpenConnection());
        }

        [TestMethod]
        public void ConexionDBFallidaPorContrasenia()
        {
            ConectorDB conector;
            conector = new ConectorDB("localhostt", "testDB", "root", "3232", "", "1");

            Assert.IsFalse(conector.OpenConnection());
        }

    }
}
