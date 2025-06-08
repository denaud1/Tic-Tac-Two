using ConsoleApp;
using GameBrain;

namespace DAL;

public class ConfigRepository : IConfigRepository
{
    public List<string> GetConfigurationNames()
    {
        if (RepoManager.ConfigType == EConfigType.Json)
        {
            return RepoManager.ConfigRepositoryJson.GetConfigurationNames();
        }
        else
        {
            return RepoManager.ConfigRepositoryDb.GetConfigurationNames();
        }
    }

    public GameConfiguration GetConfigurationByName(string name)
    {
        if (RepoManager.ConfigType == EConfigType.Json)
        {
            return RepoManager.ConfigRepositoryJson.GetConfigurationByName(name);
        }
        else
        {
            return RepoManager.ConfigRepositoryDb.GetConfigurationByName(name);
        }
    }

    public void Save(GameConfiguration gameConfig)
    {
        if (RepoManager.ConfigType == EConfigType.Json)
        {
            RepoManager.ConfigRepositoryJson.Save(gameConfig);
        }
        else
        {
            RepoManager.ConfigRepositoryDb.Save(gameConfig);
        }
    }
}