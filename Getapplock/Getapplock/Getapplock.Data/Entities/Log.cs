namespace Getapplock.Data.Entities;

public class Log
{
    public int Id { get; init; }
    
    public string Message { get; init; }
    
    public DateTimeOffset? LoggedAt { get; init; }
}