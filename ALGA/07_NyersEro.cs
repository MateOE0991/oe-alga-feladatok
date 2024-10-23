using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace OE.ALGA.Optimalizalas
{
    public class HatizsakProblema
    {
        public HatizsakProblema(int n, int wmax, int[] w, float[] p)
        {
            this.n = n;
            Wmax = wmax;
            this.w = w;
            this.p = p;
        }

        public int n { get; } // targyak szama
        public int Wmax { get; } // hatizsak sulybirasa
        public int[] w { get; } // targyak sulyai
        public float[] p { get; } // targyak ertekei


        public int OsszSuly(bool[] pakolas)
        {
            int osszSuly = 0;

            for (int i = 0; i < pakolas.Length; i++)
            {
                if (pakolas[i]) osszSuly += w[i];
            }

            return osszSuly;
        }

        public float OsszErtek(bool[] pakolas)
        {
            float osszErtek = 0;

            for (int i = 0; i < pakolas.Length; i++)
            {
                if (pakolas[i]) osszErtek += p[i];
            }

            return osszErtek;
        }
        public bool Ervenyes(bool[] pakolas)
        {
            if (OsszSuly(pakolas) <= Wmax) return true;
            else return false;
        }
    }

    public class NyersEro<T>
    {
        public int m; // megoldasok szama
        public Func<int, T> generator;
        public Func<T, float> josag;
        public int LepesSzam { get; set; }

        public NyersEro(int m, Func<int, T> generator, Func<T, float> josag)
        {
            this.m = m;
            this.generator = generator;
            this.josag = josag;
        }

        public T OptimalisMegoldas()
        {
            T o = generator(1);
            for (int i = 2; i < m; i++)
            {
                T x = generator(i);
                if (josag(x) > josag(o)) o = x; LepesSzam++;
            }
            return o;
        }
    }

    public class NyersEroHatizsakPakolas
    {

        public int LepesSzam { get; private set; }
        public HatizsakProblema problema;
        private bool[] optimalisPakolas;

        public NyersEroHatizsakPakolas(HatizsakProblema problema)
        {
            this.problema = problema;
        }

        public bool[] generator(int i)
        {
            int szam = i - 1;
            bool[] K = new bool[problema.n];

            for (int j = 0; j <= problema.n - 1; j++)
            {
                K[j] = Math.Floor(szam / Math.Pow(2, j)) % 2 == 1;
            }

            return K;

        }

        public float josag(bool[] pakolas)
        {
            if (!problema.Ervenyes(pakolas)) return -1;
            else
            {
                return problema.OsszErtek(pakolas);
            }
        }

        // jegyzet f,3,f,8,f,5
        // sajat f,3,
        public bool[] OptimalisMegoldas()
        {
            int m = (int)Math.Pow(2, problema.n); // 2^n lehetseges megoldas

            // Uj brute force objektum 
            var nyersEro = new NyersEro<bool[]>(m, generator, josag);

            // opt megoldas kereses
            bool[] optimalisPakolas = nyersEro.OptimalisMegoldas();

            // lepes szam tarolas
            LepesSzam = nyersEro.LepesSzam;

            // opt pakolas
            return optimalisPakolas;
        }


        public float OptimalisErtek()
        {
            if (optimalisPakolas == null)
            {
                optimalisPakolas = OptimalisMegoldas();
            }
            return problema.OsszErtek(optimalisPakolas); // opt megoldas erteke
        }



    }

}
