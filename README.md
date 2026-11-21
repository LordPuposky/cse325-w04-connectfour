# CSE 325 - Week 04 Assignment: Connect Four with Blazor

A two-player "Connect Four" game built with Blazor, following the Microsoft Learn module [Build a Connect Four game with Blazor](https://learn.microsoft.com/en-us/training/modules/dotnet-connect-four/), plus one additional custom feature.

**Environment:** .NET SDK 10.0.404 · Visual Studio Code · Windows

## How to run

```bash
dotnet run --project ConnectFour
```

Then open the URL shown in the terminal (for example `http://localhost:5223`).

## Tutorial work

| Unit | What was done                                                                                                                                                                                  |
| ---- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| 3    | Created the Blazor Web App (Server interactivity, per page/component) and a `Board` component with 42 slots, CSS variables and an isolated stylesheet.                                         |
| 5    | Added the `GameState` class as a singleton service (dependency injection), drop buttons with `@onclick`, `InteractiveServer` render mode, error handling, winner detection and a reset button. |
| 6    | Added `BoardColor`, `Player1Color` and `Player2Color` parameters to the `Board` component and customized them from the `Home` page.                                                            |

## Additional feature: Game Statement

Inspired by a **bank account statement**, the game keeps a record of every finished game:

- **Summary cards:** Player 1 wins, Player 2 wins, ties and total games played. The player cards use the same colors as the game pieces.
- **Current streak:** which player is on a winning streak and how many games in a row (a tie breaks the streak).
- **Statement table:** one row per finished game with number, date and time, result and number of moves, **most recent first**.

### How it works

| File                                     | Responsibility                                                                                                                                                                                                                        |
| ---------------------------------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| `GameLedger.cs`                          | `GameRecord` record (one finished game) and `GameLedger` class that stores the records and calculates totals with LINQ. Records can only be added, never edited or removed, like a ledger. Registered as a singleton in `Program.cs`. |
| `Components/Scoreboard.razor` (+ `.css`) | Displays the summary, the streak and the statement table. Receives the ledger as a `[Parameter]`.                                                                                                                                     |
| `Components/Board.razor`                 | Records a game in the ledger **only once**, at the moment it ends (it checks whether the game was already over before the click to avoid duplicate records).                                                                          |

## Notes

- The tutorial targets .NET 8. The project was created with **.NET 10** (`net10.0`) to match the installed SDK.
- `GameState` and `GameLedger` are singletons, so all browser tabs share the same game and statement while the app is running. The statement resets when the app restarts.
