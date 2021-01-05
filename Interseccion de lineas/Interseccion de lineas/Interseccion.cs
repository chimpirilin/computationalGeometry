using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Interseccion_de_lineas
{
    class Interseccion
    {
        struct Segmentos
        {
            public Segmentos(PointF A, PointF B)
            {
                a = A;
                b = B;
            }
            public PointF a;
            public PointF b;
        }

        private List<Segmentos> Puntos = new List<Segmentos>();

        public void AgregarPuntos(PointF a, PointF b)
        {
            Segmentos p = new Segmentos(a, b);
            Puntos.Add(p);
        }

        public void Vaciar()
        {
            Puntos.Clear();
        }

        private bool InteseccionDeSegmentos(PointF p1, PointF p2, 
                              PointF p3, PointF p4, ref PointF i)
        {
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

        public void Calcular(Form form)
        {
            PointF interseccion = new PointF();
            for (int i = 0; i < Puntos.Count - 1; i++)
            {
                for (int j = 0; j < Puntos.Count; j++)
                {
                    if (j != i)
                    {
                        if (InteseccionDeSegmentos(Puntos[i].a, Puntos[i].b, 
                            Puntos[j].a, Puntos[j].b, ref interseccion))
                        {
                            Graphics graphics = form.CreateGraphics();
                            graphics.FillEllipse(new SolidBrush(Color.Red), 
                                interseccion.X - 2, interseccion.Y - 2, 5, 5);
                        }
                    }
                }
            }
        }
    }
}
