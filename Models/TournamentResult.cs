namespace PrisonersDilemma.Models;

public sealed record TournamentResult(
    IReadOnlyList<TournamentStanding> Standings,
    IReadOnlyList<TournamentMatchResult> Matches,
    int RoundsPerMatch);
