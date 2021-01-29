using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleClientApp.Models
{
    public class Bier
    {
        public string BierNr { get; set; }
        public string Naam { get; set; }
        public double? Alcohol { get; set; }
        public int? SoortNr { get; set; }
        public override string ToString()
        {
            return $"BierNr: {BierNr} - Naam: {Naam} - Alcohol: {Alcohol} - SoortNr: {SoortNr}";
        }

    }
}
