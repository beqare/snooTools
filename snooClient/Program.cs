using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace snooClient
{
    internal class Program
    {
        private const string Version = "1.0";
        private const string Name = "snooClient";

        static async Task Main(string[] args)
        {
            Console.Title = Name;
            while (true)
            {
                Console.Clear();
                Console.Title = "Home" + " | " + Name + Version;
                Console.WriteLine("--- Welcome to snooClient ---\n");
                Console.WriteLine("[1] Webhook Sender");
                Console.WriteLine("[#] Infos");
                Console.WriteLine("[?] Discord");
                Console.WriteLine("[0] Exit\n");
                Console.Write("Choose an option: ");

                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        Console.Title = Name + " | " + "WebhookSender";
                        Console.Clear();
                        await WebhookSender();
                        break;
                    case "#":
                        Console.Clear();
                        Console.WriteLine("Version: " + Version);
                        break;
                    case "?":
                        Console.Clear();
                        InfoDiscord();
                        break;
                    case "0":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Invalid option!");
                        break;
                }

                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
        }

        static void InfoDiscord()
        {
            Console.WriteLine("Opening Discord...");
            System.Diagnostics.Process.Start("https://snoopti.de/discord");
            Console.WriteLine("Press any key to return to the menu...");
            Console.ReadKey();
        }

        static async Task WebhookSender()
        {
            Console.WriteLine("Enter Webhook URL:");
            string webhookUrl = Console.ReadLine();

            if (!Uri.TryCreate(webhookUrl, UriKind.Absolute, out Uri validUri))
            {
                Console.WriteLine("Invalid URL!");
                return;
            }

            Console.WriteLine("Enter Message:");
            string messageText = Console.ReadLine();

            await SendMessageToDiscordWebhook(webhookUrl, messageText);
        }

        static async Task SendMessageToDiscordWebhook(string webhookUrl, string messageText)
        {
            using (HttpClient client = new HttpClient())
            {
                var payload = new
                {
                    content = messageText
                };

                var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");

                try
                {
                    HttpResponseMessage response = await client.PostAsync(webhookUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Message successfully sent to Discord.");
                    }
                    else
                    {
                        Console.WriteLine($"Failed to send message: {response.StatusCode} - {response.ReasonPhrase}");
                    }
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
        }
    }
}
