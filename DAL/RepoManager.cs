using DAL;

namespace ConsoleApp;

public static class RepoManager
{
    public static EConfigType ConfigType = EConfigType.Database;
    
    public static AppDbContext Context = new AppDbContextFactory().CreateDbContext([]);

    public static ConfigRepository ConfigRepository = new ConfigRepository();
    public static GameRepository GameRepository = new GameRepository();

    public static ConfigRepositoryJson ConfigRepositoryJson = new ConfigRepositoryJson();
    public static ConfigRepositoryHardcoded ConfigRepositoryHardcoded = new ConfigRepositoryHardcoded();
    public static ConfigRepositoryDb ConfigRepositoryDb = new ConfigRepositoryDb(Context);

    public static GameRepositoryJson GameRepositoryJson = new GameRepositoryJson();
    public static GameRepositoryDb GameRepositoryDb = new GameRepositoryDb(Context);

    public static void ChangeConfigType(EConfigType configType)
    {
        ConfigType = configType;
    }
}