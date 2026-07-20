using PrisonersDilemma.Models;
using PrisonersDilemma.Services;
using PrisonersDilemma.Strategies;

namespace PrisonersDilemma.Tests;

public class StrategyTests
{
    [Fact]
    public void TitForTat_Starts_With_Cooperation_And_Then_Mirrors()
    {
        var strategy = new TitForTatStrategy();
        var emptyHistory = Array.Empty<RoundResult>();
        var history = new[]
        {
            new RoundResult(1, GameMove.Cooperate, GameMove.Betray, 0, 5, 0, 5)
        };

        Assert.Equal(GameMove.Cooperate, strategy.ChooseMove(emptyHistory));
        Assert.Equal(GameMove.Betray, strategy.ChooseMove(history));
    }

    [Fact]
    public void GrimTrigger_Defects_After_The_First_Betrayal()
    {
        var strategy = new GrimTriggerStrategy();

        Assert.Equal(GameMove.Cooperate, strategy.ChooseMove(Array.Empty<RoundResult>()));
        Assert.Equal(
            GameMove.Betray,
            strategy.ChooseMove(new[]
            {
                new RoundResult(1, GameMove.Cooperate, GameMove.Betray, 0, 5, 0, 5)
            }));
    }

    [Fact]
    public void RandomStrategy_Uses_Seeded_Sequence()
    {
        var strategy = new RandomStrategy(1234);
        var moves = Enumerable.Range(0, 4).Select(_ => strategy.ChooseMove(Array.Empty<RoundResult>())).ToArray();

        Assert.Equal(new[] { GameMove.Cooperate, GameMove.Betray, GameMove.Cooperate, GameMove.Betray }, moves);
    }
}
