using PrisonersDilemma.Models;
using PrisonersDilemma.Strategies;

namespace PrisonersDilemma.Services;

public sealed class GameEngine
{
    public MatchRound PlayRound(
        IGameStrategy playerA,
        IGameStrategy playerB,
        IReadOnlyList<MatchRound> roundsSoFar,
        int roundNumber,
        int playerATotal,
        int playerBTotal)
    {
        var playerAHistory = BuildPerspectiveHistory(roundsSoFar, true);
        var playerBHistory = BuildPerspectiveHistory(roundsSoFar, false);

        var playerAMove = playerA.ChooseMove(playerAHistory);
        var playerBMove = playerB.ChooseMove(playerBHistory);

        var (playerAScore, playerBScore) = Score(playerAMove, playerBMove);

        playerATotal += playerAScore;
        playerBTotal += playerBScore;

        return new MatchRound(
            roundNumber,
            playerAMove,
            playerBMove,
            playerAScore,
            playerBScore,
            playerATotal,
            playerBTotal);
    }

    public MatchResult PlayMatch(
        IGameStrategy playerA,
        IGameStrategy playerB,
        int rounds,
        string? playerAName = null,
        string? playerBName = null)
    {
        var roundList = new List<MatchRound>(rounds);
        var playerATotal = 0;
        var playerBTotal = 0;
        var mutualCooperationCount = 0;
        var mutualBetrayalCount = 0;
        var playerAExploitationCount = 0;
        var playerBExploitationCount = 0;

        for (var round = 1; round <= rounds; round++)
        {
            var nextRound = PlayRound(playerA, playerB, roundList, round, playerATotal, playerBTotal);
            roundList.Add(nextRound);
            playerATotal = nextRound.PlayerATotal;
            playerBTotal = nextRound.PlayerBTotal;

            if (nextRound.IsMutualCooperation)
            {
                mutualCooperationCount++;
            }
            else if (nextRound.IsMutualBetrayal)
            {
                mutualBetrayalCount++;
            }
            else if (nextRound.PlayerAExploitedPlayerB)
            {
                playerAExploitationCount++;
            }
            else if (nextRound.PlayerBExploitedPlayerA)
            {
                playerBExploitationCount++;
            }
        }

        return new MatchResult(
            playerAName ?? playerA.Name,
            playerBName ?? playerB.Name,
            rounds,
            roundList,
            playerATotal,
            playerBTotal,
            mutualCooperationCount,
            mutualBetrayalCount,
            playerAExploitationCount,
            playerBExploitationCount);
    }

    public static (int PlayerAScore, int PlayerBScore) Score(GameMove playerA, GameMove playerB)
    {
        return (playerA, playerB) switch
        {
            (GameMove.Cooperate, GameMove.Cooperate) => (3, 3),
            (GameMove.Cooperate, GameMove.Betray) => (0, 5),
            (GameMove.Betray, GameMove.Cooperate) => (5, 0),
            _ => (1, 1)
        };
    }

    private static IReadOnlyList<RoundResult> BuildPerspectiveHistory(IReadOnlyList<MatchRound> rounds, bool playerAView)
    {
        var history = new List<RoundResult>(rounds.Count);

        foreach (var round in rounds)
        {
            if (playerAView)
            {
                history.Add(new RoundResult(
                    round.RoundNumber,
                    round.PlayerAMove,
                    round.PlayerBMove,
                    round.PlayerAScore,
                    round.PlayerBScore,
                    round.PlayerATotal,
                    round.PlayerBTotal));
            }
            else
            {
                history.Add(new RoundResult(
                    round.RoundNumber,
                    round.PlayerBMove,
                    round.PlayerAMove,
                    round.PlayerBScore,
                    round.PlayerAScore,
                    round.PlayerBTotal,
                    round.PlayerATotal));
            }
        }

        return history;
    }
}
