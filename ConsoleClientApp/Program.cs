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
            //Oefening Linq 1 : Geef alle Biernamen met hun soortnamen, gesorteerd op biernaam, daarna op soortnaam
            //Oefening Linq 2 : Geef per biersoort het aantal bieren (geef soortnaam en aantal bieren)
            //Oefening Linq 3 : Geef voor de brouwers de gemiddelde omzet per postcode (geef postcode en gemiddelde omzet)
            //Oefening Linq 4:  Geef alle brouwers, gesorteerd per gemeente, daarna op brouwernaam
            //Oefening Linq 5:  Geef een Lijst van namen van biersoorten en per biersoort de lijst van bieren in de soort
            //Pils
            //    Jupiler
            //    Heineken
            //    Stella
            //Geuze
            //    Liefmans Kriek
            //    ...
            //Oefening Linq6: geef gesorteerde lijst terug (via Linq query) op naam van biersoort, daarna op bier (binnen één biersoort)                     


            //Oefening StockExchangeService maken:
            //Maak onder de folder Services een nieuwe klasse StockExchangeService aan die de aandeelgegevens
            //opvraagt voor een bepaald aandeel (bv InBev) ("https://financialmodelingprep.com/api/v3/quote/ABI.BR?apikey=8e5b68b6bac6e3fe5c98c5781306f694")
            //De basis-Url is "https://financialmodelingprep.com/api/v3/". Zet deze in de Constructor van StockExchangeService
            //De unieke code van het aandeel Inbev is ABI.BR 
            //Maak een Methode GeefAandeelInfoAsync(string code) in de StockEchangeService die voor een aandeel code de volgende info teruggeeft:
            //Symbol (string), Name (string), Price(double), ChangesPercentage(double) en Change(double). Maak een Model class Aandeel die deze properties heeft
            //Opgelet: wanneer je Deserialiseert met JsonConvert gebruik een List<Aandeel>. In deze List zit één enkel object
            //De url geeft de volgende gegevens terug:
            //[ {
            //            "symbol" : "ABI.BR",
            //  "name" : "Anheuser-Busch InBev SA/NV",
            //  "price" : 52.53000000,
            //  "changesPercentage" : -2.11000000,
            //  "change" : -1.13000000,
            //  "dayLow" : 52.18000000,
            //  "dayHigh" : 53.05000000,
            //  "yearHigh" : 70.48000000,
            //  "yearLow" : 29.02500000,
            //  "marketCap" : 103559741440.00000000,
            //  "priceAvg50" : 56.97000000,
            //  "priceAvg200" : 51.29733700,
            //  "volume" : 349293,
            //  "avgVolume" : 1718893,
            //  "exchange" : "EURONEXT",
            //  "open" : 52.44000000,
            //  "previousClose" : 53.66000000,
            //  "eps" : -0.37400000,
            //  "pe" : null,
            //  "earningsAnnouncement" : "2021-02-25T01:00:00.000+0000",
            //  "sharesOutstanding" : 1971439966,
            //  "timestamp" : 1611916790
            //} ]
            Console.WriteLine("Tot ziens!");
        }
    }
}
