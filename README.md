# Hall Music Player

In-store background music player with scheduled ad inserts — a C# console app built around a **reactive event bus (Rx.NET)**. Used in production: plays a shuffled music playlist in a shop hall and interrupts it every N minutes with rotating ad blocks, then resumes the music.

## How it works

Everything is an **action dispatched through an Rx `Subject`** (`AdService`) — a redux-style event bus:

```
TimeForAdDispatcher (timer, every AdTime min) ──┐
Console commands (IEnumerable → ToObservable) ──┼──► AdService.Dispatch(action) ──► subscribers Execute()
Player callbacks (track/ad finished)          ──┘
```

- **`AdService`** — `Subject<IAdServiceActions>`: single dispatch point, any part of the app subscribes to the action stream.
- **Actions** (`Play`, `Stop`, `Pause`, `Resume`, `AdPlay`, `AdPlayFinished`) — each carries its `Type()` and `Execute()`.
- **`TimeForAdDispatcher`** — dispatches `AdPlay` on a timer; listens to the same bus to suspend itself while an ad block is playing (`ADPLAY` → pause timer, `ADPLAYFINISHED` → resume).
- **Console input as a stream** — commands typed by the operator are a lazy `IEnumerable<IAdServiceActions>` turned into an observable and dispatched to the bus.
- **Queues** — `MusicQueue` (shuffled `.mp3` files from `MusicDir`, cycled endlessly), `AdBlockQueue` (ad blocks rotate: each `AdPlay` plays the next block in a round-robin), `TracksProvider` switches between the music deck and the ad deck.
- **Playback** — NAudio.

## Configuration (`appsettings.json`)

```json
{
  "MusicDir": "C:\\music",         // folder with .mp3 files, shuffled and looped
  "AdTime": 15,                    // minutes between ad blocks
  "adblocks": [
    { "FileNames": [ { "FileName": "C:\\ads\\promo1.mp3" } ] },
    { "FileNames": [ { "FileName": "C:\\ads\\promo2.mp3" } ] }
  ]
}
```

All ad file paths are validated at startup.

## Run

Build in Visual Studio (NuGet restore) and run the console app. Operator commands at runtime:

```
stop    - stop current track and start the next one
pause   - pause current track
resume  - resume current track
adplay  - play the next ad block immediately
exit    - stop the player and exit
```

## Stack

C# (.NET Framework 4), Rx.NET (System.Reactive 2.x), NAudio, Newtonsoft.Json
