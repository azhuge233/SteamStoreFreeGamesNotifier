using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SteamFreeGamesNotifier.Models.Config;
using SteamFreeGamesNotifier.Models.PostContent;
using SteamFreeGamesNotifier.Models.Record;
using SteamFreeGamesNotifier.Strings;

namespace SteamFreeGamesNotifier.Services.Notifiers {
	internal class DingTalk(ILogger<DingTalk> logger, IOptions<Config> config) : INotifiable {
		private readonly ILogger<DingTalk> _logger = logger;
		private readonly Config config = config.Value;

		public async Task SendMessage(List<FreeGameRecord> records) {
			try {
				_logger.LogDebug(NotifierStrings.debugSendMessageDingTalk);

				var url = string.Format(NotifyFormatStrings.dingTalkUrlFormat, config.DingTalkBotToken).ToString();
				var content = new DingTalkPostContent();
				var client = new HttpClient();

				foreach (var record in records) {
					content.Text.Content_ = $"{record.ToDingTalkMessage()}{NotifyFormatStrings.projectLink}";
					var data = new StringContent(JsonSerializer.Serialize(content), Encoding.UTF8, "application/json");
					var resp = await client.PostAsync(url, data);
					resp.EnsureSuccessStatusCode();
					_logger.LogDebug(await resp.Content.ReadAsStringAsync());
				}

				_logger.LogDebug($"Done: {NotifierStrings.debugSendMessageDingTalk}");
			} catch (Exception) {
				_logger.LogError($"Error: {NotifierStrings.debugSendMessageDingTalk}");
				throw;
			} finally {
				Dispose();
			}
		}

		public async Task SendMessage(string asfResult) {
			try {
				_logger.LogDebug(NotifierStrings.debugSendMessageASFDingTalk);

				var url = string.Format(NotifyFormatStrings.dingTalkUrlFormat, config.DingTalkBotToken);
				var content = new DingTalkPostContent();
				var client = new HttpClient();

				content.Text.Content_ = asfResult;

				var data = new StringContent(JsonSerializer.Serialize(content), Encoding.UTF8, "application/json");
				var resp = await client.PostAsync(url, data);
				resp.EnsureSuccessStatusCode();
				_logger.LogDebug(await resp.Content.ReadAsStringAsync());

				_logger.LogDebug($"Done: {NotifierStrings.debugSendMessageASFDingTalk}");
			} catch (Exception) {
				_logger.LogError($"Error: {NotifierStrings.debugSendMessageASFDingTalk}");
				throw;
			}
		}

		public void Dispose() {
			GC.SuppressFinalize(this);
		}
	}
}
