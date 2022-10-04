using System;
using WeatherForecast.Models;

namespace WeatherForecast.Repository
{
    public class UnitOfWork : IDisposable
    {
        private bool disposedValue;
        private ClimaTempoSimplesEntities entities = new ClimaTempoSimplesEntities();
        private PrevisaoClimaRepository previsaoClimaRepository;
        private EstadoRepository estadoRepository;
        private CidadeRepository cidadeRepository;

        public UnitOfWork()
        {
            this.previsaoClimaRepository = new PrevisaoClimaRepository(entities);
            this.estadoRepository = new EstadoRepository(entities);
            this.cidadeRepository = new CidadeRepository(entities);
        }

        public PrevisaoClimaRepository PrevisaoClimaRepository
        {
            get
            {
                return previsaoClimaRepository;
            }
        }

        public EstadoRepository EstadoRepository
        {
            get
            {
                return estadoRepository;
            }
        }
        
        public CidadeRepository CidadeRepository
        {
            get
            {
                return cidadeRepository;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~UnitOfWork()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        void IDisposable.Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}