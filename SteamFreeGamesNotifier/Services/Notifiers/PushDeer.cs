using System.Text;
using System.Web;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SteamFreeGamesNotifier.Models.Config;
using SteamFreeGamesNotifier.Models.Record;
using SteamFreeGamesNotifier.Strings;

namespace SteamFreeGamesNotifier.Services.Notifiers {
	internal class PushDeer(ILogger<PushDeer> logger, IOptions<Config> config): INotifiable {
		private readonly ILogger<PushDeer> _logger = logger;
		private readonly Config config = config.Value;

		public async Task SendMessage(List<FreeGameRecord> records) {
			try {
				_logger.LogDebug(NotifierStrings.debugSendMessagePushDeer);
				var sb = new StringBuilder();
				var client = new HttpClient();

				foreach (var record in records) {
					sb.Clear();

					_logger.LogDebug($"{NotifierStrings.debugSendMessagePushDeer} : {record.Name}");

					sb.AppendFormat(NotifyFormatStrings.pushDeerUrlFormat, config.PushDeerToken, HttpUtility.UrlEncode(record.ToPushDeerMessage()))
						.Append(HttpUtility.UrlEncode(NotifyFormatStrings.projectLink));

					var resp = await client.GetAsync(sb.ToString());
					resp.EnsureSuccessStatusCode();
					var responseContent = await resp.Content.ReadAsStringAsync();

					_logger.LogDebug(responseContent);
				}

				_logger.LogDebug($"Done: {NotifierStrings.debugSendMessagePushDeer}");
			} catch (Exception) {
				_logger.LogError($"Error: {NotifierStrings.debugSendMessagePushDeer}");
				throw;
			}
		}

		public async Task SendMessage(string asfRecord) {
			try {
				_logger.LogDebug(NotifierStrings.debugSendMessageASFPushDeer);

				var client = new HttpClient();

				var resp = await client.GetAsync(string.Format(NotifyFormatStrings.pushDeerUrlFormat, config.PushDeerToken, HttpUtility.UrlEncode(asfRecord)));
				resp.EnsureSuccessStatusCode();
				_logger.LogDebug(await resp.Content.ReadAsStringAsync());

				_logger.LogDebug($"Done: {NotifierStrings.debugSendMessageASFPushDeer}");
			} catch (Exception) {
				_logger.LogDebug($"Error: {NotifierStrings.debugSendMessageASFPushDeer}");
				throw;
			}
		}

		public void Dispose() {
			GC.SuppressFinalize(this);
		}
	}
}
