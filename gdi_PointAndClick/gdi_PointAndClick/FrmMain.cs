using System.Collections.Generic; 
namespace gdi_PointAndClick
{
    public partial class FrmMain : Form
    {
        List<Rectangle> rectangles = new List<Rectangle>();
        List<Color> rectangleColors = new List<Color>();
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

            for (int i = 0; i < rectangles.Count; i++)
            {
                Brush b = new SolidBrush(rectangleColors[i]); 
                g.FillRectangle(b, rectangles[i]);
            }

        }

        private void FrmMain_MouseClick(object sender, MouseEventArgs e)
        {
            Point mausposition = e.Location;

            if (e.Button == MouseButtons.Left) 
            {
                int randomzahl = random.Next(5, 101);
                Rectangle r = new Rectangle(mausposition.X - 20, mausposition.Y - 20, randomzahl, randomzahl);

                if (!IstQuadratUeberschnitten(r))
                {
                    rectangles.Add(r);

                    Color randomColor = Color.FromArgb((int)(0xFF << 24 ^ (random.Next(0xFFFFFF) & 0x7F7F7F)));
                    rectangleColors.Add(randomColor);

                    Refresh();
                }
            }
            else if (e.Button == MouseButtons.Right) 
            {
                for (int i = rectangles.Count - 1; i >= 0; i--)
                {
                    if (rectangles[i].Contains(mausposition))
                    {
                        rectangles.RemoveAt(i);
                        rectangleColors.RemoveAt(i);
                        Refresh();
                        break; 
                    }
                }
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