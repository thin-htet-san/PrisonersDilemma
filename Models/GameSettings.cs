namespace PrisonersDilemma.Models;

public sealed record GameSettings(
    int MatchRounds = 50,
    int TournamentRounds = 100,
    int Seed = 20260717);
