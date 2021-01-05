using System.Drawing;

namespace PuntoEnPoligono
{
    class Punto
    {
        private Point vertice;
        private Punto siguiente;
        private Punto anterior;

        public Punto(int X, int Y)
        {
            vertice = new Point(X, Y);
        }

        public Point Vertice
        {
            get
            {
                return vertice;
            }
        }

        public int X
        {
            set
            {
                vertice.X = value;
            }

            get
            {
                return vertice.X;
            }
        }

        public int Y
        {
            set
            {
                vertice.Y = value;
            }

            get
            {
                return vertice.Y;
            }
        }

        public Punto Siguiente
        {
            set
            {
                siguiente = value;
            }

            get
            {
                return siguiente;
            }
        }

        public Punto Anterior
        {
            set
            {
                anterior = value;
            }

            get
            {
                return anterior;
            }
        }
    }
}
