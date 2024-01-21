using System.Collections.Generic; // benötigt für Listen

namespace gdi_PointAndClick
{
    public partial class FrmMain : Form
    {
        List<Rectangle> rectangles = new List<Rectangle>();
        Random random = new Random();

        private bool IstQuadratUeberschnitten(Rectangle newRectangle)
        {
            foreach (Rectangle rect in rectangles)
            {
                if (rect.IntersectsWith(newRectangle))
                {
                    return true;
                }
            }
            return false;
        }
        public FrmMain()
        {
            InitializeComponent();
            ResizeRedraw = true;
        }

        private void FrmMain_Paint(object sender, PaintEventArgs e)
        {
            // Hilfsvarablen
            Graphics g = e.Graphics;
            int w = this.ClientSize.Width;
            int h = this.ClientSize.Height;

            // Zeichenmittel
            Brush b = new SolidBrush(Color.Lavender);

            foreach (Rectangle rect in rectangles)
            {
                g.FillRectangle(b, rect);
            }

        }

        private void FrmMain_MouseClick(object sender, MouseEventArgs e)
        {
            Point mausposition = e.Location;

            int randomzahl = random.Next(5, 101);

            Rectangle r = new Rectangle(mausposition.X-20, mausposition.Y-20, randomzahl, randomzahl);

            if (!IstQuadratUeberschnitten(r))
            {
                rectangles.Add(r);
                Refresh();
            }


        }

        private void FrmMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                rectangles.Clear();
                Refresh();
            }
        }
    }
}