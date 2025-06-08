namespace GameBrain;

public static class Synchronizer
{
    public static readonly Lock LockObject = new Lock();
    public static (int, int) GameBoardCoordinates { get; set; }
}