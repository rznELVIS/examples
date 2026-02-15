namespace MutexAccess;

public class MutexService : IDisposable
{
    public readonly Mutex CurrentMutex = new Mutex(false, "Global\\Counter250");
    
    public void Dispose()
    {
        CurrentMutex.Dispose();
    }
}