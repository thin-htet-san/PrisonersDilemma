using PrisonersDilemma.Models;

namespace PrisonersDilemma.Services;

public sealed class TournamentService(GameEngine engine, StrategyCatalog catalog)
{
    public TournamentResult RunTournament(GameSettings settings)
    {
        var strategies = catalog.All;
        var standings = strategies.ToDictionary(
            strategy => strategy.Id,
            strategy => new StandingAccumulator(strategy));
        var matches = new List<TournamentMatchResult>();

        var matchIndex = 0;

        for (var i = 0; i < strategies.Count; i++)
        {
            for (var j = i + 1; j < strategies.Count; j++)
            {
                var strategyA = strategies[i];
                var strategyB = strategies[j];
                var pairSeed = settings.Seed + matchIndex * 97;
                var match = engine.PlayMatch(
                    strategyA.CreateStrategy(pairSeed + 1),
                    strategyB.CreateStrategy(pairSeed + 2),
                    settings.TournamentRounds,
                    strategyA.Name,
                    strategyB.Name);

                matches.Add(new TournamentMatchResult(strategyA.Name, strategyB.Name, match));
                standings[strategyA.Id].Record(match.PlayerATotal, match.PlayerACooperationCount, match.Rounds.Count, match.PlayerATotal == match.PlayerBTotal, match.PlayerATotal > match.PlayerBTotal);
                standings[strategyB.Id].Record(match.PlayerBTotal, match.PlayerBCooperationCount, match.Rounds.Count, match.PlayerATotal == match.PlayerBTotal, match.PlayerBTotal > match.PlayerATotal);
                matchIndex++;
            }
        }

        var rankedStandings = standings.Values
            .Select(accumulator => accumulator.ToStanding())
            .OrderByDescending(standing => standing.TotalScore)
            .ThenByDescending(standing => standing.Wins)
            .ThenByDescending(standing => standing.CooperationRate)
            .ThenBy(standing => standing.StrategyName)
            .ToList();

        return new TournamentResult(rankedStandings, matches, settings.TournamentRounds);
    }

    private sealed class StandingAccumulator
    {
        private readonly StrategyDefinition _strategy;
        private int _matchesPlayed;
        private int _wins;
        private int _losses;
        private int _ties;
        private int _totalScore;
        private int _cooperationMoves;
        private int _totalMoves;

        public StandingAccumulator(StrategyDefinition strategy)
        {
            _strategy = strategy;
        }

        public void Record(int score, int cooperationMoves, int totalMoves, bool tie, bool win)
        {
            _matchesPlayed++;
            _totalScore += score;
            _cooperationMoves += cooperationMoves;
            _totalMoves += totalMoves;

            if (tie)
            {
                _ties++;
            }
            else if (win)
            {
                _wins++;
            }
            else
            {
                _losses++;
            }
        }

        public TournamentStanding ToStanding()
            => new(
                _strategy.Id,
                _strategy.Name,
                _matchesPlayed,
                _wins,
                _losses,
                _ties,
                _totalScore,
                _cooperationMoves,
                _totalMoves);
    }
}
