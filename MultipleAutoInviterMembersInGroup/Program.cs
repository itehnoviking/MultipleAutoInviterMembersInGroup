using MultipleAutoInviterMembersInGroup;
using TL;

namespace MultipleAutoIviterMembersInGroup
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var tasks = new List<Task>();

            Console.Write("How many accounts do you need to run? ");
            var amountAccounts = Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i < amountAccounts; i++)
            {
                tasks.Add(Task.Run(() => new InviteMashine().RunInviteMashine()));
                Thread.Sleep(100000);
            }

            await Task.WhenAll(tasks);
        }
    }
}

