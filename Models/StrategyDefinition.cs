using PrisonersDilemma.Strategies;

namespace PrisonersDilemma.Models;

public sealed record StrategyDefinition(
    string Id,
    string Name,
    string Description,
    string Persona,
    StrategyDifficulty Difficulty,
    int Aggression,
    int Forgiveness,
    int Complexity,
    Func<int, IGameStrategy> Factory)
{
    public IGameStrategy CreateStrategy(int seed) => Factory(seed);
}
