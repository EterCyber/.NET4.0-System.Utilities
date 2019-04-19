namespace System.Utilities.Base
{
    using System.Utilities.Models;
    using System;
    using System.Collections;

    public class MapOffsetComparer : IComparer
    {
        public int Compare(object x, object y)
        {
            MapCoord coord = (MapCoord) x;
            MapCoord coord2 = (MapCoord) y;
            int num = coord.Lon - coord2.Lon;
            if (num != 0)
            {
                return num;
            }
            return (coord.Lat - coord2.Lat);
        }
    }
}

