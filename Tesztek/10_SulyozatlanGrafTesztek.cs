using Microsoft.VisualStudio.TestTools.UnitTesting;
using OE.ALGA.Adatszerkezetek;

namespace OE.ALGA.Tesztek
{
    [TestClass]
    public class CsucsMatrixGrafTesztek
    {
        [TestMethod]
        public void MindenCsucsTeszt() //F2.
        {
            CsucsmatrixSulyozatlanEgeszGraf csg = new CsucsmatrixSulyozatlanEgeszGraf(2);
            Assert.IsTrue(csg.Csucsok.Eleme(0));
            Assert.IsTrue(csg.Csucsok.Eleme(1));
        }

        [TestMethod]
        public void MindenElTeszt() //F2.
        {
            CsucsmatrixSulyozatlanEgeszGraf csg = new CsucsmatrixSulyozatlanEgeszGraf(3);
            csg.UjEl(0, 1);
            csg.UjEl(0, 2);
            csg.UjEl(1, 2);

            Assert.IsFalse(csg.Elek.Eleme(new EgeszGrafEl(0, 0)));
            Assert.IsTrue(csg.Elek.Eleme(new EgeszGrafEl(0, 1)));
            Assert.IsTrue(csg.Elek.Eleme(new EgeszGrafEl(0, 2)));

            Assert.IsFalse(csg.Elek.Eleme(new EgeszGrafEl(1, 0)));
            Assert.IsFalse(csg.Elek.Eleme(new EgeszGrafEl(1, 1)));
            Assert.IsTrue(csg.Elek.Eleme(new EgeszGrafEl(1, 2)));

            Assert.IsFalse(csg.Elek.Eleme(new EgeszGrafEl(2, 0)));
            Assert.IsFalse(csg.Elek.Eleme(new EgeszGrafEl(2, 1)));
            Assert.IsFalse(csg.Elek.Eleme(new EgeszGrafEl(2, 2)));
        }

        [TestMethod]
        public void VezetElTeszt() //F2.
        {
            CsucsmatrixSulyozatlanEgeszGraf csg = new CsucsmatrixSulyozatlanEgeszGraf(2);
            Assert.IsFalse(csg.VezetEl(0, 1));
            csg.UjEl(0, 1);
            Assert.IsTrue(csg.VezetEl(0, 1));
            Assert.IsFalse(csg.VezetEl(1, 0));
        }

        [TestMethod]
        public void SzomszedsagTeszt() //F2.
        {
            CsucsmatrixSulyozatlanEgeszGraf csg = new CsucsmatrixSulyozatlanEgeszGraf(3);
            csg.UjEl(0, 1);
            csg.UjEl(0, 2);
            csg.UjEl(1, 2);

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
    }

    [TestClass]
    public class GrafBejarasTesztek
    {
        [TestMethod]
        public void SzelessegiBejarasTeszt() //F3.(a)
        {
            CsucsmatrixSulyozatlanEgeszGraf g = new CsucsmatrixSulyozatlanEgeszGraf(6);
            g.UjEl(0, 1);
            g.UjEl(1, 2);
            g.UjEl(1, 4);
            g.UjEl(2, 3);
            g.UjEl(2, 4);
            g.UjEl(4, 3);
            g.UjEl(3, 0);

            string ut = "";
            Halmaz<int> elertCsucsok = GrafBejarasok.SzelessegiBejaras(g, 0, (a) => { ut += a; });

            Assert.IsTrue(ut == "01243" || ut == "01423");
            for (int i = 0; i <= 4; i++)
                Assert.IsTrue(elertCsucsok.Eleme(i));
            Assert.IsFalse(elertCsucsok.Eleme(6));
        }

        [TestMethod]
        public void MelysegiBejarasTeszt() //F3.(c)
        {
            CsucsmatrixSulyozatlanEgeszGraf g = new CsucsmatrixSulyozatlanEgeszGraf(6);
            g.UjEl(0, 1);
            g.UjEl(1, 2);
            g.UjEl(1, 4);
            g.UjEl(2, 3);
            g.UjEl(2, 4);
            g.UjEl(4, 3);
            g.UjEl(3, 0);

            string ut = "";
            Halmaz<int> elertCsucsok = GrafBejarasok.MelysegiBejaras(g, 0, (a) => { ut += a; });

            Assert.IsTrue(ut == "01243" || ut == "01432" || ut == "01234");
            for (int i = 0; i <= 4; i++)
                Assert.IsTrue(elertCsucsok.Eleme(i));
            Assert.IsFalse(elertCsucsok.Eleme(6));
        }
    }
}
