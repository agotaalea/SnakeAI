using Kukac.interfaces;
using Kukac.enums;
using System.Windows.Media;
using System.Windows;
using System;
using Kukac.kukac;
namespace Kukac.types
{

    public class Palya : Rajzolhato, RajzolKovetkezo
    {
        public PalyaElemek[,] matrix;
        public int meretx, merety;

        const int size = 300;

        public Palya()
        {
            matrix = new PalyaElemek[size, size];
            restart();
        }

        public PalyaElemek getElem(int x, int y)
        {
            PalyaElemek p = matrix[x, y];
            return p;
        }

        public void restart()
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    matrix[i, j] = PalyaElemek.URES;
                }
            }
            int merete = 120;
            meretx = merete;
            merety = merete;
            for (int i = 0; i <= merete; i++)
            {
                matrix[i, 0] = PalyaElemek.FAL;
                matrix[i, merete] = PalyaElemek.FAL;
                matrix[0, i] = PalyaElemek.FAL;
                matrix[merete, i] = PalyaElemek.FAL;
            }

        }

        public void rajzol(Graphics g)
        {
            for (int i = 0; i < 200; i++)
            {
                for (int j = 0; j < 200; j++)
                {
                    if (matrix[i, j] == PalyaElemek.FAL)
                    {
                        g.setColor(Colors.Red);
                        g.fillRect(i * Logic.meret, j * Logic.meret, Logic.meret, Logic.meret);
                    }
                    else if (matrix[i, j] == PalyaElemek.URES)
                    {
                        g.setColor(Colors.White);
                        g.fillRect(i * Logic.meret, j * Logic.meret, Logic.meret, Logic.meret);
                    }
                    else if (matrix[i, j] == PalyaElemek.TEST)
                    {
                        g.setColor(Colors.Magenta);
                        g.drawRect(i * Logic.meret, j * Logic.meret, Logic.meret, Logic.meret);
                    }
                }
            }
        }

        public void rajzolKov(Graphics g)
        {
            for (int i = 0; i < 200; i++)
            {
                for (int j = 0; j < 200; j++)
                {
                    if (matrix[i, j] == PalyaElemek.FAL)
                    {
                        //                    g.setColor(Color.RED);
                        //                    g.fillRect(i*Logic.meret, j*Logic.meret, Logic.meret, Logic.meret);
                    }
                    else if (matrix[i, j] == PalyaElemek.URES)
                    {
                        //                    g.setColor(Color.WHITE);
                        //                    g.fillRect(i*Logic.meret, j*Logic.meret, Logic.meret, Logic.meret);
                    }
                    else if (matrix[i, j] == PalyaElemek.TEST)
                    {
                        //g.setColor(Color.MAGENTA);
                        //g.drawRect(i*Logic.meret, j*Logic.meret, Logic.meret, Logic.meret);
                        //g.setColor(Color.BLUE);
                        //g.fillRect(i*Logic.meret, j*Logic.meret, Logic.meret, Logic.meret);
                    }
                }
            }
        }


        public void updateKaja(Kaja kaja)
        {
            matrix[kaja.getX(), kaja.getY()] = PalyaElemek.KAJA;
        }

        public void updateKukac(Kukac kukac)
        {
            if (kukac.el && kukac.kovPontAdd.HasValue)
            {
                Point p = kukac.kovPontAdd.Value;
                matrix[(int)p.X, (int)p.Y] = PalyaElemek.TEST;
            }
            if (kukac.kovPontRemove)
            {
                Point p = kukac.getVege();
                matrix[(int)p.X, (int)p.Y] = PalyaElemek.URES;
            }
        }

        public void kajaRemoved(Kaja kaja)
        {
            int ujx;
            int ujy;
            do
            {
                Random r = new Random();
                ujx = (int)(r.Next(0, meretx));
                ujy = (int)(r.Next(0, merety));
            } while (matrix[ujx, ujy] != PalyaElemek.URES);
            kaja.setXY(ujx, ujy);

            updateKaja(kaja);
        }

    }

}