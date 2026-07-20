using PrisonersDilemma.Models;

namespace PrisonersDilemma.Strategies;

public sealed class TitForTatStrategy : IGameStrategy
{
    public string Name => "Tit for Tat";

    public string Description => "Starts cooperatively, then copies the opponent's previous move.";

    public GameMove ChooseMove(IReadOnlyList<RoundResult> history)
    {
        if (history.Count == 0)
        {
            return GameMove.Cooperate;
        }

        return history[^1].OpponentMove;
    }
}
