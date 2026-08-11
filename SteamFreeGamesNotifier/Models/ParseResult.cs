using SteamFreeGamesNotifier.Models.Record;

namespace SteamFreeGamesNotifier.Models {
	public class ParseResult {
		public List<FreeGameRecord> Records { get; set; } = [];

		public List<FreeGameRecord> NotifyRecords { get; set; } = [];
	}
}
