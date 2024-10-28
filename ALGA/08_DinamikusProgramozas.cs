namespace OE.ALGA.Optimalizalas
{
    public class DinamikusHatizsakPakolas
    {
        HatizsakProblema problema;

        public DinamikusHatizsakPakolas(HatizsakProblema problema)
        {
            this.problema = problema;
        }

        public int LepesSzam { get; private set; }

        public float[,] TablazatFeltoltes()
        {
            float[,] F = new float[problema.n + 1, problema.Wmax + 1];

            for (int t = 0; t < problema.n; t++)
            {
                F[t, 0] = 0;
            }
            for (int h = 1; h < problema.Wmax; h++)
            {
                F[0, h] = 0;
            }

            for (int t = 1; t <= problema.n; t++)
            {
                for (int h = 1; h <= problema.Wmax; h++)
                {
                    if (h >= problema.w[t - 1])
                    {
                        F[t, h] = MathF.Max(F[t - 1, h], F[t - 1, h - problema.w[t - 1]] + problema.p[t - 1]);
                    }
                    else
                    {
                        F[t, h] = F[t - 1, h];
                    }
                }

                Console.WriteLine($"Táblázat állapota t = {t} iteráció után:");
                for (int row = 0; row <= problema.n; row++)
                {
                    for (int col = 0; col <= problema.Wmax; col++)
                    {
                        Console.Write($"{F[row, col],5} ");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine("-------------------------");
            }
            return F;
        }

        public float OptimalisErtek()
        {
            float[,] F = TablazatFeltoltes();

            return F[problema.n, problema.Wmax];
        }

        public bool[] OptimalisMegoldas()
        {
            float[,] F = TablazatFeltoltes();
            bool[] megoldas = new bool[problema.n];
            int h = problema.Wmax;

            for (int t = problema.n; t > 0 && h > 0; t--)
            {
                if (F[t, h] != F[t - 1, h])
                {
                    megoldas[t - 1] = true;
                    h -= problema.w[t - 1];
                }
                else
                {
                    megoldas[t - 1] = false;
                }
            }

            return megoldas;
        }
    }
}
