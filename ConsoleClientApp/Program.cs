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
            //Pils
            var gemOmzetPerPostcode = from br in brouwers
                                      group br by br.PostCode
                                      into postGroep 
                                      select new { br = postGroep.Key, 
                                                    gemOmzet = (from gem in postGroep select gem.Omzet).Average()
                                                };
            var gemOmzetPerPostcode2 = from br in brouwers 
                                      group br by br.PostCode into postGroep 
                                      select new { br = postGroep.Key, 
                                          gemOmzet = postGroep.Average(b => b.Omzet) };
            //Oefening Linq 4:  Geef alle brouwers, gesorteerd per gemeente, daarna op brouwernaam

            var alleBrouwers = from b in brouwers
                               orderby b.Gemeente, b.BrNaam
                               select b;
            alleBrouwers.ToList().ForEach(b => Console.WriteLine($"Gemeente {b.Gemeente} - Brouwer {b.BrouwerNr} - {b.BrNaam} - Omzet: {b.Omzet}"));
            //Oefening Linq 5:  Geef een Lijst van namen van biersoorten en per biersoort de lijst van bieren in de soort
            //Pils
            //    Jupiler
            //    Heineken
            //    Stella
            //Geuze
            //    Liefmans Kriek
            //    ...

            var bierenPerSoort = from s in soorten
                                 join b in bieren on s.SoortNr equals b.SoortNr
                                 group s by s.Soort
                                 into soortGroep
                                 select new
                                 {
                                     SoortNaam = soortGroep.Key,
                                     Bieren = from bier in bieren join soort in soorten on bier.SoortNr equals soort.SoortNr where soort.Soort == soortGroep.Key select bier 
                                 };

            //Of korter, zonder group by
            var bierPersoortGroep = from s in soorten
                                    join b in bieren on s.SoortNr equals b.SoortNr
                                   into bierenGroep
                                   orderby s.Soort
                                   select new
                                   {
                                       SoortNaam = s.Soort,
                                       Bieren = from b in bierenGroep orderby b.Naam select b
                                   };
            foreach (var groep in bierPersoortGroep)
            {
                Console.WriteLine(groep.SoortNaam + ":");
                foreach (Bier b in  groep.Bieren)
                {
                    Console.WriteLine("\t" + b.Naam);
                }
            }
            var lijstMetNamenPerBiersoort2 = soorten.GroupJoin(bieren, soort => soort.SoortNr, bier => bier.SoortNr,
                        (soort, bier) => new
                        {
                            SoortNaam = soort.Soort,
                            Bieren = bieren.Where(b => b.SoortNr == soort.SoortNr)
                        }).ToList();

            foreach (var item in lijstMetNamenPerBiersoort2)
            {
                Console.WriteLine(item.SoortNaam);
                foreach (var b in item.Bieren)
                {
                    Console.WriteLine("\t" + b.Naam);
                }
            }
            //foreach (var item in bierenPerSoort)
            //{
            //    Console.WriteLine(item.SoortNaam);
            //    foreach (var b in item.Bieren)
            //    {
            //        Console.WriteLine("\t" + b.Naam);
            //    }
            //}
            //Oefening Linq6: geef gesorteerde lijst terug (via Linq query) op naam van biersoort, daarna op bier (binnen één biersoort)                     
            var lijstMetNamenPerBiersoort = lijstMetNamenPerBiersoort2.OrderBy(b => b.SoortNaam).ThenBy(b => b.Bieren.Select(b => b.Naam)).ToList();

            var bierenPerSoortOrdered = from s in soorten
                                 join b in bieren on s.SoortNr equals b.SoortNr
                                 group s by s.Soort
                                into soortGroep
                                 select new
                                 {
                                     SoortNaam = soortGroep.Key,
                                     Bieren = from bier in bieren join soort in soorten on bier.SoortNr equals soort.SoortNr orderby bier.Naam  where soort.Soort == soortGroep.Key select bier
                                 };


            foreach (var item in bierenPerSoort)
            {
                Console.WriteLine(item.SoortNaam);
                foreach (var b in item.Bieren)
                {
                    Console.WriteLine("\t" + b.Naam);
                }
            }
            //Oefening StockExchangeService maken:
            //Maak onder de folder Services een nieuwe klasse StockExchangeService aan die de aandeelgegevens
            //opvraagt voor een bepaald aandeel (bv InBev) ("https://financialmodelingprep.com/api/v3/quote/ABI.BR?apikey=8e5b68b6bac6e3fe5c98c5781306f694")
            //De basis-Url is "https://financialmodelingprep.com/api/v3/". Zet deze in de Constructor van StockExchangeService
            //De unieke code van het aandeel Inbev is ABI.BR 
            //Maak een Methode GeefAandeelInfoAsync(string code) in de StockEchangeService die voor een aandeel code de volgende info teruggeeft:
            //Symbol (string), Name (string), Price(double), ChangesPercentage(double) en Change(double). Maak een Model class Aandeel die deze properties heeft
            //Opgelet: wanneer je Deserialiseert met JsonConvert gebruik een List<Aandeel>. In deze List zit één enkel Aandeel-object
            //Roep de Methode GeefAandeelInfoAsync("ABI.BR") aan vanuit program.cs (ConsoleApp) en schrijf deze 5 properties van het aandeel naar de console
            //De url geeft de volgende gegevens terug:
            //[ {
            //            "symbol" : "ABI.BR",
            //  "name" : "Anheuser-Busch InBev SA/NV",
            //  "price" : 52.53000000,
            //  "changesPercentage" : -2.11000000,
            //  "change" : -1.13000000,
            //....
            //} ]
            Console.WriteLine("Tot ziens!");
        }
    }
}
