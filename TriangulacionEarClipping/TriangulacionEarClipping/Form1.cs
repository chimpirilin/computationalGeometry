using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace TriangulacionEarClipping
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            t = new Triangular();
            l = new List<Point>();
        }

        private Triangular t;
        private List<Point> l;
        bool band = true;        

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (band)
            {                
                t.AgregarVertices(e.X, e.Y);
                t.AgregarVerticesCopia(e.X, e.Y);
                Agregar(e.X, e.Y);
                using (Graphics graphics = CreateGraphics())
                {
                    graphics.FillEllipse(new SolidBrush(Color.Black), e.X - 2, e.Y - 2, 5, 5);
                    if (l.Count > 1)
                    {
                        graphics.DrawLines(new Pen(Color.Black), l.ToArray());
                    }
                }
            }
        }

        private void Agregar(int x, int y)
        {
            Point p = new Point(x, y);
            l.Add(p);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (l.Count() > 3)
                {
                    if (band)
                    {
                        using (Graphics grafico = CreateGraphics())
                        {
                            grafico.FillPolygon(new SolidBrush(Color.White), l.ToArray());
                            grafico.DrawPolygon(new Pen(Color.Black), l.ToArray());

                            for (int i = 0; i < l.Count; i++)
                                grafico.FillEllipse(new SolidBrush(Color.Black), l[i].X - 2, l[i].Y - 2, 5, 5);
                        }

                        band = false;

                        if (!t.triangular(this))
                        {
                            MessageBox.Show("No es un poligono simple", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Limpiar();
                        }
                    }                    
                }
            }
        }

        private void Limpiar()
        {
            t.Vaciar();
            l.Clear();
            using (Graphics grafico = CreateGraphics())
                grafico.Clear(Color.FromArgb(240, 240, 240));
            band = true;
        }

        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            Limpiar();            
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (l.Count() > 3)
            {
                if (band)
                {
                    using (Graphics grafico = CreateGraphics())
                    {
                        grafico.FillPolygon(new SolidBrush(Color.White), l.ToArray());
                        grafico.DrawPolygon(new Pen(Color.Black), l.ToArray());

                        for (int i = 0; i < l.Count; i++)
                            grafico.FillEllipse(new SolidBrush(Color.Black), l[i].X - 2, l[i].Y - 2, 5, 5);
                    }

                    band = false;

                    if (!t.triangular(this))
                    {
                        MessageBox.Show("No es un poligono simple", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Limpiar();
                    }
                }
            }
        }

        private void helpToolStripButton_Click(object sender, System.EventArgs e)
        {
            MessageBox.Show("Solo tienes que dar clicks para dibujar el poligono y luego presionar el boton calcular o presionar Enter para triangular", "Ayuda", MessageBoxButtons.OK, MessageBoxIcon.Question);
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Este es un programa triangula un poligono simple usando el algoritmo 'Ear Clipping'. Desarrollado por Hans Pierre Blanco, estudiante de ingeniería en computación, UNI-Nicaragua", "Acerca de...", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            toolStripLabel1.Text = "X: " + e.X;
            toolStripLabel2.Text = "Y: " + e.Y;
        }
    }
}
