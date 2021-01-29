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
            {
                Console.WriteLine(bier);
                Random rand = new Random();
                Console.WriteLine("Bier van de maand:");
                Bier randomBier = bieren.ElementAt(rand.Next(0, bieren.Count));
                Console.WriteLine(randomBier);
            }

           Bier bier2 = await bierenService.GeefBierVoorBierNr(5);
            if (bier2 == null)
                Console.WriteLine("Bier met BierNr 5 niet gevonden");
            else
                Console.WriteLine(bier2);

            //Biersoorten:

            List<BierSoort> soorten = await bierenService.GeefBierSoortenAsync();
            Console.WriteLine("Aantal soorten: " + soorten.Count);
            //via linq een selectie vragen
            BierSoort soort = soorten.FirstOrDefault();
            if (soort != null)
            {
                Console.WriteLine($"SoortNr: + {soort.SoortNr} - Naam: {soort.Soort}");
            }
            Console.WriteLine();


            List<Brouwer> brouwers = await bierenService.GeefBrouwersAsync();
            Console.WriteLine("Aantal Brouwers: " + brouwers.Count);
            //Brouwers met postcode=9000
            List<Brouwer> brouwersUitGent = brouwers.Where(b => b.PostCode == 9000).ToList();
            Console.WriteLine("Brouwers uit gent: " + brouwersUitGent.Count);
            brouwersUitGent.ForEach(b => Console.WriteLine("\t" + b.BrNaam));
            Console.WriteLine("Tot ziens!");
        }
    }
}
