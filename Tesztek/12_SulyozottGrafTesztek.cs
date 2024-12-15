using Microsoft.VisualStudio.TestTools.UnitTesting;
using OE.ALGA.Adatszerkezetek;

namespace OE.ALGA.Tesztek
{
    [TestClass]
    public class CsucsMatrixSulyozottGrafTesztek
    {
        [TestMethod]
        public void MindenCsucsTeszt() //F2.
        {
            CsucsmatrixSulyozottEgeszGraf csg = new CsucsmatrixSulyozottEgeszGraf(2);
            Assert.IsTrue(csg.Csucsok.Eleme(0));
            Assert.IsTrue(csg.Csucsok.Eleme(1));
        }

        [TestMethod]
        public void MindenElTeszt() //F2.
        {
            CsucsmatrixSulyozottEgeszGraf csg = new CsucsmatrixSulyozottEgeszGraf(3);
            csg.UjEl(0, 1, 1.0f);
            csg.UjEl(0, 2, 2.0f);
            csg.UjEl(1, 2, 3.0f);

            Assert.IsFalse(csg.Elek.Eleme(new SulyozottEgeszGrafEl(0, 0, 0.0f)));
            Assert.IsTrue(csg.Elek.Eleme(new SulyozottEgeszGrafEl(0, 1, 1.0f)));
            Assert.IsTrue(csg.Elek.Eleme(new SulyozottEgeszGrafEl(0, 2, 2.0f)));

            Assert.IsFalse(csg.Elek.Eleme(new SulyozottEgeszGrafEl(1, 0, 0.0f)));
            Assert.IsFalse(csg.Elek.Eleme(new SulyozottEgeszGrafEl(1, 1, 0.0f)));
            Assert.IsTrue(csg.Elek.Eleme(new SulyozottEgeszGrafEl(1, 2, 3.0f)));

            Assert.IsFalse(csg.Elek.Eleme(new SulyozottEgeszGrafEl(2, 0, 0.0f)));
            Assert.IsFalse(csg.Elek.Eleme(new SulyozottEgeszGrafEl(2, 1, 0.0f)));
            Assert.IsFalse(csg.Elek.Eleme(new SulyozottEgeszGrafEl(2, 2, 0.0f)));
        }

        [TestMethod]
        public void VezetElTeszt() //F2.
        {
            CsucsmatrixSulyozottEgeszGraf csg = new CsucsmatrixSulyozottEgeszGraf(2);
            Assert.IsFalse(csg.VezetEl(0, 1));
            csg.UjEl(0, 1, 1.0f);
            Assert.IsTrue(csg.VezetEl(0, 1));
            Assert.IsFalse(csg.VezetEl(1, 0));
        }

        [TestMethod]
        public void SzomszedsagTeszt() //F2.
        {
            CsucsmatrixSulyozottEgeszGraf csg = new CsucsmatrixSulyozottEgeszGraf(3);
            csg.UjEl(0, 1, 1.0f);
            csg.UjEl(0, 2, 1.0f);
            csg.UjEl(1, 2, 1.0f);

            Halmaz<int> a_szomszedai = csg.Szomszedai(0);
            Halmaz<int> b_szomszedai = csg.Szomszedai(1);
            Halmaz<int> c_szomszedai = csg.Szomszedai(2);

            Assert.IsFalse(a_szomszedai.Eleme(0));
            Assert.IsTrue(a_szomszedai.Eleme(1));
            Assert.IsTrue(a_szomszedai.Eleme(2));

            Assert.IsFalse(b_szomszedai.Eleme(0));
            Assert.IsFalse(b_szomszedai.Eleme(1));
            Assert.IsTrue(b_szomszedai.Eleme(2));

            Assert.IsFalse(c_szomszedai.Eleme(0));
            Assert.IsFalse(c_szomszedai.Eleme(1));
            Assert.IsFalse(c_szomszedai.Eleme(2));
        }

