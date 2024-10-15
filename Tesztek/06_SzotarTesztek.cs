using Microsoft.VisualStudio.TestTools.UnitTesting;
using OE.ALGA.Adatszerkezetek;

namespace OE.ALGA.Tesztek
{
    [TestClass()]
    public class SzotarTesztek
    {
        public static int TesztHasitoFuggveny(string kulcs) //F2.(f)
        {
            if (string.IsNullOrEmpty(kulcs))
                return 0;
            int sum = 0;
            foreach (char c in kulcs.ToCharArray())
                sum += ((byte)c);
            return (sum * sum); // a modulo osztást a szótárnak kell végeznie, mert ő tudja csak a belső tömb méretet
        }

        [TestMethod()]
        public void AlapMukodes() //F2.(f)
        {
            Szotar<string, int> sz = new HasitoSzotarTulcsordulasiTerulettel<string, int>(10, TesztHasitoFuggveny);
            sz.Beir("Bela", 5);
            sz.Beir("Lajos", 2);
            Assert.AreEqual(5, sz.Kiolvas("Bela"));
            Assert.AreEqual(2, sz.Kiolvas("Lajos"));
        }

        [TestMethod()]
        public void AlapertelmezettHasitoFuggvennyel() //F2.(f)
        {
            Szotar<string, int> sz = new HasitoSzotarTulcsordulasiTerulettel<string, int>(10);
            sz.Beir("Bela", 5);
            sz.Beir("Lajos", 2);
            Assert.AreEqual(5, sz.Kiolvas("Bela"));
            Assert.AreEqual(2, sz.Kiolvas("Lajos"));
        }

        [TestMethod()]
        public void Kulcsutkozes() //F2.(f)
        {
            Szotar<string, int> sz = new HasitoSzotarTulcsordulasiTerulettel<string, int>(10, TesztHasitoFuggveny);
            sz.Beir("Bela", 5);
            sz.Beir("Bale", 15);
            sz.Beir("Lajos", 2);
            sz.Beir("Lasoj", 12);
            Assert.AreEqual(5, sz.Kiolvas("Bela"));
            Assert.AreEqual(2, sz.Kiolvas("Lajos"));
            Assert.AreEqual(15, sz.Kiolvas("Bale"));
            Assert.AreEqual(12, sz.Kiolvas("Lasoj"));
        }

        [TestMethod()]
        [ExpectedException(typeof(HibasKulcsKivetel))]
        public void NincsElem() //F2.(f)
        {
            Szotar<string, int> sz = new HasitoSzotarTulcsordulasiTerulettel<string, int>(5, TesztHasitoFuggveny);
            sz.Beir("Bela", 5);
            sz.Beir("Lajos", 2);
            sz.Kiolvas("Ferenc");
        }

        [TestMethod()]
        public void TorlesMarad() //F2.(g)
        {
            Szotar<string, int> sz = new HasitoSzotarTulcsordulasiTerulettel<string, int>(5, TesztHasitoFuggveny);
            sz.Beir("Bela", 5);
            sz.Beir("Lajos", 2);
            sz.Torol("Bela");
            Assert.AreEqual(2, sz.Kiolvas("Lajos"));
        }

        [TestMethod()]
        [ExpectedException(typeof(HibasKulcsKivetel))]
        public void TorlesEltunt() //F2.(g)
        {
            Szotar<string, int> sz = new HasitoSzotarTulcsordulasiTerulettel<string, int>(5, TesztHasitoFuggveny);
            sz.Beir("Bela", 5);
            sz.Beir("Lajos", 2);
            sz.Torol("Bela");
            sz.Kiolvas("Bela");
        }
    }

}
