using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using WeatherForecast.Models;

namespace WeatherForecast.Repository
{
    public class CidadeRepository
    {
        private readonly ClimaTempoSimplesEntities entities;
        public CidadeRepository(ClimaTempoSimplesEntities entities)
        {
            this.entities = entities;
        }

        public async Task SalvarByRange(List<Cidade> cidade, CancellationToken cancellationToken)
        {
            entities.Cidade.AddRange(cidade);
            await entities.SaveChangesAsync(cancellationToken);
        }
    }
}