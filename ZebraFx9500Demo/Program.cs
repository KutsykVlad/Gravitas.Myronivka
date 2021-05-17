using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Symbol.RFID3;

namespace ZebraFx9500Demo
{

	public class ZebraDemo {
		private RFIDReader _rfid3;

		public void Zebra()
		{
			string hostname = "10.64.75.14";

			Console.WriteLine($"Connecting to {hostname}..");

			_rfid3 = new RFIDReader(hostname, 0, 0);
			_rfid3.Connect();

			Console.WriteLine($"Connected!");

			// registering for read tag data notification
			_rfid3.Events.ReadNotify += new Events.ReadNotifyHandler(Events_ReadNotify);
			_rfid3.Events.StatusNotify += new Events.StatusNotifyHandler(Events_StatusNotify);

			_rfid3.Actions.Inventory.Perform();
		}

		// Status Notify handler
		public void Events_StatusNotify(object sender, Events.StatusEventArgs e) {
			Console.WriteLine(e.StatusEventData.StatusEventType);
		}

		// Read Notify handler
		public void Events_ReadNotify(object sender, Events.ReadEventArgs e) {
			Console.WriteLine($"{DateTime.Now:G}");

			TagData[] tags = _rfid3.Actions.GetReadTags(1000);

			Dictionary<string,TagData> hs = new Dictionary<string, TagData>();
			foreach (TagData tagData in tags) {
				if (!hs.ContainsKey(tagData.TagID))
					hs.Add(tagData.TagID, tagData);
			}

			int i = 1;
			foreach (var tagData in hs) {
				Console.WriteLine($"\t\tNo.{i++:D3} Antena: {tagData.Value.AntennaID} Id: {tagData.Value.TagID}");
			}

			_rfid3.Actions.PurgeTags();
			Console.WriteLine($"- - -");

			//Console.WriteLine($"{DateTime.Now:G} - Antena: {e.ReadEventData.TagData.AntennaID} Id: {e.ReadEventData.TagData.TagID}");
			//if (e.ReadEventData.TagData.MemoryBankData != String.Empty)
			//	Console.WriteLine(e.ReadEventData.TagData.MemoryBank.ToString() + " : " + e.ReadEventData.TagData.MemoryBankData);

			_rfid3.Actions.Inventory.Stop();
			Thread.Sleep(1000);
			_rfid3.Actions.Inventory.Perform();
		}
	}

	class Program {
		

		static void Main(string[] args) {
			new ZebraDemo().Zebra();

			Console.ReadKey();
		}
	}
}
