# SteamFreeGamesNotifier

A CLI tool

- Fetch free games from [Steam Store](https://store.steampowered.com/search/?hwtype=0&maxprice=free&category1=998%2C994%2C21&specials=1&ndl=1).
- Send notifications to Telegram, Bark, Email, QQ, PushPlus(Wechat), DingTalk, PushDeer, Discord and MeoW.
- Auto claim detected Steam free games with ASF `addlicense` command.

Demo Telegram Channel [@azhuge233_FreeGames](https://telegram.me/azhuge233_FreeGames)

## Build

Install dotnet 10.0 SDK first, you can find installation packages/guides [here](https://dotnet.microsoft.com/download).

```shell
git clone https://github.com/azhuge233/SteamStoreFreeGamesNotifier.git
cd SteamStoreFreeGamesNotifier
dotnet publish -c Release -p:PublishDir=/your/path/here -r [win-x64/osx-x64/...] --sc
```

## Usage

Set your telegram bot token and chat ID in config.json.

Check [wiki](https://github.com/azhuge233/SteamStoreFreeGamesNotifier/wiki) for more explanations.

### Repeatedly running

The program will not add while/for loop, it's a scraper. To schedule the program, use cron.d in Linux(macOS) or Task Scheduler in Windows.
