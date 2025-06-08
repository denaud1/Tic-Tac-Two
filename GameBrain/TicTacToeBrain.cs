using System.Linq.Expressions;
using Microsoft.VisualBasic.CompilerServices;

namespace GameBrain;


public class TicTacToeBrain(GameConfiguration gameConfiguration)
{
    private GameState _gameState = new GameState(gameConfiguration);
    public EGamePiece Winner { get; set; } = EGamePiece.Empty;

    public void SetGameState(GameState gameState)
    {
        _gameState = gameState;
    }
    public string GetConfigName()
    {
        return _gameState.GameConfiguration.Name;
    }

    public string CurrentMoveBy()
    {
        return _gameState.NextMoveBy.ToString();
    }

    public EGamePiece GetCurrentMovePiece()
    {
        return _gameState.NextMoveBy;
    }

    public BoardArea[][] GameBoard
    {
        get => GetBoard();
        private set => _gameState.GameBoard = value;
    }

    public BoardArea[][] MiniBoard
    {
        get => GetMiniBoard();
        private set => _gameState.GameBoard = value;
    }
    public int DimX => _gameState.GameBoard.Length;
    public int DimY => _gameState.GameBoard[0].Length;
    private BoardArea [][] GetBoard()
    {
        var copyOfBoard = new BoardArea[_gameState.GameBoard.GetLength(0)][];
        // _gameState.GameBoard.GetLength(1)
        for (var x = 0; x < _gameState.GameBoard.Length; x++)
        {
            copyOfBoard[x] = new BoardArea[_gameState.GameBoard[x].Length];
            for (var y = 0; y < _gameState.GameBoard[x].Length; y++)
            {
                copyOfBoard[x][y] = _gameState.GameBoard[x][y];
            }
            
        }
        return copyOfBoard;
    }

    private BoardArea[][] GetMiniBoard()
    {
        var miniBoard = new BoardArea[3][];
        var corX = 0;
        var corY = 0;
        foreach (var t in GameBoard)
        {
            foreach (var t1 in t)
            {
                if (t1.AreaType == EBoardAreaType.MiniBoard)
                {
                    if (miniBoard[corX] == null)
                    {
                        miniBoard[corX] = new BoardArea[3];
                    }
                    miniBoard[corX][corY] = t1;
                    corY++;
                    if (corY == 3)
                    {
                        corY = 0;
                        corX++;
                    }
                }
            }
        }
        return miniBoard;
    }

    public (int x, int y) GetCoordinates(string input)
    {
        var inputSplit = input.Split(",");
        var x = int.Parse(inputSplit[0]);
        var y = int.Parse(inputSplit[1]);
        return (x, y);
    }

    public (int, int) GetGridCoordinates(int direction)
    {
        var directions = new List<(int xDir, int yDir)>()
        {
            (1, 0), // right
            (-1, 0), // left
            (0, 1), // down
            (0, -1), // up
            (1, 1), // down-right
            (-1, 1), // down-left
            (1, -1), // up-right
            (-1, -1) // up-left
        };
        return directions[direction - 1];
        
    }

    public bool PutPiece(int x ,int y)
    {
        if (GameBoard[x][y].AreaType != EBoardAreaType.MiniBoard)
        {
            Console.WriteLine("Move must be in the mini-board area.");
            return false;
        }
        
        if ((x >= _gameState.GameBoard.Length || x < 0)
            || (y >= _gameState.GameBoard[0].Length || y < 0))
        {
            Console.Clear();
            Console.WriteLine("Hey man! The coordinates of the board are invalid, you wanna break my board?");
            Console.WriteLine("Try again...");
            return false;
        }
        if (_gameState.GameBoard[x][y].Piece != EGamePiece.Empty)
        {
            Console.WriteLine("The area of the board is already occupied.");
            return false;
        }
        Console.Clear();
        _gameState.GameBoard[x][y].Piece = _gameState.NextMoveBy;
        return true;
    }

    public bool TakePieceOff(int x, int y)
    {
        if ((x >= _gameState.GameBoard.Length || x < 0)
            || (y >= _gameState.GameBoard[0].Length || y < 0))
        {
            Console.Clear();
            Console.WriteLine("Invalid input. Coordinates are out of board.");
            return false;
        } 
        if (_gameState.GameBoard[x][y].Piece != _gameState.NextMoveBy)
        {
            Console.WriteLine("The area of the board is already occupied.");
            return false;
        }
        GameBoard[x][y].Piece = EGamePiece.Empty;
        return true;
    }
    

