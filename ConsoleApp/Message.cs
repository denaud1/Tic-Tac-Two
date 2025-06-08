using GameBrain;

namespace ConsoleApp;

public static class Message
{
    private static string _message = default!;
    public static void RequestCoordinates(string player)
    {
        lock (Synchronizer.LockObject)
        {
            if (GameStatus.CurrentMoveType.Equals(EMoveType.GridMove)
                && GameStatus.MoveCycles == 1)
            {
                Console.WriteLine($"{player}, enter coordinates where to move grid <x,y> or choose option:");
                Console.Write(">");
            }
            if (GameStatus.CurrentMoveType.Equals(EMoveType.PieceMove)
                && GameStatus.MoveCycles == 1)
            {
                Console.WriteLine($"{player}, enter coordinates of the piece you want to replace <x,y> or choose option:");
                Console.Write(">");
            }
            if (GameStatus.CurrentMoveType.Equals(EMoveType.PieceMove)
                && GameStatus.MoveCycles == 2)
            {
                Console.WriteLine($"{player}, enter coordinates of the area where to place the piece <x,y> or choose option:");
                Console.Write(">");
            }
            if (GameStatus.CurrentMoveType.Equals(EMoveType.Empty))
            {
                Console.WriteLine($"{player}, enter coordinates <x,y> or choose option:");
                Console.Write(">");
            }
        }
        
    }

    public static void GetWinner(EGamePiece winner)
    {
        if (winner == EGamePiece.Both)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Draw!");
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(winner + " is winner!");
            Console.ResetColor();
        }
    }

    public static void GetErrorIfExists()
    {
        if (!GameStatus.MoveSuccess || _message != default!)
        {
            GetError();
        }
    }

    public static void GetError()
    {
        if (string.IsNullOrEmpty(_message))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid input. Try again.");
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(_message);
            Console.ResetColor();
            _message = default!;
        }
        
    }
    public static void AddMessage(string message)
    {
        _message = message;
    }
    public static void GetWronChoiceError()
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Invalid input. Choose something from the existing options.");
        Console.ResetColor();
    }
}