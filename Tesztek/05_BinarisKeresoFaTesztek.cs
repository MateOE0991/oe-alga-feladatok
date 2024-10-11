using Microsoft.VisualStudio.TestTools.UnitTesting;
using OE.ALGA.Adatszerkezetek;

namespace OE.ALGA.Tesztek
{
    [TestClass()]
    public class FaHalmazTesztek
    {
        [TestMethod()]
        public void Beszuras() //F4.
        {
            Halmaz<int> v = new FaHalmaz<int>();
            v.Beszur(1);
            v.Beszur(3);
            v.Beszur(2);
            Assert.IsTrue(v.Eleme(1));
            Assert.IsTrue(v.Eleme(2));
            Assert.IsTrue(v.Eleme(3));
            Assert.IsFalse(v.Eleme(4));
        }

        [TestMethod()]
        public void Torles() //F5.
        {
            Halmaz<int> v = new FaHalmaz<int>();
            v.Beszur(1);
            v.Beszur(3);
            v.Beszur(2);
            v.Torol(2);
            Assert.IsTrue(v.Eleme(1));
            Assert.IsFalse(v.Eleme(2));
            Assert.IsTrue(v.Eleme(3));
            Assert.IsFalse(v.Eleme(4));
        }

        [TestMethod()]
        public void DuplaBeszuras() //F5.
        {
            Halmaz<int> v = new FaHalmaz<int>();
            v.Beszur(1);
            v.Beszur(2);
            v.Beszur(3);
            v.Beszur(2);
            v.Torol(2);
            Assert.IsTrue(v.Eleme(1));
            Assert.IsFalse(v.Eleme(2));
            Assert.IsTrue(v.Eleme(3));
            Assert.IsFalse(v.Eleme(4));
        }

        [TestMethod()]
        public void PreorderBejaras() //F6.
        {
            Halmaz<int> v = new FaHalmaz<int>();
            v.Beszur(5);
            v.Beszur(3);
            v.Beszur(1);
            v.Beszur(8);
            v.Beszur(4);
            v.Beszur(9);
            v.Beszur(7);
            string osszefuzo = "";
            v.Bejar(x => osszefuzo += x);
            Assert.AreEqual("5314879", osszefuzo);
        }

    }
}
