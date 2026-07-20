namespace PrisonersDilemma.Models;

public sealed record RoundResult(
    int RoundNumber,
    GameMove MyMove,
    GameMove OpponentMove,
    int MyScore,
    int OpponentScore,
    int MyTotal,
    int OpponentTotal)
{
    public bool IsMutualCooperation => MyMove == GameMove.Cooperate && OpponentMove == GameMove.Cooperate;

    public bool IsMutualBetrayal => MyMove == GameMove.Betray && OpponentMove == GameMove.Betray;

    public bool IExploitedOpponent => MyMove == GameMove.Betray && OpponentMove == GameMove.Cooperate;

    public bool OpponentExploitedMe => MyMove == GameMove.Cooperate && OpponentMove == GameMove.Betray;
}
