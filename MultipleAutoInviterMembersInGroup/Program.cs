using MultipleAutoInviterMembersInGroup;
using TL;

namespace MultipleAutoIviterMembersInGroup
{
    class Program
    {
        static async Task Main(string[] args)
        {

            static string Config(string what)
            {
                var logic = new AllLogic();
                switch (what)
                {
                    case "api_id": Console.Write("API Id: "); return logic.DataSearchForConfig("api_id");
                    case "api_hash": Console.Write("API Hash: "); return logic.DataSearchForConfig("api_hash");
                    case "phone_number": Console.Write("Phone number: "); return logic.DataSearchForConfig("phone_number");
                    case "verification_code": Console.Write("Verification code: "); return Console.ReadLine();  // if sign-up is required
                    case "password": Console.Write("Password: "); return logic.DataSearchForConfig("password");     // if user has enabled 2FA
                    case "session_pathname": return logic.DataSearchForConfig("session_pathname");
                    default: return null;                  // let WTelegramClient decide the default config
                }
            }

            var logic = new AllLogic();
            var randomSecundForPause = new Random();

            Console.Write("number of accounts: ");
            var variable = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i < variable; i++)
            {

                using var client = new WTelegram.Client(Config);
                var myClient = await client.LoginUserIfNeeded();
                Console.WriteLine($"We are logged-in as {myClient.username ?? myClient.first_name + " " + myClient.last_name} (id {myClient.id})");

                Console.Write($"Enter the path in file with data for invaiting {myClient.first_name} {myClient.last_name}");
                var PATH = Console.ReadLine();

                var list = await logic.CreatedThirtyMembersFromListAndSavingBigListInFileAsync(PATH);
                var dictionary = await logic.TransformListInDictionary(list);

                var chats = await client.Messages_GetAllChats();
                var channel = chats.chats[1679143523];

                foreach (var item in dictionary)
                {
                    try
                    {
                        var user = new InputUser(item.Key, item.Value);
                        await client.AddChatUser(channel, user);
                        Console.WriteLine($"Added in chat user id {user.user_id}");
                        Thread.Sleep(randomSecundForPause.Next(100000, 150000));
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Exception");
                        Thread.Sleep(randomSecundForPause.Next(100000, 150000));
                    }
                }

            }
        }
    }
}
