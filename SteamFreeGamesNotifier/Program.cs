using Microsoft.Extensions.DependencyInjection;
using NLog;
using SteamFreeGamesNotifier.Modules;
using SteamFreeGamesNotifier.Services;

namespace SteamFreeGamesNotifier {
	internal class Program {
		private static readonly Logger logger = LogManager.GetCurrentClassLogger();

		static async Task Main() {
			try {
				var servicesProvider = DI.BuildAll();

				logger.Info(" - Start Job -");

				using (servicesProvider as IDisposable) {
					var jsonOp = servicesProvider.GetRequiredService<JsonOP>();
					var notifyOP = servicesProvider.GetRequiredService<NotifyOP>();

					var oldRecord = jsonOp.LoadData();
					servicesProvider.GetRequiredService<ConfigValidator>().CheckValid();

					// Get page source
					//var source = await servicesProvider.GetRequiredService<Scraper>().GetSource();
					var source = File.ReadAllText($"{AppDomain.CurrentDomain.BaseDirectory}test.html");

					// Parse page source
					var parseResult = servicesProvider.GetRequiredService<Parser>().Parse(source, oldRecord);

					// Notify first, then write records
					await notifyOP.Notify(parseResult.NotifyRecords);

					// Write new records
					jsonOp.WriteData(parseResult.Records);

					// Add free games through ASF, returns ASF result string
					var addlicenseResult = await servicesProvider.GetRequiredService<ASFOP>().Addlicense(parseResult.NotifyRecords);

					// Send ASF result
					await notifyOP.Notify(addlicenseResult);
				}

				logger.Info(" - Job End -\n");
			} catch (Exception ex) {
				logger.Error($"{ex.Message}\n");
			} finally {
				LogManager.Shutdown();
			}
		}
	}
}
