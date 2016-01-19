using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Web.Script.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace Jita.Common
{
    /// <summary>
    /// This class implements helper methods for serialization/deserialization
    /// </summary>
    public static class com_SerializationHelper
    {
        /// <summary>
        /// Serialize an object using the BinaryFormatter.
        /// </summary>
        /// <param name="item">The object that must be serialized</param>
        /// <returns>A Base64 string.</returns>
        public static string BinarySerialize(object item)
        {
            if (item != null)
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    binaryFormatter.Serialize(memoryStream, item);
                    return Convert.ToBase64String(memoryStream.ToArray());
                }
            }
            return null;
        }

        /// <summary>
        /// Deserialize an Base64 string into an object using the BinaryFormatter.
        /// </summary>
        /// <param name="item">The string that must be deserialized.</param>
        /// <returns>The object deserialized.</returns>
        public static object BinaryDeserialize(string item)
        {
            if (item != null && item.Length > 0)
            {
                byte[] buffer = Encoding.UTF8.GetBytes(item);
                if (buffer != null && buffer.Length > 0)
                {
                    using (MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(item)))
                    {
                        BinaryFormatter binaryFormatter = new BinaryFormatter();
                        return binaryFormatter.Deserialize(memoryStream);
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Serialize an object using the XmlSerializer.
        /// </summary>
        /// <param name="item">The object that must be serialized</param>
        /// <returns>A XML string.</returns>
        public static string XmlSerialize(object item, bool removeNamespace = false, bool omitXmlDeclaration = false)
        {
            string serialXML = String.Empty;
            try
            {
                StringBuilder sb = new StringBuilder();

                XmlWriterSettings settings = new XmlWriterSettings() { Indent = true, OmitXmlDeclaration = omitXmlDeclaration, Encoding = Encoding.UTF8 };

                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add(string.Empty, string.Empty);
                // Serialize the object
                XmlSerializer xmlSerializer = new XmlSerializer(item.GetType());
                using (XmlWriter writer = XmlWriter.Create(sb, settings))
                {
                    if (removeNamespace)
                    {
                        xmlSerializer.Serialize(writer, item, ns);
                    }
                    else
                    {
                        xmlSerializer.Serialize(writer, item);
                    }
                }
                serialXML = sb.ToString();
            }
            catch { throw; }
            return serialXML;
        }

        /// <summary>
        /// Deserialize an XML string into an object using the XmlSerializer.
        /// </summary>
        /// <param name="item">The string that must be deserialized.</param>
        /// <returns>The object deserialized.</returns>
        public static T XmlDeserialize<T>(string item)
        {
            using (StringReader stringReader = new StringReader(item))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(stringReader);
            }
            return default(T);
        }

        /// <summary>
        /// Serializes object with no namespace and no xml declaration tag. Returns pure xml of the object
        /// </summary>
        /// <param name="item">The object that must be serialized</param>
        /// <returns>A pure XML string</returns>
        public static string XmlSerializeToPureXML(Object item)
        {
            string serialXML = String.Empty;
            // set XML writer settings
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
            xmlWriterSettings.OmitXmlDeclaration = true;
            xmlWriterSettings.Indent = true;
            xmlWriterSettings.CloseOutput = true;
            xmlWriterSettings.Encoding = Encoding.UTF8;

            using (StringWriter stringWriter = new StringWriter())
            {
                // create xml using the settings
                using (XmlWriter xmlWriter = XmlWriter.Create(stringWriter, xmlWriterSettings))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(item.GetType());
                    //Empty namespace
                    XmlSerializerNamespaces xmlSerializerNamespaces = new XmlSerializerNamespaces();
                    xmlSerializerNamespaces.Add(String.Empty, String.Empty);

                    xmlSerializer.Serialize(xmlWriter, item, xmlSerializerNamespaces);
                    // get the xml and clean up
                    serialXML = stringWriter.ToString();
                    xmlWriter.Flush();
                }
                stringWriter.Flush();
            }
            //
            return serialXML;
        }

        /// <summary>
        /// Serialize with no xml declaration
        /// </summary>
        /// <param name="item">The type of the serialized object.</param>
        /// <returns>A XML with string with no xml declaration</returns>
        public static string XmlSerializeWithNoXmlDeclaration(Object item)
        {
            string serialXML = String.Empty;
            // set XML writer settings
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
            xmlWriterSettings.OmitXmlDeclaration = true;
            xmlWriterSettings.Indent = true;
            xmlWriterSettings.CloseOutput = true;
            xmlWriterSettings.Encoding = Encoding.UTF8;

            using (StringWriter stringWriter = new StringWriter())
            {
                // create xml using the settings
                using (XmlWriter xmlWriter = XmlWriter.Create(stringWriter, xmlWriterSettings))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(item.GetType());
                    xmlSerializer.Serialize(xmlWriter, item);
                    // get the xml and clean up
                    serialXML = stringWriter.ToString();
                    xmlWriter.Flush();
                }
                stringWriter.Flush();
            }
            //
            return serialXML;
        }

        /// <summary>
        /// Serialize an object using the DataContractSerializer.
        /// </summary>
        /// <param name="item">The object that must be serialized</param>
        /// <returns>A XML string.</returns>
        public static string DataContractSerialize(object item)
        {
            if (item != null)
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    DataContractSerializer serializer = new DataContractSerializer(item.GetType());
                    serializer.WriteObject(memoryStream, item);
                    return Encoding.UTF8.GetString(memoryStream.ToArray());
                }
            }
            return null;
        }

        /// <summary>
        /// Deserialize an XML string into an object using the DataContractSerializer.
        /// </summary>
        /// <param name="item">The string that must be deserialized.</param>
        /// <returns>The object deserialized.</returns>
        public static T DataContractDeserialize<T>(string item)
        {
            XmlDictionaryReader xmlDictionaryReader = null;
            try
            {
                xmlDictionaryReader = XmlDictionaryReader.CreateTextReader(Encoding.UTF8.GetBytes(item), XmlDictionaryReaderQuotas.Max);
                DataContractSerializer serializer = new DataContractSerializer(typeof(T));
                return (T)serializer.ReadObject(xmlDictionaryReader, false);
            }
            finally
            {
                if (xmlDictionaryReader != null)
                {
                    xmlDictionaryReader.Close();
                }
            }
            return default(T);
        }

        /// <summary>
        /// Serialize the object to JSON string
        /// </summary>
        /// <param name="obj">Target object</param>
        /// <returns>The serialized JSON string</returns>
        public static string JavaScriptSerialize(this object obj)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Serialize(obj);
        }

        /// <summary>
        /// Serialize the object to JSON string
        /// </summary>
        /// <param name="obj">Target object</param>
        /// <param name="recursionDepth">How deep the method recurses
        /// when serializing an object graph </param>
        /// <returns>The serialized JSON string</returns>
        public static string JavaScriptSerialize(this object obj, int recursionDepth)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            serializer.RecursionLimit = recursionDepth;
            return serializer.Serialize(obj);
        }

        /// <summary>
        /// Deserialize an object from a JSON string
        /// </summary>
        /// <typeparam name="T">Object type</typeparam>
        /// <param name="jsonString">Serialized JSON string</param>
        /// <returns>The deserialized object</returns>
        /// <remarks>If input string is not a valid JSON string,
        /// or type notmatched, return a default new object</remarks>
        public static T JavaScriptDeserialize<T>(this string jsonString)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            try
            {
                return serializer.Deserialize<T>(jsonString);
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }

        /// <summary>
        /// 序列化目标对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="obj">对象实例</param>
        /// <returns>Json字符串</returns>
        public static string JsonSerialize(object obj)
        {
            System.Runtime.Serialization.Json.DataContractJsonSerializer serializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(obj.GetType());
            MemoryStream ms = new MemoryStream();
            serializer.WriteObject(ms, obj);
            string retVal = System.Text.Encoding.UTF8.GetString(ms.ToArray());
            ms.Dispose();
            return retVal;
        }

        /// <summary>
        /// 反序列化json字符为对象实例
        /// </summary>
        /// <typeparam name="T">目标对象类型</typeparam>
        /// <param name="json">json字符串</param>
        /// <returns>对象实例</returns>
        public static T JsonDeserialize<T>(string json)
        {
            try
            {
                T obj = Activator.CreateInstance<T>();
                MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json));
                System.Runtime.Serialization.Json.DataContractJsonSerializer serializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(obj.GetType());
                obj = (T)serializer.ReadObject(ms);
                ms.Close();
                ms.Dispose();
                return obj;
            }
            catch
            {
                return default(T);
            }
        }
    }
}
