using System;

namespace WeatherForecast.Dto
{
    public class PrevisaoClimaDto
    {
        public int Id { get; set; }
        public string Estado { get; set; }
        public string Cidade { get; set; }
        public decimal? TemperaturaMaxima { get; set; }
        public decimal? TemperaturaMinima { get; set; }
        public string Clima { get; set; }
        public DateTime Data { get; internal set; }
        public string Dia { get; internal set; }
        public int DiaMes { get; internal set; }
    }
}