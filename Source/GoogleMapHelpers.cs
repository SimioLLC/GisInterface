using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using System.Threading.Tasks;

//using GoogleMapsRESTToolkit;
using GoogleApi;
using GoogleApi.Entities.Maps.Directions.Request;
using GoogleApi.Entities.Maps.Directions.Response;
using GoogleApi.Entities.Common;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;

namespace GisAddIn
{
    /// <summary>
    /// Google Map Provider
    /// </summary>
    public class GoogleMapHelpers : IMapHelper
    {
        public string GetProviderInformation()
        {
            return $"Google Maps.";
        }

        /// <summary>
        /// An asynchronous fetch of a map route (untested)
        /// </summary>
        /// <param name="mapData"></param>
        /// <param name="googleMapsKey"></param>
        /// <param name="StartStopList"></param>
        public bool GetRoute(string googleMapsKey, string originAddress, string destinationAddress, 
            out SimioMapRoute mapRoute,
             out string requestUrl, out string explanation)
        {
            requestUrl = "";
            explanation = "";
            mapRoute = null;
            try
            {
                mapRoute = new SimioMapRoute(originAddress, destinationAddress);

                DirectionsRequest request = new DirectionsRequest
                {
                    Key = googleMapsKey,

                    Origin = new Location(originAddress),
                    Destination = new Location(destinationAddress)
                };

                requestUrl = request.ToString();
                var response = GoogleApi.GoogleMaps.Directions.Query(request);

                mapRoute.SegmentList.Clear();

                // .. wait for response ..
                // Response has Routes, and Routes have legs. Typically 1-2 routes
                foreach (Route route in response.Routes)
                {
                    // Route has an overview summary and boundingbox
                    // We could make the bounding box from the one that Bing Maps sends us,
                    //but we're going to do our own to match the usa 'map'.
                    ////PointF ptLoc = new PointF((float)route.Bounds.NorthEast.Latitude, (float)route.Bounds.NorthEast.Longitude);
                    ////float width = (float)(route.Bounds.NorthEast.Latitude - route.Bounds.SouthWest.Latitude);
                    ////float height = (float)(route.Bounds.NorthEast.Longitude - route.Bounds.SouthWest.Longitude);

                    ////// We're going to bound according to the contiguous USA, which is appox.
                    ////// lat 20 to 50, and lon -60 to -130
                    ////PointF ptLoc = lonlatBox.Location; // new PointF( -130f, 20f);
                    ////float width = lonlatBox.Width; // 70f;
                    ////float height = lonlatBox.Height; // 30f; 

                    ////// Turning the thing on its side, since we want latitude to be 'Y'
                    ////mapData.LonLatBoundingBox = new RectangleF(ptLoc.X, ptLoc.Y, width, height);
                    ////mapData.Origin = new PointF(ptLoc.X + width / 2f, ptLoc.Y + height / 2f);

                    ////mapData.SimioScaling = simioScaling;

                    int ii = 0;
                    StringBuilder sb = new StringBuilder();
                    foreach (Leg leg in route.Legs)
                    {
                        // Legs have distance and duration
                        // and StartLocation and address, and EndLocation and address
                        foreach (Step step in leg.Steps)
                        {
                            double lat = step.StartLocation.Latitude;
                            double lon = step.StartLocation.Longitude;
                            MapCoordinate mcStart = new MapCoordinate(lat, lon);

                            lat = step.EndLocation.Latitude;
                            lon = step.EndLocation.Longitude;

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

                            // Now add Google-specific information
                            segment.Distance = step.Distance.Value;
                            segment.Duration = step.Duration.Value;

                            ii++;
                            sb.AppendLine($"Instructions={step.HtmlInstructions} Distance={step.Distance.Text} >> {step.Duration.Text}");
                        } // foreach step



                    } // foreach leg
                    explanation = sb.ToString();
                }
                return true;
            }
            catch (Exception ex)
            {
                explanation = $"From={originAddress} To={destinationAddress} Err={ex}";
                return false;
            }
        }


        /// <summary>
        /// Fetch the key, which is located in MyDocuments under folder SimioUserExtensions
        /// in GoogleMapsKey.txt.
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

                string googleMapKey = File.ReadAllText(filepath).Trim();
                return googleMapKey;
            }
            catch (Exception ex)
            {
                explanation = $"Cannot Get Key from File={filepath}. Err={ex.Message}";
                return string.Empty;
            }
        }

        /// <summary>
        /// Fetch the key, which is located in MyDocuments under folder SimioUserExtensions
        /// in GoogleMapsKey.txt.
        /// </summary>
        /// <returns></returns>
        public string GetKeyString()
        {
            string filepath = "";

            try
            {
                string folderpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
                filepath = Path.Combine(folderpath, "SimioUserExtensions", "GoogleMapsKey.txt");

                if (!File.Exists(filepath))
                    throw new ApplicationException($"Cannot find Filepath={filepath}");

                string googleMapKey = File.ReadAllText(filepath).Trim();
                return googleMapKey;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Cannot Get KeyFile={filepath}. Err={ex}");
            }
        }

        /// <summary>
        /// Fetch the key, which is located in MyDocuments under folder SimioUserExtensions
        /// in GoogleMapsKey.txt.
        /// </summary>
        /// <returns></returns>
        public string GetKeyString(out string explanation)
        {
            explanation = "";

            try
            {
                string folderpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
                string filepath = Path.Combine(folderpath, "SimioUserExtensions", "GoogleMapsKey.txt");

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
        /// in GoogleMapsKey.txt.
        /// </summary>
        /// <returns></returns>
        public void PutKeyString(string googleMapKey)
        {
            string filepath = "";

            try
            {
                string folderpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
                filepath = Path.Combine(folderpath, "SimioUserExtensions", "GoogleMapsKey.txt");

                if (!Directory.Exists(folderpath))
                    throw new ApplicationException($"Cannot find Folder={folderpath}");

                File.WriteAllText(filepath, googleMapKey);
                return;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Cannot Put KeyFile={filepath}. Err={ex}");
            }
        }

    }
}



