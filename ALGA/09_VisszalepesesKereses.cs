using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace OE.ALGA.Optimalizalas
{
    //public class VisszalepesesOptimalizacio<T>
    //{
    //     public int n; // ami a részproblémák/szintek számát mutatja

    //     public int[] M; // ami az egyes szinteken a lehetséges részmegoldások számát mutatja!

    //     public T[,] R; // ami a lehetséges részmegoldásokat tárolja.

    //     public Func<int, T, bool> ft;
    //     public Func<int, T, T[], bool> fk;
    //     public Func<T[], float> josag;

    //     public VisszalepesesOptimalizacio(int n, int[] m, T[,] r, Func<int, T, bool> ft, Func<int, T, T[], bool> fk, Func<T[], float> josag)
    //     {
    //         this.n = n;
    //         M = m;
    //         R = r;
    //         this.ft = ft;
    //         this.fk = fk;
    //         this.josag = josag;
    //     }

    //     public int LepesSzam { get; private set; }

    //     public void Backtrack(int szint, ref T[] E, ref bool van, ref T[] O )
    //     {
    //         int i = 0;
    //         while ( i < M[szint])
    //         {   
    //             LepesSzam++;
    //             i++;
    //             if (ft(szint, R[szint,i]))
    //             {
    //                 if (fk(szint, R[szint,i], E))
    //                 {
    //                     E[szint] = R[szint,i];
    //                     if (szint == n)
    //                     {
    //                         if (!van || josag(E) > josag(O))
    //                         {
    //                             O = E;
    //                         }
    //                         van = true;
    //                     }
    //                     else Backtrack(szint + 1, ref E, ref van, ref O);
    //                 }
    //             }
    //         }
    //     }

    //     public T[] OptimalisMegoldas()
    //     {
    //         T[] E = new T[n]; // Aktuális részmegoldás tárolása
    //         T[] O = new T[n]; // Optimális megoldás tárolása
    //         bool van = false; // Megoldás létezésének jelzése
    //         Backtrack(1, ref E, ref van, ref O); // Rekurzív visszalépéses keresés elindítása
    //         return van ? O : null; // Ha van megoldás, visszaadjuk az optimálisat
    //     }
    //}

    //public class VisszalepesesHatizsakPakolas
    //{
    //    HatizsakProblema problema;

    //    public VisszalepesesHatizsakPakolas(HatizsakProblema problema)
    //    {
    //        this.problema = problema;
    //    }

    //    int LepesSzam { get; }

    //    public bool[] OptimalisMegoldas()
    //    {
    //        int n = problema.n;
    //        int[] M = new int[n + 1];
    //        bool[,] R = new bool[n + 1, 2];

    //        for (int i = 0; i < n; i++)
    //        {
    //            M[i] = 2;
    //            R[i, 0] = true;
    //            R[i, 1] = false;
    //        }

    //        bool fk(int szint, bool[] E)
    //        {
    //            return problema.OsszSuly(E) <= problema.Wmax;
    //        }

    //        VisszalepesesOptimalizacio<bool> opt;

    //        opt.Backtrack
    //    }

    //}

    public class VisszalepesesOptimalizacio<T>
    {
        protected int n;
        protected int[] M;
        protected T[,] R;
        protected Func<int, T, bool> ft;
        protected Func<int, T, T[], bool> fk;
        protected Func<T[], double> josag;

        public int LepesSzam { get; private set; }

        public VisszalepesesOptimalizacio(int n, int[] M, T[,] R,
            Func<int, T, bool> ft, Func<int, T, T[], bool> fk,
            Func<T[], double> josag)
        {
            this.n = n;
            this.M = M;
            this.R = R;
            this.ft = ft;
            this.fk = fk;
            this.josag = josag;
        }

        protected virtual void Backtrack(int szint, T[] E, ref bool van, ref T[] O)
        {
            for (int i = 0; i < M[szint]; i++)
            {
                T r = R[szint, i];
                if (ft(szint, r) && fk(szint, r, E))
                {
                    E[szint] = r;
                    if (szint == n - 1)
                    {
                        double aktualisJosag = josag(E);
                        if (!van || aktualisJosag > josag(O))
                        {
                            Array.Copy(E, O, E.Length);
                        }
                        van = true;
                    }
                    else
                    {
                        Backtrack(szint + 1, E, ref van, ref O);
                    }
                }
                LepesSzam++;
            }
        }

        public T[] OptimalisMegoldas()
        {
            LepesSzam = 0;
            T[] E = new T[n];
            T[] O = new T[n];
            bool van = false;

            Backtrack(0, E, ref van, ref O);

            return O;
        }
    }


    public class VisszalepesesHatizsakPakolas
    {
        protected readonly HatizsakProblema problema;
        public int LepesSzam { get; private set; }

        public VisszalepesesHatizsakPakolas(HatizsakProblema problema)
        {
            this.problema = problema;
        }

        public virtual bool[] OptimalisMegoldas()
        {
            int n = problema.n;
            int[] M = Enumerable.Repeat(2, n).ToArray();
            bool[,] R = new bool[n, 2];
            for (int i = 0; i < n; i++)
            {
                R[i, 0] = true;
                R[i, 1] = false;
            }

            var optimalizacio = new VisszalepesesOptimalizacio<bool>(
                n,
                M,
                R,
                (szint, r) => true,
                (szint, r, E) =>
                {
                    int suly = 0;
                    for (int i = 0; i < szint; i++)
                        if (E[i]) suly += problema.w[i];
                    return suly + (r ? problema.w[szint] : 0) <= problema.Wmax;
                },
                (E) =>
                {
                    float osszertek = 0;
                    for (int i = 0; i < n; i++)
                        if (E[i]) osszertek += problema.p[i];
                    return osszertek;
                }
            );

            bool[] megoldas = optimalizacio.OptimalisMegoldas();
            LepesSzam = optimalizacio.LepesSzam;

            return megoldas;
        }

        public double OptimalisErtek()
        {
            var megoldas = OptimalisMegoldas();
            double osszertek = 0;
            for (int i = 0; i < problema.n; i++)
            {
                if (megoldas[i]) osszertek += problema.p[i];
            }
            return osszertek;
        }
    }
    public class SzetvalasztasEsKorlatozasOptimalizacio<T> : VisszalepesesOptimalizacio<T>
    {
        private readonly Func<int, T[], double> fb;
        public int LepesSzam { get; private set; }

        public SzetvalasztasEsKorlatozasOptimalizacio(
            int n, int[] M, T[,] R,
            Func<int, T, bool> ft,
            Func<int, T, T[], bool> fk,
            Func<T[], double> josag,
            Func<int, T[], double> fb)
            : base(n, M, R, ft, fk, josag)
        {
            this.fb = fb;
        }

        protected override void Backtrack(int szint, T[] E, ref bool van, ref T[] O)
        {
            for (int i = 0; i < M[szint]; i++)
            {
                T r = R[szint, i];
                if (ft(szint, r) && fk(szint, r, E))
                {
                    E[szint] = r;
                    double aktualisFelsoBecsles = fb(szint, E);
                    if (aktualisFelsoBecsles < josag(O)) continue;

                    if (szint == n - 1)
                    {
                        double aktualisJosag = josag(E);
                        if (!van || aktualisJosag > josag(O))
                        {
                            Array.Copy(E, O, E.Length);
                        }
                        van = true;
                    }
                    else
                    {
                        Backtrack(szint + 1, E, ref van, ref O);
                    }
                }
                LepesSzam++;
            }

        }
    }

    public class SzetvalasztasEsKorlatozasHatizsakPakolas : VisszalepesesHatizsakPakolas
    {
        public SzetvalasztasEsKorlatozasHatizsakPakolas(HatizsakProblema problema) : base(problema)
        { }

        public override bool[] OptimalisMegoldas()
        {
            int n = problema.w.Length;
            bool[] legjobbMegoldas = new bool[n]; // Inicializaljuk az eddigi legjobb megoldast
            float legjobbErtek = 0;              // Kezdjuk a legjobb erteket nullaval
            bool[] aktualisMegoldas = new bool[n]; // Aktualis megoldas allapotai

            // Korlatozo fuggveny (fk)
            bool fk(int szint, bool[] megoldas)
            {
                return problema.OsszSuly(megoldas) <= problema.Wmax;
            }

            // Becsolo fuggveny (fb)
            float fb(int szint, bool[] megoldas)
            {
                float becsultErtek = 0;
                int maradekKapacitas = problema.Wmax - problema.OsszSuly(megoldas);

                for (int i = szint; i < problema.n; i++)
                {
                    if (problema.w[i] <= maradekKapacitas)
                    {
                        becsultErtek += problema.p[i];
                        maradekKapacitas -= problema.w[i];
                    }
                    else
                    {
                        break; // Stop if we can't fit the next item
                    }
                }

                return becsultErtek;
            }


            // Backtrack eljaras
            void Backtrack(int szint)
            {
                Console.WriteLine($"Depth: {szint}, Current Solution: {string.Join(", ", aktualisMegoldas)}, Current Weight: {problema.OsszSuly(aktualisMegoldas)}, Current Value: {problema.OsszErtek(aktualisMegoldas)}");

                if (szint == n) // Alapszint: Ha az osszes targyat megvizsgaltuk
                {
                    float aktualisErtek = problema.OsszErtek(aktualisMegoldas);
                    if (aktualisErtek >= legjobbErtek)
                    {
                        legjobbErtek = aktualisErtek;
                        Array.Copy(aktualisMegoldas, legjobbMegoldas, n);
                    }
                    return;
                }

                Console.WriteLine($"Depth: {szint}, Current Solution: {string.Join(", ", aktualisMegoldas)}, Current Weight: {problema.OsszSuly(aktualisMegoldas)}, Current Value: {problema.OsszErtek(aktualisMegoldas)}");


                for (int i = 0; i <= 1; i++) // Vizsgaljuk mindket lehetoseget
                {
                    aktualisMegoldas[szint] = (i == 1);

                    if (fk(szint, aktualisMegoldas)) // Ha megfelel a korlatoknak
                    {
                        if (problema.OsszErtek(aktualisMegoldas) + fb(szint + 1, aktualisMegoldas) > legjobbErtek)
                        {
                            Console.WriteLine($"Depth: {szint}, Current Solution: {string.Join(", ", aktualisMegoldas)}, Current Weight: {problema.OsszSuly(aktualisMegoldas)}, Current Value: {problema.OsszErtek(aktualisMegoldas)}");
                            Backtrack(szint + 1);

                        }
                    }
                }

                Console.WriteLine($"Depth: {szint}, Current Solution: {string.Join(", ", aktualisMegoldas)}, Current Weight: {problema.OsszSuly(aktualisMegoldas)}, Current Value: {problema.OsszErtek(aktualisMegoldas)}");

                aktualisMegoldas[szint] = false; // Visszalepes
            }

            Backtrack(0); // Inditsuk a backtrack-et az elso szinten

            return legjobbMegoldas; // Visszaadjuk a legjobb megtalalt megoldast
            ;
        }

    }
}
