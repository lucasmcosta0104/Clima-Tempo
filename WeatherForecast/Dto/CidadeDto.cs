using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeatherForecast.Dto
{
    public class CidadeDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public MicrorregiaoDto Microrregiao { get; set; }
    }
}