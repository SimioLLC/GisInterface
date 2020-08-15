using System;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;
using BingMapsRESTToolkit;
using System.Drawing.Drawing2D;
using System.Data;
using SimioAPI;
using SimioAPI.Extensions;

namespace GisAddIn
{
    /// <summary>
    /// History:
    /// {Feb2018/dHouck} Creation.
    /// </summary>
    public class GisAddIn : IDesignAddIn
    {        

        #region IDesignAddIn Members

        /// <summary>
        /// Property returning the name of the add-in. This name may contain any characters and is used as the display name for the add-in in the UI.
        /// </summary>
        public string Name
        {
            get { return "GIS Interface"; }
        }

        /// <summary>
        /// Property returning a short description of what the add-in does.  
        /// </summary>
        public string Description
        {
            get { return "This addin demonstates an interface to GIS data"; }
        }
        
        /// <summary>
        /// Property returning an icon to display for the add-in in the UI.
        /// </summary>
        public Image Icon
        {
            get { return Properties.Resources.Icon; }
        }

        #endregion

        /// <summary>
        /// Method called when the add-in is run.
        /// </summary>
        public void Execute(IDesignContext context)
        {

            try
            {
                // Check to make sure a model has been opened in StrinbSimio
                if (context.ActiveModel == null)
                {
                    alert("You must have an active model to run this add-in.");
                    return;
                }

                IModel am = context.ActiveModel;

                StringBuilder sb = new StringBuilder();

                // Get the path to the project file
                string filepath = GetStringProperty(context.ActiveProject, "FileName");
                foreach (IIntelligentObject io in context.ActiveModel.Facility.IntelligentObjects)
                {
                    PointF pt = new PointF( (float) io.Location.X, (float) io.Location.Z);
                    sb.AppendLine($"Name={io.ObjectName} Type={io.TypeName} Location={pt}");
                    var link = io as ILinkObject;
                    if (link == null)
                    {

                    }
                }

                string pathToObjects = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                pathToObjects = Path.Combine(pathToObjects, "SimioObjects.txt");
                if ( Directory.Exists(pathToObjects))
                {
                    File.WriteAllText(pathToObjects, sb.ToString());
                }

                SimioMapRoute mapData = new SimioMapRoute("","");

                // Query the user for map data
                LaunchForm(context, mapData);

                //BuildSimioNodesAndPathFromMapData(context, mapData, transform);
                //BuildSimioObjectsFromLocationData(context, mapData, locData);

            }
            catch (Exception ex)
            {
                if (ex.Message != "Canceled")
                {
                    MessageBox.Show(ex.Message, "Error");
                }
            }
        }

        private void LaunchForm(IDesignContext context, SimioMapRoute mapData )
        {
            try
            {
                FormGisViewer FormViewer = new FormGisViewer();

                FormViewer.DesignContext = context;
                FormViewer.MapRoute = mapData;
////                FormViewer.LocationData = locData;

                FormViewer.Show();

            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Launch Error={ex}");
            }
        }




