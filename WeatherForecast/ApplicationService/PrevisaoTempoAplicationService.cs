using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using WeatherForecast.Dto;
using WeatherForecast.Enum;
using WeatherForecast.Models;
using WeatherForecast.Repository;

namespace WeatherForecast.ApplicationService
{
    public class PrevisaoTempoAplicationService
    {
        private UnitOfWork uow;

        public PrevisaoTempoAplicationService(UnitOfWork uow)
        {
            this.uow = uow;
        }

        public async Task<MinimaMaximaDto> GetPrevisaoCidadede(CancellationToken cancellationToken)
        {
            var previsaoMinima = await uow.PrevisaoClimaRepository.GetCidadeFria(cancellationToken);
            var previsaoMaxima = await uow.PrevisaoClimaRepository.GetCidadeQuente(cancellationToken);
            var listCidade = new List<string>();
            var PrevisaoCidade = new MinimaMaximaDto();
            PrevisaoCidade.Maxima = new List<MaximaDto>();
            PrevisaoCidade.Minima = new List<MinimaDto>();
            previsaoMinima.ForEach(x =>
            {
                if (!listCidade.Contains(x.Cidade) && PrevisaoCidade.Minima.Count() < 3)
                {
                    PrevisaoCidade.Minima.Add(x);
                    listCidade.Add(x.Cidade);
                }
            });

            listCidade.Clear();

            previsaoMaxima.ForEach(x =>
            {
                if (!listCidade.Contains(x.Cidade) && PrevisaoCidade.Maxima.Count() < 3)
                {
                    PrevisaoCidade.Maxima.Add(x);
                    listCidade.Add(x.Cidade);
                }
            });

            return PrevisaoCidade;
        }

        internal async Task VerificaBancoByCreate(CancellationToken cancellationToken)
        {
            if (uow.PrevisaoClimaRepository.VerificaExist())
                return;
            var http = new HttpClient();
            var response = await http.GetAsync("https://servicodados.ibge.gov.br/api/v1/localidades/estados");
            var responseCidade = await http.GetAsync("https://servicodados.ibge.gov.br/api/v1/localidades/municipios");
            var estadoDto = JsonConvert.DeserializeObject<List<EstadoDto>>(await response.Content.ReadAsStringAsync());
            var cidadeDto = JsonConvert.DeserializeObject<List<CidadeDto>>(await responseCidade.Content.ReadAsStringAsync());
            var previsoes = new List<PrevisaoClima>();
            var previsao = new PrevisaoClima();
            Random randNum = new Random();
            int count = 0;
            var estados = estadoDto.Select(x => new Estado
            {
                Id = x.Id,
                Nome = x.Nome,
                UF = x.Sigla
            });

            
            var cidade = cidadeDto.Select(x => new Cidade
            {
                Id = x.Id,
                EstadoId = x.Microrregiao.Mesorregiao.Uf.Id,
                Nome = x.Nome
            });
            
            cidade.ToList().ForEach(x =>
            {
                //Foi necessário limitar a quantidade de arquivos pois dava tempo limite excedido
                if(count < 100)
                {
                    for (int i = 0; i < 30; i++)
                    {
                        previsao = new PrevisaoClima();
                        previsao.CidadeId = x.Id;
                        previsao.TemperaturaMaxima = Convert.ToDecimal(randNum.Next(20, 41)  + randNum.NextDouble());
                        previsao.TemperaturaMinima = Convert.ToDecimal(randNum.Next(-2, 15)  + randNum.NextDouble());
                        previsao.Clima = ((EnumTempo)randNum.Next(1, 5)).ToString();
                        previsao.DataPrevisao = DateTime.Now.AddDays(i);
                        previsoes.Add(previsao);
                    }
                }
                count++;
            });

            await uow.EstadoRepository.SalvarByRange(estados.ToList(), cancellationToken);
            await uow.CidadeRepository.SalvarByRange(cidade.ToList(), cancellationToken);
            await uow.PrevisaoClimaRepository.SalvarByRange(previsoes, cancellationToken);
        }

        public async Task<List<PrevisaoClimaDto>> GetPrevisaoDia(int id, CancellationToken cancellationToken)
        {
            var previsao = await uow.PrevisaoClimaRepository.GetPrevisaoDia(id, cancellationToken);
            var listPrevisaoCidade = new List<PrevisaoClimaDto>();
            previsao.ForEach(x =>
            {
                x.Dia = x.Data.ToString("dddd",new CultureInfo("PT-pt"));
                x.DiaMes = x.Data.Day;
                listPrevisaoCidade.Add(x);
            });
            return listPrevisaoCidade;
        }
    }
}