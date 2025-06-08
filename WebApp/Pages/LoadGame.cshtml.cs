using DAL;
using Domain;
using GameBrain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages;

public class LoadGame : PageModel
{
    private readonly GameRepository _gameRepository;

    public LoadGame(GameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }

    [BindProperty] public string Error { get; set; } = default!;
    [BindProperty] public int Code { get; set; } = default!;
    [BindProperty] public bool CodeInserted { get; set; } = default!;
    public TicTacToeBrain GameInstance { get; set; } = default!;
    public IActionResult OnPost()
    {
        if (CodeInserted)
        {
            if (_gameRepository.DoesGameExist(Code))
            {
                var game = _gameRepository.LoadGame(Code);
                GameInstance = new TicTacToeBrain(game.GameConfiguration);
                
                return RedirectToPage("./PlayGame", 
                    new { playerName1 = game.PlayerX!.PlayerName
                        , playerName2 = game.PlayerO!.PlayerName
                        , ConfigName = game.GameConfiguration.Name, GameId = Code, LoadGame = true});
            }
        }
        Error = "Game not found";
        return Page();
    }
}