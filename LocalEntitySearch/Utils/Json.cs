namespace LobalSearch
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.Serialization.Json;
    using System.Text;
    using System.Xml;

    public class Json
    {
        /// <summary>
        /// Parse String array from JSON-encoded string
        /// </summary>
        public static String[] ParseStrings(String json)
        {
            return DeserializeArray<String>(json);
        }

        /// <summary>
        /// Print string array as JSON encoded string
        /// </summary>
        public static String ToJson(String[] strings)
        {
            return Serialize(strings);
        }

        /// <summary>
        /// Serialize object to JSON format.
        /// </summary>
        public static string Serialize<T>(T obj)
        {
            if (obj == null)
            {
                return String.Empty;
            }

            DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
            using (MemoryStream ms = new MemoryStream())
            {
                serializer.WriteObject(ms, obj);
                return Encoding.UTF8.GetString(ms.ToArray());
            }
        }

        /// <summary>
        /// Deserializes the specified serialized string.
        /// </summary>
        /// <typeparam name="T">Type of object to return</typeparam>
        /// <param name="serialized">The serialized object</param>
        /// <returns>instantiated object</returns>
        public static T Deserialize<T>(string serialized) where T : class
        {
            return (T)Deserialize(typeof(T), serialized);
        }

        /// <summary>
        /// Deserializes the given concrete type from the given string
        /// </summary>
        /// <param name="concreteType">concrete type to instantiate</param>
        /// <param name="serialized">serialized version of the object</param>
        /// <returns>instantiated object</returns>
        public static object Deserialize(Type concreteType, string serialized)
        {
            try
            {
                var jsonBytes = Encoding.UTF8.GetBytes(serialized);
                using (var jsonReader = JsonReaderWriterFactory.CreateJsonReader(jsonBytes, XmlDictionaryReaderQuotas.Max))
                {
                    var dcjs = new DataContractJsonSerializer(concreteType);
                    return dcjs.ReadObject(jsonReader);
                }
            }
            catch (Exception e)
            {
                throw new ApplicationException(string.Format("Json.Deserialize exception!\nTYPE: {0}\nINPUT STRING: {1}\nEXCEPTION: {2}\n", concreteType.FullName, serialized, e));
            }
        }

        /// <summary>
        /// Deserialize array from JSON representation. Returns an empty array for empty string.
        /// </summary>
        public static T[] DeserializeArray<T>(string json)
        {
            return String.IsNullOrEmpty(json) ? new T[0] : Deserialize<T[]>(json);
        }

        /// <summary>
        /// Returns the first string that is not empty and not an empty JSON array. Returns empty string if there is no any.
        /// </summary>
        public static string FirstNonEmptyArray(params string[] jsons)
        {
            string result =
                jsons.FirstOrDefault(
                    s =>
                    !String.IsNullOrEmpty(s) && !EmptyArrayJson.Equals(s, StringComparison.InvariantCultureIgnoreCase));
            return !String.IsNullOrEmpty(result) ? result : String.Empty;
        }

        public static bool JsonIsNullOrEmpty(string json)
        {
            return String.IsNullOrEmpty(json) || json == EmptyArrayJson;
        }

        private const string EmptyArrayJson = "[]";
    }

    public static class EnumerableExt
    {
        public static String ToJson<T>(this IEnumerable<T> e)
        {
            return Json.Serialize(new List<T>(e));
        }
    }
}