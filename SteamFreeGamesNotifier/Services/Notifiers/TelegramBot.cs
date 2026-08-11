using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using SteamFreeGamesNotifier.Models.Config;
using SteamFreeGamesNotifier.Models.Record;
using SteamFreeGamesNotifier.Strings;

namespace SteamFreeGamesNotifier.Services.Notifiers {
	internal class TelegramBot(ILogger<TelegramBot> logger, IOptions<Config> config): INotifiable {
		private readonly ILogger _logger = logger;
		private readonly Config config = config.Value;

		public async Task SendMessage(List<FreeGameRecord> records) {
			var BotClient = new TelegramBotClient(token: config.TelegramToken);

			try {
				foreach (var record in records) {
					_logger.LogDebug($"{NotifierStrings.debugSendMessageTelegram} : {record.Name}");

					await BotClient.SendMessage(
						chatId: config.TelegramChatID,
						text: $"{record.ToTelegramMessage()}{NotifyFormatStrings.projectLinkHTML.Replace("<br>", "\n")}",
						parseMode: ParseMode.Html
					);
				}

				_logger.LogDebug($"Done: {NotifierStrings.debugSendMessageTelegram}");
			} catch (Exception) {
				_logger.LogError($"Error: {NotifierStrings.debugSendMessageTelegram}");
				throw;
			}
		}

		public async Task SendMessage(string asfResult) {
			var BotClient = new TelegramBotClient(token: config.TelegramToken);

			try {
				_logger.LogDebug(NotifierStrings.debugSendMessageASFTelegram);

				await BotClient.SendMessage(
					chatId: config.TelegramChatID,
					text: asfResult.Replace("<", "&lt;").Replace(">", "&gt;"),
					parseMode: ParseMode.Html
				);

				_logger.LogDebug($"Done: {NotifierStrings.debugSendMessageASFTelegram}");
			} catch (Exception) {
				_logger.LogError($"Error: {NotifierStrings.debugSendMessageASFTelegram}");
				throw;
			}
		}

		public void Dispose() {
			GC.SuppressFinalize(this);
		}
	}
}
