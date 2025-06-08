using GameBrain;

namespace DAL;

public interface IGameRepository
{
    public int SaveGame(string gameState, string gameConfigName);
    public GameState LoadGame(string name);
    public List<string> GetSavedGameNames();
}