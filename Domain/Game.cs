using System.ComponentModel.DataAnnotations;

namespace Domain;

public class Game
{
    public int Id { get; set; }
    [MaxLength(128)]
    public string? Name { get; set; } = default!;

    public string? GameStateJson { get; set; } = default!;
    public int ConfigurationId { get; set; }
    public Configuration? Configuration { get; set; } = default!;
}