        /// <summary>
        /// This will create a route from the given SimioMapRoute object 
        /// by building two nodes (start and stop) and a path between them, 
        /// and also attaching the start to a Source and the stop to a Sink.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="mapData"></param>
        private void BuildSimioNodesAndPathFromMapData(IDesignContext context, SimioMapRoute mapData, SimioMapTransform transform)
        {
            try
            {
                if (mapData == null || mapData.SegmentList.Count == 0 )
                {
                    return;
                }

                var intelligentObjects = context.ActiveModel.Facility.IntelligentObjects;

                // Get scale to convert from latlon to simio meters
                float xScale = transform.SimioScaling.X; // 20f / mapData.LonLatBoundingBox.Width;
                float yScale = transform.SimioScaling.Y; // 20f / mapData.BoundingBox.Height;

                // Find the center in latlon coordinates
                float xCenter = (float) transform.Origin.X; // BoundingBox.Left + mapData.BoundingBox.Width /2f;
                float yCenter = (float) transform.Origin.Y; // BoundingBox.Bottom - mapData.BoundingBox.Height /2f;

                // Build a transformation matrix
                Matrix mat = new Matrix();  // Create identity matrix
                //mat.Rotate(-90);
                mat.Translate(-xCenter, -yCenter);  // move to origin
                mat.Scale(xScale, yScale, MatrixOrder.Append); // scale to size

                MapSegment seg = mapData.SegmentList[0];
                FacilityLocation startLoc = GisHelpers.LatLonToFacilityLocation( mat, seg.StartLocation.Lat, seg.StartLocation.Lon);

                seg = mapData.SegmentList[mapData.SegmentList.Count - 1];
                FacilityLocation endLoc = GisHelpers.LatLonToFacilityLocation( mat, seg.EndLocation.Lat, seg.EndLocation.Lon);

                var source = intelligentObjects.CreateObject("Source", startLoc) as IFixedObject;
                source.ObjectName = ConvertToName(mapData.StartName); // e.g. "Seattle";
                //var server = intelligentObjects.CreateObject("Server", new FacilityLocation(0, 0, 0)) as IFixedObject;
                var sink = intelligentObjects.CreateObject("Sink", endLoc) as IFixedObject;
                var obj = (IPropertyObject)sink;

                obj.ObjectName = ConvertToName(mapData.EndName); // e.g. "Key West";

                var node1 = intelligentObjects.CreateObject("BasicNode", startLoc );
                node1.ObjectName = ConvertToName(mapData.StartName) + "1";

                var node2 = intelligentObjects.CreateObject("BasicNode", endLoc);
                node2.ObjectName = ConvertToName(mapData.EndName) + "1";

                // Nodes is an IEnumerable, so we will create a temporary List from it to quickly get to the first node in the set
                var sourceoutput = new List<INodeObject>(source.Nodes)[0];
                var sinkinput = new List<INodeObject>(sink.Nodes)[0];

                // This path goes directly from the output of source to the input of server
                var path1 = intelligentObjects.CreateLink("Path", sourceoutput, (INodeObject) node1, null);
                // This path goes from the output of server to the input of sink, with one vertex in between
                var path2 = intelligentObjects.CreateLink("Path", (INodeObject) node2, sinkinput, new List<FacilityLocation> { endLoc});

                // Build a path from node1 to node2
                List<FacilityLocation> pathList = new List<FacilityLocation>();
                pathList.Add(node1.Location);

                foreach ( MapSegment segment in mapData.SegmentList)
                {
                    pathList.Add( GisHelpers.LatLonToFacilityLocation(mat, segment.StartLocation.Lat, segment.StartLocation.Lon));
                }

                pathList.Add(node2.Location);

                var path3 = intelligentObjects.CreateLink("Path", (INodeObject)node1, (INodeObject)node2, pathList);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Err={ex}");
            }
        }

        public string ConvertToName(string suggestedName)
        {
            string name = suggestedName;
            do
            {
                name = name.Replace(" ", "");
                name = name.Replace(",", "_");

            } while (name.IndexOf(' ') != -1 && (name.IndexOf(',') != -1));

            // If it is an integer, then assume a zip code an prefix "Zip"
            int nn;
            if ( int.TryParse(name, out nn))
            {
                name = $"Zip{name}";
            }

            return name;
        }

        ////private void BuildSimioObjectsFromLocationData(IDesignContext context, SimioMapRoute mapData, SimioLocationData locData)
        ////{
        ////    try
        ////    {
        ////        var intelligentObjects = context.ActiveModel.Facility.IntelligentObjects;

        ////        // Create the Source, Server, and Sink. Space them out along a diagonal line. The X and Z coordinate of the location specify the left to right and top to bottom 
        ////        //  coordinates from a top down view. The Y coordinate specifies the elevation. We cast them to IFixedObject here so that we can get to their Nodes collection
        ////        //  later

