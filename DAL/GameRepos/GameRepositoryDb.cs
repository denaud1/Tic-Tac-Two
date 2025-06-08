using System.Net.Mime;
using ConsoleApp;
using Domain;
using GameBrain;

namespace DAL;

public class GameRepositoryDb : IGameRepository
{
    private DAL.AppDbContext _context;

    public GameRepositoryDb(AppDbContext context)
    {
        _context = context;
    }

    public int SaveGame(string gameStateString, string gameConfigName)
    {
        var config = _context.Configurations.First(c => c.Name == gameConfigName);
        var game = new Game()
        {
            GameStateJson = gameStateString,
            Name = gameConfigName + " " + DateTime.Now.ToString("dd/MM/yyyy HH-mm-ss"),
            ConfigurationId = config.Id
        };
        _context.Games.Add(game);
        _context.SaveChanges();
        return game.Id;
    }

    public GameState LoadGame(string gameName)
    {
        return System.Text.Json.JsonSerializer
            .Deserialize<GameState>(_context.Games.First(x => x.Name == gameName).GameStateJson!)!;
    }
    public GameState LoadGame(int gameId)
    {
        return System.Text.Json.JsonSerializer
            .Deserialize<GameState>(_context.Games.First(x => x.Id == gameId).GameStateJson!)!;
    }
    public bool DoesGameExist(int gameId)
    {
        return _context.Games.FirstOrDefault(g => g.Id == gameId) != null;
    }

    public List<string> GetSavedGameNames()
    {
        return _context.Games.Select(x => x.Name).ToList()!;
    }

    public void DeleteGame(int gameId)
    {
        var game = _context.Games.First(c => c.Id == gameId);
        
        _context.Games.Remove(game);
        
        _context.SaveChanges();
    }

    public void DeleteAllGames()
    {
        _context.Games.RemoveRange(_context.Games);
        _context.SaveChanges();
    }
}