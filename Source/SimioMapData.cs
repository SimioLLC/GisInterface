using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GisAddIn
{

    public class SimioMapRoutes
    {
        public string Name { get; set; }

        public List<SimioMapRoute> RouteList { get; set; }

        public SimioMapRoutes(string name)
        {
            this.Name = name;
            RouteList = new List<SimioMapRoute>();
        }

    }

    /// <summary>
    /// Used to convert standard Map routing data to coordinates/names
    /// suitable for Simio Facility view.
    /// </summary>
    public class SimioMapTransform
    {

        /// <summary>
        /// A box that bounds all of the map objects.
        /// Needed to correctly transform coordinates to Simio.
        /// </summary>
        public RectangleF LonLatBoundingBox { get; set; }

        public PointF BoxCenter { get; set; }
        
        /// <summary>
        /// Scaling (x is meters/lon, y is meters/lat)
        /// </summary>
        public PointF SimioScaling { get; set; }

        /// <summary>
        /// The Simio Facility origin.
        /// </summary>
        public PointF Origin { get; set; }


        /// <summary>
        /// Build the bounding box that Simio will use to visualize the data.
        /// Also calculates the origin.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public RectangleF BuildBoundingBox(string x, string y, string width, string height)
        {
            try
            {
                float xx = float.Parse(x);
                float yy = float.Parse(y);
                float ww = float.Parse(width);
                float hh = float.Parse(height);
                this.LonLatBoundingBox = new RectangleF(xx, yy, ww, hh);

                this.BoxCenter = new PointF(xx + ww / 2f, yy + hh / 2f);

                return LonLatBoundingBox;

            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Cannot build bounding box. Err={ex.Message}");
            }
        }

        public PointF BuildSimioScaling(string longitude, string latitude)
        {
            try
            {
                float scaleX = float.Parse(longitude);
                float scaleY = float.Parse(latitude);
                this.SimioScaling = new PointF(scaleX, scaleY);
                return SimioScaling;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Cannot build bounding box. Err={ex.Message}");
            }
        }

        public PointF BuildSimioOrigin(string facilityX, string facilityY)
        {
            try
            {
                float xx = float.Parse(facilityX);
                float yy = float.Parse(facilityY);
                this.Origin = new PointF(xx, yy);
                return this.Origin;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Cannot build bounding box. Err={ex.Message}");
            }
        }


    }

    /// <summary>
    /// A multi-segment route from an origin to a destination
    /// A rather arbitrary canonical form, so we can get map data into a consistent
    /// and simple form regardless of what Map Source is chosen.
    /// All of the coordinates are latitude/longitude, but for Simio x,y are reordered to lon,lat.
    /// </summary>
    public class SimioMapRoute
    {
        public List<MapSegment> SegmentList { get; set; }

        /// <summary>
        /// A human name for the Start of the Route
        /// </summary>
        public string StartName { get; set; }

        /// <summary>
        /// A human name for the End of the Route
        /// </summary>
        public string EndName { get; set; }

        public SimioMapRoute(string startLocation, string endLocation)
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

    /////// <summary>
    /////// A class to hold coordinate information, such as those obtained from GeoCode resolution.
    /////// </summary>
    ////public class SimioLocationData
    ////{
    ////    public string Name { get; set; }

    ////    public List<MapCoordinate> CoordinateList;

    ////    public SimioLocationData(string name)
    ////    {
    ////        CoordinateList = new List<MapCoordinate>();
    ////    }
    ////}


    /// <summary>
    /// A segment of a path, consisting of a Start and End MapCoordinate,
    /// and perhaps other data (time/duration) as well
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

        public int Duration { get; set; }

        public int Distance { get; set; }

        public MapSegment(int index, MapCoordinate start, MapCoordinate end)
        {
            this.Index = index;
            this.StartLocation = start;
            this.EndLocation = end;

            this.Distance = 0;
            this.Duration = 0;
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