    public void SetNextMovePlayer()
    {
        _gameState.NextMoveBy = _gameState.NextMoveBy == EGamePiece.X ? EGamePiece.O : EGamePiece.X;
    }

    public void ResetGame()
    {
        var newState = new GameState(gameConfiguration);
        newState.PlayerX = _gameState.PlayerX;
        newState.PlayerO = _gameState.PlayerO;
        newState.PlayerBySymbol.Add("X", newState.PlayerX);
        newState.PlayerBySymbol.Add("O", newState.PlayerO);
        Winner = EGamePiece.Empty;
        
        SetGameState(newState);
        _gameState.NextMoveBy = EGamePiece.X;
        
    }
    public Player GetCurrentPlayer()
    {
        return _gameState.PlayerBySymbol[_gameState.NextMoveBy.ToString()];
    }

    private bool ArePlayersNamedDefined()
    {
        return _gameState is { PlayerX: not null, PlayerO: not null };
    }
    
    public bool IsGameOver()
    {
        
        var boardSizeWidth = _gameState.GameConfiguration.BoardSizeWidth;
        var boardSizeHeight = _gameState.GameConfiguration.BoardSizeHeight;
        var winCondition = _gameState.GameConfiguration.WinCondition;
        var amountOfWinners = 0;
        
        for (int x = 0; x < boardSizeWidth; x++)
        {
            for (int y = 0; y < boardSizeHeight; y++)
            {
                if (GameBoard[x][y].Piece == EGamePiece.X)
                {
                    if (IsGameOver(x, y, EGamePiece.X))
                    {
                        if (EGamePiece.X != GetCurrentMovePiece())
                        {
                            SetNextMovePlayer();
                            Winner = EGamePiece.X;
                            amountOfWinners++;
                        }
                    }
                }
            }
        }
        for (int x = 0; x < boardSizeWidth; x++)
        {
            for (int y = 0; y < boardSizeHeight; y++)
            {
                if (GameBoard[x][y].Piece == EGamePiece.O)
                {
                    if (IsGameOver(x, y, EGamePiece.O))
                    {
                        if (EGamePiece.O != GetCurrentMovePiece())
                        {
                            SetNextMovePlayer();
                            Winner = EGamePiece.O;
                            amountOfWinners++;
                        }
                    }
                }
            }
        }

        if (amountOfWinners == 1)
        {
            return true;
        } 
        if (amountOfWinners == 2)
        {
            Winner = EGamePiece.Both;
            return true;
        }

        return false;
    }

    public bool IsGameOver(int x, int y, EGamePiece piece)
    {
        var moveBy = piece;
        
        var checkDirections = new List<(int xDir, int yDir)>()
        {
            (1, 0), // right
            (1, 1), // down-right
            (0, 1), // down
            (-1, 1), // down-left
            (-1, 0), // left
            (-1, -1), // up-left
            (0, -1), // up
            (1, -1) // up-right
        };

        foreach (var (xDir, yDir) in checkDirections)
        {
            var count = 0;
            for (var step = 0; step < _gameState.GameConfiguration.WinCondition; step++)
            {
                var curX = x + step * xDir;
                var curY = y + step * yDir;
                if (curX < 0 || curY < 0 || curX >= _gameState.GameConfiguration.BoardSizeWidth ||
                    curY >= _gameState.GameConfiguration.BoardSizeHeight 
                    || GameBoard[curX][curY].AreaType != EBoardAreaType.MiniBoard)
                    continue;

                if (GameBoard[curX][curY].Piece != moveBy) continue;
                count++;
            }

            if (count == _gameState.GameConfiguration.WinCondition)
            {
                Winner = moveBy;
                return true;
            }
        }

        return false;
    }

