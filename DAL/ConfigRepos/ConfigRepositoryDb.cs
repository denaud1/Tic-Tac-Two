using ConsoleApp;
using Domain;
using GameBrain;

namespace DAL;

public class ConfigRepositoryDb
{
    private DAL.AppDbContext _context;

    public ConfigRepositoryDb(AppDbContext context)
    {
        _context = context;
    }
    public void Save(GameConfiguration gameConfig)
    {
        var config = new Configuration()
        {
            Name = gameConfig.Name,
            // ConfigJson = gameConfig.ToString()
            BoardSizeHeight = gameConfig.BoardSizeHeight,
            BoardSizeWidth = gameConfig.BoardSizeWidth,
            WinCondition = gameConfig.WinCondition,
            AmountOfPieces = gameConfig.AmountOfPieces
        };
        _context.Configurations.Add(config);
        _context.SaveChanges();
    }
    public List<string> GetConfigurationNames()
    {
        if (!_context.Configurations.Any())
        {
            foreach (var config in RepoManager.ConfigRepositoryHardcoded.GetConfigurations())
            {
                Save(config);
            }
        }
        return _context.Configurations.Select(x => x.Name).ToList()!;
    }

    public GameConfiguration GetConfigurationByName(string name)
    {
        var config = _context.Configurations.First(x => x.Name == name);
        return new GameConfiguration()
        {
            Name = config.Name,
            BoardSizeHeight = config.BoardSizeHeight,
            BoardSizeWidth = config.BoardSizeWidth,
            WinCondition = config.WinCondition,
            AmountOfPieces = config.AmountOfPieces
        };
            
        // return System.Text.Json.JsonSerializer
        //     .Deserialize<GameConfiguration>(_context.Configurations.First(x => x.Name == name).ConfigJson!);
    }
    
}