using System.ComponentModel;
using DAL;
using GameBrain;

namespace ConsoleApp;

public static class GameController
{
    private static TicTacToeBrain? _gameInstance = default!;
    private static bool _gameInstanceUsed = false;
    public static GameConfiguration Config { get; set; } = default!;
    
    public static string MainLoop()
    {

        Console.Clear();
        if (_gameInstanceUsed || _gameInstance == null)
        {
            
            GameConfigsMenu.Run();
        
            //Initialize engine
        
            _gameInstance = new TicTacToeBrain(Config);
        
            Console.Clear();
            _gameInstance.AskForPlayersNames();

            Console.Clear();
        }
        _gameInstanceUsed = true;
        
        // Coordinates for board
        var cursorLeft = Console.CursorLeft;
        var cursorTop = Console.CursorTop;
        
        do
        {
            Console.SetCursorPosition(cursorLeft, cursorTop);

            
            Console.Clear();
            Console.WriteLine("THE GAME IS STARTED!");
            Console.WriteLine();
            ConsoleUI.Visualizer.DrawBoard(_gameInstance);

            var currentPlayer = _gameInstance.GetCurrentPlayer();
            
            GameMenuPrinter.Print(currentPlayer, _gameInstance);

            GameStatus.StartProcess();
            Message.GetErrorIfExists();
            Message.RequestCoordinates(currentPlayer.PlayerName);
            
            
            var input = Console.ReadLine()!;

            var inputType = MoveValidator.GetMoveType(_gameInstance, input);
            if (inputType.Equals(EMoveType.InvalidInput))
            {
                GameStatus.MoveSuccess = false;
            }
            
            //Action with input.

            if (inputType.Equals(EMoveType.Exit))
            {
                Console.Clear();
                GameStatus.EndGameAnyWay();
                return "M";
            }

            if (inputType.Equals(EMoveType.Web))
            {
                if (RepoManager.ConfigType == EConfigType.Database)
                {
                    var gameId = RepoManager.GameRepository
                        .SaveGame(_gameInstance.GetGameStateJson(), Config.Name);
                    var url = "http://localhost:5194/LoadGame";
                    Message.AddMessage($"Insert the code: {gameId} on the website: {url}");
                }
            }
            if (GameStatus.CurrentMoveType.Equals(EMoveType.Continue))
            {
                _gameInstance.ResetGame();
                GameStatus.EndMove();
                Console.Clear();
            }
            if (inputType.Equals(EMoveType.Save))
            {
                RepoManager.GameRepository.SaveGame(
                    _gameInstance.GetGameStateJson(), 
                    _gameInstance.GetConfigName()
                );
            }
            // General move.
            if (GameStatus.CurrentMoveType.Equals(EMoveType.Empty) && inputType.Equals(EMoveType.Coordinates))
            {
                var (x, y) = _gameInstance.GetCoordinates(input);
                GameStatus.MoveSuccess = _gameInstance.PutPiece(x ,y);
                _gameInstance.GetCurrentPlayer().PiecesOnBoard++;
                GameStatus.EndMove();

                if (_gameInstance.IsGameOver(x, y, _gameInstance.GetCurrentMovePiece()))
                {
                    Console.Clear();
                    GameStatus.StartNewCycle(EMoveType.Continue);
                    GameStatus.StartProcess();
                    
                }
            }
            // Grid replace.
            if ((inputType.Equals(EMoveType.GridMove) || GameStatus.CurrentMoveType.Equals(EMoveType.GridMove)) 
                && !inputType.Equals(EMoveType.InvalidInput))
            {
                if (GameStatus.MoveCycles == 0)
                {
                    GameStatus.StartNewCycle(inputType);
                }
                else if (GameStatus.MoveCycles == 1)
                {
                    var (x, y) = _gameInstance.GetGridCoordinates(int.Parse(input));
                    GameStatus.MoveSuccess = _gameInstance.ReplaceMiniBoard(x, y);
                    if (!GameStatus.MoveSuccess)
                    {
                        Message.AddMessage("Can't move board in this way.");
                    }
                    GameStatus.EndMove();
                    if (_gameInstance.IsGameOver())
                    {
                        Console.Clear();
                        GameStatus.StartNewCycle(EMoveType.Continue);
                        GameStatus.StartProcess();
                    
                    }
                }
            }
            // Piece replace.
            if (inputType.Equals(EMoveType.PieceMove) || GameStatus.CurrentMoveType.Equals(EMoveType.PieceMove))
            {
                if (GameStatus.MoveCycles == 0)
                {
                    GameStatus.StartNewCycle(inputType);
                }

                else if (GameStatus.MoveCycles == 1)
                {
                    var (x, y) = _gameInstance.GetCoordinates(input);
                    GameStatus.MoveSuccess = _gameInstance.TakePieceOff(x, y);
                    GameStatus.StartNewCycle(GameStatus.CurrentMoveType);
                    _gameInstance.GetCurrentPlayer().PiecesOnBoard--;
                }
                else if (GameStatus.MoveCycles == 2)
                {
                    var (x, y) = _gameInstance.GetCoordinates(input);
                    GameStatus.MoveSuccess = _gameInstance.PutPiece(x, y);
                    _gameInstance.GetCurrentPlayer().PiecesOnBoard++;
                    GameStatus.EndMove();
                    if (_gameInstance.IsGameOver(x, y, _gameInstance.GetCurrentMovePiece()))
                    {
                        Console.Clear();
                        GameStatus.StartNewCycle(EMoveType.Continue);
                        GameStatus.StartProcess();
                    
                    }
                }
            }
            
                
            //End of work with input.

            if (!_gameInstance.IsPlaceForMove())
            {
                
                Console.Clear();
                ConsoleUI.Visualizer.DrawBoard(_gameInstance);
                Console.WriteLine("Draw! There is no longer a move!");
                
                return "";
            }

            if (!GameStatus.MoveInProcess)
            {
                _gameInstance.SetNextMovePlayer();
            }

            lock (Synchronizer.LockObject)
            {
                Console.SetCursorPosition(cursorLeft, cursorTop); 
            }

        } while (true);
    }

    public static void CreateExistingGameInstance(GameState gameState)
    {
        _gameInstance = new TicTacToeBrain(gameState.GameConfiguration);
        _gameInstance.SetGameState(gameState);
        _gameInstanceUsed = false;
    }
}