namespace SteamFreeGamesNotifier.Strings {
	internal class JsonOPStrings {
		#region path strings
		internal static readonly string recordsPath = $"{AppDomain.CurrentDomain.BaseDirectory}Records{Path.DirectorySeparatorChar}records.json";
		#endregion

		#region debug strings
		internal const string debugWrite = "Write records";
		internal const string debugLoadConfig = "Load config";
		internal const string debugLoadRecords = "Load previous records";
		internal const string debugNoRecords = "No records detected, quit writing records";
		#endregion
	}
}
