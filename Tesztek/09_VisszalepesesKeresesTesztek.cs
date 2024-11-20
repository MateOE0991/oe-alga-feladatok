using Microsoft.VisualStudio.TestTools.UnitTesting;
using OE.ALGA.Optimalizalas;

namespace OE.ALGA.Tesztek
{
    [TestClass()]
    public class VisszalepesesKeresesTesztek
    {
        [TestMethod()]
        public void JegyzetbenLevoPeldaErtekTeszt() //F2.
        {
            HatizsakProblema problema = new HatizsakProblema(PakolasTesztEsetek.jegyzet_n, PakolasTesztEsetek.jegyzet_Wmax, PakolasTesztEsetek.jegyzet_w, PakolasTesztEsetek.jegyzet_p);
            VisszalepesesHatizsakPakolas opt = new VisszalepesesHatizsakPakolas(problema);
            Assert.AreEqual(PakolasTesztEsetek.jegyzet_optimalis_ertek, opt.OptimalisErtek());
        }

        [TestMethod()]
        public void JegyzetbenLevoPeldaMegoldasTeszt() //F2.
        {
            HatizsakProblema problema = new HatizsakProblema(PakolasTesztEsetek.jegyzet_n, PakolasTesztEsetek.jegyzet_Wmax, PakolasTesztEsetek.jegyzet_w, PakolasTesztEsetek.jegyzet_p);
            VisszalepesesHatizsakPakolas opt = new VisszalepesesHatizsakPakolas(problema);
            CollectionAssert.AreEqual(PakolasTesztEsetek.jegyzet_optimalis_pakolas, opt.OptimalisMegoldas());
        }

        [TestMethod()]
        public void NagyPeldaMegoldasTeszt() //F2.
        {
            HatizsakProblema problema = new HatizsakProblema(PakolasTesztEsetek.nagy_n, PakolasTesztEsetek.nagy_Wmax, PakolasTesztEsetek.nagy_w, PakolasTesztEsetek.nagy_p);
            VisszalepesesHatizsakPakolas opt = new VisszalepesesHatizsakPakolas(problema);
            Assert.AreEqual(PakolasTesztEsetek.nagy_optimalis_ertek, opt.OptimalisErtek());
        }

        [TestMethod()]
        public void LepesszamVizsgalat() //F2.
        {
            HatizsakProblema problema = new HatizsakProblema(PakolasTesztEsetek.nagy_n, PakolasTesztEsetek.nagy_Wmax, PakolasTesztEsetek.nagy_w, PakolasTesztEsetek.nagy_p);
            VisszalepesesHatizsakPakolas opt = new VisszalepesesHatizsakPakolas(problema);
            Assert.AreEqual(PakolasTesztEsetek.nagy_optimalis_ertek, opt.OptimalisErtek());
            Console.WriteLine("Lépésszám: " + opt.LepesSzam);
        }
    }

    [TestClass()]
    public class SzetvalasztasEsKorlatozasTesztek
    {
        [TestMethod()]
        public void JegyzetbenLevoPeldaErtekTeszt() //F4.
        {
            HatizsakProblema problema = new HatizsakProblema(PakolasTesztEsetek.jegyzet_n, PakolasTesztEsetek.jegyzet_Wmax, PakolasTesztEsetek.jegyzet_w, PakolasTesztEsetek.jegyzet_p);
            SzetvalasztasEsKorlatozasHatizsakPakolas opt = new SzetvalasztasEsKorlatozasHatizsakPakolas(problema);
            Assert.AreEqual(PakolasTesztEsetek.jegyzet_optimalis_ertek, opt.OptimalisErtek());
        }

        [TestMethod()]
        public void JegyzetbenLevoPeldaMegoldasTeszt() //F4.
        {
            HatizsakProblema problema = new HatizsakProblema(PakolasTesztEsetek.jegyzet_n, PakolasTesztEsetek.jegyzet_Wmax, PakolasTesztEsetek.jegyzet_w, PakolasTesztEsetek.jegyzet_p);
            SzetvalasztasEsKorlatozasHatizsakPakolas opt = new SzetvalasztasEsKorlatozasHatizsakPakolas(problema);
            CollectionAssert.AreEqual(PakolasTesztEsetek.jegyzet_optimalis_pakolas, opt.OptimalisMegoldas());
        }

        [TestMethod()]
        public void NagyPeldaMegoldasTeszt() //F4.
        {
            HatizsakProblema problema = new HatizsakProblema(PakolasTesztEsetek.nagy_n, PakolasTesztEsetek.nagy_Wmax, PakolasTesztEsetek.nagy_w, PakolasTesztEsetek.nagy_p);
            SzetvalasztasEsKorlatozasHatizsakPakolas opt = new SzetvalasztasEsKorlatozasHatizsakPakolas(problema);
            Assert.AreEqual(PakolasTesztEsetek.nagy_optimalis_ertek, opt.OptimalisErtek());
        }

        [TestMethod()]
        public void LepesszamVizsgalat() //F4.
        {
            HatizsakProblema problema = new HatizsakProblema(PakolasTesztEsetek.nagy_n, PakolasTesztEsetek.nagy_Wmax, PakolasTesztEsetek.nagy_w, PakolasTesztEsetek.nagy_p);
            SzetvalasztasEsKorlatozasHatizsakPakolas opt = new SzetvalasztasEsKorlatozasHatizsakPakolas(problema);
            Assert.AreEqual(PakolasTesztEsetek.nagy_optimalis_ertek, opt.OptimalisErtek());
            Console.WriteLine("Lépésszám: " + opt.LepesSzam);
        }
    }
}
