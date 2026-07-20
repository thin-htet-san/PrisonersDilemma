using PrisonersDilemma.Models;
using PrisonersDilemma.Strategies;

namespace PrisonersDilemma.Services;

public sealed class StrategyCatalog
{
    private readonly IReadOnlyList<StrategyDefinition> _strategies;

    public StrategyCatalog()
    {
        _strategies = new List<StrategyDefinition>
        {
            new(
                "cooperate",
                "Always Cooperate",
                "A soft opening strategy that never defects.",
                "Friendly",
                StrategyDifficulty.Easy,
                1,
                5,
                1,
                _ => new AlwaysCooperateStrategy()),
            new(
                "betray",
                "Always Betray",
                "A hardline strategy that defects every round.",
                "Hardline",
                StrategyDifficulty.Easy,
                5,
                1,
                1,
                _ => new AlwaysBetrayStrategy()),
            new(
                "tit-for-tat",
                "Tit for Tat",
                "Starts with cooperation and then mirrors the opponent.",
                "Mirror",
                StrategyDifficulty.Easy,
                3,
                3,
                2,
                _ => new TitForTatStrategy()),
            new(
                "grim-trigger",
                "Grim Trigger",
                "Forgives nothing after the first betrayal.",
                "Unforgiving",
                StrategyDifficulty.Medium,
                5,
                1,
                2,
                _ => new GrimTriggerStrategy()),
            new(
                "random",
                "Random",
                "Chooses moves with no pattern and no memory.",
                "Chaotic",
                StrategyDifficulty.Easy,
                3,
                3,
                2,
                seed => new RandomStrategy(seed)),
            new(
                "pavlov",
                "Pavlov",
                "Stays with a move that earned a strong result.",
                "Adaptive",
                StrategyDifficulty.Hard,
                3,
                3,
                3,
                _ => new PavlovStrategy()),
            new(
                "adaptive-learner",
                "Adaptive Learner",
                "Shifts its style based on recent outcomes and opponent tendency.",
                "Analyst",
                StrategyDifficulty.Hard,
                3,
                3,
                4,
                _ => new AdaptiveLearnerStrategy()),
            new(
                "pattern-hunter",
                "Pattern Hunter",
                "Looks for repeating opponent sequences and counters them.",
                "Analyst",
                StrategyDifficulty.Hard,
                4,
                2,
                4,
                _ => new PatternHunterStrategy())
        };
    }

    public IReadOnlyList<StrategyDefinition> All => _strategies;

    public StrategyDefinition GetById(string id) => _strategies.First(strategy => strategy.Id == id);

    public IGameStrategy CreateStrategy(string id, int seed) => GetById(id).CreateStrategy(seed);

    public StrategyDefinition Default => _strategies[2];
}
