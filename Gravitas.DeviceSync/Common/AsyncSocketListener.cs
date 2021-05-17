using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Gravitas.DeviceSync.Common {

	public delegate void ParseResponseDelegate(byte[] dataBytes, int bytesCount);

// State object for receiving data from remote device.  
	public class StateObject
	{
		// Client socket.  
		public Socket WorkSocket = null;
		// Size of receive buffer.  
		public const int BufferSize = 256;
		// Receive buffer.  
		public byte[] Buffer = new byte[BufferSize];
		// Received data string.  
		public StringBuilder Sb = new StringBuilder();
	}

	public class AsyncSocketListener {
	
		public ParseResponseDelegate parseResponseDelegate;
		// Thread signal.  
		public ManualResetEvent AllDone = new ManualResetEvent(false);

		private readonly IPEndPoint _localEndPoint;

		public AsyncSocketListener(IPEndPoint localEndPoint) {
			this._localEndPoint = localEndPoint;
		}

		public void StartListening() {
			// Data buffer for incoming data.  
			byte[] bytes = new byte[1024];

			// Create a TCP/IP socket.  
			Socket listener = new Socket(_localEndPoint.AddressFamily,
				SocketType.Stream, ProtocolType.Tcp);

			// Bind the socket to the local endpoint and listen for incoming connections.  
			try {
				listener.Bind(_localEndPoint);
				listener.Listen(100);

				while (true) {
					// Set the event to nonsignaled state.  
					AllDone.Reset();

					// Start an asynchronous socket to listen for connections.  
					Console.WriteLine(@"Waiting for a connection...");
					listener.BeginAccept(
						new AsyncCallback(AcceptCallback),
						listener);

					// Wait until a connection is made before continuing.  
					AllDone.WaitOne();
				}

			}
			catch (Exception e) {
				Console.WriteLine(e.ToString());
			}

			Console.WriteLine('\n'+@"Press ENTER to continue...");
			Console.Read();
		}

		public void AcceptCallback(IAsyncResult ar) {
			// Signal the main thread to continue.  
			AllDone.Set();

			// Get the socket that handles the client request.  
			Socket listener = (Socket) ar.AsyncState;
			Socket handler = listener.EndAccept(ar);

			// Create the state object.  
			StateObject state = new StateObject();
			state.WorkSocket = handler;

			handler.BeginReceive(state.Buffer, 0, StateObject.BufferSize, 0,
				new AsyncCallback(ReadCallback), state);
		}

		public void ReadCallback(IAsyncResult ar) {
			// Retrieve the state object and the handler socket  
			// from the asynchronous state object.  
			StateObject state = (StateObject) ar.AsyncState;
			Socket handler = state.WorkSocket;

			int bytesRead;
			try {
				// Read data from the client socket.   
				bytesRead = handler.EndReceive(ar);
			}
			catch (SocketException socketException) {
				Console.WriteLine($@"SocketException: {socketException.SocketErrorCode.ToString()}");
				return;
			}
		
			if (bytesRead > 0) {
				// There  might be more data, so store the data received so far.  
				state.Sb.Append(Encoding.ASCII.GetString(
					state.Buffer, 0, bytesRead));

				parseResponseDelegate(state.Buffer, bytesRead);
				handler.BeginReceive(state.Buffer, 0, StateObject.BufferSize, 0,
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
				Console.WriteLine(@"Sent {0} bytes to client.", bytesSent);

				handler.Shutdown(SocketShutdown.Both);
				handler.Close();
			}
			catch (Exception e) {
				Console.WriteLine(e.ToString());
			}
		}
	}
}