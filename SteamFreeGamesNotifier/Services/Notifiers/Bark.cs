using System.Text;
using System.Web;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SteamFreeGamesNotifier.Models.Config;
using SteamFreeGamesNotifier.Models.Record;
using SteamFreeGamesNotifier.Strings;

namespace SteamFreeGamesNotifier.Services.Notifiers {
	internal class Bark(ILogger<Bark> logger, IOptions<Config> config) {
		private readonly ILogger<Bark> _logger = logger;
		private readonly Config config = config.Value;

		public async Task SendMessage(List<FreeGameRecord> records) {
			try {
				var sb = new StringBuilder();
				string url = sb.AppendFormat(NotifyFormatStrings.barkUrlFormat, config.BarkAddress, config.BarkToken).ToString();
				var client = new HttpClient();

				foreach (var record in records) {
					sb.Clear();

					_logger.LogDebug($"{NotifierStrings.debugSendMessageBark}: {record.Name}");

					sb.Append(url)
						.Append(NotifyFormatStrings.barkUrlTitle)
						.Append(HttpUtility.UrlEncode(record.ToBarkMessage()))
						.Append(HttpUtility.UrlEncode(NotifyFormatStrings.projectLink))
						.AppendFormat(NotifyFormatStrings.barkUrlArgs, HttpUtility.UrlEncode(record.Link));

					_logger.LogDebug(sb.ToString());

					var resp = await client.GetAsync(sb.ToString());
					resp.EnsureSuccessStatusCode();
					var responseContent = await resp.Content.ReadAsStringAsync();

					_logger.LogDebug(responseContent);
				}

				_logger.LogDebug($"Done: {NotifierStrings.debugSendMessageBark}");
			} catch (Exception) {
				_logger.LogDebug($"Error: {NotifierStrings.debugSendMessageBark}");
				throw;
			} finally {
				Dispose();
			}
		}

		public async Task SendMessage(string asfRecord) {
			try {
				_logger.LogDebug(NotifierStrings.debugSendMessageASFBark);

				var sb = new StringBuilder();
				string url = string.Format(NotifyFormatStrings.barkUrlFormat, config.BarkAddress, config.BarkToken);
				var client = new HttpClient();

				var resp = await client.GetAsync($"{url}{NotifyFormatStrings.barkUrlASFTitle}{HttpUtility.UrlEncode(asfRecord)}");
				resp.EnsureSuccessStatusCode();
				_logger.LogDebug(await resp.Content.ReadAsStringAsync());

				_logger.LogDebug($"Done: {NotifierStrings.debugSendMessageASFBark}");
			} catch (Exception) {
				_logger.LogDebug($"Error: {NotifierStrings.debugSendMessageASFBark}");
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
