while (true)
{
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
            break;
        case 2:
            Console.WriteLine("You selected 2");
            break;
        case 3:
            Console.WriteLine("You selected 3");
            break;
        default:
            Console.WriteLine("Invalid option. Please enter 1, 2, or 3.");
            break;
    }
}
