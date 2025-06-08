namespace GameBrain;

public class Coordinates
{
    public int X { get; set; } = default!;
    public int Y { get; set; } = default!;

    public Coordinates(int x, int y)
    {
        X = x;
        Y = y;
    }
}