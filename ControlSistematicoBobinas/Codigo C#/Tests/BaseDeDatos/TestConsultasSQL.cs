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
        ConectorDB conector;
        GeneradorDeConsultas generadorConsultas;
        String consulta;
        String nombre = "jose", contrasenia = "123", privilegio = "1";

        public TestConsultasSQL()
        {
            conector = new ConectorDB("localhost", "testDB", "root", "", "3306", "1");

            int registrosPorHoja=30;
            generadorConsultas = new GeneradorDeConsultas(registrosPorHoja);
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
            conector.OpenConnection();

            consulta = generadorConsultas.agregarUsuario(nombre,contrasenia,privilegio);

            String asd = "INSERT INTO  `testDB`.`usuarios` (`id` ,`Nombre` ,`Password` ,`Privilegio`)VALUES (NULL ,  '" + nombre + "',  '" + contrasenia + "',  '" + privilegio + "');";
            conector.HacerConsulta(asd);

            conector.CloseConnection();
        }

        [TestMethod]
        public void eliminarUsuarioALaBaseDeDatos()
        {
            
        }

    }
}
