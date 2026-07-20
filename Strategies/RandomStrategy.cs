using PrisonersDilemma.Models;

namespace PrisonersDilemma.Strategies;

public sealed class RandomStrategy : IGameStrategy
{
    private readonly Random _random;

    public RandomStrategy(int seed)
    {
        _random = new Random(seed);
    }

    public string Name => "Random";

    public string Description => "Chooses cooperate or betray with equal probability.";

    public GameMove ChooseMove(IReadOnlyList<RoundResult> history)
        => _random.Next(2) == 0 ? GameMove.Cooperate : GameMove.Betray;
}
