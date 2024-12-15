using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OE.ALGA.Adatszerkezetek
{
    public class LancElem<T>
    {
        public T tart;
        public LancElem<T>? kov;

        public LancElem(T tart, LancElem<T>? kov)
        {
            this.tart = tart;
            this.kov = kov;
        }
    }




    public class LancoltVerem<T> : Verem<T>
    {
        public LancElem<T>? fej;

        public LancoltVerem(LancElem<T>? fej = null)
        {
            this.fej = fej;
        }

        public bool Ures { get { return fej == null; } }

        public T Felso()
        {
            if (fej != null)
            {
                return fej.tart;
            }
            else throw new NincsElemKivetel();
        }

        public void Verembe(T ertek)
        {
            fej = new LancElem<T>(ertek, fej);
        }

        public T Verembol()
        {
            LancElem<T> q;

            if (fej != null)
            {
                T ertek = fej.tart;
                q = fej;
                fej = fej.kov;
                return ertek;
            }
            else throw new NincsElemKivetel();
        }
    }

    public class LancoltSor<T> : Sor<T>
    {
        LancElem<T>? fej;
        LancElem<T>? vege;

        public bool Ures => fej == null;

        public T Elso()
        {
            if (fej != null)
            {  return fej.tart; }
            else throw new NincsElemKivetel();
        }

        public void Sorba(T ertek)
        {
            LancElem<T> uj = new LancElem<T> (ertek, null);

            if (vege != null)
            {
                vege.kov = uj;
            }
            else {fej = uj;}

            vege = uj;    
        }

        public T Sorbol()
        {
            LancElem<T>? q;
            T ertek;

            if (fej != null)
            {
                ertek = fej.tart;
                q = fej;
                fej = fej.kov;
                if (fej == null)
                {
                    vege = null;
                }
                return ertek;
            }
            else throw new NincsElemKivetel();
        }
    }

    public class LancoltLista<T> : Lista<T>, IEnumerable<T>
    {
        LancElem<T>? fej;

        public int Elemszam
        { get
            {
                int i = 0;
                LancElem<T>? current = fej;

                while (current != null)
                {
                    current = current.kov;
                    i++;
                }

                return i;
            }
        }


        public void Bejar(Action<T> muvelet)
        {
            LancElem<T>? p = fej;
            while (p != null)
            {
                muvelet(p.tart);
                p = p.kov;
            }
        }

        public void Beszur(int index, T ertek)
        {
            LancElem<T>? p;

            if (fej == null || index == 0)
            {
                fej = new LancElem<T>(ertek, fej);
            }
            else
            {
                p = fej;
                int i = 1;
                while (p != null && i < index)
                {
                    p = p.kov;
                    i++;
                }
                if (i <= index)
                {
                    p.kov = new LancElem<T>(ertek, p.kov);
                }
                else throw new HibasIndexKivetel();
            }
        }

        public void Hozzafuz(T ertek)
        {
            LancElem<T> uj = new LancElem<T>(ertek,null);
            LancElem<T> p;

            if (fej == null) // ha ures akkor csak beszurja az elejere
            {
                fej = uj;
            }
            else // lepked amig a lista vegere nem er, aztan beszurja
            {
                p = fej;
                while (p.kov != null)
                {
                    p = p.kov;
                }
                p.kov = uj;
            }
        }

        public T Kiolvas(int index)
        {
            LancElem<T>? p = fej;
            int i = 0;

            while (p != null && i < index)
            {
                p = p.kov;
                i++;
            }
            if (p != null)
            {
                return p.tart;
            }
            else throw new HibasIndexKivetel();
        }

        public void Modosit(int index, T ertek)
        {
            LancElem<T>? p = fej;
            int i = 0;
            while (p != null && i < index)
            {
                p = p.kov;
                i++;
            }
            if (p != null)
            {
                p.tart = ertek;
            }
            else throw new HibasIndexKivetel();
        }

        public void Torol(T ertek)
        {
            LancElem<T>? p = fej;
            LancElem<T>? e = null;
            LancElem<T>? q;

            do
            {
                while(p != null && !(ertek.Equals(p.tart)))
                {
                    e = p;
                    p = p.kov;
                }
                if (p != null)
                {
                    q = p.kov;
                    if (e == null)
                    {
                        fej = q;
                    }
                    else { e.kov = q; }
                    p = q;
                }
            }
            while (p != null);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new LancoltListaBejaro<T>(fej);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

    }

    public class LancoltListaBejaro<T> : IEnumerator<T>
    {
        private LancElem<T>? fej;
        private LancElem<T>? aktualisElem;

        public LancoltListaBejaro(LancElem<T>? fej)
        {
            this.fej = fej;
            this.aktualisElem = null;
        }

        public T Current
        {
            get
            {
                if (aktualisElem == null)
                {
                    throw new InvalidOperationException("Nincs aktuális elem!");
                }
                return aktualisElem.tart;
            }
        }

        object IEnumerator.Current => Current;

        public bool MoveNext()
        {
            if (aktualisElem == null)
            {
                aktualisElem = fej;
            }
            else
            {
                aktualisElem = aktualisElem.kov;
            }
            return aktualisElem != null;
        }

        public void Reset()
        {
            aktualisElem = null;
        }

        public void Dispose()
        {
            // Nincs erőforrás-felszabadítás
        }

    }
}

