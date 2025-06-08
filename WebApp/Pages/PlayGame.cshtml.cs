using System.Data;
using DAL;
using GameBrain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SQLitePCL;

namespace WebApp.Pages;

public class PlayGame : PageModel
{
    private readonly ConfigRepository _configRepository;
    private readonly GameRepository _gameRepository;

    public PlayGame(ConfigRepository configRepository, GameRepository gameRepository)
    {
        _configRepository = configRepository;
        _gameRepository = gameRepository;
    }
    
    [BindProperty(SupportsGet = true)]
    public string? ConfigName { get; set; }
    [BindProperty(SupportsGet = true)]
    public string PlayerName1 { get; set; } = default!;
    [BindProperty(SupportsGet = true)]
    public string PlayerName2 { get; set; } = default!;
    
    public string WinAlert { get; set; } = default!;

    [BindProperty(SupportsGet = true)] public int GameId { get; set; } = default!;
    [BindProperty] public int X { get; set; } = default!;
    [BindProperty] public int Y { get; set; } = default!;
    [BindProperty] public EMoveType MoveType { get; set; } = default!;
    

    [BindProperty] public EGamePiece NextMoveBy { get; set; } = default!;
    [BindProperty] public bool GameOver { get; set; } = default!;
    [BindProperty] public int Continue { get; set; } = 0;

    private static List<SelectListItem> _options = new List<SelectListItem>()
    {
        new SelectListItem { Value = "0", Text = "Right" },
        new SelectListItem { Value = "1", Text = "Left" },
        new SelectListItem { Value = "2", Text = "Down" },
        new SelectListItem { Value = "3", Text = "Up" },
        new SelectListItem { Value = "4", Text = "Down-Right" },
        new SelectListItem { Value = "5", Text = "Down-Left" },
        new SelectListItem { Value = "6", Text = "Up-Right" },
        new SelectListItem { Value = "7", Text = "Up-Left" }
    };

    public SelectList GridMoveOptions { get; set; } = new SelectList(_options, "Value", "Text");

    [BindProperty]
    public string GridMoveChoice { get; set; } = default!;
    [BindProperty(SupportsGet = true)]
    public bool LoadGame { get; set; } = false;

    public TicTacToeBrain TicTacToeBrain { get; set; } = default!;

    public IActionResult OnPost()
    {
        if (!GameOver)
        {
            var dbGame = _gameRepository.LoadGame(GameId);
            TicTacToeBrain = new TicTacToeBrain(dbGame.GameConfiguration);
            TicTacToeBrain.SetGameState(dbGame);
            var moveSuccessed = false;
            if (MoveType == EMoveType.GridMove)
            {
                var (x, y) = TicTacToeBrain.GetGridCoordinates(int.Parse(GridMoveChoice) + 1);
                moveSuccessed = TicTacToeBrain.ReplaceMiniBoard(x, y);
            }
            else if (MoveType == EMoveType.Coordinates)
            {
                moveSuccessed = TicTacToeBrain.PutPiece(X, Y);
                TicTacToeBrain.GetCurrentPlayer().PiecesOnBoard++;
                MoveType = EMoveType.Empty;
            }
            else if (MoveType == EMoveType.PieceMove)
            {
                TicTacToeBrain.TakePieceOff(X, Y);
                TicTacToeBrain.GetCurrentPlayer().PiecesOnBoard--;
                MoveType = EMoveType.Coordinates;
            }
            else if(TicTacToeBrain.GetCurrentPlayer().PiecesOnBoard 
                    < TicTacToeBrain.GetCurrentPlayer().AmountOfPieces)
            {
                moveSuccessed = TicTacToeBrain.PutPiece(X, Y);
            }
            
            if (TicTacToeBrain.IsGameOver(X, Y, TicTacToeBrain.GetCurrentMovePiece()))
            {
                GameOver = true;
                WinAlert = $"{TicTacToeBrain.CurrentMoveBy()} is winner!";
            }
            else if (!TicTacToeBrain.IsPlaceForMove())
            {
                GameOver = true;
                WinAlert = "Draw!";
            }
            if (moveSuccessed)
            {
                TicTacToeBrain.GetCurrentPlayer().PiecesOnBoard++;
                TicTacToeBrain.SetNextMovePlayer();
            }

            if (!GameOver)
            {
                var currentId = GameId;
                GameId = _gameRepository.SaveGame(TicTacToeBrain.GetGameStateJson(),TicTacToeBrain.GetConfigName());
                _gameRepository.DeleteGame(currentId);
            }
        }
        else
        {
            if (Continue == 1)
            {
                GameOver = false;
                var game = _gameRepository.LoadGame(GameId);
                _gameRepository.DeleteGame(GameId);
                return RedirectToPage("./PlayGame"
                    , new { playerName1 = game.PlayerX!.PlayerName
                        , playerName2 = game.PlayerO!.PlayerName, ConfigName = game.GameConfiguration.Name });
            }
            else
            {
                return RedirectToPage("./Home");
            }
        }
        
        // GameOver = false;
        return Page();
    }

    public void OnGet()
    {
        var config = _configRepository.GetConfigurationByName(ConfigName!);
        TicTacToeBrain = new TicTacToeBrain(config);
        if (LoadGame)
        {
            var gameInstance = _gameRepository.LoadGame(GameId);
            TicTacToeBrain.SetGameState(gameInstance);
        }
        else
        {
            TicTacToeBrain.SetPlayerName(EGamePiece.X, PlayerName1);
            TicTacToeBrain.SetPlayerName(EGamePiece.O, PlayerName2);
        }
        GameId = _gameRepository.SaveGame(TicTacToeBrain.GetGameStateJson(),TicTacToeBrain.GetConfigName());
    }
    
    
}