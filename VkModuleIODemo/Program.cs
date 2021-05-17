using System;
using System.Net;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using Gravitas.Infrastructure.Common;

namespace VkModuleIODemo
{
	//<response>
	//	<led0>0</led0>
	//	<led1>1</led1>
	//	<btn0>up</btn0>
	//	<btn1>dn</btn1>
	//</response>

	[XmlRoot(ElementName = "response")]
	public class Socket2IoState {

		public static class ErrorCode {
			public static int Ok = 0;
			public static int Error = 1;
		}

		[XmlIgnore]
		public int ErrCode { get; set; } = ErrorCode.Error;

		[XmlIgnore]
		public bool Out0 { get; set; }
		[XmlIgnore]
		public bool Out1 { get; set; }

		[XmlIgnore]
		public bool In0 { get; set; }
		[XmlIgnore]
		public bool In1 { get; set; }

		#region Xml Searilisation Properties
		[XmlElement(ElementName = "led0")]
		public string Out0Name
		{
			get => Out0 ? "1" : "0";
			set => Out0 = value.Trim().ToLowerInvariant() == "1";
		}
		[XmlElement(ElementName = "led1")]
		public string Out1Name
		{
			get => Out1 ? "1" : "0";
			set => Out1 = value.Trim().ToLowerInvariant() == "1";
		}
		[XmlElement(ElementName = "btn0")]
		public string In0Name
		{
			get => In0 ? "up" : "dn";
			set => In0 = value.Trim().ToLowerInvariant() == "up";
		}
		[XmlElement(ElementName = "btn1")]
		public string In1Name
		{
			get => In1 ? "up" : "dn";
			set => In1 = value.Trim().ToLowerInvariant() == "up";
		}
		#endregion
	}

	class Program
	{
		public static string GetWebResponse(HttpWebRequest request) {
			string responseStr = null;

			try {
				using (WebResponse response = (HttpWebResponse)request.GetResponse()) {
					Stream objStream = response.GetResponseStream();

					using (var stream = new MemoryStream()) {
						byte[] buffer = new byte[2048]; // read in chunks of 2KB
						int bytesRead;
						while ((bytesRead = objStream.Read(buffer, 0, buffer.Length)) > 0) {
							stream.Write(buffer, 0, bytesRead);
						}
						responseStr = Encoding.ASCII.GetString(stream.ToArray());
					}
				}
			}
			catch { /*ignore*/ }

			return responseStr;
		}

		public static Socket2IoState GetSocket2IoState(IPAddress host, string user, string pass) {
			Socket2IoState state = new Socket2IoState();

			HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"http://{host.ToString()}/protect/status.xml");
			request.Credentials = new NetworkCredential(user, pass);
			request.Timeout = 5000;
			
			try {
				string responseStr = GetWebResponse(request);
//				state = SerializationHelper.DeserializeFromString<Socket2IoState>(responseStr);
			} catch  {
				state = new Socket2IoState();
			}

			return state;
		}

		public static Socket2IoState ChangeSocket2IoState(IPAddress host, string user, string pass, int outNo, int timeout = 0) {
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"http://{host}/protect/leds.cgi?led={outNo}&timeout={timeout}");
			request.Credentials = new NetworkCredential(user, pass);
			request.Timeout = 5000;

			GetWebResponse(request);
			
			Socket2IoState state = GetSocket2IoState(host, "admin", "vkmodule");
			return state;
		}

		public static Socket2IoState SetSocket2IoState(IPAddress host, string user, string pass, Socket2IoState newState) {
			
			Socket2IoState state = GetSocket2IoState(host, user, pass);

			if (newState == null) {
				return state;
			}

			if ((state != null) && (state.Out0 != newState.Out0)) {
				state = ChangeSocket2IoState(host, user, pass, 0);
			}
			if ((state != null) && (state.Out1 != newState.Out1)) {
				state = ChangeSocket2IoState(host, user, pass, 1);
			}

			return state;
		}

		static void Main(string[] args) {
			Socket2IoState state = GetSocket2IoState(new IPAddress(new byte[]{192, 168, 0, 191}), "admin", "vkmodule");
			Console.ReadKey();
			SetSocket2IoState(new IPAddress(new byte[] { 192, 168, 0, 191 }), "admin", "vkmodule", new Socket2IoState(){Out0 = true, Out1 = false});
			Console.ReadKey();
			SetSocket2IoState(new IPAddress(new byte[] { 192, 168, 0, 191 }), "admin", "vkmodule", new Socket2IoState() { Out0 = true, Out1 = true});
			Console.ReadKey();
			SetSocket2IoState(new IPAddress(new byte[] { 192, 168, 0, 191 }), "admin", "vkmodule", new Socket2IoState() { Out0 = false, Out1 = true });
			Console.ReadKey();
			SetSocket2IoState(new IPAddress(new byte[] { 192, 168, 0, 191 }), "admin", "vkmodule", new Socket2IoState() { Out0 = false, Out1 = false });
		}
	}
}
