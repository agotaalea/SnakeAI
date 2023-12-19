using System.Windows;
using Kukac.interfaces;
using System.Collections.Generic;
using System;
using Kukac.types;
using Kukac.ai;
using System.Windows.Media;

namespace Kukac.kukac
{

    public class Logic
    {
        Data data;
        Board board;

        public static int meret = 15;

        public Logic(Board b)
        {
            meret = 5;
            board = b;
            data = new Data();

            Palya p = new Palya();
            data.add(p);

            Kukac.types.Kukac kukacMinta = new types.Kukac(Colors.Green);
            data.add(kukacMinta);

            //Mintai mintai = new Mintai();
            //mintai.initAdat((Adat)data);
            //mintai.initKukac(data.getKukac(0));
            //data.addAi(mintai, data.getKukacok()[0]);

            OkosAi okosai = new OkosAi();
            okosai.initAdat((Adat)data);
            okosai.initKukac(data.getKukac(0));
            data.addAi(okosai, data.getKukacok()[0]);

            Kukac.types.Kukac kukacTeszt = new types.Kukac(Colors.Blue);
            data.add(kukacTeszt);

            Mintai testAI = new Mintai();
            testAI.initAdat((Adat)data);
            testAI.initKukac(data.getKukac(1));
            data.addAi(testAI, data.getKukacok()[1]);


            Kaja kaja = new Kaja();
            data.add(kaja);

            restart();
        }

        public void restart()
        {
            int border = 25;
            data.getKukacok()[0].eleszt(border, border + 1, 15);
            data.getKukacok()[1].eleszt(border - 1, data.palya.merety - border - 1, 15);
            //data.getKukacok()[2].eleszt(data.palya.meretx - border - 2, data.palya.merety - border, 15);
            //data.getKukacok()[3].eleszt(data.palya.meretx - border + 1, border - 2, 15);
            //data.getKukacok()[4].eleszt(data.palya.meretx / 2 - border - 1, data.palya.merety / 2 - border, 15);
            //data.getKukacok()[5].eleszt(data.palya.meretx / 2 + border + 1, data.palya.merety / 2 + border - 2, 15);
            //data.getKukacok()[6].eleszt(data.palya.meretx / 2 + border - 2, data.palya.merety / 2 - border + 1, 15);
            //data.getKukacok()[7].eleszt(data.palya.meretx / 2 - border, data.palya.merety / 2 + border - 1, 15);
            data.getPalya().restart();
            data.getPalya().kajaRemoved(data.getKaja());
            data.getPalya().updateKaja(data.kaja);
        }

        public void paint(Graphics g)
        {
            aiCselekszik();
            kovetkezoLepes();
            utkozesEllenorzes();
            updatePalya();
            rajzolKov(g);
            leptet();
        }

        public void updatePalya()
        {

            foreach (var i in data.getKukacok())
            {
                data.getPalya().updateKukac(i);
            }
        }

        public void utkozesEllenorzes()
        {

            Dictionary<Point?, Kukac.types.Kukac> v = new Dictionary<Point?, Kukac.types.Kukac>();

            foreach (var k2 in data.getKukacok())
            {
                if (k2.kovPontAdd.HasValue &&  k2.isEl() && v.ContainsKey(k2.kovPontAdd))
                {
                    v[k2.kovPontAdd.Value].megfoghalni();
                    k2.megfoghalni();
                }
                
                if (k2.kovPontAdd.HasValue && !v.ContainsKey(k2.kovPontAdd))
                    v.Add(k2.kovPontAdd, k2);

            }


            foreach (var i in data.getKukacok())
            {
                Kukac.types.Kukac k = i;

                k.utkozik(data.getPalya(), data.getKaja());
            }
        }

        public void rajzol(Graphics g)
        {
            foreach (var i in data.getRajzolhatok())
            {
                i.rajzol(g);
            }
        }

        public void kovetkezoLepes()
        {
            foreach (var i in data.getLeptethetok())
            {
                i.kovetkezoLepes();
            }
        }

        public void leptet()
        {
            foreach (var i in data.getLeptethetok())
            {
                i.leptet();
            }
        }

        public void rajzolKov(Graphics g)
        {
            foreach (var i in data.getRajzolkov())
            {
                i.rajzolKov(g);
            }
        }

        private void aiCselekszik()
        {
            foreach (var i in data.getAik())
            {
                Ai ai = i;
                if (data.getAikukacok()[ai].isEl())
                {
                    data.getAikukacok()[ai].irany = ai.setIrany();
                }
            }
        }
    }

}