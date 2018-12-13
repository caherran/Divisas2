using Newtonsoft.Json;
using System;

namespace Divisas2
{
    public class ExchangeRates
    {
        [JsonProperty("disclaimer")]
        public string Disclaimer { get; set; }

        [JsonProperty("license")]
        public string License { get; set; }

        [JsonProperty("timestamp")]
        public string TimeStamp { get; set; }

        [JsonProperty("base")]
        public string Base { get; set; }

        [JsonProperty("rates")]
        public Rates Rates { get; set; }
    }

    public class Rates
    {
        public double BRL { get; set; } //Real Brasilero
        public double CAD { get; set; } //Dolar Canadiense
        public double CHF { get; set; } //Franco Suizo
        public double CLP { get; set; } //Peso Chileno
        public double COP { get; set; } //Peso Colombiano
        public double DKK { get; set; } //Corona Danesa
        public double EUR { get; set; } //Euro
        public double GBP { get; set; } //Libra Esterlina
        public double INR { get; set; } //Rupia India
        public double JPY { get; set; } //Yen Japones
        public double MXN { get; set; } //Peso Mexicano
        public double USD { get; set; } //Dolar EEUU
    }
}
