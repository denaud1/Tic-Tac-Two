namespace GameBrain;

public class GameState
{
    public Player? PlayerX { get; set; } = default!;
    public Player? PlayerO { get; set; } = default!;
    
    public BoardArea[][] GameBoard { get; set; }
    public EGamePiece NextMoveBy {get; set;} = EGamePiece.X;
    public GameConfiguration GameConfiguration { get; set; }
    public Dictionary<string, Player> PlayerBySymbol { get; set; } = new Dictionary<string, Player>();

    public GameState(GameConfiguration gameConfiguration)
    {
        GameConfiguration = gameConfiguration;
        GameBoard = new BoardArea[GameConfiguration.BoardSizeWidth][];
        InitializeBigBoard();
        InitializeMiniBoard();

    }

    private void InitializeBigBoard()
    {
        for (var i = 0; i < GameBoard.Length; i++)
        {
            GameBoard[i] = new BoardArea[GameConfiguration.BoardSizeHeight];
            for (var j = 0; j < GameBoard[i].Length; j++)
            {
                GameBoard[i][j] = new BoardArea(EBoardAreaType.BigBoard
                    , EGamePiece.Empty
                    , new Coordinates(i, j));
            }
        }
    }
    private void InitializeMiniBoard()
    {
        var centerX = GameConfiguration.BoardSizeWidth / 2;
        var centerY = GameConfiguration.BoardSizeHeight / 2;

        for (var i = centerX - 1; i <= centerX + 1; i++)
        {
            for (var j = centerY - 1; j <= centerY + 1; j++)
            {
                GameBoard[i][j].AreaType = EBoardAreaType.MiniBoard;
            }
        }
    }
    
    public override string ToString()
    {
        return System.Text.Json.JsonSerializer.Serialize(this);
    }
}