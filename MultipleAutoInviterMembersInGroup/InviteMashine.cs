using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TL;

namespace MultipleAutoInviterMembersInGroup
{
    class InviteMashine
    {
        public string Config(string what)
        {
            switch (what)
            {
                case "api_id": Console.Write("API Id: "); return new AllLogic().DataSearchForConfig("api_id.txt");
                case "api_hash": Console.Write("API Hash: "); return new AllLogic().DataSearchForConfig("api_hash.txt");
                case "phone_number": Console.Write("Phone number: "); return new AllLogic().DataSearchForConfig("phone.txt");
                case "verification_code": Console.Write("Verification code: "); return Console.ReadLine();  // if sign-up is required
                case "password": Console.Write("Password: "); return new AllLogic().DataSearchForConfig("password.txt");     // if user has enabled 2FA
                case "session_pathname": return new AllLogic().DataSearchForConfig("session_pathname.txt");
                default: return null;                  // let WTelegramClient decide the default config
            }
        }

        public async Task RunInviteMashine()
        {
            Random randomSecundForPause = new Random();

            using var client = new WTelegram.Client(Config);
            var myClient = await client.LoginUserIfNeeded();
            Console.WriteLine($"We are logged-in as {myClient.username ?? myClient.first_name + " " + myClient.last_name} (id {myClient.id})");

            Console.Write($"Enter the path in file with data for invaiting {myClient.first_name} {myClient.last_name}");
            var dict = new AllLogic().TransformListInDictionary(new AllLogic().CreatedThirtyMembersFromListAndSavingBigListInFile(Console.ReadLine()));


            var chats = await client.Messages_GetAllChats();
            var channel = chats.chats[1679143523];

            var counterAddedMembers = 0;

            foreach (var item in dict)
            {
                try
                {
                    var user = new InputUser(item.Key, item.Value);
                    await client.AddChatUser(channel, user);
                    Console.WriteLine($"{myClient.first_name} {myClient.last_name} Added in chat user id {user.user_id}");
                    Thread.Sleep(randomSecundForPause.Next(100000, 150000));
                    counterAddedMembers++;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"{myClient.first_name} {myClient.last_name} has {e.Message}");
                    Thread.Sleep(randomSecundForPause.Next(100000, 150000));
                }
            }

            Console.WriteLine($"{myClient.first_name} {myClient.last_name} added {counterAddedMembers} members in chat");
        }
    }
}
