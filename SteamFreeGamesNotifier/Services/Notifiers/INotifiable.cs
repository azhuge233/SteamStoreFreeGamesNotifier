using SteamFreeGamesNotifier.Models.Record;

namespace SteamFreeGamesNotifier.Services.Notifiers {
	internal interface INotifiable: IDisposable {
		public Task SendMessage(List<FreeGameRecord> records);
		public Task SendMessage(string asfRecord);
	}
}
