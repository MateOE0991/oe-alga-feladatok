
using OE.ALGA.Optimalizalas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OE.ALGA.Optimalizalas
{
    public class VisszalepesesOptimalizacio<T>
    {
        protected int n;
        protected int[] M;
        protected T[,] R;
        protected Func<int, T, bool> ft;
        protected Func<int, T, T[], bool> fk;
        protected Func<T[], float> josag;

        public VisszalepesesOptimalizacio(int n, int[] M, T[,] R, Func<int, T, bool> ft, Func<int, T, T[], bool> fk, Func<T[], float> josag)
        {
            this.n = n;
            this.M = M;
            this.R = R;
            this.ft = ft;
            this.fk = fk;
            this.josag = josag;
        }

        public int LepesSzam { get; protected set; }

        protected virtual void Backtrack(int szint, ref T[] E, ref bool van, ref T[] Opt)
        {
            LepesSzam++;
            int i = -1;
            while (i < M[szint] - 1)
            {
                i++;

                if (ft(szint, R[szint, i]))
                {
                    if (fk(szint, R[szint, i], E))
                    {
                        E[szint] = R[szint, i];
                        if (szint == n - 1)
                        {
                            if (!van || josag(E) > josag(Opt))
                            {
                                Array.Copy(E, Opt, E.Length);
                            }
                            van = true;
                        }
                        else
                        {
                            Backtrack(szint + 1, ref E, ref van, ref Opt);
                        }
                    }
                }

            }
        }

        public T[] OptimalisMegoldas()
        {
            bool van = false;
            T[] E = new T[n];
            T[] Opt = new T[n];
            Backtrack(0, ref E, ref van, ref Opt);

            return Opt;
        }
    }

    public class VisszalepesesHatizsakPakolas
    {
        protected HatizsakProblema problema;

        public VisszalepesesHatizsakPakolas(HatizsakProblema problema)
        {
            this.problema = problema;
        }

        public int LepesSzam { get; protected set; }

        public virtual bool[] OptimalisMegoldas()
        {
            int n = problema.n;
            int[] M = new int[n];
            bool[,] R = new bool[n, 2];

            for (int i = 0; i < n; i++)
            {
                M[i] = 2;
                R[i, 0] = true;
                R[i, 1] = false;
            }

            var optim = new VisszalepesesOptimalizacio<bool>(
                n,
                M,
                R,
                (szint, e) => { return !e || problema.w[szint] <= problema.Wmax; },
                (szint, x, E) =>
                {
                    int ossz = 0;
                    for (int i = 0; i < szint; i++)
                    {
                        if (E[i])
                        {
                            ossz += problema.w[i];
                        }
                    }
                    return (ossz <= problema.Wmax) && (!x || ossz + problema.w[szint] <= problema.Wmax);
                },
                problema.OsszErtek
            );
            bool[] megold = optim.OptimalisMegoldas();
            LepesSzam = optim.LepesSzam;
            return megold;
        }

        public int OptimalisErtek()
        {
            bool[] e = OptimalisMegoldas();
            return (int)problema.OsszErtek(e);
        }
    }

    public class SzetvalasztasEsKorlatozasOptimalizacio<T> : VisszalepesesOptimalizacio<T>
    {
        Func<int, T[], float> fb;

        public SzetvalasztasEsKorlatozasOptimalizacio(int n, int[] M, T[,] R, Func<int, T, bool> ft, Func<int, T, T[], bool> fk, Func<T[], float> josag, Func<int, T[], float> fb) : base(n, M, R, ft, fk, josag)
        {
            this.fb = fb;
        }

        protected override void Backtrack(int szint, ref T[] E, ref bool van, ref T[] Opt)
        {
            LepesSzam++;
            int i = -1;
            while (i < M[szint] - 1)
            {
                i++;

                if (ft(szint, R[szint, i]))
                {
                    if (fk(szint, R[szint, i], E))
                    {
                        E[szint] = R[szint, i];
                        if (szint == n - 1)
                        {
                            if (!van || josag(E) > josag(Opt))
                            {
                                Array.Copy(E, Opt, E.Length);
                            }
                            van = true;
                        }
                        else if (josag(E) + fb(szint, E) > josag(Opt))
                        {
                            Backtrack(szint + 1, ref E, ref van, ref Opt);
                        }
                    }
                }
            }
        }
    }

    public class SzetvalasztasEsKorlatozasHatizsakPakolas : VisszalepesesHatizsakPakolas
    {
        public SzetvalasztasEsKorlatozasHatizsakPakolas(HatizsakProblema problema) : base(problema)
        {
        }
        public override bool[] OptimalisMegoldas()
        {
            int n = problema.n;
            int[] M = new int[n];
            bool[,] R = new bool[n, 2];

            for (int i = 0; i < n; i++)
            {
                M[i] = 2;
                R[i, 0] = false;
                R[i, 1] = true;
            }

            var optim = new SzetvalasztasEsKorlatozasOptimalizacio<bool>(
                n,
                M,
                R,
                (szint, e) => { return !e || problema.w[szint] <= problema.Wmax; },
                (szint, e, E) =>
                {
                    int osszSuly = 0;
                    for (int i = 0; i < szint; i++)
                    {
                        if (E[i])
                        {
                            osszSuly += problema.w[i];
                        }
                    }
                    return (osszSuly <= problema.Wmax) && (!e || osszSuly + problema.w[szint] <= problema.Wmax);
                },
                problema.OsszErtek,
                (szint, E) =>
                {
                    int osszErtek = 0;
                    for (int i = szint + 1; i < n; i++)
                    {
                        if (problema.OsszSuly(E) + problema.w[i] <= problema.Wmax)
                        {
                            osszErtek += (int)problema.p[i];
                        }

                    }
                    return osszErtek;
                }
            );

            bool[] megold = optim.OptimalisMegoldas();
            LepesSzam = optim.LepesSzam;
            return megold;
        }
    }
}
