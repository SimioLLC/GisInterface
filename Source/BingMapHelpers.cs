using BingMapsRESTToolkit;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace GisAddIn
{
    /// <summary>
    /// Bing Map Provider
    /// </summary>
    public class BingMapHelpers : IMapHelper
    {

        public string GetProviderInformation()
        {
            return $"Bing Maps.";
        }
        /// <summary>
        /// An asynchronous fetch of a map route (untested)
        /// </summary>
        /// <param name="mapData"></param>
        /// <param name="bingMapsKey"></param>
        /// <param name="StartStopList"></param>
        public static void xxGetMapRoutesAsync(SimioMapRoute mapData, string bingMapsKey, List<string> StartStopList)
        {
            try
            {
                List<SimpleWaypoint> wayPointList = new List<SimpleWaypoint>();
                foreach (string location in StartStopList)
                {
                    SimpleWaypoint wayPoint = new SimpleWaypoint(location);
                    wayPointList.Add(wayPoint);
                }

                var request = new RouteRequest
                {
                    Waypoints = wayPointList,
                    BingMapsKey = bingMapsKey
                };

                Task<Response> t = request.Execute();
                var result = t.Result;

                t.RunSynchronously();

                var r1 = t.Result;
                if (r1 != null && r1.ResourceSets != null &&
                    r1.ResourceSets.Length > 0 &&
                    r1.ResourceSets[0].Resources != null &&
                    r1.ResourceSets[0].Resources.Length > 0)
                {
                    for (var i = 0; i < r1.ResourceSets[0].Resources.Length; i++)
                    {
                        throw new ApplicationException((r1.ResourceSets[0].Resources[i] as Location).Name);
                    }
                }
                else
                {
                    throw new ApplicationException("No results found.");
                }

                Task<Response> t2 = ServiceManager.GetResponseAsync(new RouteRequest()
                {
                    BingMapsKey = bingMapsKey,
                    Waypoints = wayPointList
                });

                t2.RunSynchronously();

                var r2 = ServiceManager.GetResponseAsync(new RouteRequest()
                {
                    BingMapsKey = bingMapsKey,
                    Waypoints = wayPointList
                }).GetAwaiter().GetResult();

                if (r2 != null && r2.ResourceSets != null &&
                    r2.ResourceSets.Length > 0 &&
                    r2.ResourceSets[0].Resources != null &&
                    r2.ResourceSets[0].Resources.Length > 0)
                {
                    for (var i = 0; i < r2.ResourceSets[0].Resources.Length; i++)
                    {
                        throw new ApplicationException((r2.ResourceSets[0].Resources[i] as Location).Name);
                    }
                }
                else
                {
                    throw new ApplicationException("No results found.");
                }

                Console.ReadLine();

            }
            catch (Exception ex)
            {
                throw new Exception($"Err={ex.Message}");
            }
        }

        public static void GetMapGeoCode(SimioMapRoute mapData, string bingMapsKey, string query, List<string> nameList)
        {
            try
            {
                nameList.Clear();

                var r = ServiceManager.GetResponseAsync(new GeocodeRequest()
                {
                    BingMapsKey = bingMapsKey,
                    Query = query
                }).GetAwaiter().GetResult();

                if (r != null && r.ResourceSets != null &&
                    r.ResourceSets.Length > 0 &&
                    r.ResourceSets[0].Resources != null &&
                    r.ResourceSets[0].Resources.Length > 0)
                {
                    for (var i = 0; i < r.ResourceSets[0].Resources.Length; i++)
                    {
                        nameList.Add((r.ResourceSets[0].Resources[i] as Location).Name);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Err={ex}");
            }
        }


        /// <summary>
        /// Fetch the key, which is located in MyDocuments under folder SimioUserExtensions
        /// in BingMapsKey.txt.
        /// </summary>
        /// <returns></returns>
        public string GetKeyStringFromFile(string filepath, out string explanation)
        {
            explanation = "";

            try
            {
                if (!File.Exists(filepath))
                {
                    explanation = $"Cannot find File={filepath}";
                    return string.Empty;
                }

                string bingMapKey = File.ReadAllText(filepath).Trim();
                return bingMapKey;
            }
            catch (Exception ex)
            {
                explanation = $"Cannot Get Key from File={filepath}. Err={ex.Message}";
                return string.Empty;
            }
        }

        /// <summary>
        /// Fetch the key, which is located in MyDocuments under folder SimioUserExtensions
        /// in BingMapsKey.txt.
        /// </summary>
        /// <returns></returns>
        public string GetKeyString()
        {
            string filepath = "";

            try
            {
                string folderpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
                filepath = Path.Combine(folderpath, "SimioUserExtensions", "BingMapsKey.txt");

                if (!File.Exists(filepath))
                    throw new ApplicationException($"Cannot find Filepath={filepath}");

                string bingMapKey = File.ReadAllText(filepath).Trim();
                return bingMapKey;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Make sure you have a BingMapsKey file here={filepath}. Err={ex}");
            }
        }

        /// <summary>
        /// Fetch the key, which is located in MyDocuments under folder SimioUserExtensions
        /// in BingMapsKey.txt.
        /// </summary>
        /// <returns></returns>
        public string GetKeyString(out string explanation)
        {
            explanation = "";

            try
            {
                string folderpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
                string filepath = Path.Combine(folderpath, "SimioUserExtensions", "BingMapsKey.txt");

                string keystring = GetKeyStringFromFile(filepath, out explanation);
                if (string.IsNullOrEmpty(keystring))
                {
                    return string.Empty;
                }

                return keystring;
            }
            catch (Exception ex)
            {
                explanation = $"Cannot Get Key. Err={ex.Message}";
                return string.Empty;
            }
        }

        /// <summary>
        /// Save the key, which is located in MyDocuments under folder SimioUserExtensions
        /// in BingMapsKey.txt.
        /// </summary>
        /// <returns></returns>
        public void PutKeyString(string bingMapKey)
        {
            string filepath = "";

            try
            {
                string folderpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
                filepath = Path.Combine(folderpath, "SimioUserExtensions", "BingMapsKey.txt");

                if (!Directory.Exists(folderpath))
                    throw new ApplicationException($"Cannot find Folder={folderpath}");

                File.WriteAllText(filepath, bingMapKey);
                return;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Cannot Put KeyFile={filepath}. Err={ex}");
            }
        }

        /// <summary>
        /// This method has a lot of logic that is specific to the map type. 
        /// To process a request you can easily just call the Execute method on the request.
        /// This will build much of the SimioMapRoute object.
        /// </summary>
        /// <param name="request"></param>
        public SimioRouteResult GetRoute(string mapsKey, string originAddress, string destinationAddress)
        {
            SimioRouteResult routeResult = new SimioRouteResult();
            SimioMapRoute mapRoute = new SimioMapRoute(originAddress, destinationAddress);

            try
            {
                // Build a list of our two waypoints (from and to)
                List<SimpleWaypoint> wpList = new List<SimpleWaypoint>();
                wpList.Add(new SimpleWaypoint(originAddress));  //e.g. "Pittsburgh, PA"));
                wpList.Add(new SimpleWaypoint(destinationAddress));    // e.g. "Sewickley, PA"));

                List<RouteAttributeType> routeAttributes = new List<RouteAttributeType>();
                routeAttributes.Add(RouteAttributeType.RoutePath);

                // Construct the request and attributes
                var request = new RouteRequest()
                {
                    BingMapsKey = mapsKey,
                    Waypoints = wpList
                };

                request.RouteOptions = new RouteOptions();
                request.RouteOptions.RouteAttributes = routeAttributes;

                routeResult.Request = request.ToString();

                var start = DateTime.Now;

                // Async. Execute the request.
                var task = Task<Response>.Run(async () =>
              {
                  return await request.Execute();
              });

                Response r2 = task.Result;

                // Check if we got a good response
                if (r2 != null && r2.ResourceSets != null
                    && r2.ResourceSets.Length > 0
                    && r2.ResourceSets[0].Resources != null
                    && r2.ResourceSets[0].Resources.Length > 0)
                {
                    ResourceSet rs = (ResourceSet)r2.ResourceSets[0];
                    Route route = (Route)rs.Resources[0];
                    RouteLeg[] legs = route.RouteLegs;
                    ItineraryItem[] itineraries = legs[0].ItineraryItems;
                    ItineraryItem itinItem = itineraries[2];
                    string bb = route.BoundingBox.ToString();

                    mapRoute.SegmentList.Clear();

                    // Build something for the form's 'result textbox
                    StringBuilder sb = new StringBuilder();

                    // Create segments from the itineraries, and pick up the indices
                    // that reference the RoutePath array of lat,lon coordinates.
                    // We are assuming a single itinerary. See Bing Maps for more info.
                    for (var ii = 0; ii < itineraries.Length; ii++)
                    {
                        ItineraryItem item = itineraries[ii];

                        if (route.RoutePath != null)
                        {
                            int idxStart = item.Details[0].StartPathIndices[0];
                            int idxEnd = item.Details[0].EndPathIndices[0];

                            double lat = route.RoutePath.Line.Coordinates[idxStart][0];
                            double lon = route.RoutePath.Line.Coordinates[idxStart][1];
                            MapCoordinate mcStart = new MapCoordinate(lat, lon);

                            lat = route.RoutePath.Line.Coordinates[idxEnd][0];
                            lon = route.RoutePath.Line.Coordinates[idxEnd][1];

                            MapCoordinate mcEnd = new MapCoordinate(lat, lon);
                            MapSegment segment = null;
                            if (ii == 0)
                            {
                                segment = mapRoute.AddFirstSegment(mcStart, mcEnd);
                            }
                            else
                            {
                                segment = mapRoute.AppendSegment(mcEnd);
                            }

                            // Now add Bing-specific info
                            segment.Distance = item.TravelDistance;
                            segment.Duration = item.TravelDuration;
                        }
                        sb.AppendLine($"Compass={item.CompassDirection} Distance={item.TravelDistance} >> {item.Instruction.Text}");
                    } // for each itinerary

                    routeResult.Response = sb.ToString();
                    return routeResult;
                }
                else
                {
                    routeResult.ErrorMessage = "No results found.";
                    return routeResult;
                }

            }
            catch (Exception ex)
            {
                routeResult.ErrorMessage = $"Err={ex.Message}";
                return routeResult;
            }

        }

        ////public SimioRouteResult GetRoute(string mapsKey, string originAddress, string destinationAddress)
        ////{
        ////    SimioRouteResult result = new SimioRouteResult();
        ////    try
        ////    {

        ////        return result;
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        result.ErrorMessage = ex.Message;
        ////        return result;
        ////    }
        ////}

        public async Task<SimioRouteResult> GetRouteAsync(string mapsKey, string originAddress, string destinationAddress)
        {
            SimioRouteResult routeResult = new SimioRouteResult();
            SimioMapRoute mapRoute = new SimioMapRoute(originAddress, destinationAddress);

            try
            {
                // Build a list of our two waypoints (from and to)
                List<SimpleWaypoint> wpList = new List<SimpleWaypoint>();
                wpList.Add(new SimpleWaypoint(originAddress));  //e.g. "Pittsburgh, PA"));
                wpList.Add(new SimpleWaypoint(destinationAddress));    // e.g. "Sewickley, PA"));

                List<RouteAttributeType> routeAttributes = new List<RouteAttributeType>();
                routeAttributes.Add(RouteAttributeType.RoutePath);

                // Construct the request and attributes
                var request = new RouteRequest()
                {
                    BingMapsKey = mapsKey,
                    Waypoints = wpList
                };

                request.RouteOptions = new RouteOptions();
                request.RouteOptions.RouteAttributes = routeAttributes;

                routeResult.Request = request.ToString();

                var start = DateTime.Now;

                // Async. Execute the request.
                Response r2 = await request.Execute();

                ////Response r2 = task.Result;

                // Check if we got a good response
                if (r2 != null && r2.ResourceSets != null
                    && r2.ResourceSets.Length > 0
                    && r2.ResourceSets[0].Resources != null
                    && r2.ResourceSets[0].Resources.Length > 0)
                {
                    ResourceSet rs = (ResourceSet)r2.ResourceSets[0];
                    Route route = (Route)rs.Resources[0];
                    RouteLeg[] legs = route.RouteLegs;
                    ItineraryItem[] itineraries = legs[0].ItineraryItems;
                    ItineraryItem itinItem = itineraries[2];
                    string bb = route.BoundingBox.ToString();

                    mapRoute.SegmentList.Clear();

                    // Build something for the form's 'result textbox
                    StringBuilder sb = new StringBuilder();

                    // Create segments from the itineraries, and pick up the indices
                    // that reference the RoutePath array of lat,lon coordinates.
                    // We are assuming a single itinerary. See Bing Maps for more info.
                    for (var ii = 0; ii < itineraries.Length; ii++)
                    {
                        ItineraryItem item = itineraries[ii];

                        if (route.RoutePath != null)
                        {
                            int idxStart = item.Details[0].StartPathIndices[0];
                            int idxEnd = item.Details[0].EndPathIndices[0];

                            double lat = route.RoutePath.Line.Coordinates[idxStart][0];
                            double lon = route.RoutePath.Line.Coordinates[idxStart][1];
                            MapCoordinate mcStart = new MapCoordinate(lat, lon);

                            lat = route.RoutePath.Line.Coordinates[idxEnd][0];
                            lon = route.RoutePath.Line.Coordinates[idxEnd][1];

                            MapCoordinate mcEnd = new MapCoordinate(lat, lon);
                            MapSegment segment = null;
                            if (ii == 0)
                            {
                                segment = mapRoute.AddFirstSegment(mcStart, mcEnd);
                            }
                            else
                            {
                                segment = mapRoute.AppendSegment(mcEnd);
                            }

                            // Now add Bing-specific info
                            segment.Distance = item.TravelDistance;
                            segment.Duration = item.TravelDuration;

                        }
                        sb.AppendLine($"Compass={item.CompassDirection} Distance={item.TravelDistance} >> {item.Instruction.Text}");
                    } // for each itinerary

                    routeResult.Route = mapRoute;
                    routeResult.Response = sb.ToString();
                    return routeResult;
                }
                else
                {
                    routeResult.ErrorMessage = "No results found.";
                    return routeResult;
                }

            }
            catch (Exception ex)
            {
                routeResult.ErrorMessage = $"Err={ex.Message}";
                return routeResult;
            }


        }
    }
}
