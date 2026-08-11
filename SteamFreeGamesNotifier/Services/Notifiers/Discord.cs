using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SteamFreeGamesNotifier.Models.Config;
using SteamFreeGamesNotifier.Models.PostContent;
using SteamFreeGamesNotifier.Models.Record;
using SteamFreeGamesNotifier.Strings;

namespace SteamFreeGamesNotifier.Services.Notifiers {
	internal class Discord(ILogger<Discord> logger, IOptions<Config> config): INotifiable {
		private readonly ILogger<Discord> _logger = logger;
		private readonly Config config = config.Value;

		private readonly int DiscordMaxEmbedCount = 10;

		public async Task SendMessage(List<FreeGameRecord> records) {
			try {
				_logger.LogDebug(NotifierStrings.debugSendMessageDiscord);

				var url = config.DiscordWebhookURL;

				var client = new HttpClient();

				for (int i = 0; i <= records.Count / DiscordMaxEmbedCount; i++) {
					var content = new DiscordPostContent() {
						Content = records.Count > 1 ? "New Free Games - Steam Store" : "New Free Game - Steam Store"
					};

					for (int j = i * DiscordMaxEmbedCount; (j - i * DiscordMaxEmbedCount) < 10 && j < records.Count; j++) {
						content.Embeds.Add(
							new Embed() {
								Title = records[j].Name,
								Url = records[j].Link,
								Description = records[j].ToDiscordMessage(),
								Footer = new Footer() { Text = NotifyFormatStrings.projectLink }
							}
						);
					}

					if (content.Embeds.Count > 0) {
						var data = new StringContent(JsonSerializer.Serialize(content), Encoding.UTF8, "application/json");
						var resp = await client.PostAsync(url, data);
						resp.EnsureSuccessStatusCode();
						_logger.LogDebug(await resp.Content.ReadAsStringAsync());
					}
				}

				_logger.LogDebug($"Done: {NotifierStrings.debugSendMessageDiscord}");
			} catch (Exception) {
				_logger.LogError($"Error: {NotifierStrings.debugSendMessageDiscord}");
				throw;
			}
		}

		public async Task SendMessage(string asfResult) {
			try {
				_logger.LogDebug(NotifierStrings.debugSendMessageASFDiscord);

				var url = config.DiscordWebhookURL;

				var client = new HttpClient();

				var content = new DiscordPostContent() {
					Content = "ASF Result",
				};

				content.Embeds.Add(
					new Embed() {
						Description = asfResult
					}
				);

				var data = new StringContent(JsonSerializer.Serialize(content), Encoding.UTF8, "application/json");
				var resp = await client.PostAsync(url, data);
				resp.EnsureSuccessStatusCode();

				_logger.LogDebug($"Done: {NotifierStrings.debugSendMessageASFDiscord}");
			} catch (Exception) {
				_logger.LogError($"Error: {NotifierStrings.debugSendMessageASFDiscord}");
				throw;
			}
		}

		public void Dispose() {
			GC.SuppressFinalize(this);
		}
	}
}
