using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Globalization;

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
    }
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.Title = "Deymin by ven";
            string filepath = "C:\\Users\\santi\\source\\repos\\Deymin\\Deymin\\banner.txt";
            string filecontents = File.ReadAllText(filepath, Encoding.UTF8);
            Console.WriteLine(filecontents);
            Console.WriteLine("         Enter an ip address: ");
            string ip = Console.ReadLine();
            string url = $"https://ipinfo.io/{ip}/json";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();

                    Console.WriteLine("[+] Request Complete!");

                    string responseData = await response.Content.ReadAsStringAsync();
                    Data Ipinfo = JsonConvert.DeserializeObject<Data>(responseData);

                    Console.Clear();
                    Console.WriteLine($"Country: {Ipinfo.country}");
                    Console.WriteLine();
                    Console.WriteLine($"IP: {Ipinfo.ip}");
                    Console.WriteLine();
                    Console.WriteLine($"City: {Ipinfo.city}");
                    Console.WriteLine();
                    Console.WriteLine($"Cords: {Ipinfo.loc}");
                    Console.WriteLine();
                    Console.WriteLine($"Postal Code: {Ipinfo.postal}");
                    Console.WriteLine();
                    Console.WriteLine($"Region: {Ipinfo.region}");
                    Console.WriteLine();
                    Console.WriteLine($"ASN: {Ipinfo.region}");

                }
                catch (Exception ex) {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}