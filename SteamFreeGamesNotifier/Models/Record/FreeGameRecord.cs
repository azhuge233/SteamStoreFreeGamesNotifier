using SteamFreeGamesNotifier.Strings;
using System.Text.RegularExpressions;

namespace SteamFreeGamesNotifier.Models.Record {
	public class FreeGameRecord {
		public string Name { get; set; }

		public string AppID { get; set; }
		
		public string Link { get; set; }

		public string Review { get; set; }
		
		public string OriginalPrice { get; set; }

		public string ToTelegramMessage() {
			return string.Format(NotifyFormatStrings.telegramFormat, Name, AppID, Review, OriginalPrice, Link, RemoveSpecialCharacters(Name));
		}

		public string ToBarkMessage() {
			return string.Format(NotifyFormatStrings.barkFormat, Name, AppID, Review, OriginalPrice, Link);
		}

		public string ToEmailMessage() {
			return string.Format(NotifyFormatStrings.emailFormat, Name, AppID, Review, OriginalPrice, Link);
		}

		public string ToQQMessage() {
			return string.Format(NotifyFormatStrings.qqFormat, Name, AppID, Review, OriginalPrice, Link);
		}

		public string ToPushPlusMessage() {
			return string.Format(NotifyFormatStrings.pushPlusFormat, Name, AppID, Review, OriginalPrice, Link);
		}

		public string ToDingTalkMessage() {
			return string.Format(NotifyFormatStrings.dingTalkFormat, Name, AppID, Review, OriginalPrice, Link);
		}

		public string ToPushDeerMessage() {
			return string.Format(NotifyFormatStrings.pushDeerFormat, Name, AppID, Review, OriginalPrice, Link);
		}

		public string ToDiscordMessage() {
			return string.Format(NotifyFormatStrings.discordFormat, AppID, Review, OriginalPrice);
		}

		public string ToMeowMessage() {
			return string.Format(NotifyFormatStrings.meowFormat, Name, AppID, Review, OriginalPrice, Link);
		}

		private static string RemoveSpecialCharacters(string str) {
			return Regex.Replace(str, NotifierStrings.removeSpecialCharsRegex, string.Empty);
		}
	}
}
