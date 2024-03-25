using BingMapsRESTToolkit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Web.UI;

namespace GisAddIn
{
    /// <summary>
    /// Used to manipulate data to get sample data.
    /// It assumes the existence of a file of addresses called SampleAddresses.json
    /// From this, it can randomly pair them up to create a file of "from" and "to" addresses
    /// as a tab-delimited .csv file.
    /// </summary>
    public static class SampleHelper
    {


        public static bool GetLocationData(string path, out List<GisLocation> locationList, out string explanation)
        {
            explanation = "";
            locationList = new List<GisLocation>();
            try
            {
                if ( !File.Exists(path))
                {
                    explanation = $"No such Path={path}";
                    return false;
                }

                if (!DeserializeFromFile(path, out locationList, out explanation))
                    return false;

                return true;

            }
            catch (Exception ex)
            {
                explanation = $"Path={path} Err={ex.Message}";
                return false;
            }
        }

        public static bool CreatePairedData( List<GisLocation> locationList, int count, out List<GisFromToPair> pairList, out string explanation)
        {
            explanation = "";
            pairList = null;

            if (locationList == null || !locationList.Any())
            {
                explanation = $"LocationList is empty.";
                return false;
            }

            int nn = locationList.Count;
            int maxCount = nn * nn - nn;
            if ( count > maxCount )
            {
                explanation = $"Count={count} exceed maximum={maxCount}";
                return false;
            }

            try
            {
                Random rnd = new Random();

                Dictionary<int, GisFromToPair> pairDict = new Dictionary<int, GisFromToPair>();

                while ( pairDict.Count < count)
                {
                    int iiFrom = rnd.Next(0, locationList.Count);
                    int iiTo = -1;
                    do
                    {
                        iiTo = rnd.Next(0, locationList.Count);
                    } while ( iiTo == iiFrom );

                    GisFromToPair pair = new GisFromToPair(locationList[iiFrom], locationList[iiTo]);

                    if ( !pairDict.ContainsKey(pair.Key))
                    {
                        pairDict.Add(pair.Key, pair);
                    }
                }

                pairList = pairDict.Values.ToList();

                return true;
            }
            catch (Exception ex)
            {
                explanation = $" Err={ex.Message}";
                return false;
            }
        }

        public static bool PutToTabDelimitedPairFile(string path, List<GisFromToPair> pairList, out string explanation)
        {
            explanation = "";

            if ( pairList == null || !pairList.Any())
            {
                explanation = $"Cannot create pair file. PairList contains no entries.";
                return false;
            }

            string folder = Path.GetDirectoryName(path);
            if ( !Directory.Exists(folder))
            {
                explanation = $"Cannot create pair file. Folder={folder} does not exist.";
                return false;
            }


            try
            {
                int ii = 0;
                StringBuilder sb = new StringBuilder();
                foreach ( GisFromToPair pair in pairList)
                {
                    sb.AppendLine($"{ii++}\t{pair.FromLocation}\t{pair.ToLocation}");
                }

                File.WriteAllText(path, sb.ToString());

                return true;

            }
            catch (Exception ex)
            {
                explanation = $"Path={path} Err={ex.Message}";
                return false;
            }
        }

        public static bool GetFromTabDelimitedPairFile(string path, out List<GisFromToPair> pairList, out string explanation)
        {
            explanation = "";
            pairList = new List<GisFromToPair>();

            if ( !File.Exists(path) )
            {
                explanation = $"File={path} does not exist.";
                return false;
            }

            try
            {
                string[] lines = File.ReadAllLines(path);

                int nn = 0;
                foreach ( string line in lines)
                {
                    string[] tokens = line.Split('\t');
                    if ( tokens.Length != 3)
                    {
                        explanation = $"Line={nn} Text={line}. Expected 3 tokens, found={tokens.Length}";
                        return false;
                    }

                    GisLocation fromLoc = GisLocation.ParseFromString(tokens[1], out explanation);
                    if ( fromLoc == null )
                    {
                        explanation = $"Line={nn}. Cannot Parse Origin={tokens[1]}";
                        return false;
                    }
                    GisLocation toLoc = GisLocation.ParseFromString(tokens[2], out explanation);
                    if (toLoc == null)
                    {
                        explanation = $"Line={nn}. Cannot Parse Destination={tokens[2]}";
                        return false;
                    }

                    GisFromToPair pair = new GisFromToPair(fromLoc, toLoc);
                    if ( pair == null )
                    {
                        explanation = $"Line={nn}. Text={line}. Cannot Create Pair.";
                        return false;
                    }

                    pairList.Add(pair);
                    nn++;
                }

                return true;

            }
            catch (Exception ex)
            {
                explanation = $"Path={path} Err={ex.Message}";
                return false;
            }
        }

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
                    explanation = $"No such file={filePath}";
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
                explanation = $"File={filePath} Err={ex}";
                return false;
            }
        } // method

    }

    public class GisLocation
    {
        public string address { get; set; }

        public string city { get; set; }

        public string state { get; set; }

        public string zip { get; set; }


        /// <summary>
        /// Format is address, city state zip
        /// Note that the city contain spaces, and that the only comma in the line follows the address.
        /// </summary>
        /// <param name="encoded"></param>
        /// <param name="explanation"></param>
        /// <returns></returns>
        public static GisLocation ParseFromString(string encoded, out string explanation)
        {
            explanation = "";
            try
            {
                string[] tokens = encoded.Split(',');
                if ( tokens.Count() != 2)
                {
                    explanation = $"Encoded={encoded} Expected a comma following the address";
                    return null;
                }

                GisLocation loc = new GisLocation();
                loc.address = tokens[0];

                string[] fragments = tokens[1].Trim().Split(' ');

                List<string> tokenList = new List<string>();
                foreach ( string frag in fragments )
                {
                    if (frag.Trim() != "")
                        tokenList.Add(frag);
                }

                // The last token is the zip, next to last is the state, and all others are space delimited city
                int count = tokenList.Count;
                if ( count < 3 )
                {
                    explanation = $"Encoded={encoded}. Expected city, state, and zip";
                    return null;
                }

                loc.zip = tokenList[count - 1];

                loc.state = tokenList[count - 2];

                StringBuilder sb = new StringBuilder();
                for (int ii = 0; ii < tokenList.Count - 3; ii++)
                    sb.Append(tokenList[ii] + " ");

                loc.city = sb.ToString().Trim();

                return loc;
            }
            catch (Exception ex)
            {
                explanation = $"Encoded={encoded}. Err={ex.Message}";
                return null;
            }
        }

        public override string ToString()
        {
            return $"{address}, {city} {state} {zip}";
        }

    }

    public class GisFromToPair
    {
        public GisFromToPair(GisLocation fromLoc,  GisLocation toLoc)
        {
            this.FromLocation = fromLoc;
            this.ToLocation = toLoc;

            Key = $"{fromLoc}@@{toLoc}".GetHashCode();
        }
         
        /// <summary>
        /// A hashed key
        /// </summary>
        public int Key { get; }

        public GisLocation FromLocation { get; }

        public GisLocation ToLocation { get; }

        public override string ToString()
        {
            return $"From=[{FromLocation}] To=[{ToLocation}] Key={Key}";
        }

    }


}
