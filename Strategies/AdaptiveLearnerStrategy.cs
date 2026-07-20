using PrisonersDilemma.Models;

namespace PrisonersDilemma.Strategies;

public sealed class AdaptiveLearnerStrategy : IGameStrategy
{
    public string Name => "Adaptive Learner";

    public string Description => "Shifts toward the move that has recently produced better results.";

    public GameMove ChooseMove(IReadOnlyList<RoundResult> history)
    {
        if (history.Count == 0)
        {
            return GameMove.Cooperate;
        }

        var last = history[^1];
        var opponentCooperationRate = history.Count(round => round.OpponentMove == GameMove.Cooperate) * 1.0 / history.Count;

        if (last.MyScore >= 5)
        {
            return last.MyMove;
        }

        if (last.MyScore == 0 || last.OpponentMove == GameMove.Betray)
        {
            return GameMove.Betray;
        }

        if (opponentCooperationRate >= 0.6)
        {
            return GameMove.Cooperate;
        }

        if (opponentCooperationRate <= 0.4)
        {
            return GameMove.Betray;
        }

        return last.MyMove == GameMove.Cooperate ? GameMove.Cooperate : GameMove.Betray;
    }
}
