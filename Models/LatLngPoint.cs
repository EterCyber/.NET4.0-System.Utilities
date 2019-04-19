namespace System.Utilities.Models
{
    using System;
    using System.Runtime.CompilerServices;

    public class LatLngPoint
    {
        public LatLngPoint()
        {
        }

        public LatLngPoint(double lat, double lon)
        {
            this.LatY = lat;
            this.LonX = lon;
        }

        public double LatY { get; set; }

        public double LonX { get; set; }
    }
}

