using PrisonersDilemma.Models;

namespace PrisonersDilemma.Strategies;

public sealed class PatternHunterStrategy : IGameStrategy
{
    public string Name => "Pattern Hunter";

    public string Description => "Looks for short repeating patterns and counters the next predicted move.";

    public GameMove ChooseMove(IReadOnlyList<RoundResult> history)
    {
        if (history.Count == 0)
        {
            return GameMove.Cooperate;
        }

        var opponentMoves = history.Select(round => round.OpponentMove).ToArray();

        for (var patternLength = 3; patternLength >= 2; patternLength--)
        {
            if (opponentMoves.Length < patternLength * 2)
            {
                continue;
            }

            var currentPattern = opponentMoves.Skip(opponentMoves.Length - patternLength).Take(patternLength).ToArray();
            var previousPattern = opponentMoves.Skip(opponentMoves.Length - patternLength * 2).Take(patternLength).ToArray();

            if (currentPattern.SequenceEqual(previousPattern))
            {
                var predictedOpponentMove = currentPattern[0];
                return predictedOpponentMove == GameMove.Cooperate ? GameMove.Betray : GameMove.Cooperate;
            }
        }

        return history[^1].OpponentMove == GameMove.Cooperate ? GameMove.Betray : GameMove.Cooperate;
    }
}