        [TestMethod]
        [ExpectedException(typeof(NincsElKivetel))]
        public void NemLetezoElTeszt() //F2.
        {
            CsucsmatrixSulyozottEgeszGraf csg = new CsucsmatrixSulyozottEgeszGraf(3);
            csg.UjEl(0, 1, 1.0f);
            csg.UjEl(0, 2, 1.0f);
            csg.Suly(1, 0);
        }

        [TestMethod]
        public void ElSulyTeszt() //F2.
        {
            CsucsmatrixSulyozottEgeszGraf csg = new CsucsmatrixSulyozottEgeszGraf(3);
            csg.UjEl(0, 1, 2.0f);
            csg.UjEl(0, 2, 3.0f);
            float szum = 0.0f;
            csg.Elek.Bejar(x => szum += csg.Suly(x.Honnan, x.Hova));
            Assert.AreEqual(5.0f, szum);
        }
    }

    [TestClass]
    public class GrafUtkeresesTesztek
    {
        [TestMethod]
        public void DijkstraKicsiGrafTeszt() //F3.
        {
            CsucsmatrixSulyozottEgeszGraf csg = new CsucsmatrixSulyozottEgeszGraf(3);
            csg.UjEl(0, 1, 10.0f);
            csg.UjEl(0, 2, 20.0f);
            Szotar<int, float> hossz = Utkereses.Dijkstra(csg, 0);
            Assert.AreEqual(0.0f, hossz.Kiolvas(0));
            Assert.AreEqual(10.0f, hossz.Kiolvas(1));
            Assert.AreEqual(20.0f, hossz.Kiolvas(2));

            csg.UjEl(1, 2, 5.0f);
            hossz = Utkereses.Dijkstra(csg, 0);
            Assert.AreEqual(0.0f, hossz.Kiolvas(0));
            Assert.AreEqual(10.0f, hossz.Kiolvas(1));
            Assert.AreEqual(15.0f, hossz.Kiolvas(2));
        }

        [TestMethod]
        public void DijkstraJegyzetGrafTeszt() //F3.
        {
            CsucsmatrixSulyozottEgeszGraf csg = new CsucsmatrixSulyozottEgeszGraf(7);
            csg.UjEl(0, 1, 1.0f);
            csg.UjEl(0, 3, 2.0f);
            csg.UjEl(0, 4, 4.0f);

            csg.UjEl(1, 0, 1.0f);
            csg.UjEl(1, 3, 2.0f);
            csg.UjEl(1, 2, 9.0f);

            csg.UjEl(2, 1, 9.0f);
            csg.UjEl(2, 3, 5.0f);
            csg.UjEl(2, 5, 1.0f);

            csg.UjEl(3, 0, 2.0f);
            csg.UjEl(3, 1, 2.0f);
            csg.UjEl(3, 2, 5.0f);
            csg.UjEl(3, 5, 3.0f);

            csg.UjEl(5, 2, 1.0f);
            csg.UjEl(5, 3, 3.0f);
            csg.UjEl(5, 6, 3.0f);

            csg.UjEl(6, 5, 3.0f);

            Szotar<int, float> hossz = Utkereses.Dijkstra(csg, 1);
            Assert.AreEqual(1.0f, hossz.Kiolvas(0));
            Assert.AreEqual(0.0f, hossz.Kiolvas(1));
            Assert.AreEqual(6.0f, hossz.Kiolvas(2));
            Assert.AreEqual(2.0f, hossz.Kiolvas(3));
            Assert.AreEqual(5.0f, hossz.Kiolvas(4));
            Assert.AreEqual(5.0f, hossz.Kiolvas(5));
            Assert.AreEqual(8.0f, hossz.Kiolvas(6));
        }
    }

