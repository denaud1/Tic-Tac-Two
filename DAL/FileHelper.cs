namespace DAL;

public static class FileHelper
{
    public static readonly string BasePath = Environment
                .GetFolderPath(System.Environment.SpecialFolder.UserProfile)
                + Path.DirectorySeparatorChar + "tic-tac-toe" + Path.DirectorySeparatorChar;

    public static readonly string ConfigExtension = ".config.json";
    public static readonly string GameExtension = ".game.json";
    
    public static readonly string SaveFoulder = "Saved games" + Path.DirectorySeparatorChar;
    public static readonly string ConfigFoulder = "Configurations" + Path.DirectorySeparatorChar;

}