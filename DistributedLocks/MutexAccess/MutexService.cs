namespace MutexAccess;

public class MutexService : IDisposable
{
    public readonly Mutex CurrentMutex = new Mutex(false, "Global\\Counter");
    
    public void Dispose()
    {
        CurrentMutex.Dispose();
    }
}