using ConsoleClientApp.Models;
using ConsoleClientApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
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
            //client.DefaultRequestHeaders.Clear();
            //client.DefaultRequestHeaders.Add("Accept", "application/json");

            // var stringResponse =  client.GetStringAsync("https://localhost:44314/");
            //of de unsecured versie "http://localhost:59798"

            //var stringResponse = client.GetStringAsync("http://www.syntrawest.somee.com/api/Bieren/GeefAlleBieren");
            //var msg = await stringResponse;
            //Console.Write(msg);
            BierenService bierenService = new BierenService();
            List<Bier> bieren= await bierenService.GeefBierenAsync();
            Console.WriteLine("Aantal bieren: " + bieren.Count);
            //via linq een selectie vragen bv Eerste bier:
            Bier bier = bieren.FirstOrDefault();
            if (bier != null)
                Console.WriteLine($"BierNr: {bier.BierNr} - Naam: {bier.Naam} - Alcohol: {bier.Alcohol} - SoortNr: {bier.SoortNr}");
            Console.ReadKey();
        }
    }
}
