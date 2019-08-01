using BingMapsRESTToolkit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GisAddIn
{
    public class BingMapHelpers
    {
        /// <summary>
        /// An asynchronous fetch of a map route (untested)
        /// </summary>
        /// <param name="mapData"></param>
        /// <param name="bingMapsKey"></param>
        /// <param name="StartStopList"></param>
        public static void GetMapRouteAsync(SimioMapData mapData, string bingMapsKey, List<string> StartStopList)
        {
            try
            {
                List<SimpleWaypoint> wayPointList = new List<SimpleWaypoint>();
                foreach (string location in StartStopList)
                {
                    SimpleWaypoint wayPoint = new SimpleWaypoint(location);
                    wayPointList.Add(wayPoint);
                }

                if (true)
                {
                    var request = new RouteRequest();
                    request.Waypoints = wayPointList;
                    request.BingMapsKey = bingMapsKey;

                    Task<Response> t = request.Execute();
                    var result = t.Result;

                    t.RunSynchronously();

                    var r = t.Result;
                    if (r != null && r. ResourceSets != null &&
                        r.ResourceSets.Length > 0 &&
                        r.ResourceSets[0].Resources != null &&
                        r.ResourceSets[0].Resources.Length > 0)
                    {
                        for (var i = 0; i < r.ResourceSets[0].Resources.Length; i++)
                        {
                            throw new ApplicationException((r.ResourceSets[0].Resources[i] as Location).Name);
                        }
                    }
                    else
                    {
                        throw new ApplicationException("No results found.");
                    }

                }

                if ( true )
                {
                    Task<Response> t = ServiceManager.GetResponseAsync(new RouteRequest()
                    {
                        BingMapsKey = bingMapsKey,
                        Waypoints = wayPointList
                    });

                    t.RunSynchronously();

                    var r = ServiceManager.GetResponseAsync(new RouteRequest()
                    {
                        BingMapsKey = bingMapsKey,
                        Waypoints = wayPointList
                    }).GetAwaiter().GetResult();
                    if (r != null && r.ResourceSets != null &&
                        r.ResourceSets.Length > 0 &&
                        r.ResourceSets[0].Resources != null &&
                        r.ResourceSets[0].Resources.Length > 0)
                    {
                        for (var i = 0; i < r.ResourceSets[0].Resources.Length; i++)
                        {
                            throw new ApplicationException((r.ResourceSets[0].Resources[i] as Location).Name);
                        }
                    }
                    else
                    {
                        throw new ApplicationException("No results found.");
                    }

                }

                Console.ReadLine();

            }
            catch (Exception ex)
            {
                string x = $"Err={ex}";
            }
        }

        public static void GetMapGeoCode(SimioMapData mapData, string bingMapsKey, string query, List<string> nameList)
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
        public static string GetKeyString()
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
                throw new ApplicationException($"Cannot Get KeyFile={filepath}. Err={ex}");
            }
        }

        /// <summary>
        /// Save the key, which is located in MyDocuments under folder SimioUserExtensions
        /// in BingMapsKey.txt.
        /// </summary>
        /// <returns></returns>
        public static void PutKeyString(string bingMapKey)
        {
            string filepath = "";

            try
            {
                string folderpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
                filepath = Path.Combine(folderpath, "SimioUserExtensions", "BingMapsKey.txt");

                if (!File.Exists(filepath))
                    throw new ApplicationException($"Cannot find Filepath={filepath}");

                File.WriteAllText(filepath, bingMapKey);
                return;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Cannot Put KeyFile={filepath}. Err={ex}");
            }
        }

    }
}
