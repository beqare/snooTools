using System;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Net;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace snooClient
{
    internal class Program
    {
        // ---------------------------------------- config ----------------------------------------
        private const string Version = "2.0";
        private const string Name = "snooTools";
        private const string Author = "snoopti";
        private const string ContinueMessage = "Press any button to continue";
        private const string Title = " | " + Name + " v" + Version + " by " + Author;

        // ---------------------------------------- menu ----------------------------------------
        static async Task Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.Title = "Home" + Title;
                Console.WriteLine("--- Welcome to snooClient ---");
                Console.WriteLine("");
                Console.WriteLine("--- Tools");
                Console.WriteLine("[1] Webhooksender");
                Console.WriteLine("[2] Systemoptimizer");
                Console.WriteLine("[3] IPAdressviewer");
                Console.WriteLine("[4] Speedtest");
                Console.WriteLine("");
                Console.WriteLine("--- Informations");
                Console.WriteLine("[#] About");
                Console.WriteLine("[?] Discord");
                Console.WriteLine("[0] Exit");
                Console.WriteLine("");
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
                    case "3":
                        Console.Clear();
                        Console.Title = "IPAdressviewer" + Title;
                        showIpAdress();
                        break;
                    case "4":
                        Console.Clear();
                        Console.Title = "Speedtest" + Title;
                        await CheckInternetSpeed();
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

        // ---------------------------------------- info: discord ----------------------------------------
        static void InfoDiscord()
        {
            Console.WriteLine(ContinueMessage);
            Console.ReadKey();
            Console.WriteLine("Opening Discord...");
            System.Diagnostics.Process.Start("https://snoopti.de/discord");
        }

        // ---------------------------------------- tool: SystemOptimizer ----------------------------------------
        static void SystemOptimizer()
        {
            Console.WriteLine("--- Systemoptimizer ---");
            Console.WriteLine("Click any button to continue");
            Console.ReadKey();
            try
            {
                string username = Environment.UserName;
                string[] foldersToEmpty =
                {
                    $@"C:\Users\{username}\AppData\LocalLow\Microsoft\CryptnetUrlCache\Content",
                    $@"C:\Users\{username}\AppData\Local\D3DSCache",
                    $@"C:\Users\{username}\AppData\Local\Temp",
                    $@"C:\Users\{username}\.cache",
                    $@"C:\Users\{username}\AppData\LocalLow\Temp",
                    $@"C:\ProgramData\Microsoft\Windows\WER\ReportArchive",
                    $@"C:\Windows\SoftwareDistribution\Download",
                    $@"C:\Windows\Temp",
                    $@"C:\Users\{username}\AppData\Local\NVIDIA\DXCache",
                    $@"C:\Users\{username}\AppData\Local\NVIDIA\GLCache",
                    $@"C:\Users\{username}\AppData\Local\CapCut\User Data\Cache",
                    $@"C:\Users\{username}\AppData\Roaming\discord\Code Cache",
                    $@"C:\Users\{username}\AppData\Local\pip\cache",
                    $@"C:\Users\{username}\AppData\Roaming\Microsoft\Teams\Code Cache\js",
                    $@"C:\Users\{username}\AppData\Roaming\discord\Code Cache\js",
                    $@"C:\Users\{username}\AppData\Local\Microsoft\Edge\User Data\BrowserMetrics",
                    $@"C:\Users\{username}\AppData\Local\Google\Chrome\User Data\Default\Cache\Cache_Data",
                    $@"C:\Users\{username}\AppData\Local\Google\Chrome\User Data\Default\Service Worker\CacheStorage",
                    $@"C:\Users\{username}\AppData\Local\Google\Chrome\User Data\Profile 1\Cache\Cache_Data",
                    $@"C:\Users\{username}\AppData\Local\Google\Chrome\User Data\Profile 1\Service Worker\CacheStorage",
                    $@"C:\Users\{username}\AppData\Local\Google\Chrome\User Data\Profile 2\Cache\Cache_Data",
                    $@"C:\Users\{username}\AppData\Local\Google\Chrome\User Data\Profile 2\Service Worker\CacheStorage",
                    $@"C:\Users\{username}\AppData\Local\Google\Chrome\User Data\Profile 3\Cache\Cache_Data",
                    $@"C:\Users\{username}\AppData\Local\Google\Chrome\User Data\Profile 3\Service Worker\CacheStorage",
                    $@"C:\Users\{username}\AppData\Local\Google\Chrome\User Data\Profile 4\Cache\Cache_Data",
                    $@"C:\Users\{username}\AppData\Local\Google\Chrome\User Data\Profile 4\Service Worker\CacheStorage",
                    $@"C:\Users\{username}\AppData\Local\Google\Chrome\User Data\Profile 5\Cache\Cache_Data",
                    $@"C:\Users\{username}\AppData\Local\Google\Chrome\User Data\Profile 5\Service Worker\CacheStorage",
                    $@"C:\Users\{username}\AppData\Local\Google\Chrome\User Data\Profile 6\Cache\Cache_Data",
                    $@"C:\Users\{username}\AppData\Local\Google\Chrome\User Data\Profile 6\Service Worker\CacheStorage",
                    $@"C:\Users\{username}\AppData\Local\Google\Chrome\User Data\Profile 7\Cache\Cache_Data",
                    $@"C:\Users\{username}\AppData\Local\Google\Chrome\User Data\Profile 7\Service Worker\CacheStorage",
                    $@"C:\Users\{username}\AppData\Local\Google\Chrome\User Data\Profile 8\Cache\Cache_Data",
                    $@"C:\Users\{username}\AppData\Local\Google\Chrome\User Data\Profile 8\Service Worker\CacheStorage",
                    $@"C:\Users\{username}\AppData\Local\Google\Chrome\User Data\Profile 9\Cache\Cache_Data",
                    $@"C:\Users\{username}\AppData\Local\Google\Chrome\User Data\Profile 9\Service Worker\CacheStorage",
                    $@"C:\Users\{username}\AppData\Local\Google\Chrome\User Data\Profile 10\Cache\Cache_Data",
                    $@"C:\Users\{username}\AppData\Local\Google\Chrome\User Data\Profile 10\Service Worker\CacheStorage"
                };

                foreach (string folder in foldersToEmpty)
                {
                    EmptyFolder(folder);
                }

                Console.WriteLine("System optimized successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while optimizing the system: {ex.Message}");
            }
        }

        static void EmptyFolder(string path)
        {
            try
            {
                if (Directory.Exists(path))
                {
                    Directory.Delete(path, recursive: true);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(path);
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine(path);
                    Console.ResetColor();
                }
            }
            catch (UnauthorizedAccessException)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(path);
                Console.ResetColor();
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(path);
                Console.ResetColor();
            }
        }




        // ---------------------------------------- tool: webhook sender ----------------------------------------
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

        // ---------------------------------------- tool: showip ----------------------------------------
        static void showIpAdress()
        {
            try
            {
                string ipAddress = new WebClient().DownloadString("http://icanhazip.com").Trim();
                Console.WriteLine($"IP: {ipAddress}");

                string apiUrl = $"http://ip-api.com/json/{ipAddress}?fields=country,regionName,city,isp,lat,lon,timezone,as";
                string jsonResult = new WebClient().DownloadString(apiUrl);

                dynamic locationInfo = JsonConvert.DeserializeObject(jsonResult);
                string country = locationInfo.country;
                string region = locationInfo.regionName;
                string city = locationInfo.city;
                string isp = locationInfo.isp;
                double latitude = locationInfo.lat;
                double longitude = locationInfo.lon;
                string timezone = locationInfo.timezone;

                Console.WriteLine($"Location: {city}, {region}, {country}");
                Console.WriteLine($"ISP: {isp}");
                Console.WriteLine($"Latitude: {latitude}");
                Console.WriteLine($"Longitude: {longitude}");
                Console.WriteLine($"Timezone: {timezone}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while fetching the IP address: {ex.Message}");
            }
        }



        // ---------------------------------------- tool: speedtest ----------------------------------------
        static async Task CheckInternetSpeed()
        {
            try
            {
                string ipAddress = new WebClient().DownloadString("http://icanhazip.com").Trim();
                
                const int numTests = 5;
                var results = new List<double>();
                var url = "https://snoopti.de/download/speedtest1mb.zip";

                Console.WriteLine($"IP Adress: {ipAddress}");
                Console.WriteLine($"Server: Berlin/Karlsruhe | Germany");
                Console.WriteLine($"Running {numTests} times...");

                using (var httpClient = new HttpClient())
                {
                    httpClient.Timeout = TimeSpan.FromSeconds(30);

                    for (int i = 1; i <= numTests; i++)
                    {
                        var stopwatch = Stopwatch.StartNew();

                        try
                        {
                            var response = await httpClient.GetAsync(url);
                            response.EnsureSuccessStatusCode();
                            await response.Content.ReadAsByteArrayAsync();
                        }
                        catch (HttpRequestException ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                            continue;
                        }
                        catch (TaskCanceledException ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                            continue;
                        }

                        stopwatch.Stop();

                        double speedInMbps = CalculateSpeed(stopwatch.Elapsed);
                        Console.WriteLine($"Test {i}: {speedInMbps:F2} Mbps");
                        results.Add(speedInMbps);

                        await Task.Delay(5000);
                    }
                }

                double averageSpeed = results.Any() ? results.Average() : 0;
                Console.WriteLine($"\nAverage speed over {results.Count} tests: {averageSpeed:F2} Mbps");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }



        static double CalculateSpeed(TimeSpan elapsed)
        {
            const int fileSizeInBytes = 1024 * 1024;
            const int bitsInByte = 8;
            double seconds = elapsed.TotalSeconds;
            double bytesPerSecond = fileSizeInBytes / seconds;
            double bitsPerSecond = bytesPerSecond * bitsInByte;
            double speedInMbps = bitsPerSecond / 1024 / 1024;
            return speedInMbps;
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
