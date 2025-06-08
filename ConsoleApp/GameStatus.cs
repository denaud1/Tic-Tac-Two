using GameBrain;

namespace ConsoleApp;

public static class GameStatus
{
    public static bool MoveSuccess = true;
    public static bool MoveInProcess = false;
    public static EMoveType CurrentMoveType = EMoveType.Empty;
    public static int MoveCycles = 0;

    public static void StartNewCycle(EMoveType inputType)
    {
        if (MoveSuccess)
        {
            MoveCycles++;
            CurrentMoveType = inputType;
        }
    }

    public static void EndMove()
    {
        if (MoveSuccess)
        {
            MoveCycles = 0;
            CurrentMoveType = EMoveType.Empty;
            MoveInProcess = false;
        }
    }

    public static void StartProcess()
    {
        MoveInProcess = true;
    }

    public static void EndGameAnyWay()
    {
        MoveCycles = 0;
        CurrentMoveType = EMoveType.Empty;
        MoveInProcess = false;
        MoveSuccess = true;
    } 
}