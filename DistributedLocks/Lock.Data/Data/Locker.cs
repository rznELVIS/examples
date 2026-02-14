namespace Lock.Data.Data;

public class Locker
{
    public required string Resource { get; set; }
    
    public string LockedBy { get; set; }
    
    public DateTimeOffset? LockedAt { get; set; }
    
    public DateTimeOffset? ExpiresAt { get; set; }
}