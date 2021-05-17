using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MettlerPT6S3
{
	class Program
	{
		public static void StartClient()
		{
			// Data buffer for incoming data.  
			byte[] bytes = new byte[1024];

			// Connect to a remote device.  
			try
			{
				// Establish the remote endpoint for the socket.  
				// This example uses port 11000 on the local computer.  

				IPAddress ipAddress = new IPAddress(new byte[] { 192, 168, 0, 170 });
				IPEndPoint remoteEp = new IPEndPoint(ipAddress, 1749);

				// Create a TCP/IP  socket.  
				Socket socket = new Socket(ipAddress.AddressFamily,
					SocketType.Stream, ProtocolType.Tcp)
				{ NoDelay = true };

				// Connect the socket to the remote endpoint. Catch any errors.  
				try
				{
					socket.Connect(remoteEp);
					Console.WriteLine("Socket connected to {0}", socket.RemoteEndPoint.ToString());

					while (true)
					{
						socket.Send(Encoding.ASCII.GetBytes("P"));

						// Receive the response from the remote device.  
						for (int i = 0; i < bytes.Length; i++) {
							bytes[i] = 0;
						}
						int bytesRec = socket.Receive(bytes);

						if (bytesRec != 8) continue;

						byte chSum = 0;
						foreach (byte cardByte in bytes.Take(bytesRec - 1)) {
							chSum = (byte)(chSum ^ cardByte);
						}

						string ans = Encoding.ASCII.GetString(bytes.Skip(1).Take(bytesRec-1).ToArray());

						Console.WriteLine($"Echoed test = {ans}");

						Thread.Sleep(1 * 1000);
					}

					// Release the socket.  
//					socket.Shutdown(SocketShutdown.Both);
//					socket.Close();

				}
				catch (ArgumentNullException ane)
				{
					Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
				}
				catch (SocketException se)
				{
					Console.WriteLine("SocketException : {0}", se.ToString());
				}
				catch (Exception e)
				{
					Console.WriteLine("Unexpected exception : {0}", e.ToString());
				}

			}
			catch (Exception e)
			{
				Console.WriteLine(e.ToString());
			}
		}

		static void Main(string[] args) {
			StartClient();
		}
	}
}
