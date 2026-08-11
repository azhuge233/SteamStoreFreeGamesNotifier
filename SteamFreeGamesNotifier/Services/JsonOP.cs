using System.Text.Json;
using Microsoft.Extensions.Logging;
using SteamFreeGamesNotifier.Models.Record;
using SteamFreeGamesNotifier.Strings;

namespace SteamFreeGamesNotifier.Services {
	internal class JsonOP(ILogger<JsonOP> logger): IDisposable {
		private readonly ILogger<JsonOP> _logger = logger;

		internal void WriteData(List<FreeGameRecord> data) {
			try {
				if (data.Count > 0) {
					_logger.LogDebug(JsonOPStrings.debugWrite);
					string json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
					File.WriteAllText(JsonOPStrings.recordsPath, string.Empty);
					File.WriteAllText(JsonOPStrings.recordsPath, json);
					_logger.LogDebug($"Done: {JsonOPStrings.debugWrite}");
				} else _logger.LogDebug(JsonOPStrings.debugNoRecords);
			} catch (Exception) {
				_logger.LogError($"Error: {JsonOPStrings.debugWrite}");
				throw;
			} finally {
				Dispose();
			}
		}

		internal List<FreeGameRecord> LoadData() {
			try {
				_logger.LogDebug(JsonOPStrings.debugLoadRecords);
				var content = JsonSerializer.Deserialize<List<FreeGameRecord>>(File.ReadAllText(JsonOPStrings.recordsPath));
				_logger.LogDebug($"Done: {JsonOPStrings.debugLoadRecords}");
				return content;
			} catch (Exception) {
				_logger.LogError($"Error: {JsonOPStrings.debugLoadRecords}");
				throw;
			}
		}

		public void Dispose() {
			GC.SuppressFinalize(this);
		}
	}
}
