using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OE.ALGA.Adatszerkezetek
{
    public class TombVerem<T> : Verem<T>
    {
        T[] E;
        int n = 0;



        public TombVerem(int meret)
        {
            E = new T[meret];


        }

        public bool Ures { get{ return n == 0; }  }

        public T Felso()
        {
            if (n > 0)

                return E[n - 1];

            else throw new NincsElemKivetel();  
        }

        public void Verembe(T ertek)
        {
            if (n < E.Length)
            {
                E[n] = ertek;
                n++;
            }
            else
                throw new NincsHelyKivetel();
            
        }

        public T Verembol()
        {
            if (n > 0) return E[--n];
            else throw new NincsElemKivetel();
        }
    }

    public class TombSor<T> : Sor<T>
    {
        public T[] E;
        int e = -1; // elso elem 
        int u = 0; // utolso elem
        int n = 0; // sorban levo elemek szama

        public TombSor(int meret)
        {
            E = new T[meret]; 
        }

        public bool Ures => n == 0;

        public T Elso()
        {
            if (!Ures) return E[(e + 1) % E.Length];
            else throw new NincsElemKivetel();
        }

        public void Sorba(T ertek)
        {
            if (n < E.Length)
            {
                E[u] = ertek;
                u = ((u + 1) % E.Length);
                n++;
            }
            else throw new NincsHelyKivetel();
        }

        public T Sorbol()
        {
            if (n > 0)
            {
                n--;
                e = ((e + 1) % E.Length);
                return E[e];
            }
            else throw new NincsElemKivetel();
        }
    }


}
