namespace MovementTechnology.Data;

public class HistoryCartridge
{
    public int Id { get; set; } 
    public int CartridgeId { get; set; }
    public string? FromWhere { get; set; }
    public string? Where { get; set; }
    public string? Reason { get; set; }
    public DateTime DateTime { get; set; }
    public string UserId { get; set; }
}