using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using LibControlSistematico;

namespace Tests.BaseDeDatos
{
    /// <summary>
    /// Descripción resumida de testConsultasSQL
    /// </summary>
    [TestClass]
    public class TestConsultasSQL
    {
        HacedorDeConsultas consultador;


        [TestInitialize]
        public void Init()
        {
            consultador = new HacedorDeConsultas("localhost", "3306", "1","lectorcodigo");
        }

        [TestCleanup]
        public void Cleanup()
        {
        
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
        public void agregarUsuarioALaBaseDeDatos()
        {
            consultador.newUsuario("desodorante", "123", "1");

            consultador.getUsers();
        }


    }
}
