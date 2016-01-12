using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using LibControlSistematico;


namespace TestLectorCodigo.Turnos
{
    [TestClass]
    public class PruebaTurnos
    {
        [TestMethod]
        public void esTurnoValido21a5()
        {
            turno Turno = new turno();

            Turno.getIndiceTurno(1,0,21);
            Assert.AreEqual("21 a 5",Turno.getTurno());
        }

        [TestMethod]
        public void esTurnoValido5a13()
        {
            turno Turno = new turno();

            Turno.getIndiceTurno(1, 6, 21);
            Assert.AreEqual("5 a 13", Turno.getTurno());
        }

        [TestMethod]
        public void esTurnoValido13a21()
        {
            turno Turno = new turno();

            Turno.getIndiceTurno(1, 14, 21);
            Assert.AreEqual("13 a 21", Turno.getTurno());
        }

        [TestMethod]
        public void esTurnoInvalido21a5()
        {
            turno Turno = new turno();

            Turno.getIndiceTurno(1, 0, 21);
            Assert.AreNotEqual("5 a 13", Turno.getTurno());
        }

        [TestMethod]
        public void esTurnoInvalido5a13()
        {
            turno Turno = new turno();

            Turno.getIndiceTurno(1, 6, 21);
            Assert.AreNotEqual("21 a 5", Turno.getTurno());
        }

        [TestMethod]
        public void esTurnoInvalido13a21()
        {
            turno Turno = new turno();

            Turno.getIndiceTurno(1, 14, 21);
            Assert.AreNotEqual("5 a 13", Turno.getTurno());
        }

        [TestMethod]
        public void noCambiaDeTurnoAlEstarConMismoMaquinista()
        {
            turno Turno = new turno();

            string indice1 = Turno.getIndiceTurno(1,14,15);
            Turno.getIndiceTurno(1, 15, 15);
            Turno.getIndiceTurno(1, 16, 15);
            Turno.getIndiceTurno(1, 17, 15);
            Turno.getIndiceTurno(1, 18, 15);
            Turno.getIndiceTurno(1, 21, 15);
            string indice2 = Turno.getIndiceTurno(1, 22, 15);

            Assert.AreEqual(indice1, indice2);
        }

        [TestMethod]
        public void cambiaDeTurnoAlEstarConMismoMaquinista()
        {
            turno Turno = new turno();

            string indice1 = Turno.getIndiceTurno(1, 14, 15);
            Turno.getIndiceTurno(1, 15, 15);
            Turno.getIndiceTurno(1, 16, 15);
            Turno.getIndiceTurno(1, 17, 15);
            Turno.getIndiceTurno(1, 18, 15);
            Turno.getIndiceTurno(1, 21, 15);
            string indice2 = Turno.getIndiceTurno(2, 22, 15);

            Assert.AreNotEqual(indice1, indice2);
        }

        [TestMethod]
        public void cambiaDeTurnoEnElBordeDe21a5ConCambioDeMaquinista()
        {
            turno Turno = new turno();
            string indice1 = Turno.getIndiceTurno(1, 21, 0);
            string indice2 = Turno.getIndiceTurno(2, 4, 40);
            Assert.AreNotEqual(indice1, indice2);
        }

        [TestMethod]
        public void cambiaDeTurnoEnElBordeDe5a13ConCambioDeMaquinista()
        {
            turno Turno = new turno();
            string indice1 = Turno.getIndiceTurno(1, 5, 0);
            string indice2 = Turno.getIndiceTurno(2, 12, 40);
            Assert.AreNotEqual(indice1, indice2);
        }

        [TestMethod]
        public void cambiaDeTurnoEnElBordeDe13a21ConCambioDeMaquinista()
        {
            turno Turno = new turno();
            string indice1 = Turno.getIndiceTurno(1, 13, 0);
            string indice2 = Turno.getIndiceTurno(2, 20, 40);
            Assert.AreNotEqual(indice1, indice2);
        }

        [TestMethod]
        public void esTurnoDe21a5AlCambiarEnElBorde()
        {
            turno Turno = new turno();

            Turno.getIndiceTurno(1, 20, 40);

            Assert.AreEqual("21 a 5",Turno.getTurno());
        }

        [TestMethod]
        public void esTurnoDe5a13AlCambiarEnElBorde()
        {
            turno Turno = new turno();

            Turno.getIndiceTurno(1, 4, 40);

            Assert.AreEqual("5 a 13", Turno.getTurno());
        }

        [TestMethod]
        public void esTurnoDe13a21AlCambiarEnElBorde()
        {
            turno Turno = new turno();

            Turno.getIndiceTurno(1, 12, 40);

            Assert.AreEqual("13 a 21", Turno.getTurno());
        }

        [TestMethod]
        public void esTurno21a5SiguienteCambiandoloAUnaHoraPasada()
        {
            turno Turno = new turno();
            Turno.getIndiceTurno(1,13,10);
            Turno.getIndiceTurno(2,21,20);

            Assert.AreEqual("21 a 5",Turno.getTurno());
        }

        [TestMethod]
        public void esTurno5a13SiguienteCambiandoloAUnaHoraPasada()
        {
            turno Turno = new turno();
            Turno.getIndiceTurno(1, 22, 10);
            Turno.getIndiceTurno(2, 5, 20);

            Assert.AreEqual("5 a 13", Turno.getTurno());
        }

        [TestMethod]
        public void esTurno13a21SiguienteCambiandoloAUnaHoraPasada()
        {
            turno Turno = new turno();
            Turno.getIndiceTurno(1, 5, 10);
            Turno.getIndiceTurno(2, 15, 20);

            Assert.AreEqual("13 a 21", Turno.getTurno());
        }

    }
}
