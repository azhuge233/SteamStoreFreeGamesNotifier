namespace SteamFreeGamesNotifier.Strings {
	internal class ASFStrings {
		internal const string commandUrl = "{0}/Api/Command";

		internal const string addlicenseCommand = "addlicense asf ";

		internal const string addlicenseResponseResultKey = "Result";

		internal const string UAKey = "User-Agent";
		internal const string UAValue = "SFGN (+https://github.com/azhuge233/SteamFreeGamesNotifier)";
		internal const string AuthenticationKey = "Authentication";

		#region debug strings
		internal const string debugASFOP = "ASFOP";
		internal const string debugGenerateSubIDString = "GenerateSubIDString";
		internal const string debugFinalCommand = "Final Command: {0}";
		internal const string debugResponse = "Response: {0}";

		internal const string infoAddlicenseResult = "Addlicense result:\n{0}\n";
		internal const string infoNoRecords = "No new record, skipping addlicense";
		internal const string infoASFDisabled = "ASF disabled, skipping";
		#endregion
	}
}
