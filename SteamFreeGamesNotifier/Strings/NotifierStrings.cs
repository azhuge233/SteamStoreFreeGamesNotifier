namespace SteamFreeGamesNotifier.Strings {
	internal class NotifierStrings {
		internal const string removeSpecialCharsRegex = @"[^\p{L}\p{N}]";

		#region debug strings
		internal const string debugSendMessageBark = "Send notification to Bark";
		internal const string debugSendMessageDingTalk = "Send notifications to DingTalk";
		internal const string debugSendMessageDiscord = "Send notification to Discord";
		internal const string debugSendMessageEmail = "Send notification to Email";
		internal const string debugSendMessagePushDeer = "Send notification to PushDeer";
		internal const string debugSendMessagePushPlus = "Send notification to PushPlus";
		internal const string debugSendMessageQQHttp = "Send notifications to QQ Http";
		internal const string debugSendMessageQQWebSocket = "Send notifications to QQ WebSocket";
		internal const string debugSendMessageTelegram = "Send notification to Telegram";
		internal const string debugSendMessageMeow = "Send notification to Meow";

		internal const string debugSendMessageASFBark = "Send ASF result to Bark";
		internal const string debugSendMessageASFDingTalk = "Send ASF result to DingTalk";
		internal const string debugSendMessageASFDiscord = "Send ASF result to Discord";
		internal const string debugSendMessageASFEmail = "Send ASF result to Email";
		internal const string debugSendMessageASFPushDeer = "Send ASF result to PushDeer";
		internal const string debugSendMessageASFPushPlus = "Send ASF result to PushPlus";
		internal const string debugSendMessageASFQQHttp = "Send ASF result to QQ Http";
		internal const string debugSendMessageASFQQWebSocket = "Send ASF result to QQ WebSocket";
		internal const string debugSendMessageASFTelegram = "Send ASF result to Telegram";
		internal const string debugSendMessageASFMeow = "Send ASF result to Meow";

		internal const string debugWSReconnectionQQWebSocket = "Reconnection happened, type: {0}";
		internal const string debugWSMessageRecievedQQWebSocket = "Message received: {0}";
		internal const string debugWSDisconnectedQQWebSocket = "Disconnected: {0}";

		internal const string debugCreateMessage = "Create notification message";
		#endregion
	}
}
