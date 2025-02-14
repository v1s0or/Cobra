using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Sockets;
using System.Globalization;
using System.Threading;

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
            string filePath = "banner.txt";  // Specify the path to your banner file

            try
            {
                string content = File.ReadAllText(filePath);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(content); // Output the banner content
                Console.ResetColor();
            }
            catch (Exception)
            {
                Console.Clear();
            }

            Console.Title = "Cobra";
            string User = Environment.UserName;
            Console.WriteLine($"Hello {User}, Welcome to Cobra");
            Console.WriteLine("Enter an IP address: ");
            string ip = Console.ReadLine();
            string url = $"https://ipinfo.io/{ip}/json";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();

                    string responseData = await response.Content.ReadAsStringAsync();
                    Data Ipinfo = JsonConvert.DeserializeObject<Data>(responseData);

                    Console.Clear();
                    Console.WriteLine($"Country: {Ipinfo.country}");
                    Console.WriteLine($"IP: {Ipinfo.ip};");
                    Console.WriteLine($"Timezone: {Ipinfo.timezone}");
                    Console.WriteLine($"City: {Ipinfo.city}");
                    Console.WriteLine($"Cords: {Ipinfo.loc}");
                    Console.WriteLine($"Postal Code: {Ipinfo.postal}");
                    Console.WriteLine($"Region: {Ipinfo.region}");
                    Console.WriteLine($"\n[+] Scanning Vulnerable ports with AternalJaguar (Sit back as this may take a while)\n");

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
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.WriteLine("[!] Error has been detected while running AternalJaguar");
                        Console.ReadLine();
                    }
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine($"[!] An error occurred: {ex.Message}");
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
                    Console.WriteLine($"[+] Port {port} is open");
                }
                catch (Exception)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"[-] Port {port} is closed");
                }
            }
        }
    }
}
