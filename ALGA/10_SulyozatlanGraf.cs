using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OE.ALGA.Adatszerkezetek
{ 

    // EgeszGrafEl osztály, amely a gráf éleit reprezentálja
    public class EgeszGrafEl : GrafEl<int>, IComparable
    {
        public int Honnan { get; }
        public int Hova { get; }

        public EgeszGrafEl(int honnan, int hova)
        {
            Honnan = honnan;
            Hova = hova;
        }

        public int CompareTo(EgeszGrafEl other)
        {
            if (Honnan != other.Honnan)
                return Honnan.CompareTo(other.Honnan);
            return Hova.CompareTo(other.Hova);
        }

        public override bool Equals(object obj)
        {
            if (obj is EgeszGrafEl other)
                return Honnan == other.Honnan && Hova == other.Hova;
            return false;
        }

        public int CompareTo(object? obj)
        {
            if (obj != null && obj is EgeszGrafEl b)
            {
                if (b.Honnan != Honnan)
                {
                    return Honnan.CompareTo(b.Honnan);
                }
                else
                {
                    return Hova.CompareTo(b.Hova);
                }
            }
            else throw new InvalidOperationException();
        }

    }

    // CsucsmatrixSulyozatlanEgeszGraf osztály
    public class CsucsmatrixSulyozatlanEgeszGraf : SulyozatlanGraf<int, EgeszGrafEl>
    {
        private readonly int n;
        private readonly bool[,] M;

        public CsucsmatrixSulyozatlanEgeszGraf(int n)
        {
            this.n = n;
            M = new bool[n, n];
        }

        public int CsucsokSzama => n;

        public int ElekSzama
        {
            get
            {
                int count = 0;
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (M[i, j])
                            count++;
                    }
                }
                return count;
            }
        }

        public Halmaz<int> Csucsok
        {
            get
            {
                Halmaz<int> halmaz = new FaHalmaz<int>();
                for (int i = 0; i < n; i++)
                {
                    halmaz.Beszur(i);
                }
                return halmaz;
            }
        }

        public Halmaz<EgeszGrafEl> Elek
        {
            get
            {
                Halmaz<EgeszGrafEl> halmaz = new FaHalmaz<EgeszGrafEl>();
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (M[i, j])
                        {
                            halmaz.Beszur(new EgeszGrafEl(i, j));
                        }
                    }
                }
                return halmaz;
            }
        }

        public void UjEl(int honnan, int hova)
        {
            M[honnan, hova] = true;
        }

        public bool VezetEl(int honnan, int hova)
        {
            return M[honnan, hova];
        }

        public Halmaz<int> Szomszedai(int csucs)
        {
            FaHalmaz<int> halmaz = new FaHalmaz<int>();
            for (int i = 0; i < n; i++)
            {
                if (M[csucs, i])
                {
                    halmaz.Beszur(i);
                }
            }
            return halmaz;
        }
    }

  //   GráfBejárások osztály
    public static class GrafBejarasok
    {
        public static Halmaz<V> SzelessegiBejaras<V, E>(Graf<V, E> g, V start, Action<V> muvelet) where V : IComparable
        {
          Sor<V> S = new LancoltSor<V>();
          Halmaz<V> F = new FaHalmaz<V>();
            S.Sorba(start);
            F.Beszur(start);
          
          while (!S.Ures)
          {
                V k = S.Sorbol();
                muvelet(k);
               
                Halmaz<V> szomszedok = g.Szomszedai(k);

                szomszedok.Bejar(szomszed =>
                {
                    if (!F.Eleme(szomszed))
                    {
                        S.Sorba(szomszed);
                        F.Beszur(szomszed);
                    }

                });
          }
          return F;

        }

        public static Halmaz<V> MelysegiBejaras<V, E>(Graf<V, E> g, V start, Action<V> muvelet) where V : IComparable
        {
            var latogatott = new FaHalmaz<V>();
            MelysegiBejarasRekurzio(g, start, latogatott, muvelet);
            return latogatott;
        }

        private static void MelysegiBejarasRekurzio<V, E>(Graf<V, E> g, V k, Halmaz<V> F, Action<V> muvelet) where V : IComparable
        {
            if (F.Eleme(k))
                return;

            F.Beszur(k);
            muvelet(k);

            Halmaz<V> szomszedok = g.Szomszedai(k);

            szomszedok.Bejar(szomszed =>
            {
                MelysegiBejarasRekurzio(g, szomszed, F, muvelet);
            });

        }
    }

}
