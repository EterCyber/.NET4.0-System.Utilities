namespace System.Utilities.Common
{
    using System.Utilities.Models;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;

    public class MapOffsetDataHelper
    {
        private string offsetFullPath = string.Empty;

        public MapOffsetDataHelper(string path)
        {
            this.offsetFullPath = path;
        }

        public ArrayList GetMapCoordArrayList()
        {
            ArrayList _mapCoordArrayList = new ArrayList();
            this.GetOffsetData(delegate (MapCoord c) {
                _mapCoordArrayList.Add(c);
            });
            return _mapCoordArrayList;
        }

        public List<MapCoord> GetMapCoordList()
        {
            List<MapCoord> _mapCoordList = new List<MapCoord>();
            this.GetOffsetData(delegate (MapCoord c) {
                _mapCoordList.Add(c);
            });
            return _mapCoordList;
        }

        private void GetOffsetData(Action<MapCoord> mapCoordHanlder)
        {
            using (FileStream stream = new FileStream(this.offsetFullPath, FileMode.OpenOrCreate, FileAccess.Read))
            {
                using (BinaryReader reader = new BinaryReader(stream))
                {
                    int num = ((int) stream.Length) / 8;
                    for (int i = 0; i < num; i++)
                    {
                        byte[] bytes = reader.ReadBytes(8);
                        MapCoord coord = this.ToCoord(bytes);
                        mapCoordHanlder(coord);
                    }
                }
            }
        }

        private MapCoord ToCoord(byte[] bytes)
        {
            MapCoord coord = new MapCoord();
            byte[] destinationArray = new byte[2];
            byte[] buffer2 = new byte[2];
            byte[] buffer3 = new byte[2];
            byte[] buffer4 = new byte[2];
            Array.Copy(bytes, 0, destinationArray, 0, 2);
            Array.Copy(bytes, 2, buffer2, 0, 2);
            Array.Copy(bytes, 4, buffer3, 0, 2);
            Array.Copy(bytes, 6, buffer4, 0, 2);
            coord.Lon = BitConverter.ToInt16(destinationArray, 0);
            coord.Lat = BitConverter.ToInt16(buffer2, 0);
            coord.X_off = BitConverter.ToInt16(buffer3, 0);
            coord.Y_off = BitConverter.ToInt16(buffer4, 0);
            return coord;
        }
    }
}

