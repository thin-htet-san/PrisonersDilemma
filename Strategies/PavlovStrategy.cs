using PrisonersDilemma.Models;

namespace PrisonersDilemma.Strategies;

public sealed class PavlovStrategy : IGameStrategy
{
    public string Name => "Pavlov";

    public string Description => "Stays with successful choices and switches after weak outcomes.";

    public GameMove ChooseMove(IReadOnlyList<RoundResult> history)
    {
        if (history.Count == 0)
        {
            return GameMove.Cooperate;
        }

        var last = history[^1];

        if (last.MyScore >= 3)
        {
            return last.MyMove;
        }

        return last.MyMove == GameMove.Cooperate ? GameMove.Betray : GameMove.Cooperate;
    }
}
