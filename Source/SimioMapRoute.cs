using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GisAddIn
{
    /// <summary>
    /// A multi-segment route from an origin to a destination
    /// A rather arbitrary canonical form, so we can get map data into a consistent
    /// and simple form regardless of what Map Provider is chosen.
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
        /// Parameterless constructor for deserializing
        /// </summary>
        public SimioMapRoute()
        {
            SegmentList = new List<MapSegment>();
            StartName = "";
            EndName = "";
        }

        public override string ToString()
        {
            return $"Route: From={StartName} To={EndName}. Segments={SegmentList.Count}";
        }

        /// <summary>
        /// Build a multi-line display of the route
        /// </summary>
        /// <returns></returns>
        public string BuildDisplayString()
        {
            StringBuilder sb = new StringBuilder($"Start={this.StartName} End={EndName} {SegmentList.Count} segments.");

            foreach (MapSegment segment in SegmentList)
            {
                sb.AppendLine(segment.ToString());
            }

            return sb.ToString();
        }


        /// <summary>
        /// Create the first segment, which will have an index of zero.
        /// At the same time, clear the SegmentList.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public MapSegment AddFirstSegment( MapCoordinate start, MapCoordinate end)
        {
            SegmentList.Clear();

            MapSegment segment = new MapSegment(0, start, end);
            SegmentList.Add(segment);

            return segment;
        }

        public MapSegment AppendSegment( MapCoordinate end)
        {
            MapSegment lastSegment = SegmentList.OrderByDescending(ii => ii.Index).FirstOrDefault();
            MapSegment segment = new MapSegment(lastSegment.Index+1, lastSegment.EndLocation, end);
            SegmentList.Add(segment);

            return segment;
        }
    }
}
