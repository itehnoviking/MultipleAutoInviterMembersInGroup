﻿namespace MultipleAutoIviterMembersInGroup
{
    class Program
    {
        static async Task Main(string[] args)
        {
            static string Config(string what, string nameSession)
            {
                switch (what)
                {

                    case "api_id": Console.Write("API Id: "); return Console.ReadLine();
                    case "api_hash": Console.Write("API Hash: "); return Console.ReadLine();
                    case "phone_number": Console.Write("Phone number: "); return Console.ReadLine();
                    case "verification_code": Console.Write("Verification code: "); return Console.ReadLine();  // if sign-up is required
                    case "password": Console.Write("Password: "); return Console.ReadLine();     // if user has enabled 2FA
                    case "session_pathname": return nameSession;
                    default: return null;                  // let WTelegramClient decide the default config
                }
            }

            Console.Write("Колличество аккаунтов: ");
            var variable = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i < variable; i++)
            {
                
            }
            
            var myClient = await new WTelegram.Client(what => Config(what, "user1")).LoginUserIfNeeded();
            Console.WriteLine($"We are logged-in as {myClient.username ?? myClient.first_name + " " + myClient.last_name} (id {myClient.id})");
        }
    }
}