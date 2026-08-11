namespace SteamFreeGamesNotifier.Strings {
	internal class ScrapeStrings {
		internal const string Url = "https://store.steampowered.com/search/?hwtype=0&maxprice=free&category1=998%2C994%2C21&specials=1&ndl=1";

		internal static readonly string[] UAs = [
			"Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/151.0.0.0 Safari/537.36",
			"Mozilla/5.0 (Macintosh; Intel Mac OS X 10_14_6) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/151.0.0.0 Safari/537.36",
			"Mozilla/5.0 (Macintosh; Intel Mac OS X 10.14; rv:70.0) Gecko/20100101 Firefox/70.0",
			"Mozilla/5.0 (Windows NT 10.0; WOW64; rv:70.0) Gecko/20100101 Firefox/70.0",
			"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/151.0.0.0 Safari/537.36 Edg/151.0.0.0",
			"Mozilla/5.0 (Macintosh; Intel Mac OS X 10_14_6) AppleWebKit/605.1.15 (KHTML, like Gecko) Chrome/151.0.0.0 Safari/604.1 Edg/151.0.0.0"
		];

		#region debug strings
		internal const string debugGetSource = "Get source";
		internal const string debugGetSourceWithUrl = "Getting source: {0}";
		#endregion
	}
}
