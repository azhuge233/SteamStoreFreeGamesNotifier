using System.Text.Json.Serialization;

namespace SteamFreeGamesNotifier.Models.PostContent {
	public class QQHttpPostContent {
		[JsonPropertyName("user_id")]
		public string UserID { get; set; }

		[JsonPropertyName("message")]
		public string Message { get; set; }
	}
}
