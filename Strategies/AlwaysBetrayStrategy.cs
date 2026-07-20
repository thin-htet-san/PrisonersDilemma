using PrisonersDilemma.Models;

namespace PrisonersDilemma.Strategies;

public sealed class AlwaysBetrayStrategy : IGameStrategy
{
    public string Name => "Always Betray";

    public string Description => "Defects every round and pressures cooperative opponents.";

    public GameMove ChooseMove(IReadOnlyList<RoundResult> history) => GameMove.Betray;
}
