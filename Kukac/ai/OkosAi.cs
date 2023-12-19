using Kukac.enums;
using Kukac.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kukac.kukac;
using Kukac.types;
using System.Windows;
using Microsoft.Win32;
using System.Security.Policy;
using System.Xml.Linq;

namespace Kukac.ai
{
    internal class OkosAi : Ai
    {
        private Adat adat;
        private Test test;

        AStarNode fej;
        Iranyok irany;


        public class AStarNode
        {
            public int X { get; set; }
            public int Y { get; set; }
            public double HCost { get; set; } //céltól távolság
            public double FCost { get { return HCost; } }
            public AStarNode Parent { get; set; }
        }
      

        //Kukac.enums.Iranyok Iranyok  Bal, Fel, Jobb, Le
        //Kukac.enums.PalyaElemek Fal, Kaja, Test, Ures

        public void AStar()
        {
            fej = new AStarNode();
            fej.X = (int)test.getFej().X;
            fej.Y = (int)test.getFej().Y;

            List<AStarNode> open = new List<AStarNode>();
            List<AStarNode> closed = new List<AStarNode>();
            
            open.Add(fej);

            AStarNode kaja = new AStarNode();
            kaja.X = (int)adat.getEtel().X;
            kaja.Y = (int)adat.getEtel().Y;

            AStarNode current = new AStarNode();
            current.X = -1;
            current.Y = -1;
            while (current.X != kaja.X || current.Y != kaja.Y)
            {
                double fcost = 99999;
                int idx = -1;

                if (open.Count == 0)
                {
                    break;
                }

                current = open.OrderBy(x => x.FCost).First();
                open.Remove(current);
                closed.Add(current);
                
                List<AStarNode> neighbours = getNeighbours(current, kaja, adat);
                foreach (var neighbour in neighbours)
                {
                    if (!closed.Any(x => x.X == neighbour.X && x.Y == neighbour.Y))
                    {
                        if (open.Any(x => x.X == neighbour.X && x.Y == neighbour.Y))
                        {
                            var node = open.First(x => x.X == neighbour.X && x.Y == neighbour.Y);
                            if (node.FCost > current.FCost)
                            {
                                open.Remove(node);
                                open.Add(neighbour);
                            }
                        }
                        else
                        {
                            open.Add(neighbour);
                        }
                    }
                }
            }

            
            List<AStarNode> allomasok = new List<AStarNode>();
            allomasok.Add(closed[closed.Count - 1]);
            int i = closed.Count - 2;
            //while (i >= 0 && closed[i].Parent != null)
            //{
            //    allomasok.Add(closed[i]);
            //    i--;
            //}

            if (closed[1].X < closed[0].X)
            {
                //balra kell menni
                this.irany = Iranyok.BAL;
                //setIrany(Iranyok.BAL);
            }
            else if (closed[1].X > closed[0].X)
            {
                //jobbra kell menni
                this.irany = Iranyok.JOBB;
            }
            else if (closed[1].Y < closed[0].Y)
            {
                //fel kell menni
                this.irany = Iranyok.FEL;
            }
            else if (closed[1].Y > closed[0].Y)
            {
                //le kell menni
                this.irany = Iranyok.LE;
            }
        }

        private double GetHcost(double startX, double startY, double endX, double endY)
        {
            //céltól távolság
            double hCost = 0;
            double xDistance = Math.Abs(endX - startX);
            double yDistance = Math.Abs(endY - startY);
            hCost = (xDistance + yDistance) * 10;

            return hCost;

        }

        public List<AStarNode> getNeighbours(AStarNode current, AStarNode target, Adat data)
        {
            var starNodes = new List<AStarNode>()
            {
                new AStarNode { X = current.X, Y = current.Y - 1, Parent = current },
                new AStarNode { X = current.X, Y = current.Y + 1, Parent = current },
                new AStarNode { X = current.X - 1, Y = current.Y, Parent = current },
                new AStarNode { X = current.X + 1, Y = current.Y, Parent = current },
            };

            List<PalyaElemek> ps = new List<PalyaElemek>();
            //int x = -1;
            //int y = -1;
            foreach (var node in starNodes)
            {
                node.HCost = GetHcost(node.X, node.Y, data.getEtel().X, data.getEtel().Y);
                ps.Add(adat.getPalyaElem(node.X, node.Y));
                //x = (int)data.getPalyaMeret().Width;
                //y = (int)data.getPalyaMeret().Height;
            }

            List<AStarNode> nodes = new List<AStarNode>();
            for (int i = 0; i < starNodes.Count; i++)
            {
                int xn = starNodes[i].X;
                int yn = starNodes[i].Y;
                 if (xn >= 0 && xn <= (int)data.getPalyaMeret().Width)
                {
                    if (yn >= 0 && yn <= (int)data.getPalyaMeret().Height)
                    {
                        PalyaElemek p = adat.getPalyaElem(xn, yn);
                        if (p == PalyaElemek.URES || p == PalyaElemek.KAJA)
                        {
                            nodes.Add(starNodes[i]);
                        }
                    }
                }
            }

            return nodes;

            //return starNodes
            //    .Where(node => node.X >= 0 && node.X <= (int)data.getPalyaMeret().Width)
            //    .Where(node => node.Y >= 0 && node.Y <= (int)data.getPalyaMeret().Height)
            //    .Where(node => adat.getPalyaElem(node.Y, node.X) == PalyaElemek.URES || data.getPalyaElem(node.Y, node.X) == PalyaElemek.KAJA)
            //    .ToList();
        }

        //Meghívódik inicializáláskor, ezen keresztül kapja meg az adat-ot
        public void initAdat(Adat adat)
        {
            this.adat = adat;
        }


        //Meghívódik inicializáláskor, ezen keresztül kapja meg az irányított kukacot
        public void initKukac(Test kukac)
        {
            test = kukac;
        }


        //Minden lépés elott, amikor el a kukac, ez a metodus hívódik meg.
        //Visszatérésként az új írányt kell meghatározni!
        public Iranyok setIrany()
        {
            AStar();
            return this.irany;
        }
    }
}
