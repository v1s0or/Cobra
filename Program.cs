using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Sockets;
using System.Media;

namespace Cobra
{
    public class Data
    {
        public string city { get; set; }
        public string region { get; set; }
        public string country { get; set; }
        public string postal { get; set; }
        public string timezone { get; set; }
        public string loc { get; set; }
        public string ip { get; set; }
    }
    internal class Program
    {
        public static async Task Main()
        {
            string filePath = "Resource\\banner.txt";

            try
            {
                string content = File.ReadAllText(filePath);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(content); 
                Console.ResetColor();
            }
            catch (Exception)
            {
                Console.Clear();
            }

            Console.Title = "Cobra - [v1s0or]";
            string User = Environment.UserName;
            Console.WriteLine($"Hello {User}, Welcome to Cobra. Made by v1s0or.");
            Console.WriteLine("Enter an IP address: ");
            string ip = Console.ReadLine();
            string url = $"https://ipinfo.io/{ip}/json";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                    PlaySound();
                    string responseData = await response.Content.ReadAsStringAsync();
                    Data Ipinfo = JsonConvert.DeserializeObject<Data>(responseData);

                    Console.Clear();
                    string content = File.ReadAllText(filePath);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(content);
                    Console.ResetColor();
                    Console.WriteLine($"Country: {Ipinfo.country}");
                    Console.WriteLine($"IP: {Ipinfo.ip};");
                    Console.WriteLine($"Timezone: {Ipinfo.timezone}");
                    Console.WriteLine($"City: {Ipinfo.city}");
                    Console.WriteLine($"Cords: {Ipinfo.loc}");
                    Console.WriteLine($"Postal Code: {Ipinfo.postal}");
                    Console.WriteLine($"Region: {Ipinfo.region}");
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine($"\n[+] Scanning Vulnerable ports with AternalJaguar (Sit back as this may take a while)\n");
                    Console.ResetColor();

                    string[] ports = { "21", "22", "23", "25", "445", "3389", "5900", "4444", "10134", "1608", "1604", "50050", "139", "500", "80", "137", "139", "1433", "1434", "3306", "443"};

                    try
                    {
                        var tasks = new List<Task>();

                        foreach (string port in ports)
                        {
                            tasks.Add(CheckPortAsync(ip, port));
                        }

                        await Task.WhenAll(tasks);
                        Console.ReadLine();
                    }
                    catch (Exception)
                    {
                        FailSound();
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.WriteLine("[!] Error has been detected while running AternalJaguar");
                        Console.ResetColor();
                        Console.ReadLine();
                    }
                }
                catch (Exception ex)
                {
                    Console.Clear(); // customizing the console looks crazy on code
                    FailSound();
                    string content = File.ReadAllText(filePath);
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine(content); 
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine($"[!] An error occurred: {ex.Message}");
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nPress any key to exit...");
                    Console.ResetColor();
                    Console.ReadKey();
                }
            }
        }

        private static async Task CheckPortAsync(string ip, string port)
        {
            using (TcpClient tcpClient = new TcpClient())
            {
                try
                {
                    await tcpClient.ConnectAsync(ip, Convert.ToInt32(port));
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"[+] Port {port} is open [+]");
                }
                catch (Exception)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"[-] Port {port} is closed [-]");
                }
            }
        }
        public static void PlaySound()
        {
            SoundPlayer player = new SoundPlayer();
            player.SoundLocation = "Resource\\chime.wav";
            player.Play();
        }
        public static void FailSound()
        {
            SoundPlayer player = new SoundPlayer();
            player.SoundLocation = "Resource\\ding.wav";
            player.Play();
        }
    }
}