    [TestClass]
    public class GrafMininmalisFeszitofaTesztek
    {
        [TestMethod]
        public void KisPrimTeszt() //F4.(a)
        {
            CsucsmatrixSulyozottEgeszGraf csg = new CsucsmatrixSulyozottEgeszGraf(3);
            csg.UjEl(0, 1, 10.0f);
            csg.UjEl(0, 2, 20.0f);
            csg.UjEl(1, 2, 5.0f);
            Szotar<int, int> elek = FeszitofaKereses.Prim(csg, 0);
            Assert.AreEqual(0, elek.Kiolvas(1));
            Assert.AreEqual(1, elek.Kiolvas(2));
        }

        [TestMethod]
        public void NagyPrimTeszt() //F4.(a)
        {
            CsucsmatrixSulyozottEgeszGraf csg = new CsucsmatrixSulyozottEgeszGraf(5);
            csg.UjEl(0, 1, 5.0f);
            csg.UjEl(0, 3, 4.0f);

            csg.UjEl(1, 0, 5.0f);
            csg.UjEl(1, 3, 2.0f);
            csg.UjEl(1, 2, 1.0f);

            csg.UjEl(2, 1, 1.0f);
            csg.UjEl(2, 3, 3.0f);
            csg.UjEl(2, 4, 4.0f);

            csg.UjEl(3, 0, 4.0f);
            csg.UjEl(3, 1, 2.0f);
            csg.UjEl(3, 2, 3.0f);
            csg.UjEl(3, 4, 1.0f);

            csg.UjEl(4, 2, 4.0f);
            csg.UjEl(4, 3, 1.0f);

            Szotar<int, int> elek = FeszitofaKereses.Prim(csg, 0);
            float sum = 0.0f;
            csg.Csucsok.Bejar(x =>
            {
                if (x != 0)
                {
                    int p = elek.Kiolvas(x);
                    sum += csg.Suly(p, x);
                }
            }
            );
            Assert.AreEqual(8.0f, sum);
        }

        [TestMethod]
        public void KisKruskalTeszt() //F4.(b)
        {
            CsucsmatrixSulyozottEgeszGraf csg = new CsucsmatrixSulyozottEgeszGraf(3);
            csg.UjEl(0, 1, 10.0f);
            csg.UjEl(0, 2, 20.0f);
            csg.UjEl(1, 2, 5.0f);
            Halmaz<SulyozottEgeszGrafEl> elek = FeszitofaKereses.Kruskal(csg);
            Assert.IsTrue(elek.Eleme(new SulyozottEgeszGrafEl(0, 1, 10.0f)));
            Assert.IsFalse(elek.Eleme(new SulyozottEgeszGrafEl(0, 2, 20.0f)));
            Assert.IsTrue(elek.Eleme(new SulyozottEgeszGrafEl(1, 2, 5.0f)));
        }

        [TestMethod]
        public void NagyKruskalTeszt() //F4.(b)
        {
            CsucsmatrixSulyozottEgeszGraf csg = new CsucsmatrixSulyozottEgeszGraf(5);
            csg.UjEl(0, 1, 5.0f);
            csg.UjEl(0, 3, 4.0f);

            csg.UjEl(1, 0, 5.0f);
            csg.UjEl(1, 3, 2.0f);
            csg.UjEl(1, 2, 1.0f);

            csg.UjEl(2, 1, 1.0f);
            csg.UjEl(2, 3, 3.0f);
            csg.UjEl(2, 4, 4.0f);

            csg.UjEl(3, 0, 4.0f);
            csg.UjEl(3, 1, 2.0f);
            csg.UjEl(3, 2, 3.0f);
            csg.UjEl(3, 4, 1.0f);

            csg.UjEl(4, 2, 4.0f);
            csg.UjEl(4, 3, 1.0f);

            Halmaz<SulyozottEgeszGrafEl> elek = FeszitofaKereses.Kruskal(csg);
            float sum = 0.0f;
            elek.Bejar(x =>
            {
                sum += x.Suly;
            }
            );
            Assert.AreEqual(8.0f, sum);
        }
    }
}
