
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace OE.ALGA.Adatszerkezetek
    {
        public class Kupac<T>
        {
            protected T[] E;
            protected int n;
            protected Func<T, T, bool> magasabbPrioritas;

            public Kupac(T[] tomb, int count, Func<T, T, bool> magasabbPrioritas)
            {
                E = tomb;
                n = count;
                this.magasabbPrioritas = magasabbPrioritas;
                KupacotEpit();
            }

            public static int Bal(int i) => 2 * i + 1;
            public static int Jobb(int i) => 2 * i + 2;
            public static int Szulo(int i) => (i - 1) / 2;

            public void Csere(int i, int j)
            {
                T temp = E[i];
                E[i] = E[j];
                E[j] = temp;
            }

            protected void Kupacol(int i)
            {
                int b = Bal(i);
                int j = Jobb(i);
                int max = i;

                if (b < n && magasabbPrioritas(E[b], E[i]))
                    max = b;
                if (j < n && magasabbPrioritas(E[j], E[max]))
                    max = j;

                if (max != i)
                {
                    Csere(i, max);
                    Kupacol(max);
                }
            }

            protected void KupacotEpit()
            {
                for (int i = n / 2 - 1; i >= 0; i--)
                {
                    Kupacol(i);
                }
            }
        }
        public class KupacRendezes<T> : Kupac<T> where T : IComparable<T>
        {
            public KupacRendezes(T[] tomb)
                : base(tomb, tomb.Length, (x, y) => x.CompareTo(y) > 0) { }

            public void Rendezes()
            {
                for (int i = n - 1; i >= 1; i--)
                {
                    Csere(0, i);
                    n--;
                    Kupacol(0);
                }
            }
        }

        public class KupacPrioritasosSor<T> : Kupac<T>, PrioritasosSor<T>
        {
            public KupacPrioritasosSor(int meret, Func<T, T, bool> magasabbPrioritas) : base(new T[meret], 0, magasabbPrioritas)
            {

            }

            private void KulcsotFelvisz(int i)
            {
                while (i > 0 && magasabbPrioritas(E[i], E[Szulo(i)]))
                {
                    Csere(i, Szulo(i));
                    i = Szulo(i);
                }
            }

            public bool Ures => n == 0;

            public void Sorba(T ertek)
            {
                if (n >= E.Length)
                {
                    throw new NincsHelyKivetel();
                }

                E[n++] = ertek;
                KulcsotFelvisz(n - 1);
            }

            public T Sorbol()
            {
                if (Ures)
                {
                    throw new NincsElemKivetel();
                }

                T max = E[0];
                E[0] = E[--n];
                Kupacol(0);

                return max;
            }

            public T Elso()
            {
                if (Ures)
                {
                    throw new NincsElemKivetel();
                }

                return E[0];
            }

            public void Frissit(T ertek)
            {
                int index = Array.IndexOf(E, ertek, 0, n);

                if (index == -1)
                {
                    throw new NincsElemKivetel();
                }

                KulcsotFelvisz(index);
                Kupacol(index);
            }
        }
    }

