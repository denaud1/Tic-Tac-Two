using ConsoleApp;
using GameBrain;

namespace DAL;

public class GameRepository : IGameRepository
{
    public int SaveGame(string gameState, string gameConfigName)
    {
        var id = -1;
        if (RepoManager.ConfigType == EConfigType.Json)
        {
            RepoManager.GameRepositoryJson.SaveGame(gameState, gameConfigName);
        }
        else
        {
            id = RepoManager.GameRepositoryDb.SaveGame(gameState, gameConfigName);
        }

        return id;
    }

    public GameState LoadGame(string gameConfigName)
    {
        if (RepoManager.ConfigType == EConfigType.Json)
        {
            return RepoManager.GameRepositoryJson.LoadGame(gameConfigName);
        }
        else
        {
            return RepoManager.GameRepositoryDb.LoadGame(gameConfigName);
        }
    }
    public GameState LoadGame(int gameId)
    {
        return RepoManager.GameRepositoryDb.LoadGame(gameId);
    }
    public List<string> GetSavedGameNames()
    {
        if (RepoManager.ConfigType == EConfigType.Json)
        {
            return RepoManager.GameRepositoryJson.GetSavedGameNames();
        }
        else
        {
            return RepoManager.GameRepositoryDb.GetSavedGameNames();
        }
    }

    public void DeleteGame(int gameId)
    {
        RepoManager.GameRepositoryDb.DeleteGame(gameId);
    }
    public void DeleteAllGames()
    {
        RepoManager.GameRepositoryDb.DeleteAllGames();
    }
    public bool DoesGameExist(int gameId)
    {
        return RepoManager.GameRepositoryDb.DoesGameExist(gameId);
    }
}