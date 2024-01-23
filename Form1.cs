using System.Drawing.Drawing2D;
using System.Text;
using System.Drawing.Imaging;
using System.Diagnostics;
using System.Drawing;

namespace BraillePixelEditor
{
    public partial class Form1 : Form, IMessageFilter
    {
        public Form1()
        {
            InitializeComponent();

            bmpArt = new DirectBitmap(16, 16);

            using (var g = Graphics.FromImage(bmpArt.Bitmap))
                g.Clear(Color.White);

            pbArt.BackgroundImage = bmpArt.Bitmap;
            pbArt.Focus();

            Application.AddMessageFilter(this);
        }

        //const int MK_CONTROL = 0x0008;
        const int WM_KEYDOWN = 0x0100;

        public bool PreFilterMessage(ref Message m) {
            if (m.Msg == WM_KEYDOWN) {
                var key = (Keys)m.WParam & Keys.KeyCode;
                //Debug.Print($"{key}");

                if (key == Keys.Z && Control.ModifierKeys == Keys.Control) {
                    //Debug.Print("Undo!");
                    PerformUndo();

                    return true;
                }
            }

            return false;
        }

        ActionManager actionMan = new();

        bool hasChanged;
        int zoomFactor = 8;

        DirectBitmap bmpArt;
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

                result.Append(new char[] { (char)13, (char)10 });
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
                var lastColour = bmpArt.GetPixel(x, y);

                if (PutPixel(x, y))
                {
                    hasChanged = true;
                }
            }
            else
            {
                FloodFill(x, y);
                hasChanged = true;
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

        /// <returns>true if inside bounds</returns>
        bool PutPixel(int x, int y)
        {
            if (!InsideBounds(x, y)) return false;

            var lastColour = bmpArt.GetPixel(x, y);

            var black = cbBlackFill.Checked;
            var newColour = black ? Color.Black : Color.White;

            if (lastColour.ToArgb() == newColour.ToArgb()) return false;

            actionMan.push(new Action()
            {
                Type = ActionTypes.PutPixel,
                position = new Point(x, y),
                lastColour = lastColour
            });

            bmpArt.SetPixel(
                x, y,
                newColour);

            pbArt.Refresh();

            return true;
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

        /// <summary>
        /// https://stackoverflow.com/questions/116050/
        /// </summary>
        readonly SaveFileDialog sfd = new()
        {
            Filter = "PNG image|*.png|All files|*.*",
            InitialDirectory = Environment.GetFolderPath(
                    Environment.SpecialFolder.MyPictures)
        };

        readonly OpenFileDialog ofd = new()
        {
            Filter = "PNG image|*.png|All files|*.*",
            InitialDirectory = Environment.GetFolderPath(
                    Environment.SpecialFolder.MyPictures),
            Multiselect = false
        };

        private void btnSave_Click(object sender, EventArgs e)
        {
            Debug.Print(sfd.InitialDirectory);

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                var filename = sfd.FileName;

                try
                {
                    bmpArt.Bitmap.Save(filename, ImageFormat.Png);
                    hasChanged = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to save as PNG. Reason: " + ex.Message, "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (hasChanged && MessageBox.Show("You have unsaved changes. Continue?", "Load PNG", MessageBoxButtons.YesNo) == DialogResult.No) return;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var filename = ofd.FileName;

                    using (var tempBmp = (Bitmap)Image.FromFile(filename))
                        for (var y = 0; y < bmpArt.Height; y++)
                            for (var x = 0; x < bmpArt.Width; x++)
                                bmpArt.SetPixel(x, y,
                                    // performance bottleneck
                                    DirectBitmap.GetBrightness(tempBmp.GetPixel(x, y)) >= 0.5
                                    ? Color.White
                                    : Color.Black);

                    pbArt.Refresh();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to load PNG file. Reason: " + ex.Message, "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        void PerformUndo() {
            var lastAction = actionMan.pop();
            if (lastAction == null) return;

            switch (lastAction.Type)
            {
                case ActionTypes.PutPixel:
                    bmpArt.SetPixel(
                        lastAction.position.Value.X,
                        lastAction.position.Value.Y,
                        lastAction.lastColour.Value);

                    pbArt.Refresh();

                    break;

                default:
                    break;
            }
        }
    }
}