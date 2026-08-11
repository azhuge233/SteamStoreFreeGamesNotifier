using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Org.BouncyCastle.Asn1.Pkcs;
using SteamFreeGamesNotifier.Models.Config;
using SteamFreeGamesNotifier.Models.PostContent;
using SteamFreeGamesNotifier.Models.Record;
using SteamFreeGamesNotifier.Strings;
using System.Text;
using System.Text.Json;

namespace SteamFreeGamesNotifier.Services.Notifiers {
	internal class QQHttp(ILogger<QQHttp> logger, IOptions<Config> config): INotifiable {
		private readonly ILogger<QQHttp> _logger = logger;
		private readonly Config config = config.Value;

		public async Task SendMessage(List<FreeGameRecord> records) {
			try {
				_logger.LogDebug(NotifierStrings.debugSendMessageQQHttp);

				string url = string.Format(NotifyFormatStrings.qqHttpUrlFormat, config.QQHttpAddress, config.QQHttpPort, config.QQHttpToken);

				var client = new HttpClient();

				var content = new QQHttpPostContent {
					UserID = config.ToQQID
				};

				foreach (var record in records) {
					_logger.LogDebug($"{NotifierStrings.debugSendMessageQQHttp} : {record.Name}");

					content.Message = $"{record.ToQQMessage()}{NotifyFormatStrings.projectLink}";

					var data = new StringContent(JsonSerializer.Serialize(content), Encoding.UTF8, "application/json");
					var resp = await client.PostAsync(url, data);
					resp.EnsureSuccessStatusCode();

					_logger.LogDebug(await resp.Content.ReadAsStringAsync());
				}

				_logger.LogDebug($"Done: {NotifierStrings.debugSendMessageQQHttp}");
			} catch (Exception) {
				_logger.LogError($"Error: {NotifierStrings.debugSendMessageQQHttp}");
				throw;
			}
		}

		public async Task SendMessage(string asfResult) {
			try {
				_logger.LogDebug(NotifierStrings.debugSendMessageASFQQHttp);

				string url = string.Format(NotifyFormatStrings.qqHttpUrlFormat, config.QQHttpAddress, config.QQHttpPort, config.QQHttpToken).ToString();

				var client = new HttpClient();

				var content = new QQHttpPostContent {
					UserID = config.ToQQID,
					Message = asfResult
				};

				var data = new StringContent(JsonSerializer.Serialize(content), Encoding.UTF8, "application/json");
				var resp = await client.PostAsync(url, data);
				resp.EnsureSuccessStatusCode();

				_logger.LogDebug(await resp.Content.ReadAsStringAsync());

				_logger.LogDebug($"Done: {NotifierStrings.debugSendMessageASFQQHttp}");
			} catch (Exception) {
				_logger.LogError($"Error: {NotifierStrings.debugSendMessageASFQQHttp}");
				throw;
			}
		}

		public void Dispose() {
			GC.SuppressFinalize(this);
		}
	}
}
