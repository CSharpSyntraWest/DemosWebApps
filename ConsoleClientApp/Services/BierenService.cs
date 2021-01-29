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

    }
}

