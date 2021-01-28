using System;
using System.Net.Http;
//https://docs.microsoft.com/en-us/dotnet/csharp/tutorials/console-webapiclient
using System.Threading.Tasks;
namespace ConsoleClientApp
{
    class Program
    {
        private static HttpClient client = new HttpClient();
        static async Task Main(string[] args)
        {

            //client.DefaultRequestHeaders.Accept.Add()
            // var stringResponse =  client.GetStringAsync("https://localhost:44314/");
            //of de unsecured versie "http://localhost:59798"

            var stringResponse = client.GetStringAsync("http://www.syntrawest.somee.com/api/Bieren/GeefAlleBieren");
            var msg = await stringResponse;
            Console.Write(msg);
            Console.ReadKey();
        }
    }
}
