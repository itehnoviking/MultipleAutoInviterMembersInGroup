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
            for (int i = 0; i < Convert.ToInt32(Console.ReadLine()); i++)
            {
                tasks.Add(Task.Run(() => new InviteMashine().RunInviteMashine()));
                Thread.Sleep(100000);
            }

            await Task.WhenAll(tasks);
        }
    }
}

