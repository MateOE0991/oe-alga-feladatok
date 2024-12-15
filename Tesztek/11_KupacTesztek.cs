using Microsoft.VisualStudio.TestTools.UnitTesting;
using OE.ALGA.Adatszerkezetek;

namespace OE.ALGA.Tesztek
{
    [TestClass()]
    public class KupacRendezesTesztek
    {
        [TestMethod()]
        public void KupacEpites() //F1.
        {
            int[] A = [1, 3, 2, 4, 9, 12, 32, 21, 12, 8, 11];
            _ = new Kupac<int>(A, A.Length, (x, y) => x > y);

            for (int i = 1; i < A.Length; i++)
                Assert.IsTrue(A[Kupac<int>.Szulo(i)] >= A[i]);
        }

        [TestMethod()]
        public void KupacRendezes() //F2.
        {
            int[] A = [5, 8, 7, 0, 9, 6, 4, 1, 3, 2];
            KupacRendezes<int> k = new KupacRendezes<int>(A);
            k.Rendezes();

            for (int i = 0; i < A.Length; i++)
                Assert.AreEqual(i, A[i]);
        }
    }

    [TestClass()]
    public class KupacPrioritasosSorTesztek
    {
        [TestMethod()]
        [ExpectedException(typeof(NincsHelyKivetel))]
        public void TulSokElemTeszt() //F3.
        {
            PrioritasosSor<int> s = new KupacPrioritasosSor<int>(2, (x, y) => x > y);
            s.Sorba(1);
            s.Sorba(2);
            s.Sorba(3);
        }

        [TestMethod()]
        [ExpectedException(typeof(NincsElemKivetel))]
        public void TulKevesElemTeszt() //F3.
        {
            PrioritasosSor<int> s = new KupacPrioritasosSor<int>(5, (x, y) => x > y);
            s.Sorba(1);
            s.Sorba(2);
            s.Sorba(3);
            s.Sorbol();
            s.Sorbol();
            s.Sorbol();
            s.Sorbol();
        }

        [TestMethod()]
        public void UresTeszt() //F3.
        {
            PrioritasosSor<int> s = new KupacPrioritasosSor<int>(5, (x, y) => x > y);
            Assert.IsTrue(s.Ures);
            s.Sorba(1);
            Assert.IsFalse(s.Ures);
            s.Sorba(2);
            Assert.IsFalse(s.Ures);
            s.Sorbol();
            Assert.IsFalse(s.Ures);
            s.Elso();
            Assert.IsFalse(s.Ures);
            s.Sorbol();
            Assert.IsTrue(s.Ures);
        }

        [TestMethod()]
        public void SorbaSorbolElsoTeszt() //F3.
        {
            PrioritasosSor<int> s = new KupacPrioritasosSor<int>(10, (x, y) => x > y);
            s.Sorba(1);
            s.Sorba(4);
            Assert.AreEqual(4, s.Elso());
            Assert.AreEqual(4, s.Sorbol());
            Assert.AreEqual(1, s.Elso());
            s.Sorba(4);
            s.Sorba(2);
            s.Sorba(8);
            s.Sorba(3);
            Assert.AreEqual(8, s.Elso());
            s.Sorba(9);
            s.Sorba(5);
            Assert.AreEqual(9, s.Elso());
            Assert.AreEqual(9, s.Elso());
            Assert.AreEqual(9, s.Sorbol());
            Assert.AreEqual(8, s.Elso());
            s.Sorba(7);
            Assert.AreEqual(8, s.Sorbol());
            Assert.AreEqual(7, s.Sorbol());
            Assert.AreEqual(5, s.Sorbol());
            s.Sorba(2);
            Assert.AreEqual(4, s.Sorbol());
            Assert.AreEqual(3, s.Sorbol());
            Assert.AreEqual(2, s.Sorbol());
            Assert.AreEqual(2, s.Sorbol());
            Assert.AreEqual(1, s.Elso());
            Assert.AreEqual(1, s.Sorbol());
        }

        [TestClass()]
        class PrioritasosSzoveg : IComparable //F3.
        {
            public string Szoveg { get; set; }
            public float Prioritas { get; set; }
            public PrioritasosSzoveg(string szoveg, float prioritas)
            {
                this.Szoveg = szoveg;
                this.Prioritas = prioritas;
            }
            
