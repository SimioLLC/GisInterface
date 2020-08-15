using System;
using System.IO;
using System.Reflection;
using System.Web.Script.Serialization;

namespace PortalManager
{
    public static class JsonHelpers
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
                explanation = $"File={filePath} Err={ex.Message}";
                return false;
            }
        } // method

        /// <summary>
        /// Deserialize data from a text file.
        /// </summary>
        /// <param name="filePath">Full path to file</param>
        /// <param name="deserializedData"></param>
        /// <param name="explanation">...if false is returned</param>
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
                explanation = $"File={filePath} Err={ex.Message}";
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
        /// Copy all read/write scriptable properties from fromObj to toObj.
        /// Uses reflection.
        /// </summary>
        /// <returns></returns>
        public static bool CopyScriptableProperties(Object fromObj, Object toObj)
        {
            // Build a dictionary of our properties (todo: use attributes?)
            foreach (PropertyInfo info in fromObj.GetType().GetProperties())
            {
                try
                {
                    if (info.CanRead && info.CanWrite)
                    {
                        foreach (object attr in info.GetCustomAttributes())
                        {
                            if (attr is ScriptIgnoreAttribute)
                                goto GetNextPropertyInfo;
                        }

                        string name = info.Name;
                        var objValue = info.GetValue(fromObj);

                        info.SetValue(toObj, objValue);

                    } // read/write property?

                }
                catch (Exception ex)
                {
                    throw new ApplicationException($"Property={info.Name} Cannot copy. Err={ex.Message}");
                }

            GetNextPropertyInfo:;
            } // for each property
            return true;
        }

    }
}
