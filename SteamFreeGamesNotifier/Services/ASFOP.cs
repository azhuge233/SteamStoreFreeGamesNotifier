using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SteamFreeGamesNotifier.Models;
using SteamFreeGamesNotifier.Models.ASF;
using SteamFreeGamesNotifier.Models.Config;
using SteamFreeGamesNotifier.Models.Record;
using SteamFreeGamesNotifier.Strings;

namespace SteamFreeGamesNotifier.Services {
	internal class ASFOP(ILogger<ASFOP> logger, IOptions<Config> config) : IDisposable {
		private readonly ILogger<ASFOP> _logger = logger;
		private readonly Config config = config.Value;

		internal async Task<string> Addlicense(List<FreeGameRecord> gameList) {
			if (!config.EnableASF) {
				_logger.LogInformation(ASFStrings.infoASFDisabled);
				return string.Empty;
			}

			if (gameList.Count == 0) {
				_logger.LogInformation(ASFStrings.infoNoRecords);
				return string.Empty;
			}

			try {
				_logger.LogDebug(ASFStrings.debugASFOP);

				var client = new HttpClient();
				client.DefaultRequestHeaders.Add(ASFStrings.AuthenticationKey, config.ASFIPCPassword);
				client.DefaultRequestHeaders.Add(ASFStrings.UAKey, ASFStrings.UAValue);

				var url = string.Format(ASFStrings.commandUrl, config.ASFIPCUrl);
				var idString = GenerateSubIDString(gameList);
				var command = $"{ASFStrings.addlicenseCommand}{idString}";

				_logger.LogDebug(ASFStrings.debugFinalCommand, command);

				var content = new StringContent(JsonSerializer.Serialize(new AddlicenssPostContent() { Command = command }), Encoding.UTF8, "application/json");

				var response = await client.PostAsync(url, content);
				response.EnsureSuccessStatusCode();
				_logger.LogDebug(ASFStrings.debugResponse, response.ToString());

				var addlicenseResult = JsonSerializer.Deserialize<ASFResponseContent>(await response.Content.ReadAsStringAsync()).Result;
				_logger.LogInformation(ASFStrings.infoAddlicenseResult, addlicenseResult);

				_logger.LogDebug($"Done: {ASFStrings.debugASFOP}");
				return addlicenseResult;
			} catch (Exception) {
				_logger.LogError($"Error: {ASFStrings.debugASFOP}");
				throw;
			} finally {
				Dispose();
			}
		}


		private string GenerateSubIDString(List<FreeGameRecord> gameList) {
			try {
				_logger.LogDebug(ASFStrings.debugGenerateSubIDString);

				StringBuilder sb = new();
				gameList.ForEach(game => sb.Append(sb.Length == 0 ? game.AppID : $",{game.AppID}"));

				_logger.LogDebug($"Done: {ASFStrings.debugGenerateSubIDString}");
				return sb.ToString();
			} catch (Exception) {
				_logger.LogError($"Error: {ASFStrings.debugGenerateSubIDString}");
				throw;
			}
		}

		public void Dispose() {
			GC.SuppressFinalize(this);
		}
	}
}
