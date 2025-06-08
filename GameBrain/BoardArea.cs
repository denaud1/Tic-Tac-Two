namespace GameBrain;

public class BoardArea
{
    public EBoardAreaType AreaType { get; set; } = default!;
    public EGamePiece Piece { get; set; } = default!;
    public Coordinates? Coordinates { get; set; } = default!;

    public BoardArea(EBoardAreaType areaType, EGamePiece piece, Coordinates coordinates)
    {
        AreaType = areaType;
        Piece = piece;
        Coordinates = coordinates;
    }
}