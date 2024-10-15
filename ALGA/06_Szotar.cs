using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OE.ALGA.Adatszerkezetek
{
    public class SzotarElem<K,T>
    {
        public K Kulcs;
        public T Tart;

        public SzotarElem(K kulcs, T tart)
        {
            Kulcs = kulcs;
            Tart = tart;
        }
    }

   

    public class HasitoSzotarTulcsordulasiTerulettel<K, T> : Szotar<K, T>
    {
        SzotarElem<K, T>[] E;
        Func<K, int> h;
        private LancoltLista<SzotarElem<K, T>> U;

        public HasitoSzotarTulcsordulasiTerulettel(int meret, Func<K, int> hasitoFuggveny)
        {
            E = new SzotarElem<K, T>[meret];
            h = x => hasitoFuggveny(x) % E.Length;
            U = new LancoltLista<SzotarElem<K, T>>();
        }

        public HasitoSzotarTulcsordulasiTerulettel(int meret) : this(meret, kulcs => kulcs.GetHashCode())
        {
        }

        private SzotarElem<K,T> KulcsKeres(K kulcs)
        {
            SzotarElem<K, T> e = null;

            if (E[h(kulcs)] != null && EqualityComparer<K>.Default.Equals(E[h(kulcs)].Kulcs, kulcs))
            {
                return E[h(kulcs)];
            }
            if (U != null)  // Ellenőrizzük, hogy létezik-e a láncolt lista
            {
                U.Bejar(x =>
                {
                    if (EqualityComparer<K>.Default.Equals(x.Kulcs, kulcs))
                    {
                        e = x;  // Ha megtaláltuk, elmentjük a talált elemet
                        
                    }
                });
            }
            return e;
        }

        public void Beir(K kulcs, T ertek)
        {
            SzotarElem<K, T> x = KulcsKeres(kulcs); 

            if( x != null)
            {
                E[h(kulcs)].Tart = ertek;
            }
            
            if  (E[h(kulcs)] == null)
            {
                E[h(kulcs)] = new SzotarElem<K, T>(kulcs, ertek);
            }
            else { U.Hozzafuz(new SzotarElem<K, T>(kulcs,ertek)); }
        }

        public T Kiolvas(K kulcs) 
        {
            SzotarElem<K,T> x = KulcsKeres(kulcs);
            if (x != null)
            {
                return x.Tart;
            }
            else throw new HibasKulcsKivetel();

        }

        public void Torol(K kulcs)
        {
            if (E[h(kulcs)] != null && EqualityComparer<K>.Default.Equals(E[h(kulcs)].Kulcs, kulcs))
            E[h(kulcs)] = null;
            else
            {
                U.Bejar(x =>
                {
                    if (EqualityComparer<K>.Default.Equals(x.Kulcs, kulcs))
                    {
                       U.Torol(x);
                    }
                });
            }
        }

    }
}
