using PrisonersDilemma.Models;

namespace PrisonersDilemma.Strategies;

public interface IGameStrategy
{
    string Name { get; }

    string Description { get; }

    GameMove ChooseMove(IReadOnlyList<RoundResult> history);
}
