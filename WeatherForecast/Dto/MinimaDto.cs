using System;

namespace WeatherForecast.Dto
{
    public class MinimaDto
    {
        public int Id { get; set; }
        public int CidadeId { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }
        public decimal? TemperaturaMinima { get; set; }
        public DateTime DataPrevisao { get; internal set; }
    }
}