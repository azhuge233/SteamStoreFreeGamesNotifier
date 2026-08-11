using Microsoft.Extensions.Logging;
using SteamFreeGamesNotifier.Strings;

namespace SteamFreeGamesNotifier.Services {
	internal class Scraper(ILogger<Scraper> logger): IDisposable {
		private readonly ILogger<Scraper> _logger = logger;

		internal async Task<string> GetSource() {
			try {
				var client = GetHttpClient();

				_logger.LogDebug(ScrapeStrings.debugGetSource);

				var resp = await client.GetAsync(ScrapeStrings.Url);
				resp.EnsureSuccessStatusCode();
				var content = await resp.Content.ReadAsStringAsync();

				_logger.LogDebug($"Done: {ScrapeStrings.debugGetSource}");
				return content;
			} catch (Exception) {
				_logger.LogError($"Error: {ScrapeStrings.debugGetSource}");
				throw;
			}
		}

		private static HttpClient GetHttpClient() {
			var client = new HttpClient();

			client.DefaultRequestHeaders.Add("User-Agent", ScrapeStrings.UAs[new Random().Next(0, ScrapeStrings.UAs.Length - 1)]);

			return client;
		}

		public void Dispose() {
			GC.SuppressFinalize(this);
		}
	}
}
