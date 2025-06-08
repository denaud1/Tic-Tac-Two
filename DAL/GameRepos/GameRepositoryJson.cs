using System.Text.Json;
using GameBrain;

namespace DAL;

public class GameRepositoryJson : IGameRepository
{
    private string _savedGamePath = FileHelper.BasePath + FileHelper.SaveFoulder;

    public int SaveGame(string gameState, string gameConfigName)
    {
        // var stateJsonStr = System.Text.Json.JsonSerializer.Serialize(gameState);
        var fileName = FileHelper.BasePath
                       + FileHelper.SaveFoulder
                       + gameConfigName
                       + " " 
                       + DateTime.Now.ToString("dd/MM/yyyy HH-mm-ss") 
                       + FileHelper.GameExtension;
        CheckAndCreateDirectory();
        File.WriteAllText(fileName, gameState);
        return -1;
    }

    public GameState LoadGame(string name)
    {
        var savedGameJson = File.ReadAllText(_savedGamePath
                                             + name 
                                             + FileHelper.GameExtension);
        var gameState = JsonSerializer.Deserialize<GameState>(savedGameJson)!;

        return gameState;
    }

    public List<string> GetSavedGameNames()
    {
        CheckAndCreateDirectory();
        return Directory.GetFiles(_savedGamePath, "*" + FileHelper.GameExtension).ToList()
            .Select(fullFileName 
                => Path.GetFileNameWithoutExtension(Path.GetFileNameWithoutExtension(fullFileName)))
            .ToList();
    }

    private void CheckAndCreateDirectory()
    {
        if (!Directory.Exists(FileHelper.BasePath + FileHelper.SaveFoulder))
        {
            Directory.CreateDirectory(_savedGamePath);
        }
    }
}