        ////        // Get scale to convert from latlon to simio meters
        ////        float xScale = mapData.SimioScaling.X; // 20f / mapData.LonLatBoundingBox.Width;
        ////        float yScale = mapData.SimioScaling.Y; // 20f / mapData.BoundingBox.Height;

        ////        // Find the center in latlon coordinates
        ////        float xCenter = (float)mapData.Origin.X; // BoundingBox.Left + mapData.BoundingBox.Width /2f;
        ////        float yCenter = (float)mapData.Origin.Y; // BoundingBox.Bottom - mapData.BoundingBox.Height /2f;

        ////        // Build a transformation matrix
        ////        Matrix mat = new Matrix();  // Create identity matrix
        ////        mat.Translate(-xCenter, -yCenter);  // move to origin

        ////        if ( locData.CoordinateList.Count == 0 )
        ////        {
        ////            return;
        ////        }

        ////        MapCoordinate coord = locData.CoordinateList[0];
        ////        FacilityLocation startLoc = GisHelpers.LatLonToFacilityLocation(mat, coord.Lat, coord.Lon);

        ////        coord = locData.CoordinateList[locData.CoordinateList.Count - 1];
        ////        FacilityLocation endLoc = GisHelpers.LatLonToFacilityLocation(mat, coord.Lat, coord.Lon);

        ////        var node1 = intelligentObjects.CreateObject("BasicNode", startLoc) as INodeObject;
        ////        node1.ObjectName = mapData.StartName;

        ////        var node2 = intelligentObjects.CreateObject("BasicNode", endLoc) as INodeObject;
        ////        node2.ObjectName = mapData.EndName;

        ////        // Build a path from node1 to node2
        ////        List<FacilityLocation> pathList = new List<FacilityLocation>();
        ////        pathList.Add(node1.Location);

        ////        MapCoordinate lastCoord = null;
        ////        foreach (MapCoordinate thisCoord in locData.CoordinateList)
        ////        {
        ////            if (lastCoord != null)
        ////            {
        ////                pathList.Add(GisHelpers.LatLonToFacilityLocation(mat, lastCoord.Lat, thisCoord.Lon));
        ////            }
        ////            lastCoord = thisCoord;
        ////        }

        ////        var path3 = intelligentObjects.CreateLink("Path", (INodeObject)node1, (INodeObject)node2, pathList);
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        alert($"Err={ex}");
        ////    }
        ////}

        /// <summary>
        /// Use reflection to get a string value.
        /// The name must be an exact match, and known to be string.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        private string GetStringProperty(Object obj, string propertyName)
        {
            try
            {
                foreach (PropertyInfo pi in obj.GetType().GetProperties())
                {
                    var getter = pi.GetGetMethod();
                    if (getter.ReturnType.IsArray)
                    {
                        // Not using this now.
                    }
                    else
                    {
                        if (pi.Name == propertyName)
                        {
                            var vv = pi.GetValue(obj, null);
                            return (vv ?? "").ToString();
                        }
                    }

                }
                return string.Empty;

            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
        /// <summary>
        /// Use reflection to get a string value.
        /// The name must be an exact match, and known to be string.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        private StringBuilder BuildTreeOfClasses(object obj, int level, StringBuilder sb)
        {
            try
            {
                foreach (PropertyInfo pi in obj.GetType().GetProperties())
                {
                    if (pi.Name.Length < 20)
                    {
                        if (pi.PropertyType.IsClass && !pi.PropertyType.FullName.StartsWith("System."))
                        {
                            //object objChild = pi.GetValue();
                            object oo = pi.GetValue(obj);
                            if ( oo != null && pi.Name != "ApplicationContext")
                            {
                                string indent = new string(' ', level * 2);
                                sb.AppendFormat($"{indent}Name={pi.Name}");
                                sb = BuildTreeOfClasses(oo, level + 1, sb);
                            }
                        }
                    }
                }
                return sb;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private void alert(string message)
        {
            MessageBox.Show(message);
        }
    }
}
