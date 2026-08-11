namespace SteamFreeGamesNotifier.Strings {
	internal class NotifyFormatStrings {
		#region ToMessage() strings
		internal const string telegramFormat = "<b>Steam Store Free Games</b>\n\n" +
			"<b>{0}</b>\n\n" +
			"AppID: <code>{1}</code>\n" +
			"Review: <i><b>{2}</b></i>\n" +
			"Original Price: <i><s><b>{3}</b></s></i>\n" +
			"Link: <a href=\"{4}\" >{4}</a>\n\n" +
			"#SteamStore #Steam #{5}";
		internal const string barkFormat = "{0}\n" +
			"AppID: {1}\n" +
			"Review: {2}\n" +
			"Original Price: {3}\n" +
			"Link: {4}";
		internal const string emailFormat = "<p><b>{0}</b><br>" +
			"AppID: {1}<br>" +
			"Review: {2}<br>" +
			"Original Price: {3}<br>" +
			"Link: <a href=\"{4}\" >{4}</a></p>";
		internal const string qqFormat = "Steam Store Free Games\n\n" +
			"{0}\n\n" +
			"AppID: {1}\n" +
			"Review: {2}\n" +
			"Original Price: {3}\n" +
			"Link: {4}";
		internal const string pushPlusFormat = "<p><b>{0}</b><br>" +
			"AppID: {1}<br>" +
			"Review: {2}<br>" +
			"Original Price: {3}<br>" +
			"Link: <a href=\"{4}\" >{4}</a></p>";
		internal const string dingTalkFormat = "Steam Store Free Games\n\n" +
			"{0}\n\n" +
			"AppID: {1}\n" +
			"Review: {2}\n" +
			"Original Price: {3}\n" +
			"Link: {4}";
		internal const string pushDeerFormat = "Steam Store Free Games\n\n" +
			"{0}\n\n" +
			"AppID: {1}\n\n" +
			"Review: {2}\n\n" +
			"Original Price: {3}\n\n" +
			"Link: {4}";
		internal const string discordFormat = "AppID: {0}\n" +
			"Review: {1}\n" +
			"Original Price: {2}";
		internal const string meowFormat = "{0}\n" +
			"AppID: {1}\n" +
			"Review: {2}\n" +
			"Original Price: {3}\n" +
			"Link: {4}";
		#endregion

		#region url, title format strings
		internal const string possibleLinkFormat = "{0}\n";
		internal const string possibleLinkFormatHtml = "<a href=\"{0}\">{0}</a><br>";

		internal const string telegramTag = "\n#SteamFreeGames";

		internal const string barkUrlFormat = "{0}/{1}/";
		internal const string barkUrlTitle = "SteamStoreFreeGames/";
		internal const string barkUrlASFTitle = "SteamStoreFreeGamesASFResult/";
		internal const string barkUrlArgs = "?group=SteamStoreFreeGames" +
			"&isArchive=1" +
			"&sound=calypso" +
			"&url={0}" +
			"&copy={0}";

		internal const string emailTitleFormat = "{0} new free game(s) - Steam Store Free Games";
		internal const string emailASFTitleFormat = "Steam Store Free Games ASF Result";
		internal const string emailBodyFormat = "<br>{0}";

		internal const string qqHttpUrlFormat = "http://{0}:{1}/send_private_msg?access_token={2}";
		internal const string qqWebSocketUrlFormat = "ws://{0}:{1}/?access_token={2}";
		internal const string qqWebSocketSendAction = "send_private_msg";

		internal const string pushPlusTitleFormat = "{0} new free game(s) - Steam Store Free Games";
		internal const string pushPlusASFTitleFormat = "Steam Store Free Games ASF Result";
		internal const string pushPlusBodyFormat = "<br>{0}";
		internal const string pushPlusUrlFormat = "http://www.pushplus.plus/send?token={0}&template=html&title={1}&content=";
		internal const string pushPlusPostUrl = "http://www.pushplus.plus/send";

		internal const string dingTalkUrlFormat = "https://oapi.dingtalk.com/robot/send?access_token={0}";

		internal const string pushDeerUrlFormat = "https://api2.pushdeer.com/message/push?pushkey={0}&&text={1}";

		internal const string meowUrlFormat = "{0}/{1}";
		internal const string meowUrlTitle = "Steam Store Free Games";
		internal const string meowUrlASFTitle = "Steam Store Free Games ASF Result";
		#endregion

		internal const string projectLink = "\n\nFrom https://github.com/azhuge233/SteamStoreFreeGamesNotifier";
		internal const string projectLinkHTML = "<br><br>From <a href=\"https://github.com/azhuge233/SteamStoreFreeGamesNotifier\">SteamStoreFreeGamesNotifier</a>";
	}
}
