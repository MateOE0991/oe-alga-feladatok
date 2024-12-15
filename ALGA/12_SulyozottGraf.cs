using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OE.ALGA.Adatszerkezetek
{
    public class SulyozottEgeszGrafEl : EgeszGrafEl, SulyozottGrafEl<int>
    {

        public SulyozottEgeszGrafEl(int honnan, int hova, float suly) : base(honnan, hova)
        {
            Suly = suly;
        }

        public float Suly { get; }
    }

    public class CsucsmatrixSulyozottEgeszGraf : SulyozottGraf<int, SulyozottEgeszGrafEl>
    {
        int n;
        float[,] M;

        public CsucsmatrixSulyozottEgeszGraf(int n)
        {
            this.n = n;
            M = new float[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    M[i, j] = float.NaN;
                }
            }
        }
        public Halmaz<int> Csucsok
        {
            get
            {
                Halmaz<int> csucsok = new FaHalmaz<int>();
                for (int i = 0; i < n; i++)
                {
                    csucsok.Beszur(i);
                }
                return csucsok;
            }
        }

        public Halmaz<SulyozottEgeszGrafEl> Elek
        {
            get
            {
                Halmaz<SulyozottEgeszGrafEl> elek = new FaHalmaz<SulyozottEgeszGrafEl>();
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (!float.IsNaN(M[i, j]))
                        {
                            elek.Beszur(new SulyozottEgeszGrafEl(i, j, M[i, j]));
                        }
                    }
                }
                return elek;
            }
        }
        public int CsucsokSzama => n;

        public int ElekSzama
        {
            get
            {
                int db = 0;
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (!float.IsNaN(M[i, j]))
                        {
                            db++;
                        }
                    }
                }
                return db;
            }
        }



        public float Suly(int honnan, int hova)
        {
            if (!float.IsNaN(M[honnan, hova]))
            {
                return M[honnan, hova];
            }
            else
            {
                throw new NincsElKivetel();
            }
        }

        public Halmaz<int> Szomszedai(int point)
        {
            Halmaz<int> szomszedok = new FaHalmaz<int>();
            for (int i = 0; i < n; i++)
            {
                if (!float.IsNaN(M[point, i]))
                {
                    szomszedok.Beszur(i);
                }
            }
            return szomszedok;
        }

        public void UjEl(int honnan, int hova, float suly)
        {
            M[honnan, hova] = suly;
        }

        public bool VezetEl(int honnan, int hova)
        {
            return !float.IsNaN(M[honnan, hova]);
        }
    }

    public class Utkereses
    {
        public static Szotar<V, float> Dijkstra<V, E>(SulyozottGraf<V, E> g, V kezd)
        {
            Szotar<V, float> L = new HasitoSzotarTulcsordulasiTerulettel<V, float>(g.CsucsokSzama);
            Szotar<V, V> P = new HasitoSzotarTulcsordulasiTerulettel<V, V>(g.CsucsokSzama);
            KupacPrioritasosSor<V> S = new KupacPrioritasosSor<V>(g.CsucsokSzama, (ezmegy, ennel) => L.Kiolvas(ezmegy) < L.Kiolvas(ennel));

            g.Csucsok.Bejar(x =>
            {
                L.Beir(x, float.MaxValue);
                S.Sorba(x);
            });
            L.Beir(kezd, 0);
            S.Frissit(kezd);
            while (!S.Ures)
            {
                // Kivesszük a legkisebb távolságú csúcsot
                V u = S.Sorbol();

                // Bejárjuk a szomszédokat
                g.Szomszedai(u).Bejar(x =>
                {
                    float ujtav = L.Kiolvas(u) + g.Suly(u, x);
                    if (ujtav < L.Kiolvas(x))
                    {
                        // Ha rövidebb utat találtunk, frissítjük a távolságot és az előző csúcsot
                        L.Beir(x, ujtav);
                        P.Beir(x, u);

                        // Frissítjük a prioritási sorban az adott csúcsot
                        S.Frissit(x);
                    }
                });
            }

            // Visszatérünk a legrövidebb utak hosszával és előző csúcsaival
            return L;
        }
    }

    public class FeszitofaKereses
    {
        public static Szotar<V, V> Prim<V, E>(SulyozottGraf<V, E> g, V kezd)
            where V : IComparable
        {
            // szótár a bekötési élek súlyainak tárolására
            Szotar<V, float> K = new HasitoSzotarTulcsordulasiTerulettel<V, float>(g.CsucsokSzama);

            // szótár a bekötési élek tárolására
            Szotar<V, V> P = new HasitoSzotarTulcsordulasiTerulettel<V, V>(g.CsucsokSzama);

            // prioritási sor, amelyet a bekötési élek súlyai alapján rendezünk
            KupacPrioritasosSor<V> S = new KupacPrioritasosSor<V>(g.CsucsokSzama,
                (ezmegy, ennel) => K.Kiolvas(ezmegy) < K.Kiolvas(ennel));

            // halmaz, amelyben nyomon követjük, hogy mely csúcsok vannak a prioritási sorban
            var temp = new FaHalmaz<V>();

            // kezdeti értékek beállítása
            g.Csucsok.Bejar(x =>
            {
                K.Beir(x, float.MaxValue); // minden csúcs súlya végtelen kezdetben
                P.Beir(x, default(V)); // nincs előző csúcs
                S.Sorba(x); // csúcs hozzáadása a prioritási sorhoz
                temp.Beszur(x); // Hozzáadás a halmazhoz
            });

            // Kiindulási csúcs bekötési súlyának beállítása
            K.Beir(kezd, 0);
            S.Frissit(kezd);

            // Fő algoritmus
            while (!S.Ures)
            {
                // Kivesszük a minimális súlyú élt a prioritási sorból
                V u = S.Sorbol();
                temp.Torol(u);

                // Bejárjuk a szomszédokat
                g.Szomszedai(u).Bejar(x =>
                {
                    if (temp.Eleme(x) && g.Suly(u, x) < K.Kiolvas(x))
                    {
                        // Ha az él súlya kisebb, mint az eddig ismert legkisebb, frissítjük
                        K.Beir(x, g.Suly(u, x));
                        P.Beir(x, u);

                        // Frissítjük a prioritási sorban az adott csúcsot
                        S.Frissit(x);
                    }
                });
            }

            // Visszatérünk a minimális feszítőfa éleivel
            return P;
        }

        public static Halmaz<E> Kruskal<V, E>(SulyozottGraf<V, E> g) where E : IComparable, SulyozottGrafEl<V>
        {
            Halmaz<E> A = new FaHalmaz<E>();
            Szotar<V, int> vhalm = new HasitoSzotarTulcsordulasiTerulettel<V, int>(g.CsucsokSzama);
            int i = 0;
            g.Csucsok.Bejar(x => vhalm.Beir(x, i++));
            // Az élek prioritási sora, súly szerint növekvő sorrendben
            KupacPrioritasosSor<E> S = new KupacPrioritasosSor<E>(
                g.ElekSzama,
                (first, second) => first.Suly.CompareTo(second.Suly) < 0
            );

            // Minden él hozzáadása a prioritási sorhoz
            g.Elek.Bejar(S.Sorba);

            // Fő algoritmus
            while (!S.Ures)
            {
                // Kivesszük a legkisebb súlyú élet
                E el = S.Sorbol();

                V u = el.Honnan;
                V v = el.Hova;

                // Ha u és v különböző halmazban vannak, hozzáadjuk az élt a feszítőfához
                if (vhalm.Kiolvas(u) != vhalm.Kiolvas(v))
                {
                    A.Beszur(el);

                    // Halmazösszevonás: a vhalm értékeit frissítjük
                    int regihalm = vhalm.Kiolvas(v);
                    int ujhalm = vhalm.Kiolvas(u);

                    // Az összes v csúcshoz tartozó halmazazonosítót frissítjük
                    g.Csucsok.Bejar(csucs =>
                    {
                        if (vhalm.Kiolvas(csucs) == regihalm)
                        {
                            vhalm.Beir(csucs, ujhalm);
                        }
                    });
                }
            }
            return A;
        }
    }
}