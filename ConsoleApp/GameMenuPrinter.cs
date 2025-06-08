using GameBrain;

namespace ConsoleApp;

public static class GameMenuPrinter
{
    public static void Print(Player currentPlayer, TicTacToeBrain instance)
    {
        lock (Synchronizer.LockObject)
        {
            Console.WriteLine();
            if (GameStatus.CurrentMoveType.Equals(EMoveType.Continue))
            {
                Message.GetWinner(instance.Winner);
            }

            if (GameStatus.CurrentMoveType == EMoveType.Continue)
            {
                Console.WriteLine("C) Continue");
            }

            if (currentPlayer.PiecesOnBoard >= 2 && GameStatus.CurrentMoveType == EMoveType.Empty)
            {
                Console.WriteLine("R) Replace Piece");
                Console.WriteLine("G) Move Grid");
            }

            if (GameStatus.MoveCycles == 1 && GameStatus.CurrentMoveType == EMoveType.GridMove 
                || GameStatus.CurrentMoveType == EMoveType.InvalidInput)
            {
                Console.WriteLine("1) Right");
                Console.WriteLine("2) Left");
                Console.WriteLine("3) Down");
                Console.WriteLine("4) Up");
                Console.WriteLine("5) Down-Right");
                Console.WriteLine("6) Down-Left");
                Console.WriteLine("7) Up-Right");
                Console.WriteLine("8) Up-Left");
            }

            if (GameStatus.CurrentMoveType != EMoveType.Continue)
            {
                Console.WriteLine("S) Save the game");
                Console.WriteLine("W) Continue In Web");
            }

            Console.WriteLine("E) Exit");
            Console.WriteLine();
        }
    }
}