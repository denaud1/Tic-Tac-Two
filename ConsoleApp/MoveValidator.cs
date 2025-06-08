using System.Security.Cryptography;
using Domain;
using GameBrain;

namespace ConsoleApp;

public static class MoveValidator
{
    public static EMoveType GetMoveType(TicTacToeBrain instance, string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            Message.AddMessage("Invalid input.");
            return EMoveType.InvalidInput;
        }

        if (input.Contains(','))
        {
            if (input.Split(",").Length != 2)
            {
                Message.AddMessage("Invalid input. Must be 2 coordinates separated by comma.");
                Console.WriteLine();
                return EMoveType.InvalidInput;
            }

            if (!int.TryParse(input.Split(",")[0], out var x) || !int.TryParse(input.Split(",")[1], out var y))
            {
                return EMoveType.InvalidInput;
            }

            if (x < 0 || y < 0)
            {
                return EMoveType.InvalidInput;
            }

            if (x > instance.DimX || y > instance.DimY)
            {
                return EMoveType.InvalidInput;
            }

            if (x.GetType() != typeof(int) || y.GetType() != typeof(int))
            {
                return EMoveType.InvalidInput;
            }

            if (instance.GetCurrentPlayer().AmountOfPieces == instance.GetCurrentPlayer().PiecesOnBoard)
            {
                Message.AddMessage("All pieces are already on the board!");
                return EMoveType.InvalidInput;
            }
            return EMoveType.Coordinates;
        }

        if (GameStatus.CurrentMoveType == EMoveType.GridMove)
        {
            for (var i = 1; i < 9; i++)
            {
                var value = i.ToString();
                if (value.Equals(input))
                {
                    return EMoveType.GridMove;
                }
            }
            if (input.Equals("s", StringComparison.InvariantCultureIgnoreCase))
            {
                return EMoveType.Save;
            }
            if (input.Equals("w", StringComparison.InvariantCultureIgnoreCase))
            {
                return EMoveType.Web;
            }
            if (input.Equals("e", StringComparison.InvariantCultureIgnoreCase))
            {
                return EMoveType.Exit;
            }
            
            Message.AddMessage("Invalid input. Choose something from the existing options.");
            return EMoveType.InvalidInput;
        }
        if (input.Equals("e", StringComparison.InvariantCultureIgnoreCase))
        {
            return EMoveType.Exit;
        }
        if (input.Equals("w", StringComparison.InvariantCultureIgnoreCase))
        {
            return EMoveType.Web;
        }
        if (input.Equals("g", StringComparison.InvariantCultureIgnoreCase))
        {
            if (instance.GetCurrentPlayer().PiecesOnBoard < 2)
            {
                Message.AddMessage("You can move grid when you use at least 2 pieces!");
                return EMoveType.InvalidInput;
            }
            return EMoveType.GridMove;
        }
        if (input.Equals("s", StringComparison.InvariantCultureIgnoreCase))
        {
            return EMoveType.Save;
        }
        if (input.Equals("r", StringComparison.InvariantCultureIgnoreCase))
        {
            if (instance.GetCurrentPlayer().PiecesOnBoard < 2)
            {
                Message.AddMessage("You can move pieces when you use at least 2 pieces!");
                return EMoveType.InvalidInput;
            }
            return EMoveType.PieceMove;
        }

        if (input.Equals("c", StringComparison.InvariantCultureIgnoreCase))
        {
            return EMoveType.Continue;
        }

        return EMoveType.InvalidInput;
    }
}