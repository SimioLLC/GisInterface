using Geo.Extensions.DependencyInjection;
using GoogleApi.Entities.Common;
using GoogleApi.Entities.Interfaces;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using static GoogleApi.GoogleMaps;
using System.Windows.Forms;
using System.Text.Json;
using BingMapsRESTToolkit;
using GoogleApi.Entities.Maps.Elevation.Response;

namespace GisAddIn
{
    /// <summary>
    /// Bing Map Provider
    /// </summary>
    public class ArcGisMapHelpers : IMapHelper
    {
        HttpClient myHttpClient = new HttpClient();

        public string GetProviderInformation()
        {
            return $"ArcGIS Maps.";
        }

        /// <summary>
        /// Fetch the key, which is located in MyDocuments under folder SimioUserExtensions
        /// in ArcGisMapsKey.txt.
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

                string ArcGisMapKey = File.ReadAllText(filepath).Trim();
                return ArcGisMapKey;
            }
            catch (Exception ex)
            {
                explanation = $"Cannot Get Key from File={filepath}. Err={ex.Message}";
                return string.Empty;
            }
        }

        /// <summary>
        /// Fetch the key, which is located in MyDocuments under folder SimioUserExtensions
        /// in ArcGisMapsKey.txt.
        /// </summary>
        /// <returns></returns>
        public string GetKeyString()
        {
            string filepath = "";

            try
            {
                string folderpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
                filepath = Path.Combine(folderpath, "SimioUserExtensions", "ArcGisMapsKey.txt");

                if (!File.Exists(filepath))
                    throw new ApplicationException($"Cannot find Filepath={filepath}");

                string ArcGisMapKey = File.ReadAllText(filepath).Trim();
                return ArcGisMapKey;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Make sure you have a ArcGisMapsKey file here={filepath}. Err={ex}");
            }
        }

        /// <summary>
        /// Fetch the key, which is located in MyDocuments under folder SimioUserExtensions
        /// in ArcGisMapsKey.txt.
        /// </summary>
        /// <returns></returns>
        public string GetKeyString(out string explanation)
        {
            explanation = "";

            try
            {
                string folderpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
                string filepath = Path.Combine(folderpath, "SimioUserExtensions", "ArcGisMapsKey.txt");

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
        /// in ArcGisMapsKey.txt.
        /// </summary>
        /// <returns></returns>
        public void PutKeyString(string ArcGisMapKey)
        {
            string filepath = "";

            try
            {
                string folderpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
                filepath = Path.Combine(folderpath, "SimioUserExtensions", "ArcGisMapsKey.txt");

                if (!Directory.Exists(folderpath))
                    throw new ApplicationException($"Cannot find Folder={folderpath}");

                File.WriteAllText(filepath, ArcGisMapKey);
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
        SimioRouteResult IMapHelper.GetRoute(string mapsKey, string originAddress, string destinationAddress )
        {

            SimioRouteResult routeResult = new SimioRouteResult(); ;
            try
            {
                var task = Task<Response>.Run(async () =>
                {
                    routeResult = await GetRouteAsync(mapsKey, originAddress, destinationAddress);

                    return routeResult;
                });

                return null;
            }
            catch (Exception ex)
            {
                routeResult.ErrorMessage = $"Route. Start={originAddress}. Finish={destinationAddress}. Err={ex.Message}";
                return routeResult;
            }

        }



        /// <summary>
        /// Convert a geocode textual address (e.g. Sewickley, PA) to a location
        /// </summary>
        /// <param name="address"></param>
        /// <param name="apiKey"></param>
        /// <returns></returns>
        public async Task<GeocodeResult> GeocodeToLocationAsync(string mapApiKey, string address)
        {
            string arcgisGeocodingUrl = $"https://geocode.arcgis.com/arcgis/rest/services/World/GeocodeServer/findAddressCandidates";
            string parameters = $"?SingleLine={Uri.EscapeDataString(address)}&outFields=Match_addr,Score&outSR=4326&f=json&apiKey={mapApiKey}";
            string requestUrl = arcgisGeocodingUrl + parameters;

            var result = new GeocodeResult();
            try
            {
                using (HttpResponseMessage response = await myHttpClient.GetAsync(new Uri(requestUrl)))
                {
                    using (HttpContent content = response.Content)
                    {
                        // need these to return to Form for display
                        string resultString = await content.ReadAsStringAsync();
                        string reasonPhrase = response.ReasonPhrase;
                        HttpResponseHeaders headers = response.Headers;
                        HttpStatusCode code = response.StatusCode;

                        var geocodeResponse = JsonSerializer.Deserialize<GeocodeResponse>(resultString);
                        if (geocodeResponse?.candidates != null && geocodeResponse?.candidates.Count > 0)
                        {
                            var location = geocodeResponse.candidates[0].location;
                            Console.WriteLine($"Geocode: Addr={address} Location={location.x}, {location.y}");
                            result.Location = location;
                        }
                        else
                        {
                            Console.WriteLine("Route not found.");
                        }
                        result.Request = requestUrl;
                        result.Response = resultString;
                        result.ReasonPhrase = reasonPhrase;
                        result.Headers = headers;
                        result.StatusCode = code;
                    }
                }
            }
            catch (Exception ex)
            {
                // need to return ex.message for display.
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// Get the route for the given addresses.
        /// The Result returns the Route
        /// </summary>
        /// <param name="addressStart"></param>
        /// <param name="addressFinish"></param>
        /// <param name="mapApiKey"></param>
        /// <returns></returns>
        public async Task<SimioRouteResult> GetRouteAsync(string mapApiKey, string addressStart, string addressFinish)
        {
            string arcgisRouteServiceUrl = "https://route-api.arcgis.com/arcgis/rest/services/World/Route/NAServer/Route_World/solve";

            var resultStart = await GeocodeToLocationAsync(mapApiKey, addressStart);
            var resultFinish = await GeocodeToLocationAsync(mapApiKey, addressFinish);

            string startLatLon = $"{resultStart.Location.x},{resultStart.Location.y}";
            string finishLatLon = $"{resultFinish.Location.x},{resultFinish.Location.y}";

            // Construct the request URL
            string requestUrl = $"{arcgisRouteServiceUrl}?token={mapApiKey}&f=json&stops={startLatLon};{finishLatLon}";

            var result = new SimioRouteResult();
            try
            {
                using (HttpResponseMessage response = await myHttpClient.GetAsync(new Uri(requestUrl)))
                {
                    using (HttpContent content = response.Content)
                    {
                        // need these to return to Form for display
                        string responseString = await content.ReadAsStringAsync();
                        string reasonPhrase = response.ReasonPhrase;
                        HttpResponseHeaders headers = response.Headers;
                        HttpStatusCode code = response.StatusCode;

                        var routeResponse = JsonSerializer.Deserialize<AgRouteResponse>(responseString);
                        if (routeResponse?.routes != null && routeResponse?.routes.features.Count > 0)
                        {
                            result.Route = new SimioMapRoute(addressStart, addressFinish);
                            var feature = routeResponse.routes.features[0];

                            var paths = feature.geometry.paths;
                            foreach (var path in paths)
                            {
                                SimioMapRoute simRoute = new SimioMapRoute();
                                int segmentIndex = 0;
                                int pairIndex = 0;
                                MapSegment segment = null;

                                // Each coordinate pair will belong to two segments, so use
                                // the index modulus to create segments from the path.
                                foreach (var pair in path)
                                {
                                    MapCoordinate coord = new MapCoordinate(pair[1], pair[0]);
                                    switch (pairIndex % 2)
                                    {
                                        case 0:
                                            segment = new MapSegment();
                                            segment.Index = segmentIndex++;
                                            segment.StartLocation = coord;
                                            break;
                                        case 1:
                                            if (segment != null)
                                            {
                                                segment.EndLocation = coord;
                                                simRoute.SegmentList.Add(segment);
                                            }
                                            break;
                                    }

                                    pairIndex++;
                                }
                                result.Route = simRoute;
                            }

                            Console.WriteLine($"Feature: {feature.attributes.Total_Length} miles");
                        }
                        else
                        {
                            Console.WriteLine("Address not found.");
                        }
                        result.Request = requestUrl;
                        result.Response = responseString;
                        result.ReasonPhrase = reasonPhrase;
                        result.Headers = headers;
                        result.StatusCode = code;
                    }
                }
            }
            catch (Exception ex)
            {
                // need to return ex.message for display.
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        //=====================================================
        /// <summary>
        /// The response from the Request to ArcGIS Route request.
        /// Deserialized from the JSON.
        /// </summary>
        public class AgRouteResponse
        {
            public AgRoutes routes { get; set; }
        }

        public class AgRoutes
        {
            public List<AgFeature> features { get; set; }
        }
        public class AgFeature
        {
            public AgAttributes attributes { get; set; }
            public AgGeometry geometry { get; set; }
        }

        public class AgAttributes
        {
            public int ObjectID { get; set; }
            public string Name { get; set; }
            public double Total_Length { get; set; }
        }

        public class AgGeometry
        {
            public List<List<List<double>>> paths { get; set; }
        }

        /// <summary>
        /// A geographical lon,lat coordinate
        /// </summary>
        public class AgCoordinate
        {
            public double Lon { get; set; }
            public double Lat { get; set; }

            // Deserialize from a JSON array [x,y]
            public static implicit operator AgCoordinate(List<double> coordinates) => new AgCoordinate { Lon = coordinates[0], Lat = coordinates[1] };
        }

        ////public class SimioRoute
        ////{
        ////    public string Name { get; set; }
        ////    public List<SimioPath> PathList { get; set; } = new List<SimioPath>();

        ////    public SimioRoute(string name)
        ////    {
        ////        Name = name;
        ////        PathList = new List<SimioPath>();
        ////    }
        ////    public override string ToString()
        ////    {
        ////        return $"Name={Name}. Contains {PathList.Count} Routes.";
        ////    }


        ////}

        ////public class SimioPath
        ////{
        ////    public string Name { get; set; }

        ////    public List<SimioLatLon> LatLonList { get; set; } = new List<SimioLatLon>();

        ////    public SimioPath(string name)
        ////    {
        ////        Name = name;
        ////        LatLonList = new List<SimioLatLon>();
        ////    }

        ////    public override string ToString()
        ////    {
        ////        return $"Name={Name}. Contains {LatLonList.Count} Coordinates.";
        ////    }
        ////}

        ////public class SimioLatLon
        ////{
        ////    public double Latitude { get; set; }
        ////    public double Longitude { get; set; }

        ////    public SimioLatLon(double latitude, double longitude)
        ////    {
        ////        Latitude = latitude;
        ////        Longitude = longitude;
        ////    }

        ////    public override string ToString()
        ////    {
        ////        return $"Lat={Latitude},Lon={Longitude}";
        ////    }
        ////}


        /// <summary>
        /// what we assemble as the result of parsing the
        /// response from the ArcGIS REST call
        /// </summary>
        public class RouteResult
        {
            /// <summary>
            /// What we sent
            /// </summary>
            public string Request { get; set; }
            /// <summary>
            /// What we got back
            /// </summary>
            public string Response { get; set; }

            /// <summary>
            /// The computed location (first choice)
            /// </summary>
            public SimioMapRoute Route { get; set; }

            public string ReasonPhrase { get; set; }
            public HttpResponseHeaders Headers { get; set; }
            public HttpStatusCode StatusCode { get; set; }
            public string ErrorMessage { get; set; }
        }

        //==========================================================
        /// <summary>
        /// The response from the HTTP request to ArcGIS for geocode info
        /// Deserialized from the JSON.
        /// </summary>
        public class GeocodeResponse
        {
            public List<Candidate> candidates { get; set; }
        }

        public class Candidate
        {
            public ArcGisLocation location { get; set; }
            public string address { get; set; }
        }

        public class ArcGisLocation
        {
            public double x { get; set; }
            public double y { get; set; }
        }

        public class GeocodeResult
        {
            /// <summary>
            /// What we sent
            /// </summary>
            public string Request { get; set; }
            /// <summary>
            /// What we got back
            /// </summary>
            public string Response { get; set; }

            /// <summary>
            /// The computed location (first choice and deserialized)
            /// </summary>
            public ArcGisLocation Location { get; set; }

            public string ReasonPhrase { get; set; }
            public HttpResponseHeaders Headers { get; set; }
            public HttpStatusCode StatusCode { get; set; }
            public string ErrorMessage { get; set; }
        }
    }


}





