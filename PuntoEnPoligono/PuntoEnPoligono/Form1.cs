using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace PuntoEnPoligono
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private List<Point> Puntos = new List<Point>();
        private List<Point> L = new List<Point>();

        private PuntoEnPoligono P = new PuntoEnPoligono();
        private bool band;

        public void Vaciar()
        {
            Puntos.Clear();
            L.Clear();
            P.Vaciar();
            band = false;
        }

        public void Agregar(int x, int y)
        {
            Point vertice = new Point(x, y);
            L.Add(vertice);
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            using (Graphics grafico = CreateGraphics())
            {
                if (!band)
                {
                    P.AgregarPunto(e.X, e.Y);
                    Agregar(e.X, e.Y);
                    grafico.FillEllipse(new SolidBrush(Color.Black), e.X - 2, e.Y - 2, 5, 5);
                    if (L.Count > 1)
                        grafico.DrawLines(new Pen(Color.Black), L.ToArray());
                }

                else
                {
                    grafico.FillEllipse(new SolidBrush(Color.Black), e.X - 2, e.Y - 2, 5, 5);
                    Puntos.Add(new Point(e.X, e.Y));
                }
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            using (Graphics grafico = CreateGraphics())
            {
                if (band)
                {
                    if (e.KeyCode == Keys.Enter)
                    {
                        for (int i = 0; i < Puntos.Count; i++)
                        {
                            if (P.Dentro(Puntos[i]) == 0)
                                grafico.FillEllipse(new SolidBrush(Color.Blue), Puntos[i].X - 2, Puntos[i].Y - 2, 5, 5);
                            else if (P.Dentro(Puntos[i]) == 1)
                                grafico.FillEllipse(new SolidBrush(Color.Red), Puntos[i].X - 2, Puntos[i].Y - 2, 5, 5);
                            else if (P.Dentro(Puntos[i]) == 2)
                                grafico.FillEllipse(new SolidBrush(Color.ForestGreen), Puntos[i].X - 2, Puntos[i].Y - 2, 5, 5);
                            else if (P.Dentro(Puntos[i]) == 3)
                                grafico.FillEllipse(new SolidBrush(Color.Yellow), Puntos[i].X - 2, Puntos[i].Y - 2, 5, 5);
                        }
                    }
                }

                else
                {
                    if (e.KeyCode == Keys.Enter)
                    {
                        grafico.FillPolygon(new SolidBrush(Color.White), L.ToArray());
                        grafico.DrawPolygon(new Pen(Color.Black), L.ToArray());

                        for (int i = 0; i < L.Count; i++)
                            grafico.FillEllipse(new SolidBrush(Color.Black), L[i].X - 2, L[i].Y - 2, 5, 5);
                        band = true;
                    }
                }
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            LabelX.Text = "X: " + e.X;
            LabelY.Text = "Y: " + e.Y;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            using (Graphics grafico = CreateGraphics())
            {
                if (band)
                {
                    for (int i = 0; i < Puntos.Count; i++)
                    {
                        if (P.Dentro(Puntos[i]) == 0)
                            grafico.FillEllipse(new SolidBrush(Color.Blue), Puntos[i].X - 2, Puntos[i].Y - 2, 5, 5);
                        else if (P.Dentro(Puntos[i]) == 1)
                            grafico.FillEllipse(new SolidBrush(Color.Red), Puntos[i].X - 2, Puntos[i].Y - 2, 5, 5);
                        else if (P.Dentro(Puntos[i]) == 2)
                            grafico.FillEllipse(new SolidBrush(Color.ForestGreen), Puntos[i].X - 2, Puntos[i].Y - 2, 5, 5);
                        else if (P.Dentro(Puntos[i]) == 3)
                            grafico.FillEllipse(new SolidBrush(Color.Yellow), Puntos[i].X - 2, Puntos[i].Y - 2, 5, 5);
                    }
                }

                else
                {
                    grafico.FillPolygon(new SolidBrush(Color.White), L.ToArray());
                    grafico.DrawPolygon(new Pen(Color.Black), L.ToArray());

                    for (int i = 0; i < L.Count; i++)
                        grafico.FillEllipse(new SolidBrush(Color.Black), L[i].X - 2, L[i].Y - 2, 5, 5);
                    band = true;
                }
            }
        }

        private void helpToolStripButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Solo tienes que dar clicks para dibujar el poligono y luego presionar el segundo boton o presionar Enter para dibujar los puntos, luego presionar el segundo boton o presionar Enter para ver si el punto esta en el poligono.\r\nAzul = Punto dentro del poligono, \r\nRojo = Punto fuera del poligono, \r\nVerde = Punto es colineal con una de las aristas, \r\nAmarillo = Punto esta en uno de los vertices", "Ayuda", MessageBoxButtons.OK, MessageBoxIcon.Question);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Este es un programa para determinar si un punto esta dentro de un poligono simple (o no simple). Desarrollado por Hans Pierre Blanco, estudiante de ingeniería en computación, UNI-Nicaragua", "Acerca de...", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            using (Graphics g = CreateGraphics())
                g.Clear(Color.FromArgb(240, 240, 240));
            Vaciar();

        }
    }
}
