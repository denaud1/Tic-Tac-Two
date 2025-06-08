using System.Text.Json;
using ConsoleApp;
using GameBrain;

namespace DAL;

public class ConfigRepositoryJson : IConfigRepository
{
    // private ConfigRepositoryHardcoded _hardcodedRepo = new ConfigRepositoryHardcoded();
    private string _configFilePath = FileHelper.BasePath + FileHelper.ConfigFoulder;
    
    public List<string> GetConfigurationNames()
    {
        CheckAndCrateInitialConfig();

        return Directory
            .GetFiles(_configFilePath
                , "*" + FileHelper.ConfigExtension).ToList()
            .Select(fullFileName => Path.GetFileNameWithoutExtension(
                Path.GetFileNameWithoutExtension(fullFileName))
            )
            .ToList();
    }

    public GameConfiguration GetConfigurationByName(string name)
    {
        var configJsonStr = File.ReadAllText(_configFilePath
                                             + name 
                                             + FileHelper.ConfigExtension);
        var config = JsonSerializer.Deserialize<GameConfiguration>(configJsonStr);
        return config;
    }
    public void Save(GameConfiguration gameConfig)
    {
        CheckAndCrateInitialConfig();
        File.WriteAllText(_configFilePath + gameConfig.Name 
                                         + FileHelper.ConfigExtension, JsonSerializer.Serialize(gameConfig));
    }

    private void CheckAndCrateInitialConfig()
    {
        if (!Directory.Exists(_configFilePath))
        {
            Directory.CreateDirectory(_configFilePath);
        }
        var data =  Directory
            .GetFiles(_configFilePath, "*" + FileHelper.ConfigExtension)
            .ToList();
        if (data.Count < RepoManager.ConfigRepositoryHardcoded.GetConfigurationNames().Count)
        {
            foreach (var file in data)
            {
                File.Delete(file);
            }
            var optionNames = RepoManager.ConfigRepositoryHardcoded.GetConfigurationNames();
            foreach (var optionName in optionNames)
            {
                var gameOption = RepoManager.ConfigRepositoryHardcoded.GetConfigurationByName(optionName);
                var optionJsonStr = JsonSerializer.Serialize(gameOption);
                File.WriteAllText(_configFilePath
                                  + gameOption.Name 
                                  + FileHelper.ConfigExtension, optionJsonStr);
            }
        }
    }
}