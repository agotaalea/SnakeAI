using Kukac.interfaces;
using Kukac.enums;
using System.Windows.Media;
using System.Windows;
using System;
using System.Collections.Generic;
using Kukac.kukac;

namespace Kukac.types
{

    public class Kukac : RajzolKovetkezo, Rajzolhato, Leptetheto, Test
    {
        List<Point> test;
        public bool el;
        int hossz;
        int novel;

        public Iranyok irany;

        public Point? kovPontAdd;
        public bool kovPontRemove;
        int kovHossz;
        int kovNovel;

        int meret;

        bool _eleszt;
        bool _megfoghalni;

        Color c;


        Random random = new Random(1000);

        public Kukac(Color kukacSzine)
        {
            test = new List<Point>();
            el = false;
            _eleszt = false;
            _megfoghalni = false;
            hossz = 0;
            novel = 0;
            meret = 15;

            setKukacSzine(kukacSzine);
            irany = Iranyok.LE;
        }

        public void setKukacSzine(Color kukacSzine)
        {
            c = kukacSzine;
        }

        public void eleszt(int x, int y, int hossz)
        {
            _eleszt = true;
            _megfoghalni = false;
            this.hossz = 0;
            novel = hossz;
            el = false;
            kovPontAdd = new Point(x, y);
            kovPontRemove = false;
            irany = Iranyok.LE;
        }

        public Iranyok getIrany()
        {
            return irany;
        }

        public bool isEl()
        {
            return el;
        }

        public void kovetkezoLepes()
        {

            if (_eleszt)
            {
                _megfoghalni = false;
                test.Clear();
                kovHossz = 0;
                _eleszt = false;
                el = true;
                kovPontRemove = (novel == 0);
            }
            else if (el)
            {
                Point fej = getFej();
                kovPontAdd = new Point(
                        fej.X + irany.getXm(),
                        fej.Y + irany.getYm());
                kovHossz = hossz;
                kovPontRemove = (novel == 0);
            }
            else if (hossz > 0)
            {
                kovPontAdd = null;
                kovHossz = hossz - 1;
                kovPontRemove = (novel == 0);
            }
            else
            {
                kovPontAdd = null;
                kovHossz = hossz;
                kovPontRemove = false;
            }
            // itt az okosság:
            kovNovel = novel;
            kovHossz += (novel > 0) ? 1 : 0;  //if (novel>0) { kovHossz += 1; } else { kovHossz += 0; }
            kovNovel -= (novel > 0) ? 1 : 0;
        }

        public void megfoghalni()
        {
            _megfoghalni = true;
        }

        /// <summary>
        /// Meghalasztja a kukacot
        /// </summary>
        public void meghal()
        {
            if (el)
            {
                kovPontAdd = null;
                el = false;
                kovetkezoLepes();
            }
        }

        public void leptet()
        {
            if (kovPontRemove)
            {
                test.RemoveAt(hossz - 1);
            }

            if (kovPontAdd.HasValue)
            {
                test.Insert(0, kovPontAdd.Value);
                kovPontAdd = null;
            }
            hossz = kovHossz;
            novel = kovNovel;
            if (_megfoghalni)
            {
                meghal();
                _megfoghalni = false;
            }
            Console.WriteLine("" + hossz);
        }

        public Point getVege()
        {
            Point p = test[hossz - 1];
            return new Point(p.X, p.Y);
        }

        public Point getFej()
        {
            Point p = test[0];
            return new Point(p.X, p.Y);
        }

        public Point? getTestResz(int i)
        {
            if (i >= 0 && i < hossz)
            {
                Point p = test[i];
                return new Point(p.X, p.Y);
            }
            else
            {
                return null;
            }
        }

        public void rajzol(Graphics g)
        {
            g.setColor(c);
            foreach (var elem in test)
            {
                Point p = elem;
                g.fillRect((int)p.X * Logic.meret, (int)p.Y * Logic.meret, Logic.meret, Logic.meret);
            }
        }

        public void rajzolKov(Graphics g)
        {
            if (kovPontRemove)
            {
                Point p = getVege();
                g.setColor(Colors.White);
                g.fillRect((int)p.X * Logic.meret, (int)p.Y * Logic.meret, Logic.meret, Logic.meret);
            }
            if (kovPontAdd.HasValue)
            {
                Point p = kovPontAdd.Value;//getFej();
                g.setColor(c);
                g.fillRect((int)p.X * Logic.meret, (int)p.Y * Logic.meret, Logic.meret, Logic.meret);
            }
        }

        public void utkozik(Palya palya, Kaja kaja)
        {
            if (el && kovPontAdd.HasValue)
            {
                Point p = kovPontAdd.Value;
                if (palya.matrix[(int)p.X, (int)p.Y] == PalyaElemek.FAL ||
                        palya.matrix[(int)p.X, (int)p.Y] == PalyaElemek.TEST)
                {
                    meghal();
                }
                if (palya.matrix[(int)p.X, (int)p.Y] == PalyaElemek.KAJA)
                {
                    kovNovel += 20;
                    palya.kajaRemoved(kaja);
                }

            }
        }

        public int getHossz()
        {
            return hossz;
        }
    }

}