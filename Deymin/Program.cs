using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Globalization;
using System.Threading;

namespace Deymin
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
        public string vpn { get; set; }
        public string proxy { get; set; }
        public string tor { get; set; }
        public string relay { get; set; }
        public string hosting { get; set; }
    }
    internal class Program
    {
        public static async Task Main()
        {
            Console.Title = "Deymin";
            string User = Environment.UserName;
            Console.WriteLine($"     Hello {User}, Welcome to Deymin");
            Console.WriteLine("         Enter an ip address: ");
            string ip = Console.ReadLine();
            string url = $"https://ipinfo.io/{ip}/json";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();

                
                    Console.Clear();
                    Console.WriteLine("\n[+] Request Complete!\n");

                    string responseData = await response.Content.ReadAsStringAsync();
                    Data Ipinfo = JsonConvert.DeserializeObject<Data>(responseData);

                    Console.Clear();
                    Console.WriteLine($"Country: {Ipinfo.country}");
                    Console.WriteLine($"IP: {Ipinfo.ip};");
                    Console.WriteLine($"Timezone: {Ipinfo.timezone}");
                    Console.WriteLine($"City: {Ipinfo.city}");
                    Console.WriteLine($"Cords: {Ipinfo.loc}");
                    Console.WriteLine($"Postal Code: {Ipinfo.postal}");
                    Console.WriteLine($"\nRegion: {Ipinfo.region}\n");
                    Console.WriteLine($"VPN: {Ipinfo.vpn}");
                    Console.WriteLine($"Proxy: {Ipinfo.proxy}");
                    Console.WriteLine($"Tor: {Ipinfo.tor}");
                    Console.WriteLine($"Relay: {Ipinfo.relay}");
                    Console.WriteLine($"Hosting: {Ipinfo.hosting}");

                }

                catch (Exception)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine("[!] Error has been detected");
                    Console.ReadLine();
                }
            }
        }
    }
}
