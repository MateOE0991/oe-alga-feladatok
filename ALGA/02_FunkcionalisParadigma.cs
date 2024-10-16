using OE.ALGA.Paradigmak;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace OE.ALGA.Paradigmak
{
    public class FeltetelesFeladatTarolo<T> : FeladatTarolo<T> where T : IVegrehajthato
    {
        public FeltetelesFeladatTarolo(int meret) : base(meret)
        {
        }

        public void FeltetelesVegrehajtas(Func<T, bool> feltetel)
        {
            for (int i = 0; i < n; i++)
            {
                bool megfelel = feltetel(tarolo[i]);
                if (megfelel)
                {
                    tarolo[i].Vegrehajtas();
                }
            }
        }

        public override IEnumerator<T> GetEnumerator()
        {
            return new FeltetelesFeladatTaroloBejaro<T>(tarolo,n,bejaroFeltetel);
        }
    }



    public class FeltetelesFeladatTaroloBejaro<T> : IEnumerator<T>
    {
        readonly T[] tarolo;
        readonly int n;
        Func<T, bool> BejaroFeltetel;
        int aktualisIndex = -1;

        public FeltetelesFeladatTaroloBejaro(T[] tarolo, int n, Func<T, bool> bejaroFeltetel)
        {
            this.tarolo = tarolo;
            this.n = n;
            this.BejaroFeltetel = bejaroFeltetel;
        }
        public T Current
        {
            get
            {
                return tarolo[aktualisIndex];
            }
        }
        object IEnumerator.Current
        {
            get
            {
                throw new NotImplementedException();
            }
        }
        public void Dispose()
        {
        }
        public bool MoveNext()
        {
            if (aktualisIndex == n - 1)
                return false;
            else
            {
                aktualisIndex++;
                return true;
            }
        }
        public void Reset()
        {
            aktualisIndex = -1;
        }
    }
}

