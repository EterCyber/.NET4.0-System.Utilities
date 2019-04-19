namespace System.Utilities.Common
{
    using System.Utilities.Models;
    using System;

    public class BDGCJLatLonHelper
    {
        private const double x_pi = 52.359877559829883;

        public LatLngPoint BD09ToGCJ02(LatLngPoint bdPoint)
        {
            LatLngPoint point = new LatLngPoint();
            double x = bdPoint.LonX - 0.0065;
            double y = bdPoint.LatY - 0.006;
            double num3 = Math.Sqrt((x * x) + (y * y)) - (2E-05 * Math.Sin(y * 52.359877559829883));
            double d = Math.Atan2(y, x) - (3E-06 * Math.Cos(x * 52.359877559829883));
            point.LonX = num3 * Math.Cos(d);
            point.LatY = num3 * Math.Sin(d);
            return point;
        }

        public LatLngPoint GCJ02ToBD09(LatLngPoint gcjPoint)
        {
            LatLngPoint point = new LatLngPoint();
            double lonX = gcjPoint.LonX;
            double latY = gcjPoint.LatY;
            double num3 = Math.Sqrt((lonX * lonX) + (latY * latY)) + (2E-05 * Math.Sin(latY * 52.359877559829883));
            double d = Math.Atan2(latY, lonX) + (3E-06 * Math.Cos(lonX * 52.359877559829883));
            point.LonX = (num3 * Math.Cos(d)) + 0.0065;
            point.LatY = (num3 * Math.Cos(d)) + 0.006;
            return point;
        }
    }
}

