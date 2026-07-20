namespace PrisonersDilemma.Models;

public sealed record MatchResult(
    string PlayerAName,
    string PlayerBName,
    int RoundsPlayed,
    IReadOnlyList<MatchRound> Rounds,
    int PlayerATotal,
    int PlayerBTotal,
    int MutualCooperationCount,
    int MutualBetrayalCount,
    int PlayerAExploitationCount,
    int PlayerBExploitationCount)
{
    public int PlayerALongestCooperationStreak => LongestStreak(round => round.PlayerAMove == GameMove.Cooperate);

    public int PlayerALongestBetrayalStreak => LongestStreak(round => round.PlayerAMove == GameMove.Betray);

    public int PlayerBLongestCooperationStreak => LongestStreak(round => round.PlayerBMove == GameMove.Cooperate);

    public int PlayerBLongestBetrayalStreak => LongestStreak(round => round.PlayerBMove == GameMove.Betray);

    public int PlayerACooperationCount => Rounds.Count(round => round.PlayerAMove == GameMove.Cooperate);

    public int PlayerBCooperationCount => Rounds.Count(round => round.PlayerBMove == GameMove.Cooperate);

    public double PlayerACooperationRate => Rounds.Count == 0 ? 0 : PlayerACooperationCount * 100.0 / Rounds.Count;

    public double PlayerBCooperationRate => Rounds.Count == 0 ? 0 : PlayerBCooperationCount * 100.0 / Rounds.Count;

    public int? TurningPointRound => GetTurningPoint()?.RoundNumber;

    public string TurningPointSummary
        => GetTurningPoint() is { } turningPoint
            ? $"Round {turningPoint.RoundNumber} shifted the score gap by {turningPoint.LeadSwing} points."
            : "No clear turning point appeared.";

    public string NarrativeSummary => BuildNarrativeSummary();

    public string WinnerLabel =>
        PlayerATotal == PlayerBTotal
            ? "Tie"
            : PlayerATotal > PlayerBTotal ? PlayerAName : PlayerBName;

    private int LongestStreak(Func<MatchRound, bool> predicate)
    {
        var best = 0;
        var current = 0;

        foreach (var round in Rounds)
        {
            if (predicate(round))
            {
                current++;
                best = Math.Max(best, current);
            }
            else
            {
                current = 0;
            }
        }

        return best;
    }

    private TurnPoint? GetTurningPoint()
    {
        if (Rounds.Count == 0)
        {
            return null;
        }

        var bestRound = Rounds[0];
        var bestSwing = 0;
        var previousGap = 0;

        foreach (var round in Rounds)
        {
            var currentGap = round.PlayerATotal - round.PlayerBTotal;
            var swing = currentGap - previousGap;

            if (Math.Abs(swing) > Math.Abs(bestSwing))
            {
                bestSwing = swing;
                bestRound = round;
            }

            previousGap = currentGap;
        }

        return new TurnPoint(bestRound.RoundNumber, bestSwing);
    }

    private string BuildNarrativeSummary()
    {
        var summaryParts = new List<string>();

        if (PlayerATotal == PlayerBTotal)
        {
            summaryParts.Add("The match stayed balanced.");
        }
        else
        {
            summaryParts.Add($"{WinnerLabel} won by {Math.Abs(PlayerATotal - PlayerBTotal)} points.");
        }

        if (MutualCooperationCount > MutualBetrayalCount && PlayerALongestCooperationStreak > 1 && PlayerBLongestCooperationStreak > 1)
        {
            summaryParts.Add("Both players found a cooperation rhythm.");
        }
        else if (MutualBetrayalCount >= MutualCooperationCount)
        {
            summaryParts.Add("Retaliation dominated the match.");
        }

        if (PlayerAExploitationCount > PlayerBExploitationCount)
        {
            summaryParts.Add($"{PlayerAName} exploited {PlayerBName} more often.");
        }
        else if (PlayerBExploitationCount > PlayerAExploitationCount)
        {
            summaryParts.Add($"{PlayerBName} exploited {PlayerAName} more often.");
        }

        var turningPoint = GetTurningPoint();
        if (turningPoint is not null)
        {
            summaryParts.Add($"The sharpest swing happened on round {turningPoint.RoundNumber}.");
        }

        return string.Join(" ", summaryParts);
    }

    private sealed record TurnPoint(int RoundNumber, int LeadSwing);
}
