using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ObidRfidAutoAnsverDemo
{
	class Program
	{
		//		public static void StartClient()
		//		{
		//			// Data buffer for incoming data.  
		//			byte[] bytes = new byte[1024];

		//			// Connect to a remote device.  
		//			try
		//			{
		//				// Establish the remote endpoint for the socket.  
		//				// This example uses port 11000 on the local computer.  

		//				IPAddress ipAddress = new IPAddress(new byte[] { 192, 168, 0, 190 });
		//				IPEndPoint remoteEp = new IPEndPoint(ipAddress, 9761);

		//				// Create a TCP/IP  socket.  
		//				Socket socket = new Socket(ipAddress.AddressFamily,
		//					SocketType.Stream, ProtocolType.Tcp)
		//				{ NoDelay = true };

		//				// Connect the socket to the remote endpoint. Catch any errors.  
		//				try
		//				{
		//					socket.Connect(remoteEp);

		//					Console.WriteLine("Socket connected to {0}", socket.RemoteEndPoint.ToString());

		//					while (true) {
		//						socket.Send(new byte[] {0});

		//						// Receive the response from the remote device.  
		//						for (int i = 0; i < bytes.Length; i++) {
		//							bytes[i] = 0;
		//						}
		//						int bytesRec = socket.Receive(bytes);

		//						if (bytesRec != 11) continue;

		//						byte chSum = 0;
		//						foreach (byte cardByte in bytes.Take(bytesRec - 1)) {
		//							chSum = (byte) (chSum ^ cardByte);
		//						}

		//						Console.WriteLine("Echoed test = {0}"
		//							, string.Concat(bytes.Take(bytesRec).Select(b => b.ToString("X2") + " ")));
		//						//0b 00 11 00 00 07 00 f0 da 50 67
		//						//0B 00 11 00 00 07 00 F0 AA 54 13
		//						// -  -  -  v  v  v  v  v  v  v  -
		//						//0B 00 11 00 00 07 00 F0 AA 54 13

		//						Console.WriteLine("Card = {0}, ChSum = {1:X2}"
		//							, string.Concat(bytes.Skip(5).Take(bytesRec - 5 - 1).Select(b => b.ToString("X2") + " ")), chSum);

		//						//0700F0AA54  15 
		//						//0700F0DA50  17 
		//						Thread.Sleep(3 * 1000);
		//					}

		//					// Release the socket.  
		//					socket.Shutdown(SocketShutdown.Both);
		//					socket.Close();

		//				}
		//				catch (ArgumentNullException ane)
		//				{
		//					Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
		//				}
		//				catch (SocketException se)
		//				{
		//					Console.WriteLine("SocketException : {0}", se.ToString());
		//				}
		//				catch (Exception e)
		//				{
		//					Console.WriteLine("Unexpected exception : {0}", e.ToString());
		//				}

		//			}
		//			catch (Exception e)
		//			{
		//				Console.WriteLine(e.ToString());
		//			}
		//		}

		public static void ParseResponse(byte[] dataBytes, int bytesCount) {
			byte chSum = 0;
			foreach (byte cardByte in dataBytes.Take(bytesCount - 1))
			{
				chSum = (byte)(chSum ^ cardByte);
			}

			Console.WriteLine(@"Echoed test = {0}"
				, string.Concat(dataBytes.Take(bytesCount).Select(b => b.ToString("X2") + " ")));

			Console.WriteLine(@"Card = {0}, ChSum = {1:X2}"
				, string.Concat(dataBytes.Skip(5).Take(bytesCount - 5 - 1).Select(b => b.ToString("X2") + " ")), chSum);

		}
		
		static void Main(string[] args) {
			AsynchronousSocketListener listener = new AsynchronousSocketListener();
			listener.parseResponseDelegate += new ParseResponseDelegate(ParseResponse);
			listener.StartListening();
		}
	}
}
