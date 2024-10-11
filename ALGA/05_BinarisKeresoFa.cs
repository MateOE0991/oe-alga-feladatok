using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OE.ALGA.Adatszerkezetek
{
    public class FaElem<T> where T : IComparable
    {
        public T tart;
        public FaElem<T>? bal;
        public FaElem<T>? jobb;

        public FaElem(T tart, FaElem<T>? bal, FaElem<T>? jobb)
        {
            this.tart = tart;
            this.bal = bal;
            this.jobb = jobb;
        }
    }


    public class FaHalmaz<T> :Halmaz<T> where T : IComparable 
    {

        FaElem<T>? gyoker = null;
        
        public void Bejar(Action<T> muvelet)
        {
            ReszfaBejarasPreorder(gyoker, muvelet);
        }

        public void Beszur(T ertek)
        {
            gyoker = ReszfabaBeszur(gyoker, ertek);
        }

        public bool Eleme(T ertek)
        {
            return ReszfaEleme(gyoker, ertek);
        }

        public void Torol(T ertek)
        {
            gyoker = ReszfabolTorol(gyoker, ertek);   
        }

        protected void ReszfaBejarasPreorder(FaElem<T> p, Action<T> muvelet)
        {
            if (p != null)
            {
                muvelet(p.tart);
                ReszfaBejarasPreorder(p.bal, muvelet);
                ReszfaBejarasPreorder(p.jobb, muvelet);
            }
        }

        protected void ReszfaBejarasInorder(FaElem<T> p, Action<T> muvelet)
        {
            if (p != null)
            {
                ReszfaBejarasInorder(p.bal, muvelet);
                muvelet(p.tart);
                ReszfaBejarasInorder(p.jobb, muvelet);
            }
        }

        protected void ReszfaBejarasPostorder(FaElem<T> p, Action<T> muvelet)
        {
            if (p != null)
            {
                ReszfaBejarasPostorder(p.bal, muvelet);
                ReszfaBejarasPostorder(p.jobb, muvelet);
                muvelet(p.tart);
            }
        }

        protected bool ReszfaEleme(FaElem<T> p, T ertek)
        {
            if (p != null)
            {
                int comp = p.tart.CompareTo(ertek); // ertek.CompareTo(p.tart) lenne az alapjarat és akkor kisebb => balra / nagyobb => jobbra lenne
                if (comp == 1) // ertek nagyobb mint a tartalom, ergo balra kell keresni
                {
                    return ReszfaEleme(p.bal, ertek);
                }
                else if (comp == -1) //  vagy kisebb, azaz jobbra
                {
                    return ReszfaEleme(p.jobb, ertek);
                }
                else // ertek megvan
                {
                    return true;
                }
            }
            else { return false; }
        }

        protected FaElem<T> ReszfabaBeszur(FaElem<T> p, T ertek)
        {
            int? comp = p?.tart.CompareTo(ertek); // p az nyilvan lehet nulla ezert kell neki a conditionial op.

            if (p == null)
            {
                return new FaElem<T>(ertek, null, null);
            }
            else if (comp == 1)
            {
                p.bal = ReszfabaBeszur(p.bal, ertek);
            }
            else if (comp == -1)
            {
                p.jobb = ReszfabaBeszur(p.jobb, ertek);
            }
            return p;
            
        }

        protected FaElem<T> ReszfabolTorol(FaElem<T> p, T ertek)
        {
            int? comp = p?.tart.CompareTo(ertek);
            FaElem<T> q;

            if (p != null)
            {
                if (comp == -1)
                {
                    p.jobb = ReszfabolTorol(p.jobb, ertek);
                }
                else if (comp == 1)
                {
                    p.bal = ReszfabolTorol(p.bal, ertek);
                }
                else if (p.bal == null)
                {
                    q = p;
                    p = p.jobb;
                }
                else if (p.jobb == null)
                {
                    q = p;
                    p = p.bal;
                }
                else
                {
                    p.bal = KetGyerekesTorles(p, p.bal);
                }
                return p;
            }
            else
            {
                throw new NincsElemKivetel();
            }
        }

        protected FaElem<T> KetGyerekesTorles(FaElem<T> e, FaElem<T> r)
        {
            FaElem<T> q;
            if (r.jobb != null)
            {
                r.jobb = KetGyerekesTorles(e, r.jobb);
                return r;
            }
            else 
            {
                e.tart = r.tart;
                q = r.bal;
                return q;
            }
        }
    }
}
