using DAL;
using GameBrain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Pages;

public class NewGameCreation : PageModel
{
    private readonly ConfigRepository _configRepository;

    public NewGameCreation(ConfigRepository configRepository)
    {
        _configRepository = configRepository;
    }

    public SelectList ConfigSelectList { get; set; } = default!;
    public string? Error { get; set; }

    [BindProperty]
    public string? ConfigName { get; set; }
    [BindProperty]
    public string PlayerName1 { get; set; } = default!;
    [BindProperty]
    public string PlayerName2 { get; set; } = default!;
    
    public IActionResult OnGet()
    {
        var selectListData = _configRepository.GetConfigurationNames()
            .Select(name => new {id = name, value = name})
            .ToList();
        
        ConfigSelectList = new SelectList(selectListData, "id", "value");
        
        return Page();
        
    }
    public IActionResult OnPost()
    {
        if (ConfigName == null)
        {
            Error = "Please select a config";
            return Page();
        }
        if (string.IsNullOrWhiteSpace(PlayerName1))
        {
            PlayerName1 = "Player 1";
        }
        if (string.IsNullOrWhiteSpace(PlayerName2))
        {
            PlayerName2 = "Player 2";
        }
        return RedirectToPage("./PlayGame"
            , new { playerName1 = PlayerName1, playerName2 = PlayerName2, ConfigName = ConfigName });
    }
}