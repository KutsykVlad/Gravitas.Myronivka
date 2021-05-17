using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace MettlerPT6S3 {

	public delegate void ParseResponseDelegate(byte[] dataBytes, int bytesCount);

// State object for receiving data from remote device.  
	public class StateObject
	{
		// Client socket.  
		public Socket workSocket = null;
		// Size of receive buffer.  
		public const int BufferSize = 256;
		// Receive buffer.  
		public byte[] buffer = new byte[BufferSize];
		// Received data string.  
		public StringBuilder sb = new StringBuilder();
	}

	public class AsynchronousSocketListener {
	

		public  ParseResponseDelegate parseResponseDelegate;
		// Thread signal.  
		public ManualResetEvent allDone = new ManualResetEvent(false);

		public AsynchronousSocketListener() {
		}

		public void StartListening() {
			// Data buffer for incoming data.  
			byte[] bytes = new Byte[1024];

			// Establish the local endpoint for the socket.  
			// The DNS name of the computer  
			// running the listener is "host.contoso.com".  
			//IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
			//IPAddress ipAddress = ipHostInfo.AddressList[0];
			IPEndPoint localEndPoint = new IPEndPoint(new IPAddress(new byte[]{192, 168, 0, 10}), 9761);

			// Create a TCP/IP socket.  
			Socket listener = new Socket(localEndPoint.AddressFamily,
				SocketType.Stream, ProtocolType.Tcp);

			// Bind the socket to the local endpoint and listen for incoming connections.  
			try {
				listener.Bind(localEndPoint);
				listener.Listen(100);

				while (true) {
					// Set the event to nonsignaled state.  
					allDone.Reset();

					// Start an asynchronous socket to listen for connections.  
					Console.WriteLine("Waiting for a connection...");
					listener.BeginAccept(
						new AsyncCallback(AcceptCallback),
						listener);

					// Wait until a connection is made before continuing.  
					allDone.WaitOne();
				}

			}
			catch (Exception e) {
				Console.WriteLine(e.ToString());
			}

			Console.WriteLine("\nPress ENTER to continue...");
			Console.Read();

		}

		public void AcceptCallback(IAsyncResult ar) {
			// Signal the main thread to continue.  
			allDone.Set();

			// Get the socket that handles the client request.  
			Socket listener = (Socket) ar.AsyncState;
			Socket handler = listener.EndAccept(ar);

			// Create the state object.  
			StateObject state = new StateObject();
			state.workSocket = handler;
			handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
				new AsyncCallback(ReadCallback), state);
		}

		public void ReadCallback(IAsyncResult ar) {
			String content = String.Empty;

			// Retrieve the state object and the handler socket  
			// from the asynchronous state object.  
			StateObject state = (StateObject) ar.AsyncState;
			Socket handler = state.workSocket;

			int bytesRead;
			try {
				// Read data from the client socket.   
				bytesRead = handler.EndReceive(ar);
			}
			catch (System.Net.Sockets.SocketException socketException) {
				Console.WriteLine($"SocketException: {socketException.SocketErrorCode.ToString()}");
				return;
			}
		
			if (bytesRead > 0) {
				// There  might be more data, so store the data received so far.  
				state.sb.Append(Encoding.ASCII.GetString(
					state.buffer, 0, bytesRead));

				parseResponseDelegate(state.buffer, bytesRead);
				handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
					new AsyncCallback(ReadCallback), state);
			}
		}

		private void Send(Socket handler, String data) {
			// Convert the string data to byte data using ASCII encoding.  
			byte[] byteData = Encoding.ASCII.GetBytes(data);

			// Begin sending the data to the remote device.  
			handler.BeginSend(byteData, 0, byteData.Length, 0,
				new AsyncCallback(SendCallback), handler);
		}

		private void SendCallback(IAsyncResult ar) {
			try {
				// Retrieve the socket from the state object.  
				Socket handler = (Socket) ar.AsyncState;

				// Complete sending the data to the remote device.  
				int bytesSent = handler.EndSend(ar);
				Console.WriteLine("Sent {0} bytes to client.", bytesSent);

				handler.Shutdown(SocketShutdown.Both);
				handler.Close();

			}
			catch (Exception e) {
				Console.WriteLine(e.ToString());
			}
		}
	}
}