namespace GameBrain;

public record struct GameConfiguration()
{
    public string Name { get; set; } = default!;
    public int BoardSizeWidth { get; set; } = 3;
    public int BoardSizeHeight { get; set; } = 3;
    // public int MiniBoardSizeWidth { get; set; } = 3;
    // public int MiniBoardSizeHeight { get; set; } = 3;
    
    // How many pieces in straight to win the game
    public int WinCondition { get; set; } = 3;
    public int AmountOfPieces { get; set; } = 4;


    public override string ToString()
    {
        return System.Text.Json.JsonSerializer.Serialize(this);
    }
}