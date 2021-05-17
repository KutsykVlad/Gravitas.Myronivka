namespace Gravitas.Model {

	public static partial class Dom {

		public static class Ticket {

			public static class Status {

				public const int New = 1;
				public const int Blank = 2;
				public const int ToBeProcessed = 3;
				public const int Processing = 4;
				public const int Completed = 5;
				public const int Closed = 6;
				public const int Canceled = 10;
			}
		}
	}
}
