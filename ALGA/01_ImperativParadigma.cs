using System.Collections;

namespace OE.ALGA.Paradigmak
{
    public interface IVegrehajthato
    {
        void Vegrehajtas();
    }

    public interface IFuggo
    {
        bool FuggosegTeljesul { get; }
    }

    public class FeladatTarolo<T> : IEnumerable<T> where T : IVegrehajthato
    {
        readonly protected T[] tarolo;
        protected int n = 0;

        public FeladatTarolo(int meret)
        {
            tarolo = new T[meret];
        }

        public void Felvesz(T elem)
        {
            if (n < tarolo.Length)
                tarolo[n++] = elem;
            else
                throw new TaroloMegteltKivetel();
        }

        public virtual void MindentVegrehajt()
        {
            for (int i = 0; i < n; i++)
                tarolo[i].Vegrehajtas();
        }

        public virtual IEnumerator<T> GetEnumerator()
        {
            return new FeladatTaroloBejaro<T>(tarolo, n);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class TaroloMegteltKivetel : Exception
    {
    }

    public class FuggoFeladatTarolo<T> : FeladatTarolo<T> where T : IVegrehajthato, IFuggo
    {
        public FuggoFeladatTarolo(int meret) : base(meret)
        {
        }

        public override void MindentVegrehajt()
        {
            for (int i = 0; i < n; i++)
                if (tarolo[i].FuggosegTeljesul)
                    tarolo[i].Vegrehajtas();
        }
    }

    public class FeladatTaroloBejaro<T> : IEnumerator<T>
    {
        readonly T[] tarolo;
        readonly int n;

        public FeladatTaroloBejaro(T[] tarolo, int n)
        {
            this.tarolo = tarolo;
            this.n = n;
        }

        int aktualisIndex = -1;

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
