using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static GisAddIn.ArcGisMapHelpers;

namespace GisAddIn
{
    public interface IMapHelper
    {

        /// <summary>
        /// Fetch the key, which is located at 'filepath'
        /// </summary>
        /// <returns></returns>
        string GetKeyStringFromFile(string filepath, out string explanation);

        /// <summary>
        /// Fetch the key, which is located in MyDocuments under folder SimioUserExtensions
        /// in *MapsKey.txt.
        /// </summary>
        /// <returns></returns>
        string GetKeyString();

        /// <summary>
        /// Save the key, to MyDocuments under folder SimioUserExtensions in *MapsKey.txt.
        /// </summary>
        /// <returns></returns>
        void PutKeyString(string MapApiKey);

        /// <summary>
        /// A fetch of a map route from this map provider, for a textual origin and destination address
        /// (e.g. address, city state-abbr zip)
        /// The method places the resulting computation in the SimioMapRoute object.
        /// For diagnostics, the requestUrl to the map provider is returned (if available)
        /// </summary>
        /// <param name="mapKey"></param>
        /// <param name="originAddress"></param>
        /// <param name="destinationAddress"></param>
        SimioRouteResult GetRoute(string mapsKey, string originAddress, string destinationAddress);

        /// <summary>
        /// An async verion of GetRoute
        /// </summary>
        /// <param name="mapsKey"></param>
        /// <param name="originAddress"></param>
        /// <param name="destinationAddress"></param>
        /// <returns></returns>
        Task<SimioRouteResult> GetRouteAsync(string mapsKey, string originAddress, string destinationAddress);

        /// <summary>
        /// Information about the provider.
        /// </summary>
        /// <returns></returns>
        string GetProviderInformation();
    }

    /// <summary>
    /// what we assemble as the result of parsing the
    /// response from the GetRoute(Async) call
    /// </summary>
    public class SimioRouteResult
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
        /// The route between two points (first choice)
        /// </summary>
        public SimioMapRoute Route { get; set; }

        public string ReasonPhrase { get; set; }
        public HttpResponseHeaders Headers { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string ErrorMessage { get; set; }
    }


}
