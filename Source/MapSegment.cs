namespace GisAddIn
{
    /// <summary>
    /// A segment of a path, consisting of a Start and End MapCoordinate,
    /// and perhaps other data (time/duration) as well
    /// The 'start' of a segment (except the first) should always be equal to the 'end' of the previous segment.
    /// </summary>
    public class MapSegment
    {
        /// <summary>
        /// Should be unique within a path.
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// The first location
        /// </summary>
        public MapCoordinate StartLocation { get; set; }

        /// <summary>
        /// The last location.
        /// </summary>
        public MapCoordinate EndLocation { get; set; }

        public int Duration { get; set; }

        public int Distance { get; set; }

        public MapSegment(int index, MapCoordinate start, MapCoordinate end)
        {
            this.Index = index;
            this.StartLocation = start;
            this.EndLocation = end;

            this.Distance = 0;
            this.Duration = 0;
        }

        public MapSegment()
        {
            this.Index = -1;
            this.StartLocation = null;
            this.EndLocation = null;

            this.Distance = 0;
            this.Duration = 0;

        }

        public override string ToString()
        {
            return $"Index={Index} From={StartLocation} To={EndLocation}";
        }

    }
}
