using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OE.ALGA.Adatszerkezetek
{
    //public class SzotarElem<K, T>
    //{
    //    public K Kulcs;
    //    public T Tart;

    //    public SzotarElem(K kulcs, T tart)
    //    {
    //        Kulcs = kulcs;
    //        Tart = tart;
    //    }
    //}

    //public class HasitoSzotarTulcsordulasiTerulettel<K, T> : Szotar<K, T>
    //{
    //    SzotarElem<K, T>[] E;
    //    Func<K, int> h;
    //    private LancoltLista<SzotarElem<K, T>> U;

    //    public HasitoSzotarTulcsordulasiTerulettel(int meret, Func<K, int> hasitoFuggveny)
    //    {
    //        E = new SzotarElem<K, T>[meret];
    //        h = x => hasitoFuggveny(x) % E.Length;
    //        U = new LancoltLista<SzotarElem<K, T>>();
    //    }

    //    public HasitoSzotarTulcsordulasiTerulettel(int meret) : this(meret, kulcs => kulcs.GetHashCode())
    //    {
    //    }

    //    private SzotarElem<K, T> KulcsKeres(K kulcs)
    //    {
    //        SzotarElem<K, T> e = null;

    //        if (E[h(kulcs)] != null && EqualityComparer<K>.Default.Equals(E[h(kulcs)].Kulcs, kulcs))
    //        {
    //            return E[h(kulcs)];
    //        }
    //        if (U != null)  // Ellenőrizzük, hogy létezik-e a láncolt lista
    //        {
    //            U.Bejar(x =>
    //            {
    //                if (EqualityComparer<K>.Default.Equals(x.Kulcs, kulcs))
    //                {
    //                    e = x;  // Ha megtaláltuk, elmentjük a talált elemet
    //                }
    //            });
    //        }
    //        return e;
    //    }

    //    public void Beir(K kulcs, T ertek)
    //    {
    //        SzotarElem<K, T> x = KulcsKeres(kulcs);

    //        if (x != null)
    //        {
    //            E[h(kulcs)].Tart = ertek;
    //        }

    //        if (E[h(kulcs)] == null)
    //        {
    //            E[h(kulcs)] = new SzotarElem<K, T>(kulcs, ertek);
    //        }
    //        else { U.Hozzafuz(new SzotarElem<K, T>(kulcs, ertek)); }
    //    }

    //    public T Kiolvas(K kulcs)
    //    {
    //        SzotarElem<K, T> x = KulcsKeres(kulcs);
    //        if (x != null)
    //        {
    //            return x.Tart;
    //        }
    //        else throw new HibasKulcsKivetel();

    //    }

    //    public void Torol(K kulcs)
    //    {
    //        if (E[h(kulcs)] != null && EqualityComparer<K>.Default.Equals(E[h(kulcs)].Kulcs, kulcs))
    //            E[h(kulcs)] = null;
    //        else
    //        {
    //            U.Bejar(x =>
    //            {
    //                if (EqualityComparer<K>.Default.Equals(x.Kulcs, kulcs))
    //                {
    //                    U.Torol(x);
    //                }
    //            });
    //        }
    //    }

    //}

    // SzotarElem<K, T> osztaly
    public class SzotarElem<K, T>
    {
        public K kulcs; // Publikus mezo a kulcs tarolasahoz
        public T tart;  // Publikus mezo a tartalom tarolasahoz

        // Konstruktor a kulcs es tartalom beallitasahoz
        public SzotarElem(K kulcs, T tart)
        {
            this.kulcs = kulcs;
            this.tart = tart;
        }
    }

    // HasitoSzotarTulcsordulasiTerulettel<K, T> osztaly
    public class HasitoSzotarTulcsordulasiTerulettel<K, T> : Szotar<K, T>
    {
        private SzotarElem<K, T>[] E; // Adattarolo tomb
        private Func<K, int> h; // Hasito fuggveny
        private LancoltLista<SzotarElem<K, T>> U; // Tulcsordulasi terulet

        // Konstruktor meret es hasito fuggveny megadasaval
        public HasitoSzotarTulcsordulasiTerulettel(int meret, Func<K, int> hasitoFuggveny)
        {
            E = new SzotarElem<K, T>[meret];
            U = new LancoltLista<SzotarElem<K, T>>();

            // Kompozit hasito fuggveny maradekos osztassal
            h = kulcs => Math.Abs(hasitoFuggveny(kulcs)) % E.Length;
        }

        // Konstruktor csak meret megadasaval (alapertelmezett hasitofuggveny hasznalata)
        public HasitoSzotarTulcsordulasiTerulettel(int meret)
            : this(meret, kulcs => kulcs.GetHashCode())
        {
        }

        // Privat kulcskereses metodus
        private SzotarElem<K, T> KulcsKeres(K kulcs)
        {
            int index = h(kulcs);
            if (E[index] != null && EqualityComparer<K>.Default.Equals(E[index].kulcs, kulcs))
            {
                return E[index];
            }

            // Kereses a lancolt listaban
            SzotarElem<K, T> x = null;

            U.Bejar(elem =>
            {
                if (EqualityComparer<K>.Default.Equals(elem.kulcs, kulcs))
                {
                    x = elem;
                }

            });
            return x; // Ha nincs meg a kulcs
        }

        // Beir metodus
        public void Beir(K kulcs, T ertek)
        {
            var meglevoElem = KulcsKeres(kulcs);
            if (meglevoElem != null)
            {
                meglevoElem.tart = ertek; // Ha van mar ilyen kulcs, frissitjuk az erteket
                return;
            }

            var ujElem = new SzotarElem<K, T>(kulcs, ertek);
            int index = h(kulcs);
            if (E[index] == null)
            {
                E[index] = ujElem; // Ures helyre irunk
            }
            else
            {
                U.Hozzafuz(ujElem); // Tulcsordulasi teruletre fuzunk
            }
        }

        // Kiolvas metodus
        public T Kiolvas(K kulcs)
        {
            var elem = KulcsKeres(kulcs);
            if (elem != null)
            {
                return elem.tart; // Ha van ilyen kulcs, visszaadjuk az erteket
            }

            throw new HibasKulcsKivetel(); // Ha nincs meg, kivetelt dobunk
        }

        // Torol metodus
        public void Torol(K kulcs)
        {
            int index = h(kulcs);
            if (E[index] != null && EqualityComparer<K>.Default.Equals(E[index].kulcs, kulcs))
            {
                E[index] = null; // Torles a tombbol
                return;
            }

            var elem = KulcsKeres(kulcs);
            if (elem != null)
            {
                U.Torol(elem); // Torles a lancolt listabol
                return;
            }

            throw new HibasKulcsKivetel(); // Ha nincs meg, kivetelt dobunk
        }
    }

}
