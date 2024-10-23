using Microsoft.VisualStudio.TestTools.UnitTesting;
using OE.ALGA.Optimalizalas;

namespace OE.ALGA.Tesztek
{
    public class PakolasTesztEsetek //F1.
    {
        public static readonly bool[] uresPakolas = new bool[] { false, false, false, false, false, false };
        public static readonly bool[] feligPakolas = new bool[] { false, true, false, true, false, false };
        public static readonly bool[] teljesPakolas = new bool[] { true, true, true, true, true, true };

        public static readonly int[] jegyzet_w = new int[] { 2, 1, 1, 1, 3, 2 };
        public static readonly float[] jegyzet_p = new float[] { 4, 3, 2, 8, 7, 5 };
        public static readonly int jegyzet_n = jegyzet_w.Length;
        public static readonly int jegyzet_Wmax = 4;
        public static readonly float jegyzet_optimalis_ertek = 16;
        public static readonly bool[] jegyzet_optimalis_pakolas = new bool[] { false, true, false, true, false, true };

        public static readonly int[] nagy_w = new int[] { 21, 41, 26, 11, 37, 25, 25, 44, 33, 29, 32, 52, 41, 62, 56, 81, 43 };
        public static readonly float[] nagy_p = new float[] { 4, 3, 2, 8, 7, 5, 4, 3, 2, 5, 3, 9, 5, 1, 7, 9, 4 };
        public static readonly int nagy_n = nagy_w.Length;
        public static readonly int nagy_Wmax = 100;
        public static readonly float nagy_optimalis_ertek = 24;
    }

    [TestClass()]
    public class HatizsakTesztek
    {

        [TestMethod()]
        public void SulyTeszt() //F1.
        {
            HatizsakProblema problema = new HatizsakProblema(PakolasTesztEsetek.jegyzet_n, PakolasTesztEsetek.jegyzet_Wmax, PakolasTesztEsetek.jegyzet_w, PakolasTesztEsetek.jegyzet_p);
            Assert.AreEqual(0, problema.OsszSuly(PakolasTesztEsetek.uresPakolas));
            Assert.AreEqual(10, problema.OsszSuly(PakolasTesztEsetek.teljesPakolas));
            Assert.AreEqual(2, problema.OsszSuly(PakolasTesztEsetek.feligPakolas));
        }

        [TestMethod()]
        public void JosagTeszt() //F1.
        {
            HatizsakProblema problema = new HatizsakProblema(PakolasTesztEsetek.jegyzet_n, PakolasTesztEsetek.jegyzet_Wmax, PakolasTesztEsetek.jegyzet_w, PakolasTesztEsetek.jegyzet_p);
            Assert.AreEqual(0, problema.OsszErtek(PakolasTesztEsetek.uresPakolas));
            Assert.AreEqual(29, problema.OsszErtek(PakolasTesztEsetek.teljesPakolas));
            Assert.AreEqual(11, problema.OsszErtek(PakolasTesztEsetek.feligPakolas));
        }

        [TestMethod()]
        public void ErvenyesTeszt() //F1.
        {
            HatizsakProblema problema = new HatizsakProblema(PakolasTesztEsetek.jegyzet_n, PakolasTesztEsetek.jegyzet_Wmax, PakolasTesztEsetek.jegyzet_w, PakolasTesztEsetek.jegyzet_p);
            Assert.IsTrue(problema.Ervenyes(PakolasTesztEsetek.uresPakolas));
            Assert.IsFalse(problema.Ervenyes(PakolasTesztEsetek.teljesPakolas));
            Assert.IsTrue(problema.Ervenyes(PakolasTesztEsetek.feligPakolas));
        }
    }

    [TestClass()]
    public class NyersEroTesztek
    {
        [TestMethod()]
        public void TombLegnagyobbEleme() //F2.
        {
            int[] A = { 4, 6, 7, 4, 2, 1 };
            NyersEro<int> opt = new NyersEro<int>(
                A.Length,
                x => A[x - 1],
                x => x);
            Assert.AreEqual(7, opt.OptimalisMegoldas());
        }
    }

    [TestClass()]
    public class NyersEroHatizsakPakolasTesztek
    {
        [TestMethod()]
        public void JegyzetbenLevoPeldaErtekTeszt() //F3.
        {
            HatizsakProblema problema = new HatizsakProblema(PakolasTesztEsetek.jegyzet_n, PakolasTesztEsetek.jegyzet_Wmax, PakolasTesztEsetek.jegyzet_w, PakolasTesztEsetek.jegyzet_p);
            NyersEroHatizsakPakolas opt = new NyersEroHatizsakPakolas(problema);
            Assert.AreEqual(PakolasTesztEsetek.jegyzet_optimalis_ertek, opt.OptimalisErtek());
        }

        [TestMethod()]
        public void JegyzetbenLevoPeldaMegoldasTeszt() //F3.
        {
            HatizsakProblema problema = new HatizsakProblema(PakolasTesztEsetek.jegyzet_n, PakolasTesztEsetek.jegyzet_Wmax, PakolasTesztEsetek.jegyzet_w, PakolasTesztEsetek.jegyzet_p);
            NyersEroHatizsakPakolas opt = new NyersEroHatizsakPakolas(problema);
            CollectionAssert.AreEqual(PakolasTesztEsetek.jegyzet_optimalis_pakolas, opt.OptimalisMegoldas());
        }

        [TestMethod()]
        public void NagyPeldaMegoldasTeszt() //F3.
        {
            HatizsakProblema problema = new HatizsakProblema(PakolasTesztEsetek.nagy_n, PakolasTesztEsetek.nagy_Wmax, PakolasTesztEsetek.nagy_w, PakolasTesztEsetek.nagy_p);
            NyersEroHatizsakPakolas opt = new NyersEroHatizsakPakolas(problema);
            Assert.AreEqual(PakolasTesztEsetek.nagy_optimalis_ertek, opt.OptimalisErtek());
        }

        [TestMethod()]
        public void LepesszamVizsgalat() //F3.
        {
            HatizsakProblema problema = new HatizsakProblema(PakolasTesztEsetek.nagy_n, PakolasTesztEsetek.nagy_Wmax, PakolasTesztEsetek.nagy_w, PakolasTesztEsetek.nagy_p);
            NyersEroHatizsakPakolas opt = new NyersEroHatizsakPakolas(problema);
            Assert.AreEqual(PakolasTesztEsetek.nagy_optimalis_ertek, opt.OptimalisErtek());
            Console.WriteLine("Lépésszám: " + opt.LepesSzam);
        }
    }
}
