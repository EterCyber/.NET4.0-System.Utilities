namespace System.Utilities.Common
{
    using System.Utilities.Base;
    using System.Utilities.Models;
    using System;
    using System.Collections;

    public class MapOffsetHelper
    {
        private ArrayList mapCoordArrayList;

        public MapOffsetHelper(ArrayList offsetData)
        {
            this.mapCoordArrayList = offsetData;
        }

        public LatLngPoint GCJ02ToWGS84(LatLngPoint gcjPoint)
        {
            MapCoord coord = this.QueryOffSetData(gcjPoint);
            double pixelY = MapHelper.LatToPixel(gcjPoint.LatY, 0x12);
            double pixelX = MapHelper.LonToPixel(gcjPoint.LonX, 0x12);
            pixelY -= coord.Y_off;
            pixelX -= coord.X_off;
            double lat = MapHelper.PixelToLat(pixelY, 0x12);
            return new LatLngPoint(lat, MapHelper.PixelToLon(pixelX, 0x12));
        }

        private MapCoord QueryOffSetData(LatLngPoint point)
        {
            MapCoord coord = new MapCoord {
                Lat = (int) (point.LatY * 100.0),
                Lon = (int) (point.LonX * 100.0)
            };
            MapOffsetComparer comparer = new MapOffsetComparer();
            int num = this.mapCoordArrayList.BinarySearch(0, this.mapCoordArrayList.Count, coord, comparer);
            return (MapCoord) this.mapCoordArrayList[num];
        }

        public LatLngPoint WGS84ToGCJ02(LatLngPoint wgsPoint)
        {
            MapCoord coord = this.QueryOffSetData(wgsPoint);
            double pixelY = MapHelper.LatToPixel(wgsPoint.LatY, 0x12);
            double pixelX = MapHelper.LonToPixel(wgsPoint.LonX, 0x12);
            pixelY += coord.Y_off;
            pixelX += coord.X_off;
            double lat = MapHelper.PixelToLat(pixelY, 0x12);
            return new LatLngPoint(lat, MapHelper.PixelToLon(pixelX, 0x12));
        }
    }
}

