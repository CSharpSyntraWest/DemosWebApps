using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleClientApp.Models
{
    public class Brouwer
    {
        public int BrouwerNr { get; set; }
        public string BrNaam { get; set; }
        public string Adres { get; set; }
        public int PostCode { get; set; }
        public string Gemeente { get; set; }
        public double? Omzet { get; set; } 
    }
}
