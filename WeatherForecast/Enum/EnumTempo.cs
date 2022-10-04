using System.ComponentModel;

namespace WeatherForecast.Enum
{
    public enum EnumTempo
    {
        [Description("ensolarado")]
        ensolarado = 1,
        [Description("nublado")]
        nublado = 2,
        [Description("tempestuoso")]
        tempestuoso = 3,
        [Description("instavel")]
        instavel = 4,
        [Description("chuvoso")]
        chuvoso = 5
    }
}