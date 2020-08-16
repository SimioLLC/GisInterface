namespace GisAddIn
{
    /// <summary>
    /// Map coordinates are latitude and longitude.
    /// For visualization, i.e. conventional x,y - think lon,lat
    /// </summary>
    public class MapCoordinate
    {
        public MapCoordinate(double lat, double lon)
        {
            this.Lat = lat;
            this.Lon = lon;
        }

        /// <summary>
        /// Parameterless for deserializing
        /// </summary>
        public MapCoordinate()
        {
            this.Lat = 0;
            this.Lon = 0;
        }

        public double Lat { get; set; }
        public double Lon { get; set; }

        public override string ToString()
        {
            return $"Lat:{Lat:0.0000} Lon:{Lon:0.0000}";
        }
    }
}
