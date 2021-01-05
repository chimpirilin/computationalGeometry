using System.Drawing;
using System.Windows.Forms;

namespace Interseccion_de_lineas
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private Interseccion interseccion = new Interseccion();
        private int band;
        private PointF a = new PointF();
        private PointF b = new PointF();


        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            band++;
            
            using (Graphics graphics = CreateGraphics())
            {
                graphics.FillEllipse(new SolidBrush(Color.Black), e.X - 2, e.Y - 2, 5, 5);
                if (band == 2)
                {
                    b.X = e.X;
                    b.Y = e.Y;
                    graphics.DrawLine(new Pen(Color.Black), a, b);
                    interseccion.AgregarPuntos(a, b);
                    band = 0;
                }

                else if (band == 1)
                {
                    a.X = e.X;
                    a.Y = e.Y;
                }
            }
        }

        private void toolStripButton1_Click(object sender, System.EventArgs e)
        {
            if (band == 0)
                interseccion.Calcular(this);
        }

        private void helpToolStripButton_Click(object sender, System.EventArgs e)
        {         
            MessageBox.Show("Solo tienes que dar clicks para trazar los segmentos y luego presionar el boton calcular o presionar Enter para calcular la interseccion de los segmentos", "Ayuda", MessageBoxButtons.OK, MessageBoxIcon.Question);
        }

        private void toolStripButton2_Click(object sender, System.EventArgs e)
        {
            MessageBox.Show("Este es un programa que calcula la interseccion de segmentos. Desarrollado por Hans Pierre Blanco, estudiante de ingeniería en computación, UNI-Nicaragua", "acerca de...", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            toolStripLabel1.Text = "X: " + e.X;
            toolStripLabel2.Text = "Y: " + e.Y;
        }

        private void newToolStripButton_Click(object sender, System.EventArgs e)
        {
            using (Graphics grafico = CreateGraphics())
                grafico.Clear(Color.FromArgb(240, 240, 240));
            interseccion.Vaciar();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (band == 0)
                interseccion.Calcular(this);
        }
    }
}
