using ConsoleClientApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleClientApp.Services
{
    public class BierenService
    {
        private HttpClient _client = new HttpClient();
        public BierenService()
        {
            _client.BaseAddress = new Uri("http://www.syntrawest.somee.com/");
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public async Task<List<Bier>> GeefBierenAsync()
        {
            HttpResponseMessage response = _client.GetAsync("api/Bieren/GeefAlleBieren").Result;
            if (response.IsSuccessStatusCode) //StatusCode is 200 = OK
            {
                List<Bier> bieren = JsonConvert.DeserializeObject<List<Bier>>(await response.Content.ReadAsStringAsync());
                return bieren;
            }
            else
            {
                //foutmelding bv
                return null;
            }
        }
        //Oefening 1:  
        //1. Voeg een Model class Soort toe onder Models folder met 2 properties SoortNr (int) en Soort (string)
        //2. Maak een nieuw Methode in BierenService met naam GeefBierSoortenAsync() die een List<Soort> teruggeeft
        //3. Gebruik de url (http://www.syntrawest.somee.com/) api/Bieren/GeefAlleBierSoorten
        //4. Roep de methode van de BierenService aan vanuit Main van Progam.cs en toon enkele resultaten in de console

        //Oefening 2:
        //1. Voeg een Model class Brouwer toe onder Models folder met de juiste properties. Opgelet, omzet kan null zijn!
        // {
        //"brouwerNr": 1,
        //"brNaam": "Achouffe",
        //"adres": "Route du Village 32",
        //"postCode": 6666,
        //"gemeente": "Achouffe-Wibrin",
        //"omzet": 10000
        //     }
        //2. Maak een nieuw Methode in BierenService met naam GeefBrouwersAsync() die een List<Brouwer> teruggeeft
        //3. Gebruik de url (http://www.syntrawest.somee.com/) api/Bieren/GeefAlleBrouwers
        //4. Roep de methode van de BierenService aan vanuit Main van Progam.cs en toon enkele resultaten in de console
    }
}

