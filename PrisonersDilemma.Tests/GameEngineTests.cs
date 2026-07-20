using PrisonersDilemma.Models;
using PrisonersDilemma.Services;
using PrisonersDilemma.Strategies;

namespace PrisonersDilemma.Tests;

public class GameEngineTests
{
    private readonly GameEngine engine = new();

    [Theory]
    [InlineData(GameMove.Cooperate, GameMove.Cooperate, 3, 3)]
    [InlineData(GameMove.Cooperate, GameMove.Betray, 0, 5)]
    [InlineData(GameMove.Betray, GameMove.Cooperate, 5, 0)]
    [InlineData(GameMove.Betray, GameMove.Betray, 1, 1)]
    public void Score_Maps_Prisoners_Dilemma_Payoff_Correctly(
        GameMove playerA,
        GameMove playerB,
        int expectedA,
        int expectedB)
    {
        var (playerAScore, playerBScore) = GameEngine.Score(playerA, playerB);

        Assert.Equal(expectedA, playerAScore);
        Assert.Equal(expectedB, playerBScore);
    }

    [Fact]
    public void PlayMatch_Returns_Expected_Counts_For_TitForTat_Vs_AlwaysBetray()
    {
        var result = engine.PlayMatch(new TitForTatStrategy(), new AlwaysBetrayStrategy(), 4);

        Assert.Equal(4, result.RoundsPlayed);
        Assert.Equal(0, result.MutualCooperationCount);
        Assert.Equal(0, result.PlayerAExploitationCount);
        Assert.Equal(1, result.PlayerBExploitationCount);
        Assert.Equal(3, result.PlayerATotal);
        Assert.Equal(8, result.PlayerBTotal);
        Assert.Equal("Always Betray", result.WinnerLabel);
    }
}
