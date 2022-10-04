using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using WeatherForecast.Models;

namespace WeatherForecast.Repository
{
    public class EstadoRepository
    {
        private readonly ClimaTempoSimplesEntities entities;
        public EstadoRepository(ClimaTempoSimplesEntities entities)
        {
            this.entities = entities;
        }

        public async Task SalvarByRange(List<Estado> estado, CancellationToken cancellationToken) 
        {
            entities.Estado.AddRange(estado);
            await entities.SaveChangesAsync(cancellationToken);
        }
    }
}