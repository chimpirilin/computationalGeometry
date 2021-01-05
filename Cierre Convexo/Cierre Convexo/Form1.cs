using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Cierre_Convexo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        private CierreConvexo cierreConvexo = new CierreConvexo();
        private bool dibujarPuntos = true;

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (dibujarPuntos)
            {
                using (Graphics grafico = CreateGraphics())
                    grafico.FillEllipse(new SolidBrush(Color.Black), e.X - 2, e.Y - 2, 5, 5);
                cierreConvexo.AgegarPunto(e.X, e.Y);
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            toolStripLabel1.Text = "X: " + e.X;
            toolStripLabel2.Text = "Y: " + e.Y;
        }

        private void toolStripButton1_Click(object sender, System.EventArgs e)
        {
            MessageBox.Show("Este es un programa que calcula el cierre convexo dado los vertices de un poligono usando el algoritmo 'Monotone Chain'. Desarrollado por Hans Pierre Blanco, estudiante de ingeniería en computación, UNI-Nicaragua", "Acerca de...", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void helpToolStripButton_Click(object sender, System.EventArgs e)
        {
            MessageBox.Show("Solo tienes que dar clicks para generar los puntos y luego presionar el segundo boton o presionar Enter para generar el cierre convexo", "Ayuda", MessageBoxButtons.OK, MessageBoxIcon.Question);
        }

        private void newToolStripButton_Click(object sender, System.EventArgs e)
        {
            cierreConvexo.VaciarTodo();
            using (Graphics grafico = CreateGraphics())
                grafico.Clear(Color.FromArgb(240, 240, 240));
            toolStripButton2.Enabled = true;
            dibujarPuntos = true;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && cierreConvexo.Tam() > 2 && dibujarPuntos)
            {
                using (Graphics grafico = CreateGraphics())
                {
                    List<Point> GraficarPuntos = cierreConvexo.CalcularCierreConvexo();
                    grafico.DrawPolygon(new Pen(Color.Black), GraficarPuntos.ToArray());
                }
                toolStripButton2.Enabled = false;
                dibujarPuntos = false;
            }
        }

        private void toolStripButton2_Click(object sender, System.EventArgs e)
        {
            if (cierreConvexo.Tam() > 2)
            {
                using (Graphics grafico = CreateGraphics())
                {
                    List<Point> GraficarPuntos = cierreConvexo.CalcularCierreConvexo();
                    grafico.DrawPolygon(new Pen(Color.Black), GraficarPuntos.ToArray());
                }
                toolStripButton2.Enabled = false;
                dibujarPuntos = false;
            }
        }
    }
}
