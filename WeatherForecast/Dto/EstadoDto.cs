using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeatherForecast.Dto
{
    public class EstadoDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sigla { get; set; }
    }
}