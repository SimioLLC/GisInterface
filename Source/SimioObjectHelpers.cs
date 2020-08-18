using Org.BouncyCastle.Bcpg;
using SimioAPI;
using SimioAPI.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GisAddIn
{
    public static class SimioObjectHelpers
    {

        /// <summary>
        /// Construct Simio facility objects from all routes.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="routes"></param>
        /// <param name="transform"></param>
        /// <param name="explanation"></param>
        /// <returns></returns>
        public static bool  BuildSimioFacilityObjectsFromMapRoutes(IDesignContext context, SimioMapRoutes routes, SimioMapTransform transform, 
            out string explanation)
        {
            explanation = "";
            string marker = "Begin.";
            try
            {
                int routeCount = 0;
                foreach ( SimioMapRoute route in routes.RouteList)
                {
                    routeCount++;
                    if ( !BuildSimioFacilityObjectsFromMapRoute(context, route, transform, out explanation))
                    {
                        marker = $"Route:{routeCount} Info={route} Err={explanation}";
                        throw new ApplicationException(marker);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                explanation = $"BuildSimioFacilityObjects failure. Err={ex.Message}";
                return false;
            }
        }


        /// <summary>
        /// This will create a Facility 'route' from the given SimioMapRoute object 
        /// by building two nodes (start and stop) and a path between them, 
        /// and also attaching the start to a Source and the stop to a Sink.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="mapRoute"></param>
        public static bool BuildSimioFacilityObjectsFromMapRoute(IDesignContext context, SimioMapRoute mapRoute, SimioMapTransform transform,  out string explanation)
        {
            explanation = "";
            string marker = "Begin";
            try
            {
                if (mapRoute == null || mapRoute.SegmentList.Count == 0)
                {
                    explanation = $"MapRoute is null or without Segments";
                    return false;
                }

                var intelligentObjects = context.ActiveModel.Facility.IntelligentObjects;

                // Get scale to convert from latlon to simio meters
                float xScale = transform.SimioScaling.X; // 20f / mapData.LonLatBoundingBox.Width;
                float yScale = transform.SimioScaling.Y; // 20f / mapData.BoundingBox.Height;

                // Find the center in latlon coordinates, because we are going to transform before we scale
                float xCenter = -(float)transform.BoxCenter.X; 
                float yCenter = -(float)transform.BoxCenter.Y; 

                // Build a transformation matrix
                Matrix mat = new Matrix();  // Create identity matrix
                //mat.Rotate(-90);
                mat.Translate(xCenter, yCenter);  // move to origin
                mat.Scale(xScale, yScale, MatrixOrder.Append); // scale to size

                MapSegment seg = mapRoute.SegmentList[0];
                FacilityLocation startLoc = GisHelpers.LatLonToFacilityLocation(mat, seg.StartLocation.Lat, seg.StartLocation.Lon);

                seg = mapRoute.SegmentList[mapRoute.SegmentList.Count - 1];
                FacilityLocation endLoc = GisHelpers.LatLonToFacilityLocation(mat, seg.EndLocation.Lat, seg.EndLocation.Lon);

                var source = intelligentObjects.CreateObject("Source", startLoc) as IFixedObject;
                source.ObjectName = ConvertToName(mapRoute.StartName); // e.g. "Seattle";
                //var server = intelligentObjects.CreateObject("Server", new FacilityLocation(0, 0, 0)) as IFixedObject;
                var sink = intelligentObjects.CreateObject("Sink", endLoc) as IFixedObject;
                var obj = (IPropertyObject)sink;

                obj.ObjectName = ConvertToName(mapRoute.EndName); // e.g. "Key West";

                var node1 = intelligentObjects.CreateObject("BasicNode", startLoc);
                node1.ObjectName = ConvertToName(mapRoute.StartName) + "1";

                var node2 = intelligentObjects.CreateObject("BasicNode", endLoc);
                node2.ObjectName = ConvertToName(mapRoute.EndName) + "1";

                // Nodes is an IEnumerable, so we will create a temporary List from it to quickly get to the first node in the set
                var sourceoutput = new List<INodeObject>(source.Nodes)[0];
                var sinkinput = new List<INodeObject>(sink.Nodes)[0];

                // This path goes directly from the output of source to the input of server
                var path1 = intelligentObjects.CreateLink("Path", sourceoutput, (INodeObject)node1, null);
                // This path goes from the output of server to the input of sink, with one vertex in between
                var path2 = intelligentObjects.CreateLink("Path", (INodeObject)node2, sinkinput, new List<FacilityLocation> { endLoc });

                // Build a path from node1 to node2
                List<FacilityLocation> pathList = new List<FacilityLocation>();
                pathList.Add(node1.Location);

                int segmentCount = 0;
                foreach (MapSegment segment in mapRoute.SegmentList)
                {
                    pathList.Add(GisHelpers.LatLonToFacilityLocation(mat, segment.StartLocation.Lat, segment.StartLocation.Lon));
                    segmentCount++;
                    marker = $"Built Segment#={segmentCount} Segment={segment}";
                }

                pathList.Add(node2.Location);

                var path3 = intelligentObjects.CreateLink("Path", (INodeObject)node1, (INodeObject)node2, pathList);
                return true;
            }
            catch (Exception ex)
            {
                explanation = $"Cannot Build Nodes. Marker={marker} Err={ex}";
                return false;
            }
        }


        public static string ConvertToName(string suggestedName)
        {
            string name = suggestedName;
            do
            {
                name = name.Replace(" ", "");
                name = name.Replace(",", "_");

            } while (name.IndexOf(' ') != -1 && (name.IndexOf(',') != -1));

            // If it is an integer, then assume a zip code an prefix "Zip"
            int nn;
            if (int.TryParse(name, out nn))
            {
                name = $"Zip{name}";
            }

            return name;
        }

    }
}
