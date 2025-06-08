using GameBrain;


namespace ConsoleUI;

public static class Visualizer
{
    public static void DrawBoard(TicTacToeBrain gameInstance)
    {
        
        lock (Synchronizer.LockObject)
        {
            // Determine the maximum width needed for x and y coordinates
            var xWidth = gameInstance.DimX.ToString().Length;
            var yWidth = gameInstance.DimY.ToString().Length;
            var cellWidth = Math.Max(xWidth, 1) + 2;
            // Print column headers
            Console.Write(new string(' ', yWidth + 1)); // Padding for row numbers
            for (var x = 0; x < gameInstance.DimX; x++)
            {
                // Use padding to align multi-digit numbers
                var padding = (cellWidth - x.ToString().Length) / 2;
                var columnHeader = new string(' ', padding) 
                                   + x 
                                   + new string(' ', cellWidth - x.ToString().Length - padding);
                Console.Write(columnHeader);
            }
            Console.WriteLine();

            // Draw the board
            for (var y = 0; y < gameInstance.DimY; y++)
            {
                // Right-align y coordinate with proper padding
                Console.Write(y.ToString().PadLeft(yWidth) + " ");

                for (var x = 0; x < gameInstance.DimX; x++)
                {
                    if (gameInstance.GameBoard[x][y].AreaType == EBoardAreaType.MiniBoard)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkCyan;
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                    }

                    Console.ForegroundColor = ConsoleColor.Black;

                    // Ensure consistent width for game pieces
                    var piece = DrawGamePiece(gameInstance.GameBoard[x][y].Piece);
                    var padding = (cellWidth - piece.Length) / 2;
                    var paddedPiece = new string(' ', padding) 
                                      + piece 
                                      + new string(' ', cellWidth - piece.Length -padding);
                    Console.Write(paddedPiece);

                    Console.ResetColor();
                }
                Console.WriteLine();
            }
        }
    }

    private static string DrawGamePiece(EGamePiece piece) =>
        piece switch
        {
            EGamePiece.O => "O",
            EGamePiece.X => "X",
            _ => "*"
        };
    
}