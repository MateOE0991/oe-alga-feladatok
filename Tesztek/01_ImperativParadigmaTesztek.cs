using Microsoft.VisualStudio.TestTools.UnitTesting;
using OE.ALGA.Paradigmak;

namespace OE.ALGA.Tesztek
{
    class TesztFeladat : IVegrehajthato //F1.
    {
        public string Azonosito { get; set; }

        public bool Vegrehajtott { get; set; }

        public void Vegrehajtas()
        {
            Vegrehajtott = true;
        }

        public TesztFeladat(string nev)
        {
            this.Azonosito = nev;
        }
    }

    [TestClass()]
    public class FeladatTaroloTeszt
    {
        [TestMethod()]
        public void FelveszTeszt() //F2.(c)
        {
            FeladatTarolo<TesztFeladat> tarolo = new FeladatTarolo<TesztFeladat>(5);
            TesztFeladat a = new TesztFeladat("a");
            tarolo.Felvesz(a);
            tarolo.Felvesz(a);
            tarolo.Felvesz(a);
            tarolo.Felvesz(a);
            tarolo.Felvesz(a);
        }

        [TestMethod()]
        [ExpectedException(typeof(TaroloMegteltKivetel))]
        public void TulsokatFelveszTeszt() //F2.(c)
        {
            FeladatTarolo<TesztFeladat> tarolo = new FeladatTarolo<TesztFeladat>(5);
            TesztFeladat a = new TesztFeladat("a");
            tarolo.Felvesz(a);
            tarolo.Felvesz(a);
            tarolo.Felvesz(a);
            tarolo.Felvesz(a);
            tarolo.Felvesz(a);
            tarolo.Felvesz(a);
        }

        [TestMethod()]
        public void VegrehajtasTeszt() //F2.(d)
        {
            FeladatTarolo<TesztFeladat> tarolo = new FeladatTarolo<TesztFeladat>(10);
            TesztFeladat a = new TesztFeladat("a");
            TesztFeladat b = new TesztFeladat("b");
            tarolo.Felvesz(a);
            tarolo.Felvesz(b);
            Assert.IsFalse(a.Vegrehajtott);
            Assert.IsFalse(b.Vegrehajtott);
            tarolo.MindentVegrehajt();
            Assert.IsTrue(a.Vegrehajtott);
            Assert.IsTrue(b.Vegrehajtott);
        }
    }

    class TesztFuggoFeladat : TesztFeladat, IFuggo //F3.
    {
        public bool Vegrehajthato { get; set; }

        public virtual bool FuggosegTeljesul
        {
            get
            {
                return Vegrehajthato;
            }
        }

        public TesztFuggoFeladat(string nev) : base(nev)
        {
        }
    }

    [TestClass()]
    public class FuggoFeladatTaroloTeszt //F4.
    {
        [TestMethod()]
        public void FelveszTeszt()
        {
            FuggoFeladatTarolo<TesztFuggoFeladat> tarolo = new FuggoFeladatTarolo<TesztFuggoFeladat>(5);
            TesztFuggoFeladat a = new TesztFuggoFeladat("a");
            tarolo.Felvesz(a);
            tarolo.Felvesz(a);
            tarolo.Felvesz(a);
            tarolo.Felvesz(a);
            tarolo.Felvesz(a);
        }

        [TestMethod()]
        [ExpectedException(typeof(TaroloMegteltKivetel))]
        public void TulsokatFelveszTeszt()
        {
            FuggoFeladatTarolo<TesztFuggoFeladat> tarolo = new FuggoFeladatTarolo<TesztFuggoFeladat>(5);
            TesztFuggoFeladat a = new TesztFuggoFeladat("a");
            tarolo.Felvesz(a);
            tarolo.Felvesz(a);
            tarolo.Felvesz(a);
            tarolo.Felvesz(a);
            tarolo.Felvesz(a);
            tarolo.Felvesz(a);
        }

        [TestMethod()]
        public void VegrehajtasAmikorNemVegrehajthatokTeszt()
        {
            FuggoFeladatTarolo<TesztFuggoFeladat> tarolo = new FuggoFeladatTarolo<TesztFuggoFeladat>(5);
            TesztFuggoFeladat a = new TesztFuggoFeladat("a");
            TesztFuggoFeladat b = new TesztFuggoFeladat("b");
            tarolo.Felvesz(a);
            tarolo.Felvesz(b);
            Assert.IsFalse(a.Vegrehajtott);
            Assert.IsFalse(b.Vegrehajtott);
            tarolo.MindentVegrehajt();
            Assert.IsFalse(a.Vegrehajtott);
            Assert.IsFalse(b.Vegrehajtott);
        }

        [TestMethod()]
        public void VegrehajtasAmikorVegrehajthatokTeszt()
        {
            FuggoFeladatTarolo<TesztFuggoFeladat> tarolo = new FuggoFeladatTarolo<TesztFuggoFeladat>(5);
            TesztFuggoFeladat a = new TesztFuggoFeladat("a") { Vegrehajthato = true };
            TesztFuggoFeladat b = new TesztFuggoFeladat("b");
            tarolo.Felvesz(a);
            tarolo.Felvesz(b);
            Assert.IsFalse(a.Vegrehajtott);
            Assert.IsFalse(b.Vegrehajtott);
            tarolo.MindentVegrehajt();
            Assert.IsTrue(a.Vegrehajtott);
            Assert.IsFalse(b.Vegrehajtott);
            b.Vegrehajthato = true;
            tarolo.MindentVegrehajt();
            Assert.IsTrue(a.Vegrehajtott);
            Assert.IsTrue(b.Vegrehajtott);
        }
    }

