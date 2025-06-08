namespace GameBrain;

public class Player
{
    public string PlayerName { get; set; } = default!;
    public EGamePiece Piece { get; set; } = default!;
    public int AmountOfPieces { get; set; } = 4;
    public int PiecesOnBoard { get; set; } = 0;

    public Player(string playerName, EGamePiece piece)
    {
        PlayerName = playerName;
        Piece = piece;
    }

    public override string ToString()
    {
        return PlayerName;
    }
}