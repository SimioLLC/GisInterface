// VisioUtility.cs
// compile with: /doc:VisioUtility.xml
// <copyright>Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>
// <summary>This file contains utility classes and methods that perform
// operations commonly performed on Visio objects.</summary>

using System;
using System.Diagnostics;
using System.Resources;
using System.Windows.Forms;

namespace GisAddIn
{
    using SimioAPI;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web.Script.Serialization;

    public static class GisHelpers
    {
        /// <summary>
        /// Serialize data to a text file.
        /// </summary>
        /// <param name="dataToSerialize"></param>
        /// <param name="filePath"></param>
        /// <param name="explanation"></param>
        /// <returns></returns>
        public static bool SerializeToFile<T>(string filePath, T dataToSerialize, bool appendToFile, out string explanation)

        {
            explanation = "";
            try
            {
                JavaScriptSerializer jss = new JavaScriptSerializer();
                jss.MaxJsonLength = 100000000;
                var jsonString = jss.Serialize(dataToSerialize);

                if (appendToFile)
                {
                    File.AppendAllText(filePath, jsonString);

                }
                else
                {
                    if (File.Exists(filePath))
                        File.Delete(filePath);

                    File.WriteAllText(filePath, String.Format("{0}\n", jsonString));
                }

                return true;
            }
            catch (Exception ex)
            {
                explanation = string.Format("File={0} Err={1}", filePath, ex.ToString());
                return false;
            }
        } // method

        /// <summary>
        /// Deserialize data from a text file.
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="deserializedData"></param>
        /// <param name="explanation"></param>
        /// <returns></returns>
        public static bool DeserializeFromFile<T>(string filePath, out T deserializedData, out string explanation)

        {
            explanation = "";
            deserializedData = default(T);

            try
            {
                JavaScriptSerializer jss = new JavaScriptSerializer();

                if (!File.Exists(filePath))
                {
                    explanation = string.Format("No such file={0}", filePath);
                    return false;

                }
                else
                {
                    string jsonString = File.ReadAllText(filePath);

                    jss.MaxJsonLength = 100000000;
                    deserializedData = (T)jss.Deserialize(jsonString, typeof(T));
                }

                return true;
            }
            catch (Exception ex)
            {
                explanation = string.Format("File={0} Err={1}", filePath, ex.ToString());
                return false;
            }
        } // method

        /// <summary>
        /// Serialize data to a text file.
        /// </summary>
        /// <param name="jsonString"></param>
        /// <param name="deserializedData"></param>
        /// <param name="explanation"></param>
        /// <returns></returns>
        public static bool DeserializeFromString<T>(string jsonString, out T deserializedData, out string explanation)

        {
            explanation = "";
            deserializedData = default(T);

            try
            {
                JavaScriptSerializer jss = new JavaScriptSerializer();

                deserializedData = (T)jss.Deserialize(jsonString, typeof(T));

                return true;
            }
            catch (Exception ex)
            {
                explanation = string.Format("Err={0} String={1}", ex.ToString(), jsonString);
                return false;
            }
        } // method


        /// <summary>
        /// Given a transformation matrix and the lat, lon, convert to Simio units.
        /// Assumptions. The Latitude is Y, and the Longitude is X,
        /// and the latitude is negated to fit with the Simio scheme.
        /// which becomes Lat=-Z, and Long X, in simio
        /// </summary>
        /// <param name="mat">Transformation matrix</param>
        /// <param name="lat"></param>
        /// <param name="lon"></param>
        /// <returns></returns>
        public static FacilityLocation LatLonToFacilityLocation(Matrix mat, double lat, double lon)
        {
            PointF[] points = new PointF[1] { new PointF((float)lon, (float) lat) };

            mat.TransformPoints(points);

            PointF pt = points[0];

            FacilityLocation loc = new FacilityLocation(pt.X, 0, -pt.Y);
            return loc;

        }

    }
}

