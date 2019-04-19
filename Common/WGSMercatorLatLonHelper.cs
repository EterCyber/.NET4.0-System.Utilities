namespace System.Utilities.Common
{
    using System.Utilities.Models;
    using System;

    public class WGSMercatorLatLonHelper
    {
        public static LatLngPoint mercator2lonlat(LatLngPoint mercator)
        {
            LatLngPoint point = new LatLngPoint();
            double num = (mercator.LonX / 20037508.34) * 180.0;
            double num2 = (mercator.LatY / 20037508.34) * 180.0;
            num2 = 57.295779513082323 * ((2.0 * Math.Atan(Math.Exp((num2 * 3.1415926535897931) / 180.0))) - 1.5707963267948966);
            point.LonX = num;
            point.LatY = num2;
            return point;
        }

        public static LatLngPoint WGS84ToMercator(LatLngPoint wgsPoint)
        {
            LatLngPoint point = new LatLngPoint();
            double num = (wgsPoint.LonX * 20037508.34) / 180.0;
            double num2 = Math.Log(Math.Tan(((90.0 + wgsPoint.LatY) * 3.1415926535897931) / 360.0)) / 0.017453292519943295;
            num2 = (num2 * 20037508.34) / 180.0;
            point.LonX = num;
            point.LatY = num2;
            return point;
        }
    }
}

