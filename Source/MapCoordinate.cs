namespace GisAddIn
{
    /// <summary>
    /// Map coordinates are latitude and longitude.
    /// For conventional visualization, i.e. x (horizonatal),y (vertical) - think lon,lat
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
            return $"Lat:{Lat:0.00000} Lon:{Lon:0.00000}";
        }
    }
}
