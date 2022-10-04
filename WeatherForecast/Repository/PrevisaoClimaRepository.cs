using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using WeatherForecast.Dto;
using WeatherForecast.Models;

namespace WeatherForecast.Repository
{
    public class PrevisaoClimaRepository
    {
        private readonly ClimaTempoSimplesEntities entities;
        public PrevisaoClimaRepository(ClimaTempoSimplesEntities entities)
        {
            this.entities = entities;
        }

        public async Task SalvarByRange(List<PrevisaoClima> previsaoClima, CancellationToken cancellationToken)
        {
            entities.PrevisaoClima.AddRange(previsaoClima);
            await entities.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<MinimaDto>> GetCidadeFria(CancellationToken cancellationToken)
        {
            return await entities.PrevisaoClima
                 .Include(x => x.Cidade)
                 .Include(x => x.Cidade.Estado)
                 .OrderBy(x => x.TemperaturaMinima).Take(3)
                 .Select(x => new MinimaDto
                 {
                     Id = x.Id,       
                     Cidade = x.Cidade.Nome,
                     UF = x.Cidade.Estado.UF,
                     TemperaturaMinima = x.TemperaturaMinima,
                     DataPrevisao = x.DataPrevisao
                 }).ToListAsync(cancellationToken);
        }

        public async Task<List<MaximaDto>> GetCidadeQuente(CancellationToken cancellationToken)
        {
            return await entities.PrevisaoClima
                 .Include(x => x.Cidade)
                 .Include(x => x.Cidade.Estado)
                 .OrderByDescending(x => x.TemperaturaMaxima).Take(3)
                 .Select(x => new MaximaDto
                 {
                     Id = x.Id,
                     Cidade = x.Cidade.Nome,
                     UF = x.Cidade.Estado.UF,
                     TemperaturaMaxima = x.TemperaturaMaxima,
                     DataPrevisao = x.DataPrevisao
                 }).ToListAsync(cancellationToken);
        }

        internal bool VerificaExist()
        {
            return entities.Cidade.Any();
        }

        public async  Task<List<PrevisaoClimaDto>> GetPrevisaoDia(int id, CancellationToken cancellationToken)
        {
            var previsao = await entities.PrevisaoClima.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            return await entities.PrevisaoClima
                .Include(x => x.Cidade.Estado)
                .Include(x => x.Cidade)
                .Where(x => x.CidadeId == previsao.CidadeId && x.DataPrevisao.Day >= DateTime.Now.Day)
                .OrderBy(x => x.DataPrevisao.Day).Take(7)
                .Select(x => new PrevisaoClimaDto
                {
                    Id = x.CidadeId,
                    Cidade = x.Cidade.Nome,
                    Estado = x.Cidade.Estado.UF,
                    TemperaturaMaxima = x.TemperaturaMaxima,
                    TemperaturaMinima = x.TemperaturaMinima,
                    Data = x.DataPrevisao,
                    Clima = x.Clima,
                }).ToListAsync(cancellationToken);
        }
    }
}