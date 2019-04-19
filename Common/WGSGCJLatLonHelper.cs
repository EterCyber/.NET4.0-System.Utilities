namespace System.Utilities.Common
{
    using System.Utilities.Models;
    using System;

    public class WGSGCJLatLonHelper
    {
        private const double a = 6378245.0;
        private const double ee = 0.0066934216229659433;
        private const double pi = 3.1415926535897931;

        public static LatLngPoint GCJ02ToWGS84(LatLngPoint gcjPoint)
        {
            if (MapHelper.OutOfChina(gcjPoint))
            {
                return gcjPoint;
            }
            LatLngPoint point = Transform(gcjPoint);
            return new LatLngPoint(gcjPoint.LatY - point.LatY, gcjPoint.LonX - point.LonX);
        }

        private static LatLngPoint Transform(LatLngPoint point)
        {
            LatLngPoint point2 = new LatLngPoint();
            double num = TransformLat(point.LonX - 105.0, point.LatY - 35.0);
            double num2 = TransformLon(point.LonX - 105.0, point.LatY - 35.0);
            double a = (point.LatY / 180.0) * 3.1415926535897931;
            double d = Math.Sin(a);
            d = 1.0 - ((0.0066934216229659433 * d) * d);
            double num5 = Math.Sqrt(d);
            num = (num * 180.0) / ((6335552.7170004258 / (d * num5)) * 3.1415926535897931);
            num2 = (num2 * 180.0) / (((6378245.0 / num5) * Math.Cos(a)) * 3.1415926535897931);
            point2.LatY = num;
            point2.LonX = num2;
            return point2;
        }

        private static double TransformLat(double lonX, double latY)
        {
            double num = ((((-100.0 + (2.0 * lonX)) + (3.0 * latY)) + ((0.2 * latY) * latY)) + ((0.1 * lonX) * latY)) + (0.2 * Math.Sqrt(Math.Abs(lonX)));
            num += (((20.0 * Math.Sin((6.0 * lonX) * 3.1415926535897931)) + (20.0 * Math.Sin((2.0 * lonX) * 3.1415926535897931))) * 2.0) / 3.0;
            num += (((20.0 * Math.Sin(latY * 3.1415926535897931)) + (40.0 * Math.Sin((latY / 3.0) * 3.1415926535897931))) * 2.0) / 3.0;
            return (num + ((((160.0 * Math.Sin((latY / 12.0) * 3.1415926535897931)) + (320.0 * Math.Sin((latY * 3.1415926535897931) / 30.0))) * 2.0) / 3.0));
        }

        private static double TransformLon(double lonX, double latY)
        {
            double num = ((((300.0 + lonX) + (2.0 * latY)) + ((0.1 * lonX) * lonX)) + ((0.1 * lonX) * latY)) + (0.1 * Math.Sqrt(Math.Abs(lonX)));
            num += (((20.0 * Math.Sin((6.0 * lonX) * 3.1415926535897931)) + (20.0 * Math.Sin((2.0 * lonX) * 3.1415926535897931))) * 2.0) / 3.0;
            num += (((20.0 * Math.Sin(lonX * 3.1415926535897931)) + (40.0 * Math.Sin((lonX / 3.0) * 3.1415926535897931))) * 2.0) / 3.0;
            return (num + ((((150.0 * Math.Sin((lonX / 12.0) * 3.1415926535897931)) + (300.0 * Math.Sin((lonX / 30.0) * 3.1415926535897931))) * 2.0) / 3.0));
        }

        public static LatLngPoint WGS84ToGCJ02(LatLngPoint wgsPoint)
        {
            if (MapHelper.OutOfChina(wgsPoint))
            {
                return wgsPoint;
            }
            LatLngPoint point = Transform(wgsPoint);
            return new LatLngPoint(wgsPoint.LatY + point.LatY, wgsPoint.LonX + point.LonX);
        }
    }
}

