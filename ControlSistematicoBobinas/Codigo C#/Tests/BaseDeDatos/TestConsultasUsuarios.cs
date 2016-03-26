using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using LibControlSistematico;

namespace Tests
{
    /// <summary>
    /// Descripción resumida de testConsultasSQL
    /// </summary>
    [TestClass]
    public class TestConsultasUsuarios
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
        public void agregarUsuarioALaBaseDeDatos()
        {
            hacedorDeConsultas.agregarUsuario("usuario1", "123", "1");
            int cantidadUsuarios = hacedorDeConsultas.cantidadUsuarios();

            Assert.AreEqual(cantidadUsuarios,1);
        }

        [TestMethod]
        public void agregarDosUsuarioALaBaseDeDatos()
        {
            hacedorDeConsultas.agregarUsuario("usuario1", "123", "1");
            hacedorDeConsultas.agregarUsuario("usuario2", "123", "1");
            int cantidadUsuarios = hacedorDeConsultas.cantidadUsuarios();

            Assert.AreEqual(cantidadUsuarios, 2);
        }

        [TestMethod]
        public void agregartresUsuarioALaBaseDeDatos()
        {
            hacedorDeConsultas.agregarUsuario("usuario1", "123", "1");
            hacedorDeConsultas.agregarUsuario("usuario2", "123", "1");
            hacedorDeConsultas.agregarUsuario("usuario3", "123", "1");
            int cantidadUsuarios = hacedorDeConsultas.cantidadUsuarios();

            Assert.AreEqual(cantidadUsuarios, 3);
        }

        [TestMethod]
        public void agregar40UsuarioALaBaseDeDatos()
        {
            for (int i=0; i<40;i++ )
                hacedorDeConsultas.agregarUsuario("usuario"+i, "123", "1");
            
            int cantidadUsuarios = hacedorDeConsultas.cantidadUsuarios();

            Assert.AreEqual(cantidadUsuarios, 40);
        }

        [TestMethod]
        public void eliminarUsuario() 
        {
            hacedorDeConsultas.agregarUsuario("usuario1", "123", "1");
            string indice = hacedorDeConsultas.getIndiceUsuario("usuario1");
            hacedorDeConsultas.borrarUsuario(indice);
            int cantidadUsuarios = hacedorDeConsultas.cantidadUsuarios();

            Assert.AreEqual(cantidadUsuarios, 0);
        }

        [TestMethod]
        public void eliminar40Usuarios() 
        {
            for (int i = 0; i < 40; i++)
                hacedorDeConsultas.agregarUsuario("usuario" + i, "123", "1");

            string indice;

            for (int i = 0; i < 40; i++) { 
                indice = hacedorDeConsultas.getIndiceUsuario("usuario"+i);
                hacedorDeConsultas.borrarUsuario(indice);
            }

            int cantidadUsuarios = hacedorDeConsultas.cantidadUsuarios();

            Assert.AreEqual(cantidadUsuarios, 0);
        }

        [TestMethod]
        public void actualizarUsuario()
        {
            hacedorDeConsultas.agregarUsuario("Usuario1","123","1");
            string indice = hacedorDeConsultas.getIndiceUsuario("usuario1");
            hacedorDeConsultas.updateUsuario(indice,"elnombremodificado","frfrf","1");
            indice = hacedorDeConsultas.getIndiceUsuario("elnombremodificado");
            hacedorDeConsultas.borrarUsuario(indice);

            int cantidadUsuarios = hacedorDeConsultas.cantidadUsuarios();

            Assert.AreEqual(cantidadUsuarios, 0);
        }

        [TestMethod]
        public void actualizar40Usuarios()
        {
            string indice;
            for (int i = 0; i < 40; i++)
            {
                hacedorDeConsultas.agregarUsuario("Usuario1", "123", "1");
                indice = hacedorDeConsultas.getIndiceUsuario("usuario1");
                hacedorDeConsultas.updateUsuario(indice, "elnombremodificado"+i, "frfrf", "1");
            }

            for (int i = 0; i < 40; i++)
            {
                indice = hacedorDeConsultas.getIndiceUsuario("elnombremodificado"+i);
                hacedorDeConsultas.borrarUsuario(indice);
            }

            int cantidadUsuarios = hacedorDeConsultas.cantidadUsuarios();

            Assert.AreEqual(cantidadUsuarios, 0);
        }


