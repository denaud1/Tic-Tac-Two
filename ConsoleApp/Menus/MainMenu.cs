namespace ConsoleApp;

public static class MainMenu
{
    public static void Run()
    {
        var isInputCorrect = true;
        do
        {
            Console.Clear();
            Console.WriteLine("TIC-TAC-TWO");
            Console.WriteLine("============================");
            Console.WriteLine("N) New Game");
            Console.WriteLine("L) Load Game");
            Console.WriteLine("C) Create New Config");
            Console.WriteLine("E) Exit");
            if (!isInputCorrect)
            {
                Message.GetWronChoiceError();
                isInputCorrect = true;
            }
            Console.Write("> ");
            var input = Console.ReadLine();

            if (input == null)
            {
                isInputCorrect = false;
            }
            else if (string.Equals(input, "N", StringComparison.OrdinalIgnoreCase))
            {
                GameController.MainLoop();
            }
            else if (string.Equals(input, "L", StringComparison.OrdinalIgnoreCase))
            {
                SavedGames.Run();
            } 
            else if (string.Equals(input, "C", StringComparison.OrdinalIgnoreCase))
            {
                CustomConfigBuilder.Run();
            }
            else if (string.Equals(input, "E", StringComparison.OrdinalIgnoreCase))
            {
                return;
            }
            else
            {
                isInputCorrect = false;
            }
        } while(true);
    }
}