namespace System.Utilities.Common
{
    using System.Utilities.Models;
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.Security.Cryptography;

    public class VerifyImageHelper
    {
        private static Bitmap charbmp = new Bitmap(40, 40);
        private static Font[] fonts = new Font[] { new Font(new FontFamily("Times New Roman"), (float) (0x10 + NumberHelper.Random(3)), FontStyle.Regular), new Font(new FontFamily("Georgia"), (float) (0x10 + NumberHelper.Random(3)), FontStyle.Regular), new Font(new FontFamily("Arial"), (float) (0x10 + NumberHelper.Random(3)), FontStyle.Regular), new Font(new FontFamily("Comic Sans MS"), (float) (0x10 + NumberHelper.Random(3)), FontStyle.Regular) };
        private static Matrix m = new Matrix();
        private static RNGCryptoServiceProvider rand = new RNGCryptoServiceProvider();
        private static byte[] randb = new byte[4];

        public VerifyImageInfo GenerateImage(string code, int width, int height, Color bgcolor, int textcolor)
        {
            VerifyImageInfo info = new VerifyImageInfo {
                ImageFormat = ImageFormat.Jpeg,
                ContentType = "image/pjpeg"
            };
            width = 90;
            height = 30;
            Bitmap image = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            Graphics graphics = Graphics.FromImage(image);
            graphics.SmoothingMode = SmoothingMode.HighSpeed;
            graphics.Clear(bgcolor);
            int num = (textcolor == 2) ? 60 : 0;
            Pen pen = new Pen(Color.FromArgb(NumberHelper.Random(50) + num, NumberHelper.Random(50) + num, NumberHelper.Random(50) + num), 1f);
            SolidBrush brush = new SolidBrush(Color.FromArgb(NumberHelper.Random(100), NumberHelper.Random(100), NumberHelper.Random(100)));
            for (int i = 0; i < 3; i++)
            {
                graphics.DrawArc(pen, NumberHelper.Random(20) - 10, NumberHelper.Random(20) - 10, NumberHelper.Random(width) + 10, NumberHelper.Random(height) + 10, NumberHelper.Random(-100, 100), NumberHelper.Random(-200, 200));
            }
            Graphics graphics2 = Graphics.FromImage(charbmp);
            float x = -18f;
            for (int j = 0; j < code.Length; j++)
            {
                m.Reset();
                m.RotateAt((float) (NumberHelper.Random(50) - 0x19), new PointF((float) (NumberHelper.Random(3) + 7), (float) (NumberHelper.Random(3) + 7)));
                graphics2.Clear(Color.Transparent);
                graphics2.Transform = m;
                brush.Color = Color.Black;
                x = (x + 18f) + NumberHelper.Random(5);
                PointF point = new PointF(x, 2f);
                graphics2.DrawString(code[j].ToString(), fonts[NumberHelper.Random((int) (fonts.Length - 1))], brush, new PointF(0f, 0f));
                graphics2.ResetTransform();
                graphics.DrawImage(charbmp, point);
            }
            brush.Dispose();
            graphics.Dispose();
            graphics2.Dispose();
            info.Image = image;
            return info;
        }
    }
}