            public int CompareTo(object? obj)
            {
                if (obj is not PrioritasosSzoveg o)
                    throw new NullReferenceException();
                else
                    return Prioritas.CompareTo(o.Prioritas);
            }
        }

        [TestMethod()]
        public void PrioritasValtozasTeszt() //F3.
        {
            PrioritasosSzoveg a = new PrioritasosSzoveg("a", 10.0f);
            PrioritasosSzoveg b = new PrioritasosSzoveg("b", 5.0f);
            PrioritasosSzoveg c = new PrioritasosSzoveg("c", 2.0f);
            PrioritasosSzoveg d = new PrioritasosSzoveg("d", 12.0f);
            PrioritasosSzoveg e = new PrioritasosSzoveg("e", 15.0f);
            PrioritasosSzoveg f = new PrioritasosSzoveg("f", 9.0f);
            PrioritasosSzoveg g = new PrioritasosSzoveg("g", 2.0f);
            PrioritasosSor<PrioritasosSzoveg> s = new KupacPrioritasosSor<PrioritasosSzoveg>(10, (x, y) => x.CompareTo(y) > 0);
            s.Sorba(a);
            s.Sorba(b);
            s.Sorba(c);
            s.Sorba(d);
            s.Sorba(e);
            Assert.AreEqual("e", s.Elso().Szoveg);
            d.Prioritas = 22.0f;
            s.Frissit(d);
            Assert.AreEqual("d", s.Elso().Szoveg);
            d.Prioritas = 8.0f;
            s.Frissit(d);
            e.Prioritas = 7.0f;
            s.Frissit(e);
            Assert.AreEqual("a", s.Sorbol().Szoveg);
            s.Sorba(f);
            s.Sorba(g);
            Assert.AreEqual("f", s.Sorbol().Szoveg);
            Assert.AreEqual("d", s.Sorbol().Szoveg);
            Assert.AreEqual("e", s.Sorbol().Szoveg);
            Assert.AreEqual("b", s.Sorbol().Szoveg);
            c.Prioritas = 1.5f;
            s.Frissit(c);
            Assert.AreEqual("g", s.Sorbol().Szoveg);
            Assert.AreEqual("c", s.Sorbol().Szoveg);
            Assert.IsTrue(s.Ures);
        }
    }

    [TestClass()]
    public class KupacTesztekKulsoFuggvennyel //F3.
    {
        /// <summary>
        /// Nincs külön rendező függvény, ezért ABC sorrendben rendez az IComparable alapján.
        /// </summary>
        [TestMethod()]
        public void KupacEpitesIComparableAlapjan()
        {
            KupacPrioritasosSor<string> ps = new KupacPrioritasosSor<string>(10, (x, y) => x.CompareTo(y) > 0);
            ps.Sorba("oszibarack");
            ps.Sorba("alma");
            ps.Sorba("korte");
            ps.Sorba("birsalma");
            ps.Sorba("barack");
            ps.Sorba("dio");
            Assert.AreEqual("oszibarack", ps.Sorbol());
            Assert.AreEqual("korte", ps.Sorbol());
            Assert.AreEqual("dio", ps.Sorbol());
            Assert.AreEqual("birsalma", ps.Sorbol());
            Assert.AreEqual("barack", ps.Sorbol());
            Assert.AreEqual("alma", ps.Sorbol());
        }

        /// <summary>
        /// Van egy saját hossz alapú rendező függvény, ezért elsőként a leghosszabb stringeket adja vissza.
        /// </summary>
        [TestMethod()]
        public void KupacEpitesSajatFuggvennyel() //F3.
        {
            KupacPrioritasosSor<string> ps = new KupacPrioritasosSor<string>(10, (ez, ennel) => ez.Length > ennel.Length);
            ps.Sorba("oszibarack");
            ps.Sorba("alma");
            ps.Sorba("korte");
            ps.Sorba("birsalma");
            ps.Sorba("barack");
            ps.Sorba("dio");
            Assert.AreEqual("oszibarack", ps.Sorbol());
            Assert.AreEqual("birsalma", ps.Sorbol());
            Assert.AreEqual("barack", ps.Sorbol());
            Assert.AreEqual("korte", ps.Sorbol());
            Assert.AreEqual("alma", ps.Sorbol());
            Assert.AreEqual("dio", ps.Sorbol());
        }
    }
}
