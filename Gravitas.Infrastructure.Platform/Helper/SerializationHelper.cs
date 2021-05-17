using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Gravitas.Infrastrructure.Common
{
	public class SerializationHelper
	{
		/// <summary>
		/// Serializes the given object data to string representation
		/// </summary>
		/// <param name="dataToSerialize">the object data to serialize</param>
		/// <param name="objectType">the object class type</param>
		/// <returns>a string representation for demo purposes</returns>
		private static string Serialize<T>(object dataToSerialize) where T : class
		{
			string objAsString;
			// instantiate an XmlSerializer class
			XmlSerializer xs = new XmlSerializer(typeof(T));
			// for the purpose of explaining, 
			// we will be writing to a string
			// in real world, this might be writing to a file
			StringBuilder sb = new StringBuilder();
			// wrap the serialization 
			using (StringWriter sw = new StringWriter(sb))
			{
				// now serialize the data
				xs.Serialize(sw, dataToSerialize);
				objAsString = sb.ToString();
			}

			return objAsString;
		}

		private static T Deserialize<T>(string serializedData) where T : class 
		{
			T returnObj;
			// instantiate an XmlSerializer class
			XmlSerializer xs = new XmlSerializer(typeof(T));
			// wrap the serialization 
			using (StringReader sr = new StringReader(serializedData))
			{
				// now de-serialize the data
				returnObj = (T) xs.Deserialize(sr);
			}

			return returnObj;
		}

		public static void SerializeToFile<T>(object dataToSerialize, string filePath) where T : class
		{
			var xmlWriter =
				new XmlTextWriter(filePath, Encoding.Unicode) { Formatting = Formatting.Indented };

			xmlWriter.WriteRaw(Serialize<T>(dataToSerialize));
			xmlWriter.Close();
		}

		public static T DeserializeFromFile<T>(string filePath) where T : class
		{
			T returnObj = null;

			if (File.Exists(filePath)) {
				StreamReader sr = new StreamReader(filePath, Encoding.Unicode);
				returnObj = Deserialize<T>(sr.ReadToEnd());
			}
			return returnObj;
		}

		public static T DeserializeFromStream<T>(Stream stream) where T : class
		{
			if (stream == null)
				return null;
			
			StreamReader sr = new StreamReader(stream);
			T returnObj = Deserialize<T>(sr.ReadToEnd());
			
			return returnObj;
		}

		public static T DeserializeFromString<T>(string str) where T : class
		{
			if (string.IsNullOrWhiteSpace(str))
				return null;

			T returnObj = Deserialize<T>(str);

			return returnObj;
		}
	}
}
