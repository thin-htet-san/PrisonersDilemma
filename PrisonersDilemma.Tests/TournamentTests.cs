using PrisonersDilemma.Models;
using PrisonersDilemma.Services;

namespace PrisonersDilemma.Tests;

public class TournamentTests
{
    [Fact]
    public void Tournament_Ranking_Is_Stable_For_A_Fixed_Seed()
    {
        var service = new TournamentService(new GameEngine(), new StrategyCatalog());
        var result = service.RunTournament(new GameSettings(TournamentRounds: 12, Seed: 99));

        Assert.Equal(8, result.Standings.Count);
        Assert.Equal("Grim Trigger", result.Standings[0].StrategyName);
        Assert.True(result.Standings[0].TotalScore >= result.Standings[^1].TotalScore);
        Assert.Equal(28, result.Matches.Count);
    }
}
