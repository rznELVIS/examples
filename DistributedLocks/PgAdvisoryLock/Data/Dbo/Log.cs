namespace PgAdvisoryLock.Data.Dbo;

public class Log
{
    public int Id { get; set; }
    
    public string Message { get; set; }
    
    public DateTimeOffset? LoggedAt { get; set; }
}