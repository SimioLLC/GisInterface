using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GisAddIn
{
    /// <summary>
    /// A rather arbitrary canonical form, so we can get map data into a consistent
    /// and simple form regardless of what Map Source is chosen.
    /// All of the coordinates are latitude/longitude, but for x,y are reordered to lon,lat.
    /// </summary>
    public class SimioMapData
    {
        public List<MapSegment> SegmentList { get; set; }

        /// <summary>
        /// A box that bounds all of the map objects.
        /// Needed to correctly transform coordinates to Simio.
        /// </summary>
        public RectangleF LonLatBoundingBox { get; set; }

        /// <summary>
        /// Scaling (x is meters/lon, y is meters/lat)
        /// </summary>
        public PointF SimioScaling { get; set; }

        /// <summary>
        /// The Origin calculated from the bounding box.
        /// Used to center the objects.
        /// </summary>
        public PointF Origin { get; set; }

        /// <summary>
        /// A human name for the Start
        /// </summary>
        public string StartName { get; set; }

        /// <summary>
        /// A human name for the End
        /// </summary>
        public string EndName { get; set; }

        public SimioMapData(string startLocation, string endLocation)
        {
            SegmentList = new List<MapSegment>();
            StartName = startLocation;
            EndName = endLocation;
        }

        /// <summary>
        /// Create the first segment, which will have an index of zero.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public void AddFirstSegment( MapCoordinate start, MapCoordinate end)
        {
            SegmentList.Clear();

            MapSegment segment = new MapSegment(0, start, end);
            SegmentList.Add(segment);
        }

        public void AppendSegment( MapCoordinate end)
        {
            MapSegment lastSegment = SegmentList.OrderByDescending(ii => ii.Index).FirstOrDefault();
            MapSegment segment = new MapSegment(lastSegment.Index+1, lastSegment.EndLocation, end);
            SegmentList.Add(segment);
        }
    }

    /// <summary>
    /// A class to hold coordinate information, such as those obtained from GeoCode resolution.
    /// </summary>
    public class SimioLocationData
    {
        public string Name { get; set; }

        public List<MapCoordinate> CoordinateList;

        public SimioLocationData(string name)
        {
            CoordinateList = new List<MapCoordinate>();
        }
    }


    /// <summary>
    /// A segment of a path, consisting of a Start and End MapCoordinate.
    /// The 'start' of a segment (except the first) should always be equal to the 'end' of the previous segment.
    /// </summary>
    public class MapSegment
    {
        /// <summary>
        /// Should be unique within a path.
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// The first location
        /// </summary>
        public MapCoordinate StartLocation { get; set; }

        /// <summary>
        /// The last location.
        /// </summary>
        public MapCoordinate EndLocation { get; set; }

        public MapSegment(int index, MapCoordinate start, MapCoordinate end)
        {
            this.Index = index;
            this.StartLocation = start;
            this.EndLocation = end;
        }

        public override string ToString()
        {
            return $"Index={Index} From={StartLocation} To={EndLocation}";
        }

    }

    /// <summary>
    /// Map coordinates are latitude and longitude.
    /// For visualization, i.e. conventional x,y - think lon,lat
    /// </summary>
    public class MapCoordinate
    {
        public MapCoordinate(double lat, double lon)
        {
            this.Lat = lat;
            this.Lon = lon;
        }

        public double Lat { get; set; }
        public double Lon { get; set; }

        public override string ToString()
        {
            return $"Lat:{Lat.ToString("0.0000")} Lon:{Lon.ToString("0.0000")}";
        }
    }
}
