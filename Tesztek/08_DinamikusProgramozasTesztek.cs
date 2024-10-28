using Microsoft.VisualStudio.TestTools.UnitTesting;
using OE.ALGA.Optimalizalas;

namespace OE.ALGA.Tesztek
{
    [TestClass()]
    public class DinamikusProgramozasTesztek
    {
        [TestMethod()]
        public void JegyzetbenLevoPeldaErtekTeszt() //F1.
        {
            HatizsakProblema problema = new HatizsakProblema(PakolasTesztEsetek.jegyzet_n, PakolasTesztEsetek.jegyzet_Wmax, PakolasTesztEsetek.jegyzet_w, PakolasTesztEsetek.jegyzet_p);
            DinamikusHatizsakPakolas opt = new DinamikusHatizsakPakolas(problema);
            Assert.AreEqual(PakolasTesztEsetek.jegyzet_optimalis_ertek, opt.OptimalisErtek());
        }

        [TestMethod()]
        public void JegyzetbenLevoPeldaMegoldasTeszt() //F1.
        {
            HatizsakProblema problema = new HatizsakProblema(PakolasTesztEsetek.jegyzet_n, PakolasTesztEsetek.jegyzet_Wmax, PakolasTesztEsetek.jegyzet_w, PakolasTesztEsetek.jegyzet_p);
            DinamikusHatizsakPakolas opt = new DinamikusHatizsakPakolas(problema);
            CollectionAssert.AreEqual(PakolasTesztEsetek.jegyzet_optimalis_pakolas, opt.OptimalisMegoldas());
        }

        [TestMethod()]
        public void NagyPeldaMegoldasTeszt() //F1.
        {
            HatizsakProblema problema = new HatizsakProblema(PakolasTesztEsetek.nagy_n, PakolasTesztEsetek.nagy_Wmax, PakolasTesztEsetek.nagy_w, PakolasTesztEsetek.nagy_p);
            DinamikusHatizsakPakolas opt = new DinamikusHatizsakPakolas(problema);
            Assert.AreEqual(PakolasTesztEsetek.nagy_optimalis_ertek, opt.OptimalisErtek());
        }

        [TestMethod()]
        public void LepesszamVizsgalat() //F1.
        {
            HatizsakProblema problema = new HatizsakProblema(PakolasTesztEsetek.nagy_n, PakolasTesztEsetek.nagy_Wmax, PakolasTesztEsetek.nagy_w, PakolasTesztEsetek.nagy_p);
            DinamikusHatizsakPakolas opt = new DinamikusHatizsakPakolas(problema);
            Assert.AreEqual(PakolasTesztEsetek.nagy_optimalis_ertek, opt.OptimalisErtek());
            Console.WriteLine("Lépésszám: " + opt.LepesSzam);
        }
    }
}
