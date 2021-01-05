using System.Drawing;
using System.Collections.Generic;

namespace PuntoEnPoligono
{
    class PuntoEnPoligono
    {
        public List<Punto> verticesPoligono = new List<Punto>();
        private bool antihorario;

        public void AgregarPunto(int x, int y)
        {
            Punto vertice = new Punto(x, y);
            verticesPoligono.Add(vertice);
        }

        public void Vaciar()
        {
            verticesPoligono.Clear();
        }

        private void Asignar() //Asigna siguiente y anterior a cada punto
        {
            for (int i = 0; i < verticesPoligono.Count; i++)
            {
                if (antihorario)
                {
                    if (i == 0)
                    {
                        verticesPoligono[i].Anterior = verticesPoligono[verticesPoligono.Count - 1];
                        verticesPoligono[i].Siguiente = verticesPoligono[i + 1];
                    }

                    else if (i == verticesPoligono.Count - 1)
                    {
                        verticesPoligono[i].Anterior = verticesPoligono[i - 1];
                        verticesPoligono[i].Siguiente = verticesPoligono[0];
                    }

                    else
                    {
                        verticesPoligono[i].Anterior = verticesPoligono[i - 1];
                        verticesPoligono[i].Siguiente = verticesPoligono[i + 1];
                    }
                }

                else
                {
                    if (i == 0)
                    {
                        verticesPoligono[i].Siguiente = verticesPoligono[verticesPoligono.Count - 1];
                        verticesPoligono[i].Anterior = verticesPoligono[i + 1];
                    }

                    else if (i == verticesPoligono.Count - 1)
                    {
                        verticesPoligono[i].Siguiente = verticesPoligono[i - 1];
                        verticesPoligono[i].Anterior = verticesPoligono[0];
                    }

                    else
                    {
                        verticesPoligono[i].Siguiente = verticesPoligono[i - 1];
                        verticesPoligono[i].Anterior = verticesPoligono[i + 1];
                    }
                }
            }
        }

        private int Area(Punto p1, Punto p2, Punto p3)
        {
            return (p1.X * p2.Y) + (p2.X * p3.Y) + (p3.X * p1.Y) -
                      (p1.X * p3.Y) - (p2.X * p1.Y) - (p3.X * p2.Y);
        }

        private double AreaPoligono()
        {
            double area = 0;
            for (int i = 1; i < verticesPoligono.Count - 2; i++)
                area += Area(verticesPoligono[0], verticesPoligono[i], verticesPoligono[i + 1]);

            return area;
        }

        private int Orientacion(Punto p)
        {
            int res = Area(p.Anterior, p, p.Siguiente);

            if (res == 0)
                return 0; //Colinear
            return (res > 0) ? 1 : -1; //Retorna 1 si el area es positiva y -1 si es negativa  
        }



        private bool InterseccionDeSegmentos(Punto p1, Punto p2,
                              Punto p3, Punto p4)
        {
            PointF i = new PointF();
            float mayor1;
            float menor1;
            float mayor2;
            float menor2;
            float A1 = p2.Y - p1.Y;
            float B1 = p1.X - p2.X;
            float C1 = A1 * p1.X + B1 * p1.Y;
            float A2 = p4.Y - p3.Y;
            float B2 = p3.X - p4.X;
            float C2 = A2 * p3.X + B2 * p3.Y;
            float denom = A1 * B2 - A2 * B1;

            if (denom == 0.0)
                return false;
            i.X = (C1 * B2 - C2 * B1) / denom;
            i.Y = (A1 * C2 - A2 * C1) / denom;

            if (p1.X > p2.X)
            {
                mayor1 = p1.X;
                menor1 = p2.X;

            }

            else
            {
                mayor1 = p2.X;
                menor1 = p1.X;
            }

            if (p3.X > p4.X)
            {
                mayor2 = p3.X;
                menor2 = p4.X;

            }
            else
            {
                mayor2 = p4.X;
                menor2 = p3.X;
            }

            if (i.X >= menor1 && i.X <= mayor1 &&
                i.X >= menor2 && i.X <= mayor2)
                return true;
            return false;
        }


        
        public int Dentro(Point punto)
        {
            Punto P = new Punto(0, 0);
            Punto po = new Punto(punto.X, punto.Y);
            int intersecciones = 0;

            if (AreaPoligono() < 0)
                antihorario = true;
            Asignar();

            for (int i = 0; i < verticesPoligono.Count; i++)
            {
                if (verticesPoligono[i].X == punto.X &&
                         verticesPoligono[i].Y == punto.Y)
                    return 3; //Retorna 3 si el punto esta en uno de los vertices
            }

            for (int i = 0; i < verticesPoligono.Count; i++)
            {
                if (Area(verticesPoligono[i], po, verticesPoligono[i].Siguiente) == 0)
                    return 2; //Retorna 2 si el punto esta en una de las aristas

                else if (InterseccionDeSegmentos(verticesPoligono[i], verticesPoligono[i].Siguiente,
                    P, po))
                    intersecciones++;
            }

            if (intersecciones % 2 == 0)
                return 1; //Retorna 1 si el punto esta fuera del poligono
            return 0; //Retorna 0 si el punto esta dentro del poligono
        }
    }
}
