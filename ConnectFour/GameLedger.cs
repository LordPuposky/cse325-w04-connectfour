namespace ConnectFour;

/// <summary>
/// One finished game, recorded like a transaction in a bank statement.
/// </summary>
public record GameRecord(int Number, DateTime FinishedAt, GameState.WinState Result, int Moves)
{
    public string ResultText => Result switch
    {
        GameState.WinState.Player1_Wins => "Player 1 Wins",
        GameState.WinState.Player2_Wins => "Player 2 Wins",
        GameState.WinState.Tie => "Tie",
        _ => "In progress"
    };
}

/// <summary>
/// Keeps the history of finished games and calculates the score summary.
/// </summary>
public class GameLedger
{
    private readonly List<GameRecord> records = new();

    /// <summary>
    /// Finished games, most recent first (like a bank statement).
    /// </summary>
    public IReadOnlyList<GameRecord> Records =>
        records.OrderByDescending(r => r.Number).ToList();

    public int Player1Wins => records.Count(r => r.Result == GameState.WinState.Player1_Wins);
    public int Player2Wins => records.Count(r => r.Result == GameState.WinState.Player2_Wins);
    public int Ties => records.Count(r => r.Result == GameState.WinState.Tie);
    public int TotalGames => records.Count;

    /// <summary>
    /// Records a finished game. Rejects games that have not ended.
    /// </summary>
    public void AddRecord(GameState.WinState result, int moves)
    {
        if (result == GameState.WinState.No_Winner)
        {
            throw new ArgumentException("Only finished games can be recorded.");
        }

        records.Add(new GameRecord(records.Count + 1, DateTime.Now, result, moves));
    }

    /// <summary>
    /// Returns the player on a winning streak and how many games in a row.
    /// A tie breaks the streak. Returns (0, 0) when there is no active streak.
    /// </summary>
    public (int Player, int Count) CurrentStreak()
    {
        if (records.Count == 0)
        {
            return (0, 0);
        }

        var lastResult = records[^1].Result;
        if (lastResult == GameState.WinState.Tie)
        {
            return (0, 0);
        }

        var count = 0;
        for (var i = records.Count - 1; i >= 0 && records[i].Result == lastResult; i--)
        {
            count++;
        }

        return ((int)lastResult, count);
    }
}
