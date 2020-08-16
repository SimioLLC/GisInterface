using System;
using System.Drawing;

namespace GisAddIn
{
    /// <summary>
    /// Used to convert standard Map routing data to coordinates/names
    /// suitable for Simio Facility view.
    /// </summary>
    public class SimioMapTransform
    {

        /// <summary>
        /// A box that bounds all of the map objects.
        /// Needed to correctly transform coordinates to Simio.
        /// </summary>
        public RectangleF LonLatBoundingBox { get; set; }

        public PointF BoxCenter { get; set; }
        
        /// <summary>
        /// Scaling (x is meters/lon, y is meters/lat)
        /// </summary>
        public PointF SimioScaling { get; set; }

        /// <summary>
        /// The Simio Facility origin.
        /// </summary>
        public PointF Origin { get; set; }


        /// <summary>
        /// Build the bounding box that Simio will use to visualize the data.
        /// Also calculates the origin.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public RectangleF BuildBoundingBox(string x, string y, string width, string height)
        {
            try
            {
                float xx = float.Parse(x);
                float yy = float.Parse(y);
                float ww = float.Parse(width);
                float hh = float.Parse(height);
                this.LonLatBoundingBox = new RectangleF(xx, yy, ww, hh);

                this.BoxCenter = new PointF(xx + ww / 2f, yy + hh / 2f);

                return LonLatBoundingBox;

            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Cannot build bounding box. Err={ex.Message}");
            }
        }

        public PointF BuildSimioScaling(string longitude, string latitude)
        {
            try
            {
                float scaleX = float.Parse(longitude);
                float scaleY = float.Parse(latitude);
                this.SimioScaling = new PointF(scaleX, scaleY);
                return SimioScaling;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Cannot build bounding box. Err={ex.Message}");
            }
        }

        public PointF BuildSimioOrigin(string facilityX, string facilityY)
        {
            try
            {
                float xx = float.Parse(facilityX);
                float yy = float.Parse(facilityY);
                this.Origin = new PointF(xx, yy);
                return this.Origin;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Cannot build bounding box. Err={ex.Message}");
            }
        }


    }
}
