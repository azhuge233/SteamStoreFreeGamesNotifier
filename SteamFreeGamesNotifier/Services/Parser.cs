using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using SteamFreeGamesNotifier.Models;
using SteamFreeGamesNotifier.Models.Record;
using SteamFreeGamesNotifier.Strings;

namespace SteamFreeGamesNotifier.Services {
	internal class Parser(ILogger<Parser> logger): IDisposable {
		private readonly ILogger<Parser> _logger = logger;

		public ParseResult Parse(string source, List<FreeGameRecord> oldRecords) {
			try {
				_logger.LogDebug(ParseStrings.debugParse);

				var htmlDoc = new HtmlDocument();
				htmlDoc.LoadHtml(source);

				var parseResult = new ParseResult();

				var searchResultDiv = htmlDoc.DocumentNode.SelectSingleNode(ParseStrings.SearchResultDiv);

				if (searchResultDiv != null) {
					var gameEntries = searchResultDiv.SelectNodes(ParseStrings.GameEntryXPath).ToList();

					foreach (var entry in gameEntries) { 
						var appID = entry.GetAttributeValue(ParseStrings.DataAttribute_AppID, string.Empty);
						var gameStoreLink = entry.GetAttributeValue(ParseStrings.DataAttribute_Href, string.Empty);
						var gameName = entry.SelectSingleNode(ParseStrings.GameNameXPath)?.InnerText?.Trim() ?? string.Empty;
						var gameReview = entry.SelectSingleNode(ParseStrings.GameReviewXPath)?.GetAttributeValue(ParseStrings.DataAttribute_Review, "None");
						var gameOriginalPrice = entry.SelectSingleNode(ParseStrings.GameOriginalPriceXPath)?.InnerText?.Trim() ?? "None";

						if (string.IsNullOrEmpty(appID)) throw new Exception($"AppID for game {entry.InnerText} is missing");

						appID = $"app/{appID}";
						gameStoreLink = gameStoreLink.Split('?').FirstOrDefault();
						gameReview = gameReview.Split("<br>", 2).FirstOrDefault().Trim();

						var newFreeGameRecord = new FreeGameRecord {
							AppID = appID,
							Name = gameName,
							Link = gameStoreLink,
							Review = gameReview,
							OriginalPrice = gameOriginalPrice
						};

						_logger.LogDebug("---------------------------------");
						_logger.LogDebug(ParseStrings.debugFoundFreeGame, newFreeGameRecord.Name, newFreeGameRecord.AppID, newFreeGameRecord.Review, newFreeGameRecord.OriginalPrice);
						_logger.LogDebug("---------------------------------");

						parseResult.Records.Add(newFreeGameRecord);

						if (oldRecords.Count == 0 || oldRecords.Any(record => record.AppID == newFreeGameRecord.AppID)) {
							_logger.LogInformation(ParseStrings.infoFoundNewFreeGame, newFreeGameRecord.Name);
							parseResult.NotifyRecords.Add(newFreeGameRecord);
						} else _logger.LogDebug(ParseStrings.debugFoundInPreviousRecord, newFreeGameRecord.Name);
					}
				} else _logger.LogDebug(ParseStrings.debugFoundNoFreeGames);

				_logger.LogDebug($"Done: {ParseStrings.debugParse}");
				return parseResult;
			} catch (Exception) {
				_logger.LogError($"Error: {ParseStrings.debugParse}");
				throw;
			}
		}

		public void Dispose() {
			GC.SuppressFinalize(this);
		}
	}
}
