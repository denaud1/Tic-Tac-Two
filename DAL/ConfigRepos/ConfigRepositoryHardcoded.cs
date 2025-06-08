using GameBrain;

namespace DAL;

public class ConfigRepositoryHardcoded : IConfigRepository
{
    private List<GameConfiguration> _gameConfiguration = new List<GameConfiguration>()
    {
        new GameConfiguration()
        {
            Name = "Classical board",
            BoardSizeWidth = 5,
            BoardSizeHeight = 5
        },
        new GameConfiguration()
        {
            Name = "Lite board"
        },
        new GameConfiguration()
        {
            Name = "Big board",
            BoardSizeWidth = 15,
            BoardSizeHeight = 15
            
        }
    };
    public void Save(GameConfiguration gameConfiguration)
    {
        _gameConfiguration.Add(gameConfiguration);
    }
    public List<string> GetConfigurationNames()
    {
        return _gameConfiguration
            .OrderBy(x => x.Name)
            .Select(config => config.Name)
            .ToList();
    }

    public GameConfiguration GetConfigurationByName(string name)
    {
        return _gameConfiguration.Single(c => c.Name == name);
    }

    public List<GameConfiguration> GetConfigurations()
    {
        return _gameConfiguration;
    }

}