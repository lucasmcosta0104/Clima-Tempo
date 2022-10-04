using System;

namespace WeatherForecast.Dto
{
    public class MaximaDto
    {
        public int Id { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }
        public decimal? TemperaturaMaxima { get; set; }
        public DateTime DataPrevisao { get; set; }
    }
}