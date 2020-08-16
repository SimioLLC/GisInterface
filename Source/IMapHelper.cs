using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        void PutKeyString(string bingMapKey);

        /// <summary>
        /// A fetch of a map route from this map provider, for a textual origin and destination address
        /// (e.g. address, city state-abbr zip)
        /// The method places the resulting computation in the SimioMapRoute object.
        /// For diagnostics, the requestUrl to the map provider is returned (if available)
        /// </summary>
        /// <param name="mapData"></param>
        /// <param name="googleMapsKey"></param>
        /// <param name="StartStopList"></param>
        bool GetRoute(string mapsKey, string originAddress, string destinationAddress,
            out SimioMapRoute mapRoute,
            out string requestUrl, out string explanation);

        /// <summary>
        /// Information about the provider.
        /// </summary>
        /// <returns></returns>
        string GetProviderInformation();
    }
}
