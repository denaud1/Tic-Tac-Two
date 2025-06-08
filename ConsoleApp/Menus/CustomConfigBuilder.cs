using GameBrain;

namespace ConsoleApp;

public static class CustomConfigBuilder
{
    
    public static void Run()
    {
        var configuration = new GameConfiguration();
        var nameOfGame = "";
        var width = 0;
        var height = 0;
        var amountOfPieces = 0;
        var winCondition = 0;

        do
        {
            Console.Clear();
            Console.WriteLine("NEW CONFIGURATION");
            Console.WriteLine("============================");
            if (string.IsNullOrEmpty(nameOfGame))
            {
                Console.WriteLine("Please enter a game name:");
                Console.Write(">");
                nameOfGame = Console.ReadLine();
            }
            else if (width == 0)
            {
                Console.WriteLine("Please enter a game board width (not smaller than 3):");
                Console.Write(">");
                var input = Console.ReadLine();
                if (int.TryParse(input, out var parsedInput))
                {
                    if (parsedInput >= 3)
                    {
                        width = parsedInput;
                    }
                }
            }
            else if (height == 0)
            {
                Console.WriteLine("Please enter a game board height (not smaller than 3):");
                Console.Write(">");
                var input = Console.ReadLine();
                if (int.TryParse(input, out var parsedInput))
                {
                    if (parsedInput >= 3)
                    {
                        height = parsedInput;
                    }
                }
            }
            else if (amountOfPieces == 0)
            {
                Console.WriteLine("Please enter an amount of game pieces (not less than 3):");
                Console.Write(">");
                var input = Console.ReadLine();
                if (int.TryParse(input, out var parsedInput))
                {
                    if (parsedInput >= 3)
                    {
                        amountOfPieces = parsedInput;
                    }
                }
            }
            else if (winCondition == 0)
            {
                Console.WriteLine("Please enter a win condition (not not more than amount of pieces):");
                Console.Write(">");
                var input = Console.ReadLine();
                if (int.TryParse(input, out var parsedInput))
                {
                    if (parsedInput <= amountOfPieces)
                    {
                        winCondition = parsedInput;
                    }
                }
            }
            else
            {
                configuration.Name = nameOfGame;
                configuration.WinCondition = winCondition;
                configuration.BoardSizeHeight = height;
                configuration.BoardSizeWidth = width;
                configuration.AmountOfPieces = amountOfPieces;
                RepoManager.ConfigRepository.Save(configuration);
                return;
            }
            

        } while (true);

    }
}