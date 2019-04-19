namespace System.Utilities.Common
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;

    public class ThumbHelper
    {
        public static void MakeThumbnail(string originalImagePath, string thumbnailPath, int width, int height, string mode, string type)
        {
            string str;
            Image image = Image.FromFile(originalImagePath);
            int num = width;
            int num2 = height;
            int x = 0;
            int y = 0;
            int num5 = image.Width;
            int num6 = image.Height;
            if (((str = mode) != null) && (str != "HW"))
            {
                if (str == "W")
                {
                    num2 = (image.Height * width) / image.Width;
                }
                else if (str == "H")
                {
                    num = (image.Width * height) / image.Height;
                }
                else if (str == "Cut")
                {
                    if ((((double) image.Width) / ((double) image.Height)) > (((double) num) / ((double) num2)))
                    {
                        num6 = image.Height;
                        num5 = (image.Height * num) / num2;
                        y = 0;
                        x = (image.Width - num5) / 2;
                    }
                    else
                    {
                        num5 = image.Width;
                        num6 = (image.Width * height) / num;
                        x = 0;
                        y = (image.Height - num6) / 2;
                    }
                }
                else if (str == "DB")
                {
                    if ((((double) image.Width) / ((double) num)) < (((double) image.Height) / ((double) num2)))
                    {
                        num2 = height;
                        num = (image.Width * height) / image.Height;
                    }
                    else
                    {
                        num = width;
                        num2 = (image.Height * width) / image.Width;
                    }
                }
            }
            Image image2 = new Bitmap(num, num2);
            Graphics graphics = Graphics.FromImage(image2);
            graphics.InterpolationMode = InterpolationMode.High;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.Clear(Color.Transparent);
            graphics.DrawImage(image, new Rectangle(0, 0, num, num2), new Rectangle(x, y, num5, num6), GraphicsUnit.Pixel);
            try
            {
                if (type == "JPG")
                {
                    image2.Save(thumbnailPath, ImageFormat.Jpeg);
                }
                else if (type == "BMP")
                {
                    image2.Save(thumbnailPath, ImageFormat.Bmp);
                }
                else if (type == "GIF")
                {
                    image2.Save(thumbnailPath, ImageFormat.Gif);
                }
                else if (type == "PNG")
                {
                    image2.Save(thumbnailPath, ImageFormat.Png);
                }
                else
                {
                    image2.Save(thumbnailPath, image.RawFormat);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                image.Dispose();
                image2.Dispose();
                graphics.Dispose();
            }
        }
    }
}

