using System;
using System.IO;
using System.Net;
using System.Text;

namespace CameraVlcDemo
{

	class Program
	{

		public static bool SaveHttpImage(string host, string user, string pass, string path) {

			try {

				HttpWebRequest request = (HttpWebRequest)WebRequest.Create(host);
				request.Credentials = new NetworkCredential(user, pass);
				request.Timeout = 10000;

				using (WebResponse response = (HttpWebResponse)request.GetResponse()) {

					Stream responseStream = response.GetResponseStream();
					if (responseStream == null) {
						return false;
					}
					using (Stream fileStream = File.Create(path)) {
						responseStream.CopyTo(fileStream);
					}
				}
			}
			catch {
				return false;
			}

			return true;
		}

		static void Main(string[] args) {
			SaveHttpImage("http://10.64.75.10/ISAPI/Streaming/channels/101/picture?snapShotImageType=JPEG",
				"admin",
				"Qq19832004",
				@"C:\camImage.jpeg");
		}
	}

	//class Program {
	//	static void Main(string[] args) {
	//		Stream thumb1 = new System.IO.MemoryStream();

	//		var ffMpeg = new NReco.VideoConverter.FFMpegConverter();
	//		ffMpeg.GetVideoThumbnail("rtsp://admin:Qq19832004@192.168.1.64:554",
	//			Path.Combine(Environment.CurrentDirectory, "output2.jpeg"));
	//	}
	//}

	//class Program
	//{
	//	static void Main(string[] args)
	//	{
	//		DirectoryInfo libDirectory = new DirectoryInfo(Path.Combine(Environment.CurrentDirectory, "libvlc", IntPtr.Size == 4 ? "win-x86" : "win-x64"));

	//		var options = new string[]
	//		{
	//			":dummy-quiet "
	//			// VLC options can be given here. Please refer to the VLC command line documentation.
	//		};

	//		var mediaPlayer = new Vlc.DotNet.Core.VlcMediaPlayer(libDirectory, options);

	//		var mediaOptions = new string[]
	//		{
	//			//":sout=#file{dst="+Path.Combine(Environment.CurrentDirectory, "output3.mov")+"}",
	//			":sout-keep"//,
	//			//":snapshot-path=#file{dst="+Path.Combine(Environment.CurrentDirectory, "output2.jpeg")+"}"
	//		};

	//		mediaPlayer.SetMedia("rtsp://admin:Qq19832004@192.168.1.64:554", mediaOptions);

	//		var fi = new FileInfo(Path.Combine(Environment.CurrentDirectory, "output.jpeg"));
	//		mediaPlayer.Play();
	//		Thread.Sleep(2000);
	//		mediaPlayer.TakeSnapshot(fi);
	//		mediaPlayer.Stop();

	//		bool playFinished = false;
	//		mediaPlayer.PositionChanged += (sender, e) =>
	//		{
	//			Console.Write("\r" + Math.Floor(e.NewPosition * 100) + "%");
	//		};

	//		mediaPlayer.EncounteredError += (sender, e) =>
	//		{
	//			Console.Error.Write("An error occurred");
	//			playFinished = true;
	//		};

	//		mediaPlayer.EndReached += (sender, e) => {
	//			playFinished = true;
	//		};

	//		mediaPlayer.Play();

	//		// Ugly, sorry, that's just an example...
	//		while (!playFinished)
	//		{
	//			Thread.Sleep(TimeSpan.FromMilliseconds(500));
	//		}
	//	}
	//}
}
