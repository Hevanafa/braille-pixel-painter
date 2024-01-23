using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace BraillePixelEditor
{
    /// <summary>
    /// https://stackoverflow.com/questions/24701703
    /// </summary>
    internal class DirectBitmap : IDisposable
    {
        public Bitmap Bitmap { get; private set; }
        public int[] Bits { get; private set; }
        public bool Disposed { get; private set; }
        public int Height { get; private set; }
        public int Width { get; private set; }

        protected GCHandle BitsHandle { get; private set; }

        public DirectBitmap(int width, int height)
        {
            Width = width;
            Height = height;
            Bits = new int[width * height];
            BitsHandle = GCHandle.Alloc(Bits, GCHandleType.Pinned);
            Bitmap = new Bitmap(width, height, width * 4, PixelFormat.Format32bppPArgb, BitsHandle.AddrOfPinnedObject());
        }

        /// <summary>
        /// https://stackoverflow.com/questions/26233781/
        /// </summary>
        public static double GetBrightness(Color colour) => 0.2126 * colour.R + 0.7152 * colour.G + 0.0722 * colour.B;

        [Obsolete("Better use the load PNG button for now.")]
        public DirectBitmap(Bitmap bitmap)
        {
            Width = bitmap.Width;
            Height = bitmap.Height;
            BitsHandle = GCHandle.Alloc(Bits, GCHandleType.Pinned);
            Bitmap = bitmap;

            Bits = Enumerable.Range(0, Height).Select(y =>
                Enumerable.Range(0, Width).Select(x =>
                    (GetBrightness(bitmap.GetPixel(x, y)) >= 0.5 ? Color.White : Color.Black).ToArgb()
                )).SelectMany(x => x).ToArray();
        }

        public void SetPixel(int x, int y, Color colour)
        {
            int index = x + (y * Width);
            int col = colour.ToArgb();

            Bits[index] = col;
        }

        public Color GetPixel(int x, int y)
        {
            int index = x + (y * Width);
            int col = Bits[index];
            Color result = Color.FromArgb(col);

            return result;
        }

        public void Dispose()
        {
            if (Disposed) return;
            Disposed = true;
            Bitmap.Dispose();
            BitsHandle.Free();
        }
    }
}