    class TesztElokovetelmenytolFuggoFeladat : TesztFuggoFeladat //F4.
    {
        readonly TesztFeladat elokovetelmeny;

        public TesztElokovetelmenytolFuggoFeladat(string nev, TesztFeladat elokovetelmeny) : base(nev)
        {
            this.elokovetelmeny = elokovetelmeny;
        }

        public override bool FuggosegTeljesul
        {
            get
            {
                return base.FuggosegTeljesul && elokovetelmeny.Vegrehajtott;
            }
        }
    }

    [TestClass()]
    public class FuggoFeladatTaroloElokovetelmenyekkelTeszt //F4.
    {
        [TestMethod()]
        public void ElokovetelmenyesTeszt()
        {
            FuggoFeladatTarolo<TesztFuggoFeladat> tarolo = new FuggoFeladatTarolo<TesztFuggoFeladat>(5);
            TesztFuggoFeladat a = new TesztFuggoFeladat("a");
            TesztElokovetelmenytolFuggoFeladat b = new TesztElokovetelmenytolFuggoFeladat("b", a) { Vegrehajthato = true };
            // a->b
            tarolo.Felvesz(a);
            tarolo.Felvesz(b);
            tarolo.MindentVegrehajt();
            Assert.IsFalse(a.Vegrehajtott);
            Assert.IsFalse(b.Vegrehajtott);
            a.Vegrehajthato = true;
            tarolo.MindentVegrehajt();
            Assert.IsTrue(a.Vegrehajtott);
            Assert.IsTrue(b.Vegrehajtott);
        }

        [TestMethod()]
        public void TobbKorosElokovetelmenyesTeszt()
        {
            FuggoFeladatTarolo<TesztFuggoFeladat> tarolo = new FuggoFeladatTarolo<TesztFuggoFeladat>(5);
            TesztFuggoFeladat a = new TesztFuggoFeladat("a") { Vegrehajthato = true };
            TesztElokovetelmenytolFuggoFeladat b = new TesztElokovetelmenytolFuggoFeladat("b", a) { Vegrehajthato = true };
            TesztElokovetelmenytolFuggoFeladat c = new TesztElokovetelmenytolFuggoFeladat("c", a) { Vegrehajthato = true };
            TesztElokovetelmenytolFuggoFeladat d = new TesztElokovetelmenytolFuggoFeladat("d", b) { Vegrehajthato = true };
            // a->b->d
            //  ->c
            tarolo.Felvesz(d);
            tarolo.Felvesz(c);
            tarolo.Felvesz(b);
            tarolo.Felvesz(a);
            tarolo.MindentVegrehajt();
            Assert.IsTrue(a.Vegrehajtott);
            Assert.IsFalse(b.Vegrehajtott); // a 'b' kiértékelése az 'a' végrehajtása előtt volt, ezért az még nem lett feldolgozva
            Assert.IsFalse(c.Vegrehajtott); // a 'c' kiértékelése az 'a' végrehajtása előtt volt, ezért az még nem lett feldolgozva
            Assert.IsFalse(d.Vegrehajtott);
            tarolo.MindentVegrehajt();
            Assert.IsTrue(a.Vegrehajtott);
            Assert.IsTrue(b.Vegrehajtott);
            Assert.IsTrue(c.Vegrehajtott);
            Assert.IsFalse(d.Vegrehajtott); // a 'd' kiértékelése a 'b' végrehajtása előtt volt, ezért az még nem lett feldolgozva
            tarolo.MindentVegrehajt();
            Assert.IsTrue(a.Vegrehajtott);
            Assert.IsTrue(b.Vegrehajtott);
            Assert.IsTrue(c.Vegrehajtott);
            Assert.IsTrue(d.Vegrehajtott);
        }
    }

    class BejarasokTeszt //F5.
    {
        [TestMethod()]
        public void FeladatTaroloBejaroTeszt()
        {
            FeladatTarolo<TesztFeladat> tarolo = new FeladatTarolo<TesztFeladat>(10);
            TesztFeladat a = new TesztFeladat("a");
            TesztFeladat b = new TesztFeladat("b");
            tarolo.Felvesz(a);
            tarolo.Felvesz(b);
            string nevek = "";
            foreach (TesztFeladat u in tarolo)
            {
                nevek += u.Azonosito;
            }
            Assert.AreEqual("ab", nevek);
        }

        [TestMethod()]
        public void FuggoFeladatTaroloBejaroTeszt()
        {
            FuggoFeladatTarolo<TesztFuggoFeladat> tarolo = new FuggoFeladatTarolo<TesztFuggoFeladat>(5);
            TesztFuggoFeladat a = new TesztFuggoFeladat("a");
            TesztFuggoFeladat b = new TesztFuggoFeladat("b") { Vegrehajthato = true };
            tarolo.Felvesz(a);
            tarolo.Felvesz(b);
            tarolo.MindentVegrehajt();
            string nevek = "";
            foreach (TesztFuggoFeladat u in tarolo)
            {
                if (u.Vegrehajtott)
                    nevek += u.Azonosito;
            }
            Assert.AreEqual("b", nevek);
        }

    }
}