    public bool ReplaceMiniBoard(int x, int y)
    {
        if (IsMiniBoardMovementPossible(x ,y))
        {
            var board = MiniBoard;
            
            foreach (var column in board)
            {
                foreach (var area in column)
                {
                    var cordX = area.Coordinates!.X;
                    var cordY = area.Coordinates!.Y;
                    GameBoard[cordX][cordY].AreaType = EBoardAreaType.BigBoard;
                }
            }
            
            foreach (var column in board)
            {
                foreach (var area in column)
                {
                    var cordX = area.Coordinates!.X;
                    var cordY = area.Coordinates!.Y;
                    GameBoard[cordX + x][cordY + y].AreaType = EBoardAreaType.MiniBoard;
                }
            }

            return true;
        }
        
        return false;
    }

    private bool IsMiniBoardMovementPossible(int x, int y)
    {
        var board = GetMiniBoard();
        var maxSize = gameConfiguration.WinCondition - 1;

        var boardCorners = new List<(int dirX, int dirY)>()
        {
            (0, 0), // upper left
            (maxSize, maxSize), // lower right
            (0, maxSize), // lower left
            (maxSize, 0) // upper right
        };

        foreach (var (dirX, dirY) in boardCorners)
        {
            var moveInX = board[dirX][dirY].Coordinates!.X + x;
            var moveInY = board[dirX][dirY].Coordinates!.Y + y;
            if (moveInX < 0 || moveInY < 0 || moveInX >= DimX || moveInY >= DimY)
            {
                return false;
            }
        }
        return true;
    }

    public bool IsPlaceForMove()
    {
        for (var i = 0; i < DimX; i++)
        {
            for (var j = 0; j < DimY; j++)
            {
                if (_gameState.GameBoard[i][j].Piece == EGamePiece.Empty
                    && _gameState.GameBoard[i][j].AreaType == EBoardAreaType.MiniBoard)
                {
                    return true;
                }
            }
        }

        return false;
    }

    public void SetPlayerName(EGamePiece piece, string playerName)
    {
        var name = playerName;
        if (piece == EGamePiece.X)
        {
            if (string.IsNullOrEmpty(name))
            {
                name = "Player 1";
            }
            _gameState.PlayerX = new Player(playerName, piece);
            _gameState.PlayerBySymbol.Add(piece.ToString(), _gameState.PlayerX);
        } 
        else if (piece == EGamePiece.O)
        {
            if (string.IsNullOrEmpty(name))
            {
                name = "Player 2";
            }
            _gameState.PlayerO = new Player(playerName, piece);
            _gameState.PlayerBySymbol.Add(piece.ToString(), _gameState.PlayerO);
        }
    }
    public void AskForPlayersNames()
    {
        do
        {
            lock (Synchronizer.LockObject)
            {
                if (_gameState.PlayerX == null)
                {
                    Console.WriteLine("CHOOSE PLAYERS NAMES");
                    Console.WriteLine("============================");
                    Console.WriteLine("Please enter a player 1 name");
                    Console.Write(">");
                    var input = Console.ReadLine();
                    if (input is null || input.Length == 0)
                    {
                        Console.Clear();
                        Console.WriteLine("CHOOSE PLAYERS NAMES");
                        Console.WriteLine("============================");
                        SetPlayerName(EGamePiece.X, "Player 1");
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("CHOOSE PLAYERS NAMES");
                        Console.WriteLine("============================");
                        SetPlayerName(EGamePiece.X, input);
                        Console.WriteLine();
                    }
                }
                if (_gameState.PlayerO == null && _gameState.PlayerX != null)
                {
                    Console.Clear();
                    Console.WriteLine("CHOOSE PLAYERS NAMES");
                    Console.WriteLine("============================");
                    Console.WriteLine("Please enter a player 2 name");
                    Console.Write(">");
                    var input = Console.ReadLine();
                    if (input is null || input.Length == 0)
                    {
                        Console.Clear();
                        Console.WriteLine("CHOOSE PLAYERS NAMES");
                        Console.WriteLine("============================");
                        SetPlayerName(EGamePiece.O, "Player 2");
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("CHOOSE PLAYERS NAMES");
                        Console.WriteLine("============================");
                        SetPlayerName(EGamePiece.O, input);
                        Console.WriteLine();
                    }
                
                }
                Console.WriteLine();
            }
        } while (!ArePlayersNamedDefined());
    }

    public string GetGameStateJson()
    {
        return _gameState.ToString();
    }
}