        [TestMethod]
        public void fallaAgregarUsuarioALaBaseDeDatos()
        {
            hacedorDeConsultas.agregarUsuario("usuario1", "123", "1");
            int cantidadUsuarios = hacedorDeConsultas.cantidadUsuarios();

            Assert.AreNotEqual(cantidadUsuarios, 2);
        }

        [TestMethod]
        public void FallaAgregarDosUsuarioALaBaseDeDatos()
        {
            hacedorDeConsultas.agregarUsuario("usuario1", "123", "1");
            hacedorDeConsultas.agregarUsuario("usuario2", "123", "1");
            int cantidadUsuarios = hacedorDeConsultas.cantidadUsuarios();

            Assert.AreNotEqual(cantidadUsuarios, 3);
        }

        [TestMethod]
        public void FallaAgregartresUsuarioALaBaseDeDatos()
        {
            hacedorDeConsultas.agregarUsuario("usuario1", "123", "1");
            hacedorDeConsultas.agregarUsuario("usuario2", "123", "1");
            hacedorDeConsultas.agregarUsuario("usuario3", "123", "1");
            int cantidadUsuarios = hacedorDeConsultas.cantidadUsuarios();

            Assert.AreNotEqual(cantidadUsuarios, 4);
        }

        [TestMethod]
        public void FallaAgregar40UsuarioALaBaseDeDatos()
        {
            for (int i = 0; i < 40; i++)
                hacedorDeConsultas.agregarUsuario("usuario" + i, "123", "1");

            int cantidadUsuarios = hacedorDeConsultas.cantidadUsuarios();

            Assert.AreNotEqual(cantidadUsuarios, 41);
        }

        [TestMethod]
        public void FallaEliminarUsuario()
        {
            hacedorDeConsultas.agregarUsuario("usuario1", "123", "1");
            string indice = hacedorDeConsultas.getIndiceUsuario("usuario1");
            hacedorDeConsultas.borrarUsuario(indice);
            int cantidadUsuarios = hacedorDeConsultas.cantidadUsuarios();

            Assert.AreNotEqual(cantidadUsuarios, 1);
        }

        [TestMethod]
        public void FallaEliminar40Usuarios()
        {
            for (int i = 0; i < 40; i++)
                hacedorDeConsultas.agregarUsuario("usuario" + i, "123", "1");

            string indice;

            for (int i = 0; i < 40; i++)
            {
                indice = hacedorDeConsultas.getIndiceUsuario("usuario" + i);
                hacedorDeConsultas.borrarUsuario(indice);
            }

            int cantidadUsuarios = hacedorDeConsultas.cantidadUsuarios();

            Assert.AreNotEqual(cantidadUsuarios, 40);
        }

        [TestMethod]
        public void FallaActualizarUsuario()
        {
            hacedorDeConsultas.agregarUsuario("Usuario1", "123", "1");
            string indice = hacedorDeConsultas.getIndiceUsuario("usuario1");
            hacedorDeConsultas.updateUsuario(indice, "elnombremodificado", "frfrf", "1");
            indice = hacedorDeConsultas.getIndiceUsuario("elnombremodificado");
            hacedorDeConsultas.borrarUsuario(indice);

            int cantidadUsuarios = hacedorDeConsultas.cantidadUsuarios();

            Assert.AreNotEqual(cantidadUsuarios, 1);
        }

        [TestMethod]
        public void FallaActualizar40Usuarios()
        {
            string indice;
            for (int i = 0; i < 40; i++)
            {
                hacedorDeConsultas.agregarUsuario("Usuario1", "123", "1");
                indice = hacedorDeConsultas.getIndiceUsuario("usuario1");
                hacedorDeConsultas.updateUsuario(indice, "elnombremodificado"+i, "frfrf", "1");
            }

            for (int i = 0; i < 40; i++)
            {
                indice = hacedorDeConsultas.getIndiceUsuario("elnombremodificado"+i);
                hacedorDeConsultas.borrarUsuario(indice);
            }

            int cantidadUsuarios = hacedorDeConsultas.cantidadUsuarios();

            Assert.AreNotEqual(cantidadUsuarios, 40);
        }


    }
}
