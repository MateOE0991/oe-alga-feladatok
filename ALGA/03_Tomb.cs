using System;
using System.Collections;
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



    public class TombLista<T> : Lista<T>, IEnumerable<T>
    {
        T[] E;
        int n;

        public int Elemszam { get { return n; } }

        public TombLista(int meret = 1)
        {
            E = new T[meret];

        }

        public void Bejar(Action<T> muvelet)
        {
            for (int i = 0; i < n; i++)
            {
                muvelet(E[i]);
            }
        }

        public T[] TombNoveles()
        {
            T[] temp = E;
            E = new T[E.Length * 2];

            for (int i = 0; i < temp.Length; i++)
            {
                E[i] = temp[i];
            }
            return E;
        }

        public void Beszur(int index, T ertek)
        {

            if (index < 0 || index > n)
                throw new HibasIndexKivetel();

            if (n == E.Length) TombNoveles();

            for (int i = n; i > index; i--) E[i] = E[i - 1];

            E[index] = ertek;
            n++;
        }

        public void Hozzafuz(T ertek)
        {
            Beszur(n, ertek);
        }

        public T Kiolvas(int index)
        {
            if (index <= n) return E[index];
            else throw new HibasIndexKivetel();
        }

        public void Modosit(int index, T ertek)
        {
            if (index <= n) E[index] = ertek;
            else throw new HibasIndexKivetel();
        }

        public void Torol(T ertek)
        {
            int db = 0;

            for (int i = 0; i < n; i++)
            {
                if (EqualityComparer<T>.Default.Equals(ertek, E[i]))
                {
                    db++;
                }
                else E[i - db] = E[i];
            }

            n -= db;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new TombListaBejaro<T>(E,n);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public class TombListaBejaro<T> : IEnumerator<T>
        {
            T[] E;
            int n;
            int position = -1;

            public TombListaBejaro(T[] E, int n)
            {
                this.E = E;
                this.n = n;
            }

            public bool MoveNext()
            {
                position++;
                return (position < n);
            }

            public void Reset()
            {
                position = -1; 
            }

            public void Dispose()
            {
                
            }

            public T Current
            {
                get
                {
                    if (position < 0 || position >= n)
                        throw new InvalidOperationException();  
                    return E[position];  
                }
            }

            object IEnumerator.Current => Current;
        }



    }

   

   
}
