using Concurrency;

Console.WriteLine("Welcome to the concurrency playground");

while (true)
{
    Console.WriteLine("Please select an option:");
    Console.WriteLine("1. Increment a counter in parallel");
    Console.WriteLine("2. Option 2");
    Console.WriteLine("3. Option 3");
    Console.Write("Enter a number (1, 2, or 3): ");
    string? input = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(input) || !int.TryParse(input, out int option))
    {
        Console.WriteLine("Invalid input. Please enter a valid number.");
        continue;
    }

    switch (option)
    {
        case 1:
            Console.WriteLine("You selected 1");
            await ParallelIncrement.RunIncrements(1000000, 4);
            await ParallelIncrement.RunIncrementsWithLock(1000000, 4);
            Console.WriteLine();
            break;
        case 2:
            Console.WriteLine("You selected 2");
            await RunParallelWithdrawals.RunWithdrawals(10, 10, 100);
            break;
        case 3:
            Console.WriteLine("You selected 3");
            break;
        default:
            Console.WriteLine("Invalid option. Please enter 1, 2, or 3.");
            break;
    }
}
