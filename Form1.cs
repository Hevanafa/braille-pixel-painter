using System.Drawing.Drawing2D;
using System.Text;

namespace BraillePixelEditor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            bmpArt = new DirectBitmap(16, 16);

            using (var g = Graphics.FromImage(bmpArt.Bitmap))
                g.Clear(Color.White);

            pbArt.BackgroundImage = bmpArt.Bitmap;

        }

        int zoomFactor = 8;

        readonly DirectBitmap bmpArt;
        //Bitmap displayBitmap;


        private void btnRender_Click(object sender, EventArgs e)
        {
            var result = new StringBuilder();

            for (var outerY = 0; outerY < bmpArt.Height / 3; outerY++)
            {
                for (var outerX = 0; outerX < bmpArt.Width / 2; outerX++)
                {
                    int[] cell = new int[6];

                    for (var y = 0; y < 3; y++)
                        for (var x = 0; x < 2; x++)
                        {
                            cell[2 - y + (1 - x) * 3] =
                                bmpArt.GetPixel(outerX * 2 + x, outerY * 3 + y).ToArgb() == Color.Black.ToArgb() ? 1 : 0;
                        }

                    result.Append((char)(0x2800 + Convert.ToInt32(string.Join("", cell), 2)));
                }

                result.Append(new char[] { (char) 13, (char) 10 });
            }

            txbBraille.Text = result.ToString();
        }

        bool isPainting;

        private void pbArt_MouseDown(object sender, MouseEventArgs e)
        {
            var (x, y) = (e.Location.X / zoomFactor, e.Location.Y / zoomFactor);

            if (!cbBucketTool.Checked)
            {
                isPainting = true;
                PutPixel(x, y);
            }
            else
            {
                FloodFill(x, y);
                pbArt.Refresh();
            }
        }

        bool InsideBounds(int x, int y) =>
            !(x < 0 || x >= bmpArt.Width || y < 0 || y >= bmpArt.Height);

        void FloodFill(int x, int y)
        {
            if (!InsideBounds(x, y)) return;
            if (bmpArt.GetPixel(x, y).ToArgb() ==
                (cbBlackFill.Checked ? Color.Black : Color.White).ToArgb()) return;

            bmpArt.SetPixel(
                x, y,
                cbBlackFill.Checked ? Color.Black : Color.White);

            FloodFill(x - 1, y);
            FloodFill(x + 1, y);
            FloodFill(x, y - 1);
            FloodFill(x, y + 1);
        }

        void PutPixel(int x, int y) {
            if (!InsideBounds(x, y)) return;

            var black = cbBlackFill.Checked;

            bmpArt.SetPixel(
                x, y,
                black ? Color.Black : Color.White);

            pbArt.Refresh();
        }

        private void pbArt_MouseUp(object sender, MouseEventArgs e)
        {
            isPainting = false;
        }

        private void pbArt_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isPainting) return;

            var (x, y) = (e.Location.X / zoomFactor, e.Location.Y / zoomFactor);

            PutPixel(x, y);
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            isPainting = false;
        }


        private void pbArt_Paint(object sender, PaintEventArgs e)
        {
            // https://stackoverflow.com/questions/54720916/
            e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.Half;

            e.Graphics.DrawImage(
                bmpArt.Bitmap,
                new Rectangle { X = 0, Y = 0, Width = pbArt.Width, Height = pbArt.Height });
        }
    }
}