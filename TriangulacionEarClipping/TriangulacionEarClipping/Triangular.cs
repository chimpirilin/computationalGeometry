using System.Drawing;
using System.Collections.Generic;

namespace TriangulacionEarClipping
{
    class Triangular
    {
        private List<Punto> verticesPoligono = new List<Punto>();
        List<Punto> copia = new List<Punto>();
        private bool antihorario;

        public void AgregarVertices(int x, int y)
        {
            Punto vertice = new Punto(x, y, verticesPoligono.Count);
            verticesPoligono.Add(vertice);
        }

        public void AgregarVerticesCopia(int x, int y)
        {
            Punto vertice = new Punto(x, y, copia.Count);
            copia.Add(vertice);
        }

        public void Vaciar()
        {
            verticesPoligono.Clear();
            copia.Clear();
        }

        private void Asignar(List<Punto> p) //Asigna siguiente y anterior a cada punto
        {
            if (AreaPoligono() < 0)
                antihorario = true;
            else
                antihorario = false;

            for (int i = 0; i < p.Count; i++)
            {
                if (antihorario)
                {
                    if (i == 0)
                    {
                        p[i].Anterior = p[p.Count - 1];
                        p[i].Siguiente = p[i + 1];
                    }

                    else if (i == p.Count - 1)
                    {
                        p[i].Anterior = p[i - 1];
                        p[i].Siguiente = p[0];
                    }

                    else
                    {
                        p[i].Anterior = p[i - 1];
                        p[i].Siguiente = p[i + 1];
                    }
                }

                else
                {
                    if (i == 0)
                    {
                        p[i].Siguiente = p[p.Count - 1];
                        p[i].Anterior = p[i + 1];
                    }

                    else if (i == p.Count - 1)
                    {
                        p[i].Siguiente = p[i - 1];
                        p[i].Anterior = p[0];
                    }

                    else
                    {
                        p[i].Siguiente = p[i - 1];
                        p[i].Anterior = p[i + 1];
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

        private bool EsOreja(Punto p)
        {
            int intercepciones = 0;
            if (Orientacion(p) > 0 || Orientacion(p) == 0)
                return false;
            for (int i = 0; i < copia.Count; i++)            
                if (InterseccionDeSegmentos(p.Anterior, p.Siguiente, copia[i], copia[i].Siguiente))
                    intercepciones++;

            if (intercepciones < 5)
                return true;
            else
                return false;
        }

        private bool EsPoligonoSimple()
        {
            int intercepciones = 0;
            Punto p = copia[0];
            Punto q = p.Siguiente;
            for (int i = 0; i < copia.Count; i++)
            {
                intercepciones = 0;
                for (int j = 0; j < copia.Count; j++)
                {
                    if (InterseccionDeSegmentos(p, p.Siguiente, q, q.Siguiente))
                        intercepciones++;
                    q = q.Siguiente;
                }

                if (intercepciones > 2)
                    return false;
                p = p.Siguiente;
            }
            return true;
        }

        public bool triangular(Form1 form)
        {
            Punto p = verticesPoligono[0];
            Punto temp = p;
            if (AreaPoligono() < 0)
                antihorario = true;
            Asignar(verticesPoligono);
            Asignar(copia);

            int tam = verticesPoligono.Count;

            while (p.Siguiente != p.Anterior)
            {
                if (!EsPoligonoSimple())
                    return false;

                if (EsOreja(p))
                {
                    Graphics grafico = form.CreateGraphics();
                    grafico.DrawLine(new Pen(Color.Black), p.Anterior.Vertice, p.Siguiente.Vertice);
                    p.Anterior.Siguiente = p.Siguiente;
                    p.Siguiente.Anterior = p.Anterior;
                    p = p.Siguiente;
                    verticesPoligono.Remove(temp);
                    temp = p;
                }

                else
                    p = p.Siguiente;                
            }
            return true;
        }
    }
}
