using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Gravitas.DeviceSync.Common {

	public static class SocketHelper {

		public static void Send(Socket handler, String data) {
			byte[] byteData = Encoding.ASCII.GetBytes(data);
			handler.Send(byteData, 0, byteData.Length, 0);
		}

		public static string Receive(Socket handler) {
			byte[] bytes = new byte[1024];

			for (int i = 0; i < bytes.Length; i++) {
				bytes[i] = 0;
			}

			int bytesRec = handler.Receive(bytes);
			return Encoding.ASCII.GetString(bytes.Take(bytesRec).ToArray());
		}

		public static int Receive(Socket handler, out byte[] bytes) {
			//byte[] 
			bytes = new byte[1024];

			for (int i = 0; i < bytes.Length; i++) {
				bytes[i] = 0;
			}

			int bytesRec = handler.Receive(bytes);
			return bytesRec;
		}
	}
}