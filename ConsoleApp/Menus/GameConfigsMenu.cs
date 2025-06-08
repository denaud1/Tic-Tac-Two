namespace ConsoleApp;

public class GameConfigsMenu
{
    public static void Run()
    {
        var configs = RepoManager.ConfigRepository.GetConfigurationNames();
        var isInputCorrect = true;
        do
        {
            Console.Clear();
            Console.WriteLine("CHOOSE GAME CONFIG");
            Console.WriteLine("============================");
            for (var i = 0; i < configs.Count; i++)
            {
                Console.WriteLine($"{(i + 1).ToString()}) {configs[i]}");
            }
            Console.WriteLine("C) Create New Config");
            Console.WriteLine("B) Back");
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
            
            else if (string.Equals(input, "C", StringComparison.OrdinalIgnoreCase))
            {
                CustomConfigBuilder.Run();
            }
            else if (string.Equals(input, "B", StringComparison.OrdinalIgnoreCase))
            {
                return;
            }
            else if (int.TryParse(input, out var configId))
            {
                if (configs.Count > configId - 1 && configId - 1 >= 0)
                {
                    GameController.Config = RepoManager.ConfigRepository
                        .GetConfigurationByName(configs[configId - 1]);
                    Console.Clear();
                    return;
                }
            }
            else
            {
                isInputCorrect = false;
            }
        } while(true);
    }
}