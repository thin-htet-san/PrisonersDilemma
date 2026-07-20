namespace PrisonersDilemma.Models;

public sealed record MatchRound(
    int RoundNumber,
    GameMove PlayerAMove,
    GameMove PlayerBMove,
    int PlayerAScore,
    int PlayerBScore,
    int PlayerATotal,
    int PlayerBTotal)
{
    public bool IsMutualCooperation => PlayerAMove == GameMove.Cooperate && PlayerBMove == GameMove.Cooperate;

    public bool IsMutualBetrayal => PlayerAMove == GameMove.Betray && PlayerBMove == GameMove.Betray;

    public bool PlayerAExploitedPlayerB => PlayerAMove == GameMove.Betray && PlayerBMove == GameMove.Cooperate;

    public bool PlayerBExploitedPlayerA => PlayerAMove == GameMove.Cooperate && PlayerBMove == GameMove.Betray;
}
