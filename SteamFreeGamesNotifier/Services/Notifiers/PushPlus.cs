using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SteamFreeGamesNotifier.Models.Config;
using SteamFreeGamesNotifier.Models.PostContent;
using SteamFreeGamesNotifier.Models.Record;
using SteamFreeGamesNotifier.Strings;
using System.Text;
using System.Text.Json;
using System.Web;

namespace SteamFreeGamesNotifier.Services.Notifiers {
	internal class PushPlus(ILogger<PushPlus> logger, IOptions<Config> config): INotifiable {
		private readonly ILogger<PushPlus> _logger = logger;
		private readonly Config config = config.Value;

		public async Task SendMessage(List<FreeGameRecord> records) {
			try {
				_logger.LogDebug(NotifierStrings.debugSendMessagePushPlus);

				var client = new HttpClient();

				var title = string.Format(NotifyFormatStrings.pushPlusTitleFormat, records.Count);

				var postContent = new PushPlusPostContent() {
					Token = config.PushPlusToken,
					Title = title,
					Content = CreateMessage(records)
				};

				var resp = await client.PostAsync(NotifyFormatStrings.pushPlusPostUrl, new StringContent(JsonSerializer.Serialize(postContent), Encoding.UTF8, "application/json"));
				resp.EnsureSuccessStatusCode();
				_logger.LogDebug(await resp.Content.ReadAsStringAsync());

				_logger.LogDebug($"Done: {NotifierStrings.debugSendMessagePushPlus}");
			} catch (Exception) {
				_logger.LogError($"Error: {NotifierStrings.debugSendMessagePushPlus}");
				throw;
			}
		}

		public async Task SendMessage(string asfResult) {
			try {
				_logger.LogDebug(NotifierStrings.debugSendMessageASFPushPlus);

				var client = new HttpClient();

				var title = NotifyFormatStrings.pushPlusASFTitleFormat;

				var postContent = new PushPlusPostContent() {
					Token = config.PushPlusToken,
					Title = title,
					Content = CreateMessage(asfResult)
				};

				var resp = await client.PostAsync(NotifyFormatStrings.pushPlusPostUrl, new StringContent(JsonSerializer.Serialize(postContent), Encoding.UTF8, "application/json"));
				resp.EnsureSuccessStatusCode();
				_logger.LogDebug(await resp.Content.ReadAsStringAsync());

				_logger.LogDebug($"Done: {NotifierStrings.debugSendMessageASFPushPlus}");
			} catch (Exception) {
				_logger.LogError($"Error: {NotifierStrings.debugSendMessageASFPushPlus}");
				throw;
			}
		}

		private string CreateMessage(List<FreeGameRecord> records) {
			try {
				_logger.LogDebug(NotifierStrings.debugCreateMessage);

				var sb = new StringBuilder();

				records.ForEach(record => sb.AppendFormat(NotifyFormatStrings.pushPlusBodyFormat, record.ToPushPlusMessage()));

				sb.Append(NotifyFormatStrings.projectLinkHTML);

				_logger.LogDebug($"Done: {NotifierStrings.debugCreateMessage}");
				return sb.ToString();
			} catch (Exception) {
				_logger.LogError($"Error: {NotifierStrings.debugCreateMessage}");
				throw;
			}
		}

		private string CreateMessage(string asfResult) {
			try {
				_logger.LogDebug(NotifierStrings.debugCreateMessage);

				var sb = new StringBuilder();

				sb.Append(asfResult.Replace("\n", "<br>"));

				sb.Append(NotifyFormatStrings.projectLinkHTML);

				_logger.LogDebug($"Done: {NotifierStrings.debugCreateMessage}");
				return sb.ToString();
			} catch (Exception) {
				_logger.LogError($"Error: {NotifierStrings.debugCreateMessage}");
				throw;
			}
		}

		public void Dispose() {
			GC.SuppressFinalize(this);
		}
	}
}
