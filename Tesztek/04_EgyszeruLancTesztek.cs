using Microsoft.VisualStudio.TestTools.UnitTesting;
using OE.ALGA.Adatszerkezetek;

namespace OE.ALGA.Tesztek
{
    [TestClass()]
    public class LancoltVeremTesztek
    {
        [TestMethod()]
        public void MindenJolMukodik() //F2.
        {
            Verem<int> v = new LancoltVerem<int>();
            v.Verembe(1);
            v.Verembe(3);
            v.Verembe(2);
            Assert.AreEqual(2, v.Verembol());
            v.Verembe(4);
            Assert.AreEqual(4, v.Verembol());
            Assert.AreEqual(3, v.Verembol());
            Assert.AreEqual(1, v.Verembol());
        }

        [TestMethod()]
        [ExpectedException(typeof(NincsElemKivetel))]
        public void TulKevesElem() //F2.
        {
            Verem<int> v = new LancoltVerem<int>();
            v.Verembe(1);
            v.Verembe(3);
            v.Verembe(2);
            v.Verembol();
            v.Verembol();
            v.Verembol();
            v.Verembol();
        }

        [TestMethod()]
        public void Felso() //F2.
        {
            Verem<int> v = new LancoltVerem<int>();
            v.Verembe(1);
            v.Verembe(3);
            v.Verembe(2);
            Assert.AreEqual(2, v.Felso());
            Assert.AreEqual(2, v.Felso());
            v.Verembol();
            Assert.AreEqual(3, v.Felso());
        }

        [TestMethod()]
        public void Ures() //F2.
        {
            Verem<int> v = new LancoltVerem<int>();
            Assert.IsTrue(v.Ures);
            v.Verembe(1);
            Assert.IsFalse(v.Ures);
            v.Verembol();
            Assert.IsTrue(v.Ures);
        }
    }

    [TestClass()]
    public class LancoltSorTesztek
    {
        [TestMethod()]
        public void AlapMukodes() //F3.
        {
            Sor<int> s = new LancoltSor<int>();
            s.Sorba(1);
            s.Sorba(3);
            s.Sorba(2);
            Assert.AreEqual(1, s.Sorbol());
            Assert.AreEqual(3, s.Sorbol());
            Assert.AreEqual(2, s.Sorbol());
        }

        [TestMethod()]
        [ExpectedException(typeof(NincsElemKivetel))]
        public void TulKevesElem() //F3.
        {
            Sor<int> s = new LancoltSor<int>();
            s.Sorba(1);
            s.Sorba(3);
            s.Sorba(2);
            s.Sorbol();
            s.Sorbol();
            s.Sorbol();
            s.Sorbol();
        }

        [TestMethod()]
        public void Elso() //F3.
        {
            Sor<int> s = new LancoltSor<int>();
            s.Sorba(1);
            s.Sorba(3);
            s.Sorba(2);
            Assert.AreEqual(1, s.Elso());
            Assert.AreEqual(1, s.Elso());
            s.Sorbol();
            Assert.AreEqual(3, s.Elso());
        }

        [TestMethod()]
        public void Ures() //F3.
        {
            Sor<int> s = new LancoltSor<int>();
            Assert.IsTrue(s.Ures);
            s.Sorba(1);
            Assert.IsFalse(s.Ures);
            s.Sorbol();
            Assert.IsTrue(s.Ures);
        }
    }

    [TestClass]
    public class LancoltListaTesztek
    {
        [TestMethod]
        public void Bejaras() //F4.
        {
            Lista<int> l = new LancoltLista<int>();
            l.Hozzafuz(1);
            l.Hozzafuz(3);
            l.Hozzafuz(2);
            string s = "";
            l.Bejar(x => s += x.ToString());
            Assert.AreEqual("132", s);
        }

        [TestMethod]
        public void HozzaFuzes() //F4.
        {
            Lista<int> l = new LancoltLista<int>();
            l.Hozzafuz(1);
            l.Hozzafuz(3);
            l.Hozzafuz(2);
            Assert.AreEqual(1, l.Kiolvas(0));
            Assert.AreEqual(3, l.Kiolvas(1));
            Assert.AreEqual(2, l.Kiolvas(2));
        }

        [TestMethod]
        public void Meret() //F4.
        {
            Lista<string> l = new LancoltLista<string>();
            Assert.AreEqual(0, l.Elemszam);
            l.Hozzafuz("A");
            Assert.AreEqual(1, l.Elemszam);
            l.Hozzafuz("B");
            Assert.AreEqual(2, l.Elemszam);
        }

        [TestMethod]
        public void Novekedes() //F4.
        {
            Lista<int> l = new LancoltLista<int>();
            for (int i = 0; i < 1000; i++)
                l.Hozzafuz(i * i);
            for (int i = 0; i < 1000; i++)
                Assert.AreEqual(i * i, l.Kiolvas(i));
        }

        [TestMethod]
        public void Beszuras() //F4.
        {
            Lista<int> l = new LancoltLista<int>();
            l.Beszur(0, 1);
            l.Beszur(0, 2);
            l.Beszur(1, 3);
            l.Beszur(3, 4);
            l.Beszur(2, 5);
            Assert.AreEqual(2, l.Kiolvas(0));
            Assert.AreEqual(3, l.Kiolvas(1));
            Assert.AreEqual(5, l.Kiolvas(2));
            Assert.AreEqual(1, l.Kiolvas(3));
            Assert.AreEqual(4, l.Kiolvas(4));
        }

        [TestMethod]
        public void Torles() //F4.
        {
            Lista<int> l = new LancoltLista<int>();
            l.Hozzafuz(1);
            l.Hozzafuz(3);
            l.Hozzafuz(2);
            l.Hozzafuz(3);
            l.Hozzafuz(4);
            l.Torol(3);
            Assert.AreEqual(1, l.Kiolvas(0));
            Assert.AreEqual(2, l.Kiolvas(1));
            Assert.AreEqual(4, l.Kiolvas(2));
        }

        [TestMethod]
        public void Modositas() //F4.
        {
            Lista<int> l = new LancoltLista<int>();
            l.Hozzafuz(1);
            l.Hozzafuz(3);
            l.Hozzafuz(2);
            l.Modosit(1, 5);
            l.Modosit(0, 4);
            Assert.AreEqual(4, l.Kiolvas(0));
            Assert.AreEqual(5, l.Kiolvas(1));
            Assert.AreEqual(2, l.Kiolvas(2));
        }

        [TestMethod]
        public void ForeachBejaras() //F5.
        {
            LancoltLista<string> l = new LancoltLista<string>();
            l.Hozzafuz("a");
            l.Hozzafuz("c");
            l.Hozzafuz("d");
            l.Hozzafuz("b");
            string osszefuzo = "";
            foreach (string x in l)
            {
                osszefuzo += x;
            }
            Assert.AreEqual("acdb", osszefuzo);
        }
    }
}
