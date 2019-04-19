namespace System.Utilities.Common
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public static class ImageHelper
    {
        public static readonly string AllowExt = ".jpe|.jpeg|.jpg|.png|.tif|.tiff|.bmp";

        public static void AddWaterPic(string imagePath, string waterImagePath)
        {
            Image image = Image.FromFile(imagePath);
            Image image2 = Image.FromFile(waterImagePath);
            if ((image != null) && (image2 != null))
            {
                Graphics graphics = Graphics.FromImage(image);
                graphics.DrawImage(image2, new Rectangle(image.Width - image2.Width, image.Height - image2.Height, image2.Width, image2.Height), 0, 0, image2.Width, image2.Height, GraphicsUnit.Pixel);
                graphics.Dispose();
                image.Save(imagePath + ".temp");
                image.Dispose();
                File.Delete(imagePath);
                File.Move(imagePath + ".temp", imagePath);
            }
        }

        public static Bitmap AdjustBrightness(this Bitmap Image, int Value)
        {
            if ((Value >= 0) && (Value <= 100))
            {
                Value = 100 - Value;
                Bitmap image = Image;
                float num = ((float) Value) / 255f;
                Bitmap bitmap2 = new Bitmap(image.Width, image.Height);
                Graphics graphics = Graphics.FromImage(bitmap2);
                float[][] numArray2 = new float[5][];
                float[] numArray3 = new float[5];
                numArray3[0] = 1f;
                numArray2[0] = numArray3;
                float[] numArray4 = new float[5];
                numArray4[1] = 1f;
                numArray2[1] = numArray4;
                float[] numArray5 = new float[5];
                numArray5[2] = 1f;
                numArray2[2] = numArray5;
                float[] numArray6 = new float[5];
                numArray6[3] = 1f;
                numArray2[3] = numArray6;
                numArray2[4] = new float[] { num, num, num, 1f, 1f };
                float[][] newColorMatrix = numArray2;
                ColorMatrix matrix = new ColorMatrix(newColorMatrix);
                ImageAttributes imageAttr = new ImageAttributes();
                imageAttr.SetColorMatrix(matrix);
                graphics.DrawImage(image, new Rectangle(0, 0, image.Width, image.Height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, imageAttr);
                imageAttr.Dispose();
                graphics.Dispose();
                return bitmap2;
            }
            return Image;
        }

        public static bool CompareByArray(this Bitmap b1, Bitmap b2)
        {
            IntPtr ptr = new IntPtr(-1);
            MemoryStream stream = new MemoryStream();
            try
            {
                b1.Save(stream, ImageFormat.Png);
                byte[] buffer = stream.ToArray();
                stream.Position = 0L;
                b2.Save(stream, ImageFormat.Png);
                byte[] buffer2 = stream.ToArray();
                ptr = memcmp(buffer, buffer2, new IntPtr(buffer.Length));
            }
            finally
            {
                stream.Close();
            }
            return (ptr.ToInt32() == 0);
        }

        public static bool CompareByBase64String(this Bitmap b1, Bitmap b2)
        {
            string str;
            string str2;
            MemoryStream stream = new MemoryStream();
            try
            {
                b1.Save(stream, ImageFormat.Png);
                str = Convert.ToBase64String(stream.ToArray());
                stream.Position = 0L;
                b2.Save(stream, ImageFormat.Png);
                str2 = Convert.ToBase64String(stream.ToArray());
            }
            finally
            {
                stream.Close();
            }
            return str.Equals(str2);
        }

        public static bool CompareByMemCmp(this Bitmap b1, Bitmap b2)
        {
            bool flag;
            if ((b1 == null) != (b2 == null))
            {
                return false;
            }
            if (b1.Size != b2.Size)
            {
                return false;
            }
            BitmapData bitmapdata = b1.LockBits(new Rectangle(new Point(0, 0), b1.Size), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            BitmapData data2 = b2.LockBits(new Rectangle(new Point(0, 0), b2.Size), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            try
            {
                IntPtr ptr = bitmapdata.Scan0;
                IntPtr ptr2 = data2.Scan0;
                int num2 = bitmapdata.Stride * b1.Height;
                flag = memcmp(ptr, ptr2, (long) num2) == 0;
            }
            finally
            {
                b1.UnlockBits(bitmapdata);
                b2.UnlockBits(data2);
            }
            return flag;
        }

        public static bool CompareByPixel(this Bitmap b1, Bitmap b2)
        {
            bool flag = false;
            if ((b1.Width == b2.Width) && (b1.Height == b2.Height))
            {
                flag = true;
                for (int i = 0; i < b1.Width; i++)
                {
                    for (int j = 0; j < b1.Height; j++)
                    {
                        Color pixel = b1.GetPixel(i, j);
                        Color color2 = b2.GetPixel(i, j);
                        if (pixel != color2)
                        {
                            flag = false;
                            break;
                        }
                    }
                }
            }
            return flag;
        }

        [DllImport("msvcrt.dll")]
        private static extern int memcmp(IntPtr b1, IntPtr b2, long count);
        [DllImport("msvcrt.dll")]
        private static extern IntPtr memcmp(byte[] b1, byte[] b2, IntPtr count);
        public static Bitmap MergerImage(int mergerImageWidth, int mergerImageHeight, int imageX, int imageY, Color backgroundColor, params Bitmap[] maps)
        {
            int length = maps.Length;
            Bitmap image = new Bitmap(mergerImageWidth, mergerImageHeight);
            using (Graphics graphics = Graphics.FromImage(image))
            {
                graphics.Clear(Color.White);
                for (int i = 0; i < length; i++)
                {
                    graphics.DrawImage(maps[i], i * imageX, imageY, maps[i].Width, maps[i].Height);
                }
            }
            return image;
        }

        public static Bitmap ToBitmap(string imagePath)
        {
            return (Bitmap) Image.FromFile(imagePath, false);
        }

        public static byte[] ToBytes(this Bitmap bitmap)
        {
            return bitmap.ToBytes(bitmap.RawFormat);
        }

        public static byte[] ToBytes(this Bitmap bitmap, ImageFormat format)
        {
            MemoryStream stream = null;
            byte[] buffer2;
            try
            {
                stream = new MemoryStream();
                bitmap.Save(stream, format);
                byte[] buffer = new byte[stream.Length];
                buffer2 = stream.ToArray();
            }
            finally
            {
                stream.Close();
            }
            return buffer2;
        }

        public static byte[] ToBytes(this Image Image, ImageFormat imageFormat)
        {
            byte[] buffer = null;
            using (MemoryStream stream = new MemoryStream())
            {
                using (Bitmap bitmap = new Bitmap(Image))
                {
                    bitmap.Save(stream, imageFormat);
                    stream.Position = 0L;
                    buffer = new byte[stream.Length];
                    stream.Read(buffer, 0, Convert.ToInt32(stream.Length));
                    stream.Flush();
                }
            }
            return buffer;
        }

        public static Image ToImage(byte[] byteArray)
        {
            using (MemoryStream stream = new MemoryStream(byteArray))
            {
                Image image = Image.FromStream(stream);
                stream.Flush();
                return image;
            }
        }
    }
}

