try
{
    try
    {
        Console.WriteLine("try");

        throw new Exception("try exception");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"catch exception: {ex.Message}");
        throw new Exception("catch exception");
    }
    finally
    {
        Console.WriteLine("finally");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"catch exception: {ex.Message}");
}

Console.WriteLine("Completed.");