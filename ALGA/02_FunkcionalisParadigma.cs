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

        public Func<T, bool> BejaroFeltetel { get; set; }

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
            return new FeltetelesFeladatTaroloBejaro<T>(tarolo,n,BejaroFeltetel);
        }
    }



    public class FeltetelesFeladatTaroloBejaro<T> : IEnumerator<T>
    {
        readonly T[] tarolo;
        readonly int n;
        Func<T, bool> BejaroFeltetel;
        int aktualisIndex = -1;
        public T Current
        {
            get
            {
                return tarolo[aktualisIndex];
            }
        }

        public FeltetelesFeladatTaroloBejaro(T[] tarolo, int n, Func<T, bool> bejaroFeltetel = null)
        {
            this.tarolo = tarolo;
            this.n = n;
            this.BejaroFeltetel = bejaroFeltetel ?? (x => true);
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
            // Keressük a következő elemet, ami megfelel a feltételnek
            while (++aktualisIndex < n)
            {
                if (BejaroFeltetel(tarolo[aktualisIndex]))
                {
                    return true;
                }
            }
            return false; // Ha nincs több megfelelő elem
        }
        public void Reset()
        {
            aktualisIndex = -1;
        }
    }
}

