namespace Concurrency;

public class BankAccount
{
    public int Balance { get; private set; }

    public BankAccount(int numberOfWithdrawals, int withdrawalAmount, int numberOfTasks)
    {
        Balance = numberOfWithdrawals * withdrawalAmount * numberOfTasks;
    }

    public void Withdraw(int amount)
    {
        if (Balance >= amount)
        {
            // Use non-thread-safe operation to demonstrate race condition
            Balance -= amount;
        }
        else
        {
            throw new InvalidOperationException("Insufficient funds");
        }
    }

    public void LockedWithdraw(int amount, object lockObject)
    {
        lock (lockObject)
        {
            if (Balance >= amount)
            {
                Balance -= amount;
            }
            else
            {
                throw new InvalidOperationException("Insufficient funds");
            }
        }
    }
}

public static class RunParallelWithdrawals
{
    public static async Task RunWithdrawals(int numberOfWithdrawals, int withdrawalAmount, int numberOfTasks)
    {
        var account = new BankAccount(numberOfWithdrawals, withdrawalAmount, numberOfTasks);
        var lockedAccount = new BankAccount(numberOfWithdrawals, withdrawalAmount, numberOfTasks);
        Console.WriteLine($"Bank account has initial balance of {account.Balance}");
        Console.WriteLine($"Running {numberOfTasks} tasks, each withdrawing {withdrawalAmount} from the account {numberOfWithdrawals} times.");

        var tasks = new List<Task>();
        var lockObject = new object();
        for (int i = 0; i < numberOfTasks; i++)
        {
            // spawn tasks to withdraw from the account in parallel
            tasks.Add(Task.Run(() =>
            {
                for (int j = 0; j < numberOfWithdrawals; j++)
                {
                    try
                    {
                        account.Withdraw(withdrawalAmount);
                        lockedAccount.LockedWithdraw(withdrawalAmount, lockObject);
                    }
                    catch (InvalidOperationException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }));
        }
        await Task.WhenAll(tasks);
        Console.WriteLine($"Final account balance: {account.Balance}");
        Console.WriteLine($"Final locked account balance: {lockedAccount.Balance}");
    }
}