using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Net.Smtp;
using SteamFreeGamesNotifier.Models.Config;
using SteamFreeGamesNotifier.Models.Record;
using SteamFreeGamesNotifier.Strings;

namespace SteamFreeGamesNotifier.Services.Notifiers {
	internal class Email(ILogger<Email> logger, IOptions<Config> config): INotifiable {
		private readonly ILogger<Email> _logger = logger;
		private readonly Config config = config.Value;

		public async Task SendMessage(List<FreeGameRecord> records) {
			try {
				_logger.LogDebug(NotifierStrings.debugSendMessageEmail);

				var message = CreateMessage(records, config.FromEmailAddress, config.ToEmailAddress);

				using var client = new SmtpClient();
				client.Connect(config.SMTPServer, config.SMTPPort, true);
				client.Authenticate(config.AuthAccount, config.AuthPassword);
				await client.SendAsync(message);
				client.Disconnect(true);

				_logger.LogDebug($"Done: {NotifierStrings.debugSendMessageEmail}");
			} catch (Exception) {
				_logger.LogError($"Error: {NotifierStrings.debugSendMessageEmail}");
				throw;
			}
		}

		public async Task SendMessage(string asfResult) {
			try {
				_logger.LogDebug(NotifierStrings.debugSendMessageASFEmail);

				var message = CreateMessage(asfResult, config.FromEmailAddress, config.ToEmailAddress);

				using var client = new SmtpClient();
				client.Connect(config.SMTPServer, config.SMTPPort, true);
				client.Authenticate(config.AuthAccount, config.AuthPassword);
				await client.SendAsync(message);
				client.Disconnect(true);

				_logger.LogDebug($"Done: {NotifierStrings.debugSendMessageASFEmail}");
			} catch (Exception) {
				_logger.LogError($"Error: {NotifierStrings.debugSendMessageASFEmail}");
				throw;
			}
		}

		private MimeMessage CreateMessage(List<FreeGameRecord> pushList, string fromAddress, string toAddress) {
			try {
				_logger.LogDebug(NotifierStrings.debugCreateMessage);

				var message = new MimeMessage();

				message.From.Add(new MailboxAddress("Steam Store Free Games", fromAddress));
				message.To.Add(new MailboxAddress("Receiver", toAddress));

				var sb = new StringBuilder();

				message.Subject = sb.AppendFormat(NotifyFormatStrings.emailTitleFormat, pushList.Count).ToString();
				sb.Clear();

				pushList.ForEach(record => sb.AppendFormat(NotifyFormatStrings.emailBodyFormat, record.ToEmailMessage()));

				message.Body = new TextPart("html") {
					Text = sb.Append(NotifyFormatStrings.projectLinkHTML).ToString()
				};

				_logger.LogDebug($"Done: {NotifierStrings.debugCreateMessage}");
				return message;
			} catch (Exception) {
				_logger.LogError($"Error: {NotifierStrings.debugCreateMessage}");
				throw;
			}
		}

		private MimeMessage CreateMessage(string asfResult, string fromAddress, string toAddress) {
			try {
				_logger.LogDebug(NotifierStrings.debugCreateMessage);

				var message = new MimeMessage();

				message.From.Add(new MailboxAddress("Steam Store Free Games ASF Result", fromAddress));
				message.To.Add(new MailboxAddress("Receiver", toAddress));

				message.Subject = NotifyFormatStrings.emailASFTitleFormat;

				message.Body = new TextPart("html") {
					Text = asfResult.Replace("<", "&lt;").Replace(">", "&gt;").Replace("\n", "<br>")
				};

				_logger.LogDebug($"Done: {NotifierStrings.debugCreateMessage}");
				return message;
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
