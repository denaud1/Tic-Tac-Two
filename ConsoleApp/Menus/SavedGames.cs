using DAL;

namespace ConsoleApp;

public static class SavedGames
{
    public static void Run()
    {
        var savedGames = RepoManager.GameRepository.GetSavedGameNames();
        var count = 0;
        do
        {
            Console.Clear();
            Console.WriteLine("SAVED GAMES");
            Console.WriteLine("============================");
            if (savedGames.Count == 0)
            {
                Console.WriteLine("No saved games found");
                if (count > 0)
                {
                    Message.GetError();
                }
            }
            else
            {
                for (var i = 0; i < savedGames.Count; i++)
                {
                    Console.WriteLine($"{(i + 1).ToString()}) {savedGames[i]}");
                }
                
            }
            if (count > 0)
            {
                Message.GetError();
                count = 0;
            }
            Console.WriteLine("B) Back");
            Console.Write(">");
            var input = Console.ReadLine();
            if (string.Equals(input, "B", StringComparison.OrdinalIgnoreCase))
            {
                return;
            }
            if (int.TryParse(input, out var gameId))
            {
                if (savedGames.Count > gameId - 1 && gameId - 1 >= 0)
                {
                    var gameState = RepoManager.GameRepository.LoadGame(savedGames[gameId - 1]);
                    GameController.CreateExistingGameInstance(gameState);
                    Console.Clear();
                    GameController.MainLoop();
                }
            }

            count++;
        } while (true);
    }
}