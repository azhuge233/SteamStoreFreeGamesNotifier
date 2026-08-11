using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SteamFreeGamesNotifier.Models.Config;
using SteamFreeGamesNotifier.Models.PostContent;
using SteamFreeGamesNotifier.Models.Record;
using SteamFreeGamesNotifier.Strings;

namespace SteamFreeGamesNotifier.Services.Notifiers {
	internal class Meow(ILogger<Meow> logger, IOptions<Config> config): INotifiable {
		private readonly ILogger<Meow> _logger = logger;
		private readonly Config config = config.Value;

		public async Task SendMessage(List<FreeGameRecord> records) {
			try {
				_logger.LogDebug(NotifierStrings.debugSendMessageMeow);

				var url = string.Format(NotifyFormatStrings.meowUrlFormat, config.MeowAddress, config.MeowNickname);

				var content = new MeowPostContent() {
					Title = NotifyFormatStrings.meowUrlTitle
				};

				var client = new HttpClient();

				foreach (var record in records) {
					content.Message = record.ToMeowMessage();
					content.Url = record.Link;

					var data = new StringContent(JsonSerializer.Serialize(content), Encoding.UTF8, "application/json");
					var resp = await client.PostAsync(url, data);
					resp.EnsureSuccessStatusCode();

					_logger.LogDebug(await resp.Content.ReadAsStringAsync());
					await Task.Delay(3000); // rate limit
				}

				_logger.LogDebug($"Done: {NotifierStrings.debugSendMessageMeow}");
			} catch (Exception) {
				_logger.LogError($"Error: {NotifierStrings.debugSendMessageMeow}");
				throw;
			}
		}

		public async Task SendMessage(string asfResult) {
			try {
				_logger.LogDebug(NotifierStrings.debugSendMessageASFMeow);

				var url = string.Format(NotifyFormatStrings.meowUrlFormat, config.MeowAddress, config.MeowNickname);
				var content = new MeowPostContent() {
					Title = NotifyFormatStrings.meowUrlASFTitle,
					Message = asfResult,
					Url = string.Empty
				};

				var client = new HttpClient();

				var data = new StringContent(JsonSerializer.Serialize(content), Encoding.UTF8, "application/json");
				var resp = await client.PostAsync(url, data);
				resp.EnsureSuccessStatusCode();

				_logger.LogDebug(await resp.Content.ReadAsStringAsync());

				_logger.LogDebug($"Done: {NotifierStrings.debugSendMessageASFMeow}");
			} catch (Exception) {
				_logger.LogDebug($"Error: {NotifierStrings.debugSendMessageASFMeow}");
				throw;
			} finally {
				Dispose();
			}
		}

		public void Dispose() {
			GC.SuppressFinalize(this);
		}
	}
}
