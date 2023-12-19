using Kukac.interfaces;
using System.Windows;
using System.Windows.Media;
using Kukac.kukac;
namespace Kukac.types
{


    public class Kaja : RajzolKovetkezo, Rajzolhato, Etel
    {
        int x, y;

        public Kaja()
        {
            x = 0;
            y = 0;
        }

        public int getX()
        {
            return x;
        }

        public int getY()
        {
            return y;
        }

        public void setX(int x)
        {
            this.x = x;
        }

        public void setY(int y)
        {
            this.y = y;
        }


        public void rajzolKov(Graphics g)
        {
            g.setColor(Colors.Cyan);
            g.fillRect(x * Logic.meret, y * Logic.meret, Logic.meret, Logic.meret);
        }


        public void rajzol(Graphics g)
        {
            g.setColor(Colors.Cyan);
            g.fillRect(x * Logic.meret, y * Logic.meret, Logic.meret, Logic.meret);
        }


        public Point getEtel()
        {
            return new Point(x, y);
        }

        public void setXY(int ujx, int ujy)
        {
            this.x = ujx;
            this.y = ujy;
        }

    }
}