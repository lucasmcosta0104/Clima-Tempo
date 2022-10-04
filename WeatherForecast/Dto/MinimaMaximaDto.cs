using System.Collections.Generic;

namespace WeatherForecast.Dto
{
    public class MinimaMaximaDto
    {
        public List<MaximaDto> Maxima { get; set; }
        public List<MinimaDto> Minima { get; set; }
    }
}