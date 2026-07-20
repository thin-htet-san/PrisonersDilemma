namespace PrisonersDilemma.Models;

public sealed record TournamentMatchResult(
    string StrategyA,
    string StrategyB,
    MatchResult MatchResult);
