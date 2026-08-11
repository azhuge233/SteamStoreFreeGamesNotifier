using System.Net.WebSockets;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Websocket.Client;
using SteamFreeGamesNotifier.Models.Config;
using SteamFreeGamesNotifier.Models.Record;
using SteamFreeGamesNotifier.Models.WebSocketContent;
using SteamFreeGamesNotifier.Strings;

namespace SteamFreeGamesNotifier.Services.Notifiers {
	internal class QQWebSocket(ILogger<QQWebSocket> logger, IOptions<Config> config): INotifiable {
		private readonly ILogger<QQWebSocket> _logger = logger;
		private readonly Config config = config.Value;

		public async Task SendMessage(List<FreeGameRecord> records) {
			try {
				_logger.LogDebug(NotifierStrings.debugSendMessageQQWebSocket);

				var packets = GetSendPacket(config, records);

				using var client = GetWSClient(config);

				await client.Start();

				foreach (var packet in packets) {
					await client.SendInstant(JsonSerializer.Serialize(packet));
					await Task.Delay(600);
				}

				await client.Stop(WebSocketCloseStatus.NormalClosure, string.Empty);

				_logger.LogDebug($"Done: {NotifierStrings.debugSendMessageQQWebSocket}");
			} catch (Exception) {
				_logger.LogDebug($"Error: {NotifierStrings.debugSendMessageQQWebSocket}");
				throw;
			}
		}

		public async Task SendMessage(string asfResult) {
			try {
				_logger.LogDebug(NotifierStrings.debugSendMessageASFQQWebSocket);

				using var client = GetWSClient(config);

				await client.Start();

				await client.SendInstant(JsonSerializer.Serialize(GetSendPacket(config, asfResult)));

				await Task.Delay(500);

				await client.Stop(WebSocketCloseStatus.NormalClosure, string.Empty);
				client.Dispose();

				_logger.LogDebug($"Done: {NotifierStrings.debugSendMessageASFQQWebSocket}");
			} catch (Exception) {
				_logger.LogDebug($"Error: {NotifierStrings.debugSendMessageASFQQWebSocket}");
				throw;
			}
		}

		private WebsocketClient GetWSClient(NotifyConfig config) {
			var url = new Uri(string.Format(NotifyFormatStrings.qqWebSocketUrlFormat, config.QQWebSocketAddress, config.QQWebSocketPort, config.QQWebSocketToken));

			#region new websocket client
			var client = new WebsocketClient(url);
			client.ReconnectionHappened.Subscribe(info => _logger.LogDebug(NotifierStrings.debugWSReconnectionQQWebSocket, info.Type));
			client.MessageReceived.Subscribe(msg => _logger.LogDebug(NotifierStrings.debugWSMessageRecievedQQWebSocket, msg));
			client.DisconnectionHappened.Subscribe(msg => _logger.LogDebug(NotifierStrings.debugWSDisconnectedQQWebSocket, msg));
			#endregion

			return client;
		}

		private static List<WSPacket> GetSendPacket(NotifyConfig config, List<FreeGameRecord> records) {
			return records.Select(record => new WSPacket() {
				Action = NotifyFormatStrings.qqWebSocketSendAction,
				Params = new Param {
					UserID = config.ToQQID,
					Message = $"{record.ToQQMessage()}{NotifyFormatStrings.projectLink}"
				}
			}).ToList();
		}

		private static WSPacket GetSendPacket(NotifyConfig config, string asfResult) {
			return new WSPacket() {
				Action = NotifyFormatStrings.qqWebSocketSendAction,
				Params = new Param() {
					UserID = config.ToQQID,
					Message = asfResult
				}
			};
		}

		public void Dispose() {
			GC.SuppressFinalize(this);
		}
	}
}
