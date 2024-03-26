using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using System.Threading.Tasks;

//using GoogleMapsRESTToolkit;
using GoogleApi;
using GoogleApi.Entities.Common;
using GoogleApi.Entities.Maps.Common;
using GoogleApi.Entities.Maps.Directions.Request;
using GoogleApi.Entities.Maps.Directions.Response;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using GoogleApi.Entities.Maps.Geocoding.PlusCode.Request;
//using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using GoogleApi.Entities.Maps.Routes.Directions.Response;
using System.Configuration;


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

        HttpClient myHttpClient = new HttpClient();



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

        public SimioRouteResult GetRoute(string mapsKey, string originAddress, string destinationAddress)
        {
            throw new NotImplementedException();
        }

        public async Task<SimioRouteResult> GetRouteAsync(string mapKey, string originAddress, string destinationAddress)
        {
            var baseUrl = "https://maps.googleapis.com/maps/api/directions/json";
            var requestUri = $"{baseUrl}?origin={originAddress}&destination={destinationAddress}&key={mapKey}";

            var result = new SimioRouteResult();

            using (HttpResponseMessage response = await myHttpClient.GetAsync(requestUri))
            {
                if (response.IsSuccessStatusCode)
                {
                    // need these to return to Form for display
                    var responseBody = await response.Content.ReadAsStringAsync();

                    string reasonPhrase = response.ReasonPhrase;
                    HttpResponseHeaders headers = response.Headers;
                    HttpStatusCode code = response.StatusCode;

                    DirectionsResponse directionResponse = JsonSerializer.Deserialize<DirectionsResponse>(responseBody);
                    // Assuming you want to process the first route, first leg, and its steps
                    if (directionResponse?.Routes != null && directionResponse.Routes.Count > 0)
                    {
                        result.Route = new SimioMapRoute(originAddress, destinationAddress);
                        int segmentIndex = 0;
                        foreach (var step in directionResponse.Routes[0].Legs[0].Steps)
                        {
                            MapCoordinate coordStart = new MapCoordinate(step.StartLocation.Latitude, step.StartLocation.Longitude);
                            MapCoordinate coordEnd = new MapCoordinate(step.EndLocation.Latitude, step.EndLocation.Longitude);

                            MapSegment segment = new MapSegment(segmentIndex, coordStart, coordEnd);

                            segment.Duration = step.Duration.Value;
                            segment.Distance = step.Distance.Value;

                            result.Route.SegmentList.Add(segment);
                            segmentIndex++;
                        }
                    }
                    return result;

                } // using response

                result.ErrorMessage = $"Could not retrieve route";
                return result;

            } // method
        }

        //================================================================
        // These classes are used to deserialize the json response
        //================================================================

        /// <summary>
        /// Top level. A list of routes
        /// </summary>
        public class DirectionsResponse
        {
            [JsonPropertyName("geocoded_waypoints")]
            public List<GeocodedWaypoint> GeocodedWaypoints { get; set; }

            [JsonPropertyName("routes")]
            public List<Route> Routes { get; set; }

            [JsonPropertyName("status")]
            public string Status { get; set; }
        }

        public class GeocodedWaypoint
        {
            [JsonPropertyName("geocoder_status")]
            public string GeocoderStatus { get; set; }

            [JsonPropertyName("place_id")]
            public string PlaceId { get; set; }

            [JsonPropertyName("types")]
            public List<string> Types { get; set; }
        }

        public class Route
        {
            [JsonPropertyName("legs")]
            public List<Leg> Legs { get; set; }

            // Include other properties of a Route as needed
        }

        public class Leg
        {
            [JsonPropertyName("steps")]
            public List<Step> Steps { get; set; }

            // Include other properties of a Leg as needed
        }

        public class Step
        {
            [JsonPropertyName("start_location")]
            public Location StartLocation { get; set; }

            [JsonPropertyName("end_location")]
            public Location EndLocation { get; set; }

            [JsonPropertyName("distance")]
            public TextValue Distance { get; set; }

            [JsonPropertyName("duration")]
            public TextValue Duration { get; set; }

            [JsonPropertyName("html_instructions")]
            public string HtmlInstructions { get; set; }

            [JsonPropertyName("polyline")]
            public Polyline Polyline { get; set; }

            [JsonPropertyName("travel_mode")]
            public string TravelMode { get; set; }

            [JsonPropertyName("maneuver")]
            public string Maneuver { get; set; } // Optional, not always present

            [JsonPropertyName("transit_details")]
            public TransitDetails TransitDetails { get; set; } // Optional, for transit mode

            // Include other properties of a Step as needed
        }
        public class TextValue
        {
            [JsonPropertyName("text")]
            public string Text { get; set; }

            [JsonPropertyName("value")]
            public int Value { get; set; }
        }

        public class Location
        {
            [JsonPropertyName("lat")]
            public double Latitude { get; set; }

            [JsonPropertyName("lng")]
            public double Longitude { get; set; }
        }

        public class Polyline
        {
            [JsonPropertyName("points")]
            public string Points { get; set; }
        }

        // Example of a TransitDetails class, focusing on a few properties for demonstration
        public class TransitDetails
        {
            [JsonPropertyName("arrival_stop")]
            public TransitStop ArrivalStop { get; set; }

            [JsonPropertyName("departure_stop")]
            public TransitStop DepartureStop { get; set; }

            [JsonPropertyName("line")]
            public TransitLine Line { get; set; }

            // Add additional properties as needed
        }

        public class TransitStop
        {
            [JsonPropertyName("name")]
            public string Name { get; set; }

            [JsonPropertyName("location")]
            public Location Location { get; set; }
        }

        public class TransitLine
        {
            [JsonPropertyName("name")]
            public string Name { get; set; }

            [JsonPropertyName("short_name")]
            public string ShortName { get; set; }

            [JsonPropertyName("color")]
            public string Color { get; set; }

            // Add additional properties as needed
        }

    }

}



