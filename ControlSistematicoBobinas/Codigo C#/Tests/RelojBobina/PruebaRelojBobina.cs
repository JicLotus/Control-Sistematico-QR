using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using LibControlSistematico;

namespace TestLectorCodigo.relojBobina
{
    [TestClass]
    public class pruebaRelojBobina
    {
        [TestMethod]
        public void noEsCopiaAlIniciarElRelojBobina()
        {
            RelojBobina relojBobina = new RelojBobina();

            Assert.IsFalse(relojBobina.esCopia(0));
        }

        [TestMethod]
        public void esCopiaPreguntandoDeNuevoSiEsCopia()
        {
            RelojBobina relojBobina = new RelojBobina();
            relojBobina.esCopia(0);

            Assert.IsTrue(relojBobina.esCopia(0));
        }

        [TestMethod]
        public void noEsCopiaAlIniciarElRelojYTenerUnTiempoMayorEntreBobina()
        {
            RelojBobina relojBobina = new RelojBobina();
            relojBobina.esCopia(0);
            Assert.IsFalse(relojBobina.esCopia(600));
        }
    }
}
