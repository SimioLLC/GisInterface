using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GisAddIn
{
    /// <summary>
    /// Primarily a list of routes.
    /// Map route providers now often provide a list of routes to
    /// choose from.
    /// </summary>
    public class SimioMapRoutes
    {
        public string Name { get; set; }

        /// <summary>
        /// Info such as provider name and version.
        /// </summary>
        public string ProviderInformation { get; set; }

        public List<SimioMapRoute> RouteList { get; set; }

        public SimioMapRoutes(string name)
        {
            this.Name = name;
            RouteList = new List<SimioMapRoute>();
        }

        /// <summary>
        /// Parameterless constructor for deserializing
        /// </summary>
        public SimioMapRoutes()
        {
            Name = "";
            RouteList = new List<SimioMapRoute>();
        }

        public override string ToString()
        {
            return $"Routes: Name={Name} Routes={RouteList.Count}";
        }

        public string BuildDisplayString()
        {
            StringBuilder sb = new StringBuilder($"Name={this.Name} has {RouteList.Count} routes.");

            foreach ( SimioMapRoute route in RouteList)
            {
                sb.AppendLine(route.BuildDisplayString());
            }

            return sb.ToString();
        }

    }
}
