using System.ComponentModel.DataAnnotations;

namespace Domain;

public class Configuration
{
    public int Id { get; set; }
    [MaxLength(128)]
    public string? Name { get; set; }

    public int BoardSizeWidth { get; set; }

    public int BoardSizeHeight { get; set; }

    public int WinCondition { get; set; }

    public int AmountOfPieces { get; set; }

    public ICollection<Game>? Games { get; set; }
    // public string? ConfigJson { get; set; }
}