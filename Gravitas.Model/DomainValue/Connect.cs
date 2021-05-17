namespace Gravitas.Model {

	public static partial class Dom {

		public static class Connect {

			public static class Type {

				public const int Sms = 1;
				public const int Email = 2;
			}

			public static class State {

				public const int Draft = 1;
				public const int TxProcessing = 2;
				public const int TxProcessed = 3;
				public const int RxProcessing = 4;
				public const int RxProcessed = 5;
				public const int Done = 6;
				public const int Error = 99;
			}

			public static class Dirrection {

				public const int Incoming = 1;
				public const int Outgoing = 2;
			}
		}
	}
}
