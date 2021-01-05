using System.Collections.Generic;
using System.Linq;
using System.Drawing;

namespace Cierre_Convexo
{
    class CierreConvexo
    {
        private List<Point> Puntos = new List<Point>();
        private List<Point> UpperHull = new List<Point>();
        private List<Point> LowerHull = new List<Point>();

        private int Orientacion(Point p1, Point p2, Point p3)
        {
            //El producto cruz para determinar la orientacion
            int res = (p1.X * p2.Y) + (p2.X * p3.Y) + (p3.X * p1.Y) -
                      (p1.X * p3.Y) - (p2.X * p1.Y) - (p3.X * p2.Y);
            if (res == 0)
                return 0; //Colinear
            return (res > 0) ? 1 : -1; //Retorna 1 si el area es positiva y -1 si es negativa
        }

        public void AgegarPunto(int x, int y)
        {
            Point Punto = new Point(x, y);
            Puntos.Add(Punto);
        }

        public int Tam()
        {
            return Puntos.Count;
        }

        public void VaciarTodo()
        {
            UpperHull.Clear();
            LowerHull.Clear();
            Puntos.Clear();
        }


        public List<Point> CalcularCierreConvexo()
        {
            Puntos = Puntos.OrderBy(p => p.X).ToList();
            //Vamos a calcular el lower hull
            LowerHull.Add(Puntos[0]);
            LowerHull.Add(Puntos[1]);
            int indice;

            for (int i = 2; i < Puntos.Count; i++)
            {
                LowerHull.Add(Puntos[i]);
                indice = LowerHull.Count - 1;
                while ((LowerHull.Count > 2) && (Orientacion(LowerHull[indice-2], LowerHull[indice-1], LowerHull[indice])) >= 0)
                {
                    LowerHull.RemoveAt(LowerHull.Count - 2);
                    indice = LowerHull.Count - 1;
                }
            }

            UpperHull.Add(Puntos[Puntos.Count - 1]);
            UpperHull.Add(Puntos[Puntos.Count - 2]);

            for (int i = Puntos.Count - 3; i >= 0; i--)
            {
                UpperHull.Add(Puntos[i]);
                indice = UpperHull.Count - 1;
                while ((UpperHull.Count > 2) && (Orientacion(UpperHull[indice - 2], UpperHull[indice - 1], UpperHull[indice])) >= 0)
                {
                    UpperHull.RemoveAt(UpperHull.Count - 2);
                    indice = UpperHull.Count - 1;
                }
            }

            UpperHull.RemoveAt(0);
            UpperHull.RemoveAt(UpperHull.Count - 1);
            return LowerHull.Concat(UpperHull).ToList();            
        }

    }
}
