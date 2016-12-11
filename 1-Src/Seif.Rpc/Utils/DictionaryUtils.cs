using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Web;

namespace Seif.Rpc.Utils
{
    public static class DictionaryUtils
    {
        public static string ToUrlString(IDictionary<string, string> dictionary)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var key in dictionary.Keys)
            {
                var value = HttpUtility.UrlEncode(dictionary[key] ?? "");

                sb.AppendFormat("&{0}={1}", key, value ?? "");
            }
            return sb.ToString();
        }

        public static IDictionary<string, string> GetFromUrl(string urlString)
        {
            if (string.IsNullOrEmpty(urlString)) return new Dictionary<string, string>();

            var paras = urlString.Split(new char[] { '&' }, StringSplitOptions.RemoveEmptyEntries);

            return paras.Select(paraPair => paraPair.Split('='))
                .ToDictionary(keyvalues => keyvalues[0], keyvalues => HttpUtility.UrlDecode(keyvalues[1]));
        }

        public static IDictionary<string, string> GetFromConfig(
            KeyValueConfigurationCollection keyValueConfigurationCollection)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();

            foreach (KeyValueConfigurationElement item in keyValueConfigurationCollection)
            {
                dictionary.Add(item.Key, item.Value);
            }
            return dictionary;
        }
        public static KeyValueConfigurationCollection  ToConfig(
           IDictionary<string, string> dictionary)
        {
            KeyValueConfigurationCollection keyValueConfigurationCollection = new KeyValueConfigurationCollection();

            foreach (var item in dictionary)
            {
                keyValueConfigurationCollection.Add(item.Key, item.Value);
            }
            return keyValueConfigurationCollection;
        }

        public static string TryGetValue(this IDictionary<string, string> dictionary, string key, string defaultValue = null)
        {
            if (dictionary.ContainsKey(key)) return dictionary[key];

            return defaultValue;
        }

        /// <summary>
        /// Merge two dictionary, and create a union set of all elements. If an element appears twice, use value in the <paramref name="addtional"/> dictionary.
        /// </summary>
        /// <param name="dictionary"></param>
        /// <param name="addtional"></param>
        /// <returns></returns>
        public static IDictionary<string, string> MergeAndNew(this IDictionary<string, string> dictionary,
            IDictionary<string, string> addtional)
        {

            var result = dictionary.Clone();
            if (addtional == null || !addtional.Any()) return result;

            foreach (var newKey in addtional.Keys)
            {
                if (result.ContainsKey(newKey))
                {
                    result[newKey] = addtional[newKey];
                }
                else
                {
                    result.Add(new KeyValuePair<string, string>(newKey, addtional[newKey]));
                }
            }
            return result;
        }

        public static IDictionary<string, string> Clone(this IDictionary<string, string> dictionary)
        {
            if (dictionary == null) return null;

            BinaryFormatter formatter = new BinaryFormatter(null, new StreamingContext(StreamingContextStates.Clone));
            MemoryStream stream = new MemoryStream();
            formatter.Serialize(stream, dictionary);
            stream.Position = 0;
            object clonedObj = formatter.Deserialize(stream);
            stream.Close();
            return clonedObj as IDictionary<string, string>;
        } 
    }
}