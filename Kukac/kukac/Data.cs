using Kukac.interfaces;
using System.Windows;
using System;
using Kukac.types;
using System.Collections.Generic;
using Kukac.enums;

namespace Kukac.kukac
{
    public class Data : Adat
    {
        List<RajzolKovetkezo> rajzolkov;
        List<Rajzolhato> rajzolhatok;
        List<Leptetheto> leptethetok;
        List<Kukac.types.Kukac> kukacok;
        List<Object> elemek;
        List<Ai> aik;
        Dictionary<Ai, Kukac.types.Kukac> aikukacok;
        public Palya palya;
        public Kaja kaja;

        public Data()
        {
            rajzolkov = new List<RajzolKovetkezo>();
            rajzolhatok = new List<Rajzolhato>();
            leptethetok = new List<Leptetheto>();
            elemek = new List<object>();
            kukacok = new List<types.Kukac>();
            aik = new List<Ai>();
            aikukacok = new Dictionary<Ai, types.Kukac>();
            palya = new Palya();
            kaja = new Kaja();
        }

        public PalyaElemek getPalyaElem(int x, int y)
        {
            return palya.getElem(x, y);
        }

        public Test getKukac(int i)
        {
            return (Test)kukacok[i];
        }

        public void add(Object o)
        {
            if (o is Kaja)
            {
                kaja = (Kaja)o;
            }
            if (o is Palya)
            {
                palya = (Palya)o;
            }
            if (o is Kukac.types.Kukac)
            {
                kukacok.Add((Kukac.types.Kukac)o);
            }
            if (o is RajzolKovetkezo)
            {
                rajzolkov.Add((RajzolKovetkezo)o);
            }
            if (o is Rajzolhato)
            {
                rajzolhatok.Add((Rajzolhato)o);
            }
            if (o is Leptetheto)
            {
                leptethetok.Add((Leptetheto)o);
            }

            elemek.Add(o);
        }

        public Kaja getKaja()
        {
            return kaja;
        }

        public Point getEtel()
        {
            return kaja.getEtel();
        }

        public List<Leptetheto> getLeptethetok()
        {
            return leptethetok;
        }


        public List<Object> getElemek()
        {
            return elemek;
        }

        public List<Rajzolhato> getRajzolhatok()
        {
            return rajzolhatok;
        }


        public List<RajzolKovetkezo> getRajzolkov()
        {
            return rajzolkov;
        }

        public List<Kukac.types.Kukac> getKukacok()
        {
            return kukacok;
        }

        public Palya getPalya()
        {
            return palya;
        }

        public void addAi(Ai ai, Kukac.types.Kukac k)
        {
            aik.Add(ai);
            aikukacok.Add(ai, k);
        }

        public List<Ai> getAik()
        {
            return aik;
        }

        public Dictionary<Ai, Kukac.types.Kukac> getAikukacok()
        {
            return aikukacok;
        }

        public Size getPalyaMeret()
        {
            return new Size(palya.meretx, palya.merety);
        }

        public int getKukacDarab()
        {
            return kukacok.Count;
        }


    }
}