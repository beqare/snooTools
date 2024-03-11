using System;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using System.IO;

namespace snooClient
{
    internal class Program
    {
        // -------------------- config --------------------
        private const string Version = "1.0";
        private const string Name = "snooClient";
        private const string Title = " | " + Name + Version;

        // -------------------- menu --------------------
        static async Task Main(string[] args)
        {
            Console.Title = Name;
            while (true)
            {
                Console.Clear();
                Console.Title = "Home" + Title;
                Console.WriteLine("--- Welcome to snooClient ---\n");
                Console.WriteLine("[1] Webhooksender");
                Console.WriteLine("[2] Systemoptimizer");
                Console.WriteLine("[#] Infos");
                Console.WriteLine("[?] Discord");
                Console.WriteLine("[0] Exit\n");
                Console.Write("Choose an option: ");

                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        Console.Clear();
                        Console.Title = "Webhooksender" + Title;
                        await WebhookSender();
                        break;
                    case "2":
                        Console.Clear();
                        Console.Title = "Systemoptimizer" + Title;
                        SystemOptimizer();
                        break;
                    case "#":
                        Console.Clear();
                        Console.Title = "Informations" + Title;
                        Console.WriteLine("Version: " + Version);
                        break;
                    case "?":
                        Console.Clear();
                        InfoDiscord();
                        break;
                    case "0":
                        Console.Clear();
                        Console.Title = "Exit" + Title;
                        Countdown(3);
                        Environment.Exit(0);
                        break;
                    default:
                        Console.Clear();
                        Console.Title = "Error" + Title;
                        Console.WriteLine("Invalid option!");
                        break;
                }

                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
        }

        // -------------------- info: discord --------------------
        static void InfoDiscord()
        {
            Console.WriteLine("Opening Discord...");
            System.Diagnostics.Process.Start("https://snoopti.de/discord");
            Console.WriteLine("Press any key to return to the menu...");
            Console.ReadKey();
        }

        // -------------------- tool: SystemOptimizer --------------------
        static void SystemOptimizer()
        {
            try
            {
                string username = Environment.UserName;

                EmptyFolder($@"C:\Users\{username}\AppData\Local\Temp");
                EmptyFolder($@"C:\Users\{username}\AppData\LocalLow\Temp");

                Console.WriteLine("System optimized successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while optimizing the system: {ex.Message}");
            }
        }

        static void EmptyFolder(string path)
        {
            if (Directory.Exists(path))
            {
                try
                {
                    DirectoryInfo directory = new DirectoryInfo(path);
                    foreach (FileInfo file in directory.GetFiles())
                    {
                        try
                        {
                            file.Delete();
                            Console.WriteLine($"File {file.FullName} deleted successfully.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error deleting file {file.FullName}: {ex.Message}");
                        }
                    }
                    foreach (DirectoryInfo subDirectory in directory.GetDirectories())
                    {
                        try
                        {
                            EmptyFolder(subDirectory.FullName);
                            subDirectory.Delete();
                            Console.WriteLine($"Folder {subDirectory.FullName} deleted successfully.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error deleting folder {subDirectory.FullName}: {ex.Message}");
                        }
                    }
                    Console.WriteLine($"Folder {path} emptied successfully.");
                }
                catch (UnauthorizedAccessException ex)
                {
                    Console.WriteLine($"Error: Access to some files or directories in {path} is denied.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while emptying the folder {path}: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine($"Folder {path} does not exist.");
            }
        }


        // -------------------- tool: webhook sender --------------------
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

        // -------------------- system: countdown --------------------
        static void Countdown(int seconds)
        {
            for (int i = seconds; i > 0; i--)
            {
                Console.WriteLine($"Exiting in {i} seconds...");
                Thread.Sleep(1000);
                Console.Clear();
            }
        }
    }
}
