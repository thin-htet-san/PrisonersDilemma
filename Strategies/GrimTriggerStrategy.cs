using PrisonersDilemma.Models;

namespace PrisonersDilemma.Strategies;

public sealed class GrimTriggerStrategy : IGameStrategy
{
    public string Name => "Grim Trigger";

    public string Description => "Cooperates until betrayed once, then defects forever.";

    public GameMove ChooseMove(IReadOnlyList<RoundResult> history)
    {
        return history.Any(round => round.OpponentMove == GameMove.Betray)
            ? GameMove.Betray
            : GameMove.Cooperate;
    }
}
