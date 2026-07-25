namespace Concurrency;

public static class ParallelIncrement
{
    public static async Task RunIncrements(int numberOfIncrements, int numberOfTasks)
    {
        Console.WriteLine($"Running {numberOfTasks} tasks, each incrementing a counter {numberOfIncrements} times.");
        var counter = 0;
        var tasks = new List<Task>();
        for (int i = 0; i < numberOfTasks; i++)
        {
            // spawn tasks to increment the counter in parallel
            tasks.Add(Task.Run(() =>
            {
                for (int j = 0; j < numberOfIncrements; j++)
                {
                    // Increment is three operations that don't happen atomically
                    counter++;
                }
            }));
        }
        await Task.WhenAll(tasks);
        Console.WriteLine($"Final counter value: {counter}");
        Console.WriteLine($"Expected counter value: {numberOfIncrements * numberOfTasks}");
    }

    public static async Task RunIncrementsWithLock(int numberOfIncrements, int numberOfTasks)
    {
        Console.WriteLine($"Running {numberOfTasks} tasks, each incrementing a counter {numberOfIncrements} times with a lock.");
        var counter = 0;
        var lockObject = new object();
        var tasks = new List<Task>();
        for (int i = 0; i < numberOfTasks; i++)
        {
            // spawn tasks to increment the counter in parallel
            tasks.Add(Task.Run(() =>
            {
                for (int j = 0; j < numberOfIncrements; j++)
                {
                    // Lock the counter to ensure that only one thread can increment it at a time
                    lock (lockObject)
                    {
                        counter++;
                    }
                }
            }));
        }
        await Task.WhenAll(tasks);
        Console.WriteLine($"Final counter value: {counter}");
        Console.WriteLine($"Expected counter value: {numberOfIncrements * numberOfTasks}");
    }
}