namespace SteamFreeGamesNotifier.Strings {
	internal class ParseStrings {
		#region XPath
		internal const string SearchResultDiv = """.//div[@id="search_result_container"]//div[@id="search_resultsRows"]""";
		internal const string GameEntryXPath = """.//a""";
		internal const string GameNameXPath = """.//span[@class="title"]""";
		internal const string GameReviewXPath = """.//span[contains(@class, "search_review_summary")]""";
		internal const string GameOriginalPriceXPath = """.//div[@class="discount_original_price"]""";
		#endregion

		#region Data Attributes
		internal const string DataAttribute_Href = "href";
		internal const string DataAttribute_AppID = "data-ds-appid";
		internal const string DataAttribute_Review = "data-tooltip-html";
		#endregion

		#region debug strings
		internal const string debugParse = "Parse";

		internal const string debugFoundFreeGame = "Found free game: {0} | {1} | {2} | {3}";
		internal const string debugFoundInPreviousRecord = "Found in previous records: {0}";
		internal const string infoFoundNewFreeGame = "Found new free game: {0}";
		internal const string debugFoundNoFreeGames = "No free games found";
		#endregion
	}
}
