using PrisonersDilemma.Models;

namespace PrisonersDilemma.Strategies;

public sealed class AlwaysCooperateStrategy : IGameStrategy
{
    public string Name => "Always Cooperate";

    public string Description => "Starts friendly and never escalates.";

    public GameMove ChooseMove(IReadOnlyList<RoundResult> history) => GameMove.Cooperate;
}
