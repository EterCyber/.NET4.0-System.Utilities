namespace System.Utilities.Common
{
    using System;

    public static class MathHelper
    {
        public static double GetDistance(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt(Math.Pow(x1 - x2, 2.0) + Math.Pow(y1 - y2, 2.0));
        }
    }
}

