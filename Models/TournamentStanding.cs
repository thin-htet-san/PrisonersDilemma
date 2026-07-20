namespace PrisonersDilemma.Models;

public sealed record TournamentStanding(
    string StrategyId,
    string StrategyName,
    int MatchesPlayed,
    int Wins,
    int Losses,
    int Ties,
    int TotalScore,
    int CooperationMoves,
    int TotalMoves)
{
    public double CooperationRate => TotalMoves == 0 ? 0 : CooperationMoves * 100.0 / TotalMoves;

    public double AverageScorePerMatch => MatchesPlayed == 0 ? 0 : TotalScore * 1.0 / MatchesPlayed;
}
