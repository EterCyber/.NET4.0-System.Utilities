namespace System.Utilities.Common
{
    using System.Utilities.Models;
    using System;

    public class MapHelper
    {
        private const double e = 2.7182818284590451;
        private const double pi = 3.1415926535897931;

        public static double GetDistance(LatLngPoint fromPoint, LatLngPoint toPoint)
        {
            double num = 6371000.0;
            double num2 = (Math.Cos((fromPoint.LatY * 3.1415926535897931) / 180.0) * Math.Cos((toPoint.LatY * 3.1415926535897931) / 180.0)) * Math.Cos(((fromPoint.LonX - toPoint.LonX) * 3.1415926535897931) / 180.0);
            double num3 = Math.Sin((fromPoint.LatY * 3.1415926535897931) / 180.0) * Math.Sin((toPoint.LatY * 3.1415926535897931) / 180.0);
            double d = num2 + num3;
            if (d > 1.0)
            {
                d = 1.0;
            }
            if (d < -1.0)
            {
                d = -1.0;
            }
            return (Math.Acos(d) * num);
        }

        public static GeoPoint GetQueryLocation(LatLngPoint point)
        {
            int y = (int) (point.LatY * 100.0);
            int x = (int) (point.LonX * 100.0);
            double num3 = ((double) ((int) ((point.LatY * 1000.0) + 0.499999))) / 10.0;
            double num4 = ((double) ((int) ((point.LonX * 1000.0) + 0.499999))) / 10.0;
            for (double i = point.LonX; i < (point.LonX + 1.0); i += 0.5)
            {
                for (double j = point.LatY; i < (point.LatY + 1.0); j += 0.5)
                {
                    if (((i <= num4) && (num4 < (i + 0.5))) && ((num3 >= j) && (num3 < (j + 0.5))))
                    {
                        return new GeoPoint((int) (i + 0.5), (int) (j + 0.5));
                    }
                }
            }
            return new GeoPoint(x, y);
        }

        public static double LatToPixel(double lat, int zoom)
        {
            double num = Math.Sin((lat * 3.1415926535897931) / 180.0);
            double num2 = Math.Log((1.0 + num) / (1.0 - num));
            return ((((int) 0x80) << zoom) * (1.0 - (num2 / 6.2831853071795862)));
        }

        public static double LonToPixel(double lng, int zoom)
        {
            return (((lng + 180.0) * (((long) 0x100L) << zoom)) / 360.0);
        }

        public static bool OutOfChina(LatLngPoint latlon)
        {
            if (((latlon.LonX >= 72.004) && (latlon.LatY <= 137.8347)) && ((latlon.LonX >= 0.8293) && (latlon.LatY <= 55.8271)))
            {
                return false;
            }
            return true;
        }

        public static double PixelToLat(double pixelY, int zoom)
        {
            double y = 6.2831853071795862 * (1.0 - (pixelY / ((double) (((int) 0x80) << zoom))));
            double num2 = Math.Pow(2.7182818284590451, y);
            double d = (num2 - 1.0) / (num2 + 1.0);
            return ((Math.Asin(d) * 180.0) / 3.1415926535897931);
        }

        public static double PixelToLon(double pixelX, int zoom)
        {
            return (((pixelX * 360.0) / ((double) (((long) 0x100L) << zoom))) - 180.0);
        }
    